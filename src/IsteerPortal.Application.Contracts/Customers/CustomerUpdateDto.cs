using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace IsteerPortal.Customers
{
    public class CustomerUpdateDto : IHasConcurrencyStamp
    {
        public string? Name { get; set; }
        public string? Website { get; set; }
        public string? EmailId { get; set; }
        public string? ContactNumber { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}