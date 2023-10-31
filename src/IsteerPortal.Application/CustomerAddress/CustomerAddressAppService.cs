using IsteerPortal.Shared;
using IsteerPortal.Customers;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using IsteerPortal.Permissions;
using IsteerPortal.CustomerAddress;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using IsteerPortal.Shared;

namespace IsteerPortal.CustomerAddress
{

    [Authorize(IsteerPortalPermissions.CustomerAddress.Default)]
    public class CustomerAddressAppService : ApplicationService, ICustomerAddressAppService
    {
        private readonly IDistributedCache<CustomerAddresExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ICustomerAddresRepository _customerAddresRepository;
        private readonly CustomerAddresManager _customerAddresManager;
        private readonly IRepository<Customer, Guid> _customerRepository;

        public CustomerAddressAppService(ICustomerAddresRepository customerAddresRepository, CustomerAddresManager customerAddresManager, IDistributedCache<CustomerAddresExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Customer, Guid> customerRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _customerAddresRepository = customerAddresRepository;
            _customerAddresManager = customerAddresManager; _customerRepository = customerRepository;
        }

        public virtual async Task<PagedResultDto<CustomerAddresWithNavigationPropertiesDto>> GetListAsync(GetCustomerAddressInput input)
        {
            var totalCount = await _customerAddresRepository.GetCountAsync(input.FilterText, input.Address1, input.Address2, input.ZIPCODE, input.CustomerId);
            var items = await _customerAddresRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Address1, input.Address2, input.ZIPCODE, input.CustomerId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CustomerAddresWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CustomerAddresWithNavigationProperties>, List<CustomerAddresWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<CustomerAddresWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerAddresWithNavigationProperties, CustomerAddresWithNavigationPropertiesDto>
                (await _customerAddresRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<CustomerAddresDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CustomerAddres, CustomerAddresDto>(await _customerAddresRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCustomerLookupAsync(LookupRequestDto input)
        {
            var query = (await _customerRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Customer>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Customer>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(IsteerPortalPermissions.CustomerAddress.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _customerAddresRepository.DeleteAsync(id);
        }

        [Authorize(IsteerPortalPermissions.CustomerAddress.Create)]
        public virtual async Task<CustomerAddresDto> CreateAsync(CustomerAddresCreateDto input)
        {

            var customerAddres = await _customerAddresManager.CreateAsync(
            input.CustomerId, input.Address1, input.Address2, input.ZIPCODE
            );

            return ObjectMapper.Map<CustomerAddres, CustomerAddresDto>(customerAddres);
        }

        [Authorize(IsteerPortalPermissions.CustomerAddress.Edit)]
        public virtual async Task<CustomerAddresDto> UpdateAsync(Guid id, CustomerAddresUpdateDto input)
        {

            var customerAddres = await _customerAddresManager.UpdateAsync(
            id,
            input.CustomerId, input.Address1, input.Address2, input.ZIPCODE, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CustomerAddres, CustomerAddresDto>(customerAddres);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerAddresExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var customerAddress = await _customerAddresRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Address1, input.Address2, input.ZIPCODE);
            var items = customerAddress.Select(item => new
            {
                Address1 = item.CustomerAddres.Address1,
                Address2 = item.CustomerAddres.Address2,
                ZIPCODE = item.CustomerAddres.ZIPCODE,

                Customer = item.Customer?.Name,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "CustomerAddress.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CustomerAddresExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}