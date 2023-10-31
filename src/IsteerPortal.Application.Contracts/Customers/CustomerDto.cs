using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace IsteerPortal.Customers
{
    public class CustomerDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string? Name { get; set; }
        public string? Website { get; set; }
        public string? EmailId { get; set; }
        public string? ContactNumber { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}