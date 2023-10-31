using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace IsteerPortal.Customers
{
    public class CustomerManager : DomainService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerManager(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> CreateAsync(
        string name, string website, string emailId, string contactNumber)
        {

            var customer = new Customer(
             GuidGenerator.Create(),
             name, website, emailId, contactNumber
             );

            return await _customerRepository.InsertAsync(customer);
        }

        public async Task<Customer> UpdateAsync(
            Guid id,
            string name, string website, string emailId, string contactNumber, [CanBeNull] string concurrencyStamp = null
        )
        {

            var customer = await _customerRepository.GetAsync(id);

            customer.Name = name;
            customer.Website = website;
            customer.EmailId = emailId;
            customer.ContactNumber = contactNumber;

            customer.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _customerRepository.UpdateAsync(customer);
        }

    }
}