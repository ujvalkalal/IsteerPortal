using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace IsteerPortal.Customers
{
    public class CustomerCreateDto
    {
        public string? Name { get; set; }
        public string? Website { get; set; }
        public string? EmailId { get; set; }
        public string? ContactNumber { get; set; }
    }
}