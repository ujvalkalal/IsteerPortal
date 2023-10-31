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

namespace IsteerPortal.Customers
{
    public class EfCoreCustomerRepository : EfCoreRepository<IsteerPortalDbContext, Customer, Guid>, ICustomerRepository
    {
        public EfCoreCustomerRepository(IDbContextProvider<IsteerPortalDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Customer>> GetListAsync(
            string filterText = null,
            string name = null,
            string website = null,
            string emailId = null,
            string contactNumber = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, website, emailId, contactNumber);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string name = null,
            string website = null,
            string emailId = null,
            string contactNumber = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name, website, emailId, contactNumber);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Customer> ApplyFilter(
            IQueryable<Customer> query,
            string filterText,
            string name = null,
            string website = null,
            string emailId = null,
            string contactNumber = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name.Contains(filterText) || e.Website.Contains(filterText) || e.EmailId.Contains(filterText) || e.ContactNumber.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(website), e => e.Website.Contains(website))
                    .WhereIf(!string.IsNullOrWhiteSpace(emailId), e => e.EmailId.Contains(emailId))
                    .WhereIf(!string.IsNullOrWhiteSpace(contactNumber), e => e.ContactNumber.Contains(contactNumber));
        }
    }
}