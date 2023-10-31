using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace IsteerPortal.CustomerAddress
{
    public interface ICustomerAddresRepository : IRepository<CustomerAddres, Guid>
    {
        Task<CustomerAddresWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<CustomerAddresWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string address1 = null,
            string address2 = null,
            string zIPCODE = null,
            Guid? customerId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<CustomerAddres>> GetListAsync(
                    string filterText = null,
                    string address1 = null,
                    string address2 = null,
                    string zIPCODE = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string address1 = null,
            string address2 = null,
            string zIPCODE = null,
            Guid? customerId = null,
            CancellationToken cancellationToken = default);
    }
}