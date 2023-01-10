using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
    public class ItemAttributeValuesAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IItemAttributeValuesAppService _itemAttributeValuesAppService;
        private readonly IRepository<ItemAttributeValue, Guid> _itemAttributeValueRepository;

        public ItemAttributeValuesAppServiceTests()
        {
            _itemAttributeValuesAppService = GetRequiredService<IItemAttributeValuesAppService>();
            _itemAttributeValueRepository = GetRequiredService<IRepository<ItemAttributeValue, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _itemAttributeValuesAppService.GetListAsync(new GetItemAttributeValuesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.ItemAttributeValue.Id == Guid.Parse("a3d5ab55-1f66-4a90-9b36-42ae9b380290")).ShouldBe(true);
            result.Items.Any(x => x.ItemAttributeValue.Id == Guid.Parse("c05ec9d8-99e4-444e-b228-c5273e917ec0")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _itemAttributeValuesAppService.GetAsync(Guid.Parse("a3d5ab55-1f66-4a90-9b36-42ae9b380290"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("a3d5ab55-1f66-4a90-9b36-42ae9b380290"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ItemAttributeValueCreateDto
            {
                AttrValName = "887c5dafaec5466f96d07615e7ab470723f9c6047b764d13aaa24f320baa857968deac1fbcbd4bf8b63a69e0e2015db324ea07f73ba442239618ccc57c0afe78576ead9edb294d00a019882c1b82dba07c7250809ca34bef833b7fd56f4016e63d1a21a06dfb4d1ca2c6ee9574a35d0f0c943baf74f548868f7ee63bbba138e",
                ItemAttributeId = Guid.Parse("491c657a-c618-4c58-bde2-b5156e5728f3"),

            };

            // Act
            var serviceResult = await _itemAttributeValuesAppService.CreateAsync(input);

            // Assert
            var result = await _itemAttributeValueRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AttrValName.ShouldBe("887c5dafaec5466f96d07615e7ab470723f9c6047b764d13aaa24f320baa857968deac1fbcbd4bf8b63a69e0e2015db324ea07f73ba442239618ccc57c0afe78576ead9edb294d00a019882c1b82dba07c7250809ca34bef833b7fd56f4016e63d1a21a06dfb4d1ca2c6ee9574a35d0f0c943baf74f548868f7ee63bbba138e");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ItemAttributeValueUpdateDto()
            {
                AttrValName = "e2b28539fa8d460f85ed15495f167f08d3a376d3e2bc460eae124f0c8b2fa94d5e5cfe723bc84757bd69cd8ebcd8ea0848bfb37f23bd4d3c8ae7c535b7d805691df71805cc194b68b93a28589744bfe22ea3e033ae284da2a953fbaf32e064e6cbae3561aaf745f794a6c55ef7718dcc13b28c39eca64abf886ef3b8f490941",
                ItemAttributeId = Guid.Parse("491c657a-c618-4c58-bde2-b5156e5728f3"),

            };

            // Act
            var serviceResult = await _itemAttributeValuesAppService.UpdateAsync(Guid.Parse("a3d5ab55-1f66-4a90-9b36-42ae9b380290"), input);

            // Assert
            var result = await _itemAttributeValueRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.AttrValName.ShouldBe("e2b28539fa8d460f85ed15495f167f08d3a376d3e2bc460eae124f0c8b2fa94d5e5cfe723bc84757bd69cd8ebcd8ea0848bfb37f23bd4d3c8ae7c535b7d805691df71805cc194b68b93a28589744bfe22ea3e033ae284da2a953fbaf32e064e6cbae3561aaf745f794a6c55ef7718dcc13b28c39eca64abf886ef3b8f490941");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _itemAttributeValuesAppService.DeleteAsync(Guid.Parse("a3d5ab55-1f66-4a90-9b36-42ae9b380290"));

            // Assert
            var result = await _itemAttributeValueRepository.FindAsync(c => c.Id == Guid.Parse("a3d5ab55-1f66-4a90-9b36-42ae9b380290"));

            result.ShouldBeNull();
        }
    }
}