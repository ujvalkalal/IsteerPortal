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
using IsteerPortal.Customers;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using IsteerPortal.Shared;

namespace IsteerPortal.Customers
{

    [Authorize(IsteerPortalPermissions.Customers.Default)]
    public class CustomersAppService : ApplicationService, ICustomersAppService
    {
        private readonly IDistributedCache<CustomerExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerManager _customerManager;

        public CustomersAppService(ICustomerRepository customerRepository, CustomerManager customerManager, IDistributedCache<CustomerExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _customerRepository = customerRepository;
            _customerManager = customerManager;
        }

        public virtual async Task<PagedResultDto<CustomerDto>> GetListAsync(GetCustomersInput input)
        {
            var totalCount = await _customerRepository.GetCountAsync(input.FilterText, input.Name, input.Website, input.EmailId, input.ContactNumber);
            var items = await _customerRepository.GetListAsync(input.FilterText, input.Name, input.Website, input.EmailId, input.ContactNumber, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CustomerDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Customer>, List<CustomerDto>>(items)
            };
        }

        public virtual async Task<CustomerDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Customer, CustomerDto>(await _customerRepository.GetAsync(id));
        }

        [Authorize(IsteerPortalPermissions.Customers.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _customerRepository.DeleteAsync(id);
        }

        [Authorize(IsteerPortalPermissions.Customers.Create)]
        public virtual async Task<CustomerDto> CreateAsync(CustomerCreateDto input)
        {

            var customer = await _customerManager.CreateAsync(
            input.Name, input.Website, input.EmailId, input.ContactNumber
            );

            return ObjectMapper.Map<Customer, CustomerDto>(customer);
        }

        [Authorize(IsteerPortalPermissions.Customers.Edit)]
        public virtual async Task<CustomerDto> UpdateAsync(Guid id, CustomerUpdateDto input)
        {

            var customer = await _customerManager.UpdateAsync(
            id,
            input.Name, input.Website, input.EmailId, input.ContactNumber, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Customer, CustomerDto>(customer);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _customerRepository.GetListAsync(input.FilterText, input.Name, input.Website, input.EmailId, input.ContactNumber);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Customer>, List<CustomerExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Customers.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CustomerExcelDownloadTokenCacheItem { Token = token },
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