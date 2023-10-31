using IsteerPortal.Customers;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using IsteerPortal.CustomerAddress;

namespace IsteerPortal.CustomerAddress
{
    public class CustomerAddressDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICustomerAddresRepository _customerAddresRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CustomersDataSeedContributor _customersDataSeedContributor;

        public CustomerAddressDataSeedContributor(ICustomerAddresRepository customerAddresRepository, IUnitOfWorkManager unitOfWorkManager, CustomersDataSeedContributor customersDataSeedContributor)
        {
            _customerAddresRepository = customerAddresRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _customersDataSeedContributor = customersDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _customersDataSeedContributor.SeedAsync(context);

            await _customerAddresRepository.InsertAsync(new CustomerAddres
            (
                id: Guid.Parse("a34547e3-65c2-4920-8442-772a966faefb"),
                address1: "950e1d1ff3da4",
                address2: "15421590bac2452",
                zIPCODE: "028ef3fdc8c94df18f7c5d018f95abe4275ad7460d6e472cb165531033126900af",
                customerId: null
            ));

            await _customerAddresRepository.InsertAsync(new CustomerAddres
            (
                id: Guid.Parse("4a0e359b-c4e1-45e6-aea4-8c01fee320ac"),
                address1: "88666bb8e",
                address2: "caf8c0878f824eb6bf536fea315a47d7a517566a76864fd1a685856fc2e30e180bb9a9c36",
                zIPCODE: "c634066fcacd4b1dad279b86547dd6774",
                customerId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}