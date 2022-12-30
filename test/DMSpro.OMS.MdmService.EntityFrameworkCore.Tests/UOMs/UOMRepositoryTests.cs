using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.UOMs
{
    public class UOMRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IUOMRepository _uOMRepository;

        public UOMRepositoryTests()
        {
            _uOMRepository = GetRequiredService<IUOMRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _uOMRepository.GetListAsync(
                    code: "ae697faeeda74e08a939",
                    name: "aeaedb79c78940f4a6d98be0ee7aceefd1bfc94df57e48e4b9b960ea86c7d4af6f58e5a4870f4118966463ca1bc0e7e765f294ab7baf4e74b1984c4f5f9b938807faf21c3abd411faa067debf32ef73fc81dabd97c6248e4ae3ba46b8e13a4b5d7ea2312"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _uOMRepository.GetCountAsync(
                    code: "f5990c9776d4470eb38c",
                    name: "c7862d933af640a393967ce035506860acb2ae023b46413aa07565a862fbd9c176d3cd76d30142aab4155e5e8d9bf8640e83be24f4fb44db9c897d8bf8961fc6671ef89f270d4f2c9ef83ed0eeed7c479bc3cac0ceda47b3bd8564fe069ac10573b88f89"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}