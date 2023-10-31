using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace IsteerPortal.CustomerAddress
{
    public class CustomerAddresUpdateDto : IHasConcurrencyStamp
    {
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? ZIPCODE { get; set; }
        public Guid? CustomerId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}