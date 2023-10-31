using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace IsteerPortal.CustomerAddress
{
    public class CustomerAddresManager : DomainService
    {
        private readonly ICustomerAddresRepository _customerAddresRepository;

        public CustomerAddresManager(ICustomerAddresRepository customerAddresRepository)
        {
            _customerAddresRepository = customerAddresRepository;
        }

        public async Task<CustomerAddres> CreateAsync(
        Guid? customerId, string address1, string address2, string zIPCODE)
        {

            var customerAddres = new CustomerAddres(
             GuidGenerator.Create(),
             customerId, address1, address2, zIPCODE
             );

            return await _customerAddresRepository.InsertAsync(customerAddres);
        }

        public async Task<CustomerAddres> UpdateAsync(
            Guid id,
            Guid? customerId, string address1, string address2, string zIPCODE, [CanBeNull] string concurrencyStamp = null
        )
        {

            var customerAddres = await _customerAddresRepository.GetAsync(id);

            customerAddres.CustomerId = customerId;
            customerAddres.Address1 = address1;
            customerAddres.Address2 = address2;
            customerAddres.ZIPCODE = zIPCODE;

            customerAddres.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _customerAddresRepository.UpdateAsync(customerAddres);
        }

    }
}