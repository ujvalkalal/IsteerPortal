using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using IsteerPortal.Customers;

namespace IsteerPortal.Customers
{
    public class CustomersDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public CustomersDataSeedContributor(ICustomerRepository customerRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _customerRepository = customerRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _customerRepository.InsertAsync(new Customer
            (
                id: Guid.Parse("66c3307d-59d2-4782-9d4b-d2e27309ef31"),
                name: "e7031cc5408649b0837934d5117fdbee3c08e28fb2a44940b557f83e91ce333ddac279386bb5453fa8442fd9c8393697",
                website: "826b1551",
                emailId: "0016a15d2c0a4b81b3d603d401c7dbacd671cd3eca9",
                contactNumber: "93239529527146d08447a42400cc0cf6020f3993012f42e8b760dba264d8c65230aab29589e449559d054de52"
            ));

            await _customerRepository.InsertAsync(new Customer
            (
                id: Guid.Parse("a97070ca-9dde-46cd-8204-651d297c741a"),
                name: "d5cf94f517",
                website: "aa602de727e94c50bf2b52718ee",
                emailId: "9b8915d758ef",
                contactNumber: "b43101b2d25e408"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}