using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using IsteerPortal.CustomerAddress;
using IsteerPortal.EntityFrameworkCore;
using Xunit;

namespace IsteerPortal.CustomerAddress
{
    public class CustomerAddresRepositoryTests : IsteerPortalEntityFrameworkCoreTestBase
    {
        private readonly ICustomerAddresRepository _customerAddresRepository;

        public CustomerAddresRepositoryTests()
        {
            _customerAddresRepository = GetRequiredService<ICustomerAddresRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerAddresRepository.GetListAsync(
                    address1: "950e1d1ff3da4",
                    address2: "15421590bac2452",
                    zIPCODE: "028ef3fdc8c94df18f7c5d018f95abe4275ad7460d6e472cb165531033126900af"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("a34547e3-65c2-4920-8442-772a966faefb"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerAddresRepository.GetCountAsync(
                    address1: "88666bb8e",
                    address2: "caf8c0878f824eb6bf536fea315a47d7a517566a76864fd1a685856fc2e30e180bb9a9c36",
                    zIPCODE: "c634066fcacd4b1dad279b86547dd6774"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}