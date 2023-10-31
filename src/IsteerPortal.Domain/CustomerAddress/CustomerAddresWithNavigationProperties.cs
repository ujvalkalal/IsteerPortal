using IsteerPortal.Customers;

using System;
using System.Collections.Generic;

namespace IsteerPortal.CustomerAddress
{
    public class CustomerAddresWithNavigationProperties
    {
        public CustomerAddres CustomerAddres { get; set; }

        public Customer Customer { get; set; }
        

        
    }
}