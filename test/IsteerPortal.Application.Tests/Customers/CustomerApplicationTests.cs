using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace IsteerPortal.Customers
{
    public class CustomersAppServiceTests : IsteerPortalApplicationTestBase
    {
        private readonly ICustomersAppService _customersAppService;
        private readonly IRepository<Customer, Guid> _customerRepository;

        public CustomersAppServiceTests()
        {
            _customersAppService = GetRequiredService<ICustomersAppService>();
            _customerRepository = GetRequiredService<IRepository<Customer, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _customersAppService.GetListAsync(new GetCustomersInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("66c3307d-59d2-4782-9d4b-d2e27309ef31")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("a97070ca-9dde-46cd-8204-651d297c741a")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customersAppService.GetAsync(Guid.Parse("66c3307d-59d2-4782-9d4b-d2e27309ef31"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("66c3307d-59d2-4782-9d4b-d2e27309ef31"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerCreateDto
            {
                Name = "5dde7d424ad845798efa6189c353f2f941ba357967f447398847570c36786ca83ceb2da9ac2441dea9078159c6dc1",
                Website = "2f2babe976914008a553a509fc0f039490652325c7c2485091d91c771f5a6b",
                EmailId = "10b1879471b24b77bc0e23bb661b3004295d4ece764e43629e6c280082941a2e8c252fc9a6584f28800f4c54fa76e",
                ContactNumber = "4eeb6ec7300f4c21af638558bef7fe3c81c1dd1c87bb43dbac2119c57e0e81658128a6f89d4"
            };

            // Act
            var serviceResult = await _customersAppService.CreateAsync(input);

            // Assert
            var result = await _customerRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("5dde7d424ad845798efa6189c353f2f941ba357967f447398847570c36786ca83ceb2da9ac2441dea9078159c6dc1");
            result.Website.ShouldBe("2f2babe976914008a553a509fc0f039490652325c7c2485091d91c771f5a6b");
            result.EmailId.ShouldBe("10b1879471b24b77bc0e23bb661b3004295d4ece764e43629e6c280082941a2e8c252fc9a6584f28800f4c54fa76e");
            result.ContactNumber.ShouldBe("4eeb6ec7300f4c21af638558bef7fe3c81c1dd1c87bb43dbac2119c57e0e81658128a6f89d4");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerUpdateDto()
            {
                Name = "2c23e8445cca42e8a6cd05375f1a7ae7276ad347",
                Website = "4b88a2e2c21a456883a25be3b25a3a75398b0a2fea174fa4a4c995b48d83e3e3d2b739",
                EmailId = "8e6bbf1d99d64732a7310c80ffe1fc7b498df348a95a4430b53946a9f42ac",
                ContactNumber = "6a2bf005f9e3406e9ddf62d7c77708"
            };

            // Act
            var serviceResult = await _customersAppService.UpdateAsync(Guid.Parse("66c3307d-59d2-4782-9d4b-d2e27309ef31"), input);

            // Assert
            var result = await _customerRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("2c23e8445cca42e8a6cd05375f1a7ae7276ad347");
            result.Website.ShouldBe("4b88a2e2c21a456883a25be3b25a3a75398b0a2fea174fa4a4c995b48d83e3e3d2b739");
            result.EmailId.ShouldBe("8e6bbf1d99d64732a7310c80ffe1fc7b498df348a95a4430b53946a9f42ac");
            result.ContactNumber.ShouldBe("6a2bf005f9e3406e9ddf62d7c77708");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customersAppService.DeleteAsync(Guid.Parse("66c3307d-59d2-4782-9d4b-d2e27309ef31"));

            // Assert
            var result = await _customerRepository.FindAsync(c => c.Id == Guid.Parse("66c3307d-59d2-4782-9d4b-d2e27309ef31"));

            result.ShouldBeNull();
        }
    }
}