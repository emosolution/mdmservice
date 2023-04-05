using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{
    public class CustomerGroupGeosAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ICustomerGroupGeosAppService _customerGroupGeosAppService;
        private readonly IRepository<CustomerGroupGeo, Guid> _customerGroupGeoRepository;

        public CustomerGroupGeosAppServiceTests()
        {
            _customerGroupGeosAppService = GetRequiredService<ICustomerGroupGeosAppService>();
            _customerGroupGeoRepository = GetRequiredService<IRepository<CustomerGroupGeo, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _customerGroupGeosAppService.GetListAsync(new GetCustomerGroupGeosInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.CustomerGroupGeo.Id == Guid.Parse("421897c5-aa57-451f-aab6-deab196cf440")).ShouldBe(true);
            result.Items.Any(x => x.CustomerGroupGeo.Id == Guid.Parse("d7f0d2d0-ba61-487d-b3d4-6ef61013e063")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerGroupGeosAppService.GetAsync(Guid.Parse("421897c5-aa57-451f-aab6-deab196cf440"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("421897c5-aa57-451f-aab6-deab196cf440"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerGroupGeoCreateDto
            {
                Description = "015c743bffdb4cd081a1",
                Active = true,
                CustomerGroupId = Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f"),
                GeoMaster0Id = Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367"),
                GeoMaster1Id = Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367"),
                GeoMaster2Id = Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367"),
                GeoMaster3Id = Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367"),
                GeoMaster4Id = Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367")
            };

            // Act
            var serviceResult = await _customerGroupGeosAppService.CreateAsync(input);

            // Assert
            var result = await _customerGroupGeoRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("015c743bffdb4cd081a1");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerGroupGeoUpdateDto()
            {
                Description = "e04a9d9905334b859988",
                Active = true,
                CustomerGroupId = Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f"),
                GeoMaster0Id = Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367"),
                GeoMaster1Id = Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367"),
                GeoMaster2Id = Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367"),
                GeoMaster3Id = Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367"),
                GeoMaster4Id = Guid.Parse("d9d076a2-ac81-4ca0-950c-5a573acea367")
            };

            // Act
            var serviceResult = await _customerGroupGeosAppService.UpdateAsync(Guid.Parse("421897c5-aa57-451f-aab6-deab196cf440"), input);

            // Assert
            var result = await _customerGroupGeoRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("e04a9d9905334b859988");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerGroupGeosAppService.DeleteAsync(Guid.Parse("421897c5-aa57-451f-aab6-deab196cf440"));

            // Assert
            var result = await _customerGroupGeoRepository.FindAsync(c => c.Id == Guid.Parse("421897c5-aa57-451f-aab6-deab196cf440"));

            result.ShouldBeNull();
        }
    }
}