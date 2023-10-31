using IsteerPortal.Customers;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace IsteerPortal.CustomerAddress
{
    public class CustomerAddresWithNavigationPropertiesDto
    {
        public CustomerAddresDto CustomerAddres { get; set; }

        public CustomerDto Customer { get; set; }

    }
}