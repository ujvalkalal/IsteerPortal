using IsteerPortal.Web.Pages.CustomerAddress;
using IsteerPortal.CustomerAddress;
using IsteerPortal.Web.Pages.Customers;
using Volo.Abp.AutoMapper;
using IsteerPortal.Customers;
using AutoMapper;

namespace IsteerPortal.Web;

public class IsteerPortalWebAutoMapperProfile : Profile
{
    public IsteerPortalWebAutoMapperProfile()
    {
        //Define your object mappings here, for the Web project

        CreateMap<CustomerDto, CustomerUpdateViewModel>();
        CreateMap<CustomerUpdateViewModel, CustomerUpdateDto>();
        CreateMap<CustomerCreateViewModel, CustomerCreateDto>();

        CreateMap<CustomerAddresDto, CustomerAddresUpdateViewModel>();
        CreateMap<CustomerAddresUpdateViewModel, CustomerAddresUpdateDto>();
        CreateMap<CustomerAddresCreateViewModel, CustomerAddresCreateDto>();
    }
}