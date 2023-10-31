using IsteerPortal.Customers;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace IsteerPortal.CustomerAddress
{
    public class CustomerAddres : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string? Address1 { get; set; }

        [CanBeNull]
        public virtual string? Address2 { get; set; }

        [CanBeNull]
        public virtual string? ZIPCODE { get; set; }
        public Guid? CustomerId { get; set; }

        public CustomerAddres()
        {

        }

        public CustomerAddres(Guid id, Guid? customerId, string address1, string address2, string zIPCODE)
        {

            Id = id;
            Address1 = address1;
            Address2 = address2;
            ZIPCODE = zIPCODE;
            CustomerId = customerId;
        }

    }
}