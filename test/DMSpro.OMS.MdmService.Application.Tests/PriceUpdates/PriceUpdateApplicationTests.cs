using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
    public class PriceUpdatesAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IPriceUpdatesAppService _priceUpdatesAppService;
        private readonly IRepository<PriceUpdate, Guid> _priceUpdateRepository;

        public PriceUpdatesAppServiceTests()
        {
            _priceUpdatesAppService = GetRequiredService<IPriceUpdatesAppService>();
            _priceUpdateRepository = GetRequiredService<IRepository<PriceUpdate, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _priceUpdatesAppService.GetListAsync(new GetPriceUpdatesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.PriceUpdate.Id == Guid.Parse("78a68318-a06f-4b7c-83bf-04c616e9ea92")).ShouldBe(true);
            result.Items.Any(x => x.PriceUpdate.Id == Guid.Parse("7f7060db-f287-4989-bf0e-81aed3a69434")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _priceUpdatesAppService.GetAsync(Guid.Parse("78a68318-a06f-4b7c-83bf-04c616e9ea92"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("78a68318-a06f-4b7c-83bf-04c616e9ea92"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new PriceUpdateCreateDto
            {
                Code = "ffed9172aac94ff485cb",
                Description = "d197b8bab9a84d60b52226733248d30501d887ca1442472fbd7c975741b5f5da710b8eccda75431ba634a45abc2d044bd2ccc68130cf4c7096455dd783d26772f801cacd5caf4744a38c4113efe15f4eb4d02811b7234f20b395ae41484769cd8a9e5d21c24d4cc193a5c7c197d8da9ee4ba984493f94e18985f25b2b7b100a9d8478ab62bca43c8b9b34ac31dab315aeb51fcc7ad034630a30b65ba2c9d80726c3ab79288f5436390455a0f51f9a3600f86c6c6d5a048d4a5a0bfb069d9714c46fa3037eacf4d68b477ab8c5c780dfea849387cd0a1499bb1ee48ef397e40436d38420995414a6bb6d164a0cf02f84be401cdb9ffc44d56a377",
                EffectiveDate = new DateTime(2005, 6, 18),
                Status = default,
                UpdateStatusDate = new DateTime(2002, 6, 21),
                PriceListId = Guid.Parse("587b2afd-c04a-4dda-bcb1-759beb5e3a41")
            };

            // Act
            var serviceResult = await _priceUpdatesAppService.CreateAsync(input);

            // Assert
            var result = await _priceUpdateRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("ffed9172aac94ff485cb");
            result.Description.ShouldBe("d197b8bab9a84d60b52226733248d30501d887ca1442472fbd7c975741b5f5da710b8eccda75431ba634a45abc2d044bd2ccc68130cf4c7096455dd783d26772f801cacd5caf4744a38c4113efe15f4eb4d02811b7234f20b395ae41484769cd8a9e5d21c24d4cc193a5c7c197d8da9ee4ba984493f94e18985f25b2b7b100a9d8478ab62bca43c8b9b34ac31dab315aeb51fcc7ad034630a30b65ba2c9d80726c3ab79288f5436390455a0f51f9a3600f86c6c6d5a048d4a5a0bfb069d9714c46fa3037eacf4d68b477ab8c5c780dfea849387cd0a1499bb1ee48ef397e40436d38420995414a6bb6d164a0cf02f84be401cdb9ffc44d56a377");
            result.EffectiveDate.ShouldBe(new DateTime(2005, 6, 18));
            result.Status.ShouldBe(default);
            result.UpdateStatusDate.ShouldBe(new DateTime(2002, 6, 21));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new PriceUpdateUpdateDto()
            {
                Code = "d7216abb28dc44a48bd7",
                Description = "e3e89efc4f5b486096eba8772d856f4467c2c80305a34248b5bbb82a1a84d6bd90ad90e65f5b4cd3b5772c1297bce3d19774d00688084a198cc4ab45b2d180a66c6ccd3e5d134bfebdfd7c577a78835216edfca7d5344004bbe76ae5fec4cc99ac1636b3bc764c29b0eb4c8616db50e9e87922a82d014aab9d36a9eebfc86a79a57b3115ab9a46908af04064f89eb1d7c5d583bd30b8421e9a6c5de70a904d0bd8c04564c29f4211942ee39920401262c01ce85c9e124c08bd999952826e310ec5f23b0ad217460a8837bc093484bb4e90a5da31a043405f9f3f98732641a9145dc77c2dab7a46b8a340348c51dbb410fa0d65a056074af083e1",
                EffectiveDate = new DateTime(2022, 9, 7),
                Status = default,
                UpdateStatusDate = new DateTime(2014, 6, 10),
                PriceListId = Guid.Parse("587b2afd-c04a-4dda-bcb1-759beb5e3a41")
            };

            // Act
            var serviceResult = await _priceUpdatesAppService.UpdateAsync(Guid.Parse("78a68318-a06f-4b7c-83bf-04c616e9ea92"), input);

            // Assert
            var result = await _priceUpdateRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("d7216abb28dc44a48bd7");
            result.Description.ShouldBe("e3e89efc4f5b486096eba8772d856f4467c2c80305a34248b5bbb82a1a84d6bd90ad90e65f5b4cd3b5772c1297bce3d19774d00688084a198cc4ab45b2d180a66c6ccd3e5d134bfebdfd7c577a78835216edfca7d5344004bbe76ae5fec4cc99ac1636b3bc764c29b0eb4c8616db50e9e87922a82d014aab9d36a9eebfc86a79a57b3115ab9a46908af04064f89eb1d7c5d583bd30b8421e9a6c5de70a904d0bd8c04564c29f4211942ee39920401262c01ce85c9e124c08bd999952826e310ec5f23b0ad217460a8837bc093484bb4e90a5da31a043405f9f3f98732641a9145dc77c2dab7a46b8a340348c51dbb410fa0d65a056074af083e1");
            result.EffectiveDate.ShouldBe(new DateTime(2022, 9, 7));
            result.Status.ShouldBe(default);
            result.UpdateStatusDate.ShouldBe(new DateTime(2014, 6, 10));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _priceUpdatesAppService.DeleteAsync(Guid.Parse("78a68318-a06f-4b7c-83bf-04c616e9ea92"));

            // Assert
            var result = await _priceUpdateRepository.FindAsync(c => c.Id == Guid.Parse("78a68318-a06f-4b7c-83bf-04c616e9ea92"));

            result.ShouldBeNull();
        }
    }
}