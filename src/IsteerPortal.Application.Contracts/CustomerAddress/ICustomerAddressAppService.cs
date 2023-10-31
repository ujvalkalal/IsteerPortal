using IsteerPortal.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using IsteerPortal.Shared;

namespace IsteerPortal.CustomerAddress
{
    public interface ICustomerAddressAppService : IApplicationService
    {
        Task<PagedResultDto<CustomerAddresWithNavigationPropertiesDto>> GetListAsync(GetCustomerAddressInput input);

        Task<CustomerAddresWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<CustomerAddresDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetCustomerLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<CustomerAddresDto> CreateAsync(CustomerAddresCreateDto input);

        Task<CustomerAddresDto> UpdateAsync(Guid id, CustomerAddresUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerAddresExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}