using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{
    public class SalesOrgHeadersAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ISalesOrgHeadersAppService _salesOrgHeadersAppService;
        private readonly IRepository<SalesOrgHeader, Guid> _salesOrgHeaderRepository;

        public SalesOrgHeadersAppServiceTests()
        {
            _salesOrgHeadersAppService = GetRequiredService<ISalesOrgHeadersAppService>();
            _salesOrgHeaderRepository = GetRequiredService<IRepository<SalesOrgHeader, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _salesOrgHeadersAppService.GetListAsync(new GetSalesOrgHeadersInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("4308f81c-1cb1-418e-b180-430d2e91fdbf")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("7753baa0-bd90-49f3-87ac-4cc3c687e075")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _salesOrgHeadersAppService.GetAsync(Guid.Parse("4308f81c-1cb1-418e-b180-430d2e91fdbf"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("4308f81c-1cb1-418e-b180-430d2e91fdbf"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new SalesOrgHeaderCreateDto
            {
                Code = "0b3fe5705cf146e3913e",
                Name = "a58ac8c106bb4abc9f6a47a486261ca484a10792edbb4811a613b24b2f0ee3f19cde66cc604b4832a335036f8acbf3d2194bea23934641659c924ad6ea354328201c8a2150d94e7d82704d08bba49f4ab0f5736384ed49ae9db1f182452dc924f365f4a505b44e5bb0c38856f0bba212e623330cc485488eb61a34cf19d7a7c",
                Active = true
            };

            // Act
            var serviceResult = await _salesOrgHeadersAppService.CreateAsync(input);

            // Assert
            var result = await _salesOrgHeaderRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("0b3fe5705cf146e3913e");
            result.Name.ShouldBe("a58ac8c106bb4abc9f6a47a486261ca484a10792edbb4811a613b24b2f0ee3f19cde66cc604b4832a335036f8acbf3d2194bea23934641659c924ad6ea354328201c8a2150d94e7d82704d08bba49f4ab0f5736384ed49ae9db1f182452dc924f365f4a505b44e5bb0c38856f0bba212e623330cc485488eb61a34cf19d7a7c");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new SalesOrgHeaderUpdateDto()
            {
                Code = "f86eede1e55a466ca9db",
                Name = "462f31fc8a7c48c6acc43ed50b6d29af15f137e602704aff819695c630c5d12903dfe77ee7dc4c96a5b9e1c3a95743721f8f881393524a33875213728e0e7b8166e3ce5a3a4b44c693717194aafc234fef9a4741dd8c4dea9528784d2839acdf36a266528d5345faa164c047b0e030b2d023e37feb044623b500b74a6d807d4",
                Active = true
            };

            // Act
            var serviceResult = await _salesOrgHeadersAppService.UpdateAsync(Guid.Parse("4308f81c-1cb1-418e-b180-430d2e91fdbf"), input);

            // Assert
            var result = await _salesOrgHeaderRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("f86eede1e55a466ca9db");
            result.Name.ShouldBe("462f31fc8a7c48c6acc43ed50b6d29af15f137e602704aff819695c630c5d12903dfe77ee7dc4c96a5b9e1c3a95743721f8f881393524a33875213728e0e7b8166e3ce5a3a4b44c693717194aafc234fef9a4741dd8c4dea9528784d2839acdf36a266528d5345faa164c047b0e030b2d023e37feb044623b500b74a6d807d4");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _salesOrgHeadersAppService.DeleteAsync(Guid.Parse("4308f81c-1cb1-418e-b180-430d2e91fdbf"));

            // Assert
            var result = await _salesOrgHeaderRepository.FindAsync(c => c.Id == Guid.Parse("4308f81c-1cb1-418e-b180-430d2e91fdbf"));

            result.ShouldBeNull();
        }
    }
}