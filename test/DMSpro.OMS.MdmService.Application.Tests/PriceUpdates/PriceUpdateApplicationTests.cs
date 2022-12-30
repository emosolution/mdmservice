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
            result.Items.Any(x => x.PriceUpdate.Id == Guid.Parse("0c78b139-1fd2-4733-b193-ecfa442b57f4")).ShouldBe(true);
            result.Items.Any(x => x.PriceUpdate.Id == Guid.Parse("f92a255c-7a02-427f-bf29-27bd85f1d3fc")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _priceUpdatesAppService.GetAsync(Guid.Parse("0c78b139-1fd2-4733-b193-ecfa442b57f4"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("0c78b139-1fd2-4733-b193-ecfa442b57f4"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new PriceUpdateCreateDto
            {
                Code = "a6e8eaf45f774bf1abdc",
                Description = "f140f78913da43b0abbde4add3588300b9dea09f1bcf4edeace7b67de631b31d85473422704c47d",
                EffectiveDate = new DateTime(2012, 11, 10),
                Status = default,
                UpdateStatusDate = new DateTime(2006, 11, 21),
                PriceListId = Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5")
            };

            // Act
            var serviceResult = await _priceUpdatesAppService.CreateAsync(input);

            // Assert
            var result = await _priceUpdateRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("a6e8eaf45f774bf1abdc");
            result.Description.ShouldBe("f140f78913da43b0abbde4add3588300b9dea09f1bcf4edeace7b67de631b31d85473422704c47d");
            result.EffectiveDate.ShouldBe(new DateTime(2012, 11, 10));
            result.Status.ShouldBe(default);
            result.UpdateStatusDate.ShouldBe(new DateTime(2006, 11, 21));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new PriceUpdateUpdateDto()
            {
                Code = "d73e81b4819d4e2aacac",
                Description = "5d0b36cdf0fe4e1",
                EffectiveDate = new DateTime(2020, 2, 4),
                Status = default,
                UpdateStatusDate = new DateTime(2002, 10, 7),
                PriceListId = Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5")
            };

            // Act
            var serviceResult = await _priceUpdatesAppService.UpdateAsync(Guid.Parse("0c78b139-1fd2-4733-b193-ecfa442b57f4"), input);

            // Assert
            var result = await _priceUpdateRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("d73e81b4819d4e2aacac");
            result.Description.ShouldBe("5d0b36cdf0fe4e1");
            result.EffectiveDate.ShouldBe(new DateTime(2020, 2, 4));
            result.Status.ShouldBe(default);
            result.UpdateStatusDate.ShouldBe(new DateTime(2002, 10, 7));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _priceUpdatesAppService.DeleteAsync(Guid.Parse("0c78b139-1fd2-4733-b193-ecfa442b57f4"));

            // Assert
            var result = await _priceUpdateRepository.FindAsync(c => c.Id == Guid.Parse("0c78b139-1fd2-4733-b193-ecfa442b57f4"));

            result.ShouldBeNull();
        }
    }
}