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
        public async Task GetAsync()
        {
            // Act
            var result = await _priceUpdatesAppService.GetAsync(Guid.Parse("552e1fe9-81ad-4bee-9ade-1f1ae70f867f"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("552e1fe9-81ad-4bee-9ade-1f1ae70f867f"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new PriceUpdateCreateDto
            {
                Code = "9c9f7be4ab0548a3a4f3",
                Description = "27b13685da34457c980fed2f82284c18640640288e4247fd86c1f5a358e1ef4e6f599ff027ad4ab2af2fe97220308f9151d73331db6d4d7f9909c2f831a41c4087cc3ba0dbc4489a9c7212fc8ea4ddcdbd7ea6e9508347398d99c4fac881d1df6d9c57967cad43eb8951a1ce9c93abb9828bac482f144250867afc0e2a53758c97b34ce2647e458489f7d4cf78c2c345a574a4798a29435a8dc4b437c4bd0c31ea615b97718044ed823e936bd71065f535f1ce9e4be9423ca9a4fb78872d7630165bf32dbc5e4a36b50c208556662dcbf19d1feeaa3b4a25a65930ff8f0c1bfa22e5695ddb94467c9a3fc1e8c1c1ef1479d61da5b9e14551883f",
                PriceListId = Guid.Parse("4758593c-d51b-4934-a996-ee0572f5c083")
            };

            // Act
            var serviceResult = await _priceUpdatesAppService.CreateAsync(input);

            // Assert
            var result = await _priceUpdateRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("9c9f7be4ab0548a3a4f3");
            result.Description.ShouldBe("27b13685da34457c980fed2f82284c18640640288e4247fd86c1f5a358e1ef4e6f599ff027ad4ab2af2fe97220308f9151d73331db6d4d7f9909c2f831a41c4087cc3ba0dbc4489a9c7212fc8ea4ddcdbd7ea6e9508347398d99c4fac881d1df6d9c57967cad43eb8951a1ce9c93abb9828bac482f144250867afc0e2a53758c97b34ce2647e458489f7d4cf78c2c345a574a4798a29435a8dc4b437c4bd0c31ea615b97718044ed823e936bd71065f535f1ce9e4be9423ca9a4fb78872d7630165bf32dbc5e4a36b50c208556662dcbf19d1feeaa3b4a25a65930ff8f0c1bfa22e5695ddb94467c9a3fc1e8c1c1ef1479d61da5b9e14551883f");
            result.EffectiveDate.ShouldBe(null);
            result.EndDate.ShouldBe(null);
            result.Status.ShouldBe(PriceUpdateStatus.OPEN);
            result.IsScheduled.ShouldBe(false);
            result.ReleasedDate.ShouldBe(null);
            result.CancelledDate.ShouldBe(null);
            result.CompleteDate.ShouldBe(null);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new PriceUpdateUpdateDto()
            {
                Description = "32291bcf24be41508c065ef9467fd2dcf44e1f34281044e8821da9deaf494c31863e6edb467f4c1e9af61ade035a4c6e60219da069ce48a78da81016e5e84d0f4cfb97566a4447e8bc6a55f2998208e0013ed96dcd1f468b956ff4be7ba12a6e6ae7cb00587244a2a78062d4e85a1da9fdba0adecb02408fa59cc125e2125955201f8fdcb3854f70af25952a2b5ad08bba359681583d402d8dd5737c10fc0c4f8ef912c4257742f8a61baf910ec129d31a935064866548d0a05665b0b3c509e718b3e788a26d4618974614977e7d1dccab940998af1c4345ac690f0932c2d3d93246a891a71d4096bea138d6c27d8835a7bd6a5d6ab64771af6c",
            };

            // Act
            var serviceResult = await _priceUpdatesAppService.UpdateAsync(Guid.Parse("552e1fe9-81ad-4bee-9ade-1f1ae70f867f"), input);

            // Assert
            var result = await _priceUpdateRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("2f91575e4eb24b058ae9");
            result.Description.ShouldBe("32291bcf24be41508c065ef9467fd2dcf44e1f34281044e8821da9deaf494c31863e6edb467f4c1e9af61ade035a4c6e60219da069ce48a78da81016e5e84d0f4cfb97566a4447e8bc6a55f2998208e0013ed96dcd1f468b956ff4be7ba12a6e6ae7cb00587244a2a78062d4e85a1da9fdba0adecb02408fa59cc125e2125955201f8fdcb3854f70af25952a2b5ad08bba359681583d402d8dd5737c10fc0c4f8ef912c4257742f8a61baf910ec129d31a935064866548d0a05665b0b3c509e718b3e788a26d4618974614977e7d1dccab940998af1c4345ac690f0932c2d3d93246a891a71d4096bea138d6c27d8835a7bd6a5d6ab64771af6c");
            result.EffectiveDate.ShouldBe(null);
            result.EndDate.ShouldBe(null);
            result.Status.ShouldBe(PriceUpdateStatus.OPEN);
            result.IsScheduled.ShouldBe(false);
            result.ReleasedDate.ShouldBe(null);
            result.CancelledDate.ShouldBe(null);
            result.CompleteDate.ShouldBe(null);
        }
    }
}