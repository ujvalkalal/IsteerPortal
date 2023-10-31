using IsteerPortal.CustomerAddress;
using System;
using IsteerPortal.Shared;
using Volo.Abp.AutoMapper;
using IsteerPortal.Customers;
using AutoMapper;

namespace IsteerPortal;

public class IsteerPortalApplicationAutoMapperProfile : Profile
{
    public IsteerPortalApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Customer, CustomerDto>();
        CreateMap<Customer, CustomerExcelDto>();

        CreateMap<CustomerAddres, CustomerAddresDto>();
        CreateMap<CustomerAddres, CustomerAddresExcelDto>();
        CreateMap<CustomerAddresWithNavigationProperties, CustomerAddresWithNavigationPropertiesDto>();
        CreateMap<Customer, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));
    }
}