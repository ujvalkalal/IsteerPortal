using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using IsteerPortal.Customers;
using IsteerPortal.EntityFrameworkCore;
using Xunit;

namespace IsteerPortal.Customers
{
    public class CustomerRepositoryTests : IsteerPortalEntityFrameworkCoreTestBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerRepositoryTests()
        {
            _customerRepository = GetRequiredService<ICustomerRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerRepository.GetListAsync(
                    name: "e7031cc5408649b0837934d5117fdbee3c08e28fb2a44940b557f83e91ce333ddac279386bb5453fa8442fd9c8393697",
                    website: "826b1551",
                    emailId: "0016a15d2c0a4b81b3d603d401c7dbacd671cd3eca9",
                    contactNumber: "93239529527146d08447a42400cc0cf6020f3993012f42e8b760dba264d8c65230aab29589e449559d054de52"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("66c3307d-59d2-4782-9d4b-d2e27309ef31"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerRepository.GetCountAsync(
                    name: "d5cf94f517",
                    website: "aa602de727e94c50bf2b52718ee",
                    emailId: "9b8915d758ef",
                    contactNumber: "b43101b2d25e408"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}