using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace IsteerPortal.CustomerAddress
{
    public class CustomerAddressAppServiceTests : IsteerPortalApplicationTestBase
    {
        private readonly ICustomerAddressAppService _customerAddressAppService;
        private readonly IRepository<CustomerAddres, Guid> _customerAddresRepository;

        public CustomerAddressAppServiceTests()
        {
            _customerAddressAppService = GetRequiredService<ICustomerAddressAppService>();
            _customerAddresRepository = GetRequiredService<IRepository<CustomerAddres, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _customerAddressAppService.GetListAsync(new GetCustomerAddressInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.CustomerAddres.Id == Guid.Parse("a34547e3-65c2-4920-8442-772a966faefb")).ShouldBe(true);
            result.Items.Any(x => x.CustomerAddres.Id == Guid.Parse("4a0e359b-c4e1-45e6-aea4-8c01fee320ac")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerAddressAppService.GetAsync(Guid.Parse("a34547e3-65c2-4920-8442-772a966faefb"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("a34547e3-65c2-4920-8442-772a966faefb"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerAddresCreateDto
            {
                Address1 = "cee7ec8a6d0b403ebe16f7e104963ea1667ab",
                Address2 = "f62c58254f814bf28f1983a4552760",
                ZIPCODE = "ccf780574cf44978b3b44be7995f083ae6e357c6ff4a4a33b09a1a63c0731105c6149b902e9f479e899389b0f485ea0e"
            };

            // Act
            var serviceResult = await _customerAddressAppService.CreateAsync(input);

            // Assert
            var result = await _customerAddresRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Address1.ShouldBe("cee7ec8a6d0b403ebe16f7e104963ea1667ab");
            result.Address2.ShouldBe("f62c58254f814bf28f1983a4552760");
            result.ZIPCODE.ShouldBe("ccf780574cf44978b3b44be7995f083ae6e357c6ff4a4a33b09a1a63c0731105c6149b902e9f479e899389b0f485ea0e");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerAddresUpdateDto()
            {
                Address1 = "6bc3b85919614537b53ff1d706a80418bd664c3aceaf4056a1c",
                Address2 = "ebf71d1d8101489ba39bcc724c7156af7531a762e4d74c239bd6633b49221dfe02d7d",
                ZIPCODE = "847146"
            };

            // Act
            var serviceResult = await _customerAddressAppService.UpdateAsync(Guid.Parse("a34547e3-65c2-4920-8442-772a966faefb"), input);

            // Assert
            var result = await _customerAddresRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Address1.ShouldBe("6bc3b85919614537b53ff1d706a80418bd664c3aceaf4056a1c");
            result.Address2.ShouldBe("ebf71d1d8101489ba39bcc724c7156af7531a762e4d74c239bd6633b49221dfe02d7d");
            result.ZIPCODE.ShouldBe("847146");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerAddressAppService.DeleteAsync(Guid.Parse("a34547e3-65c2-4920-8442-772a966faefb"));

            // Assert
            var result = await _customerAddresRepository.FindAsync(c => c.Id == Guid.Parse("a34547e3-65c2-4920-8442-772a966faefb"));

            result.ShouldBeNull();
        }
    }
}