using IsteerPortal.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using IsteerPortal.EntityFrameworkCore;

namespace IsteerPortal.CustomerAddress
{
    public class EfCoreCustomerAddresRepository : EfCoreRepository<IsteerPortalDbContext, CustomerAddres, Guid>, ICustomerAddresRepository
    {
        public EfCoreCustomerAddresRepository(IDbContextProvider<IsteerPortalDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<CustomerAddresWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(customerAddres => new CustomerAddresWithNavigationProperties
                {
                    CustomerAddres = customerAddres,
                    Customer = dbContext.Set<Customer>().FirstOrDefault(c => c.Id == customerAddres.CustomerId)
                }).FirstOrDefault();
        }

        public async Task<List<CustomerAddresWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string address1 = null,
            string address2 = null,
            string zIPCODE = null,
            Guid? customerId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, address1, address2, zIPCODE, customerId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerAddresConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<CustomerAddresWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from customerAddres in (await GetDbSetAsync())
                   join customer in (await GetDbContextAsync()).Set<Customer>() on customerAddres.CustomerId equals customer.Id into customers
                   from customer in customers.DefaultIfEmpty()
                   select new CustomerAddresWithNavigationProperties
                   {
                       CustomerAddres = customerAddres,
                       Customer = customer
                   };
        }

        protected virtual IQueryable<CustomerAddresWithNavigationProperties> ApplyFilter(
            IQueryable<CustomerAddresWithNavigationProperties> query,
            string filterText,
            string address1 = null,
            string address2 = null,
            string zIPCODE = null,
            Guid? customerId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.CustomerAddres.Address1.Contains(filterText) || e.CustomerAddres.Address2.Contains(filterText) || e.CustomerAddres.ZIPCODE.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(address1), e => e.CustomerAddres.Address1.Contains(address1))
                    .WhereIf(!string.IsNullOrWhiteSpace(address2), e => e.CustomerAddres.Address2.Contains(address2))
                    .WhereIf(!string.IsNullOrWhiteSpace(zIPCODE), e => e.CustomerAddres.ZIPCODE.Contains(zIPCODE))
                    .WhereIf(customerId != null && customerId != Guid.Empty, e => e.Customer != null && e.Customer.Id == customerId);
        }

        public async Task<List<CustomerAddres>> GetListAsync(
            string filterText = null,
            string address1 = null,
            string address2 = null,
            string zIPCODE = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, address1, address2, zIPCODE);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerAddresConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string address1 = null,
            string address2 = null,
            string zIPCODE = null,
            Guid? customerId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, address1, address2, zIPCODE, customerId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CustomerAddres> ApplyFilter(
            IQueryable<CustomerAddres> query,
            string filterText,
            string address1 = null,
            string address2 = null,
            string zIPCODE = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Address1.Contains(filterText) || e.Address2.Contains(filterText) || e.ZIPCODE.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(address1), e => e.Address1.Contains(address1))
                    .WhereIf(!string.IsNullOrWhiteSpace(address2), e => e.Address2.Contains(address2))
                    .WhereIf(!string.IsNullOrWhiteSpace(zIPCODE), e => e.ZIPCODE.Contains(zIPCODE));
        }
    }
}