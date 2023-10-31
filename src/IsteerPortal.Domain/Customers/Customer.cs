using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace IsteerPortal.Customers
{
    public class Customer : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string? Name { get; set; }

        [CanBeNull]
        public virtual string? Website { get; set; }

        [CanBeNull]
        public virtual string? EmailId { get; set; }

        [CanBeNull]
        public virtual string? ContactNumber { get; set; }

        public Customer()
        {

        }

        public Customer(Guid id, string name, string website, string emailId, string contactNumber)
        {

            Id = id;
            Name = name;
            Website = website;
            EmailId = emailId;
            ContactNumber = contactNumber;
        }

    }
}