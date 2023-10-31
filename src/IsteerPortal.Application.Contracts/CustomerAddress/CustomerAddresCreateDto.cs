using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace IsteerPortal.CustomerAddress
{
    public class CustomerAddresCreateDto
    {
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? ZIPCODE { get; set; }
        public Guid? CustomerId { get; set; }
    }
}