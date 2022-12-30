using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public class SalesOrgHierarchiesAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ISalesOrgHierarchiesAppService _salesOrgHierarchiesAppService;
        private readonly IRepository<SalesOrgHierarchy, Guid> _salesOrgHierarchyRepository;

        public SalesOrgHierarchiesAppServiceTests()
        {
            _salesOrgHierarchiesAppService = GetRequiredService<ISalesOrgHierarchiesAppService>();
            _salesOrgHierarchyRepository = GetRequiredService<IRepository<SalesOrgHierarchy, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _salesOrgHierarchiesAppService.GetListAsync(new GetSalesOrgHierarchiesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.SalesOrgHierarchy.Id == Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5")).ShouldBe(true);
            result.Items.Any(x => x.SalesOrgHierarchy.Id == Guid.Parse("272d040f-b303-4617-b1cd-dc72b98aebbd")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _salesOrgHierarchiesAppService.GetAsync(Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new SalesOrgHierarchyCreateDto
            {
                Code = "01c61c6132424fceb6a6",
                Name = "2ff58aeb7d9b4899a9aaf5c80883b44423ff7063c2c5408892aa8085ab8d7f5dfbde40ead774496fbcf91cdcf30",
                Level = 4,
                IsRoute = true,
                IsSellingZone = true,
                HierarchyCode = "6c5839759aa54255a5ba20fefdee82c4fed2526d0eeb49109a748f920a5c652a85a1d830dcb249ab9800a2",
                Active = true,
                SalesOrgHeaderId = Guid.Parse("df3df657-5875-4172-b267-cc75c79376a9"),

            };

            // Act
            var serviceResult = await _salesOrgHierarchiesAppService.CreateAsync(input);

            // Assert
            var result = await _salesOrgHierarchyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("01c61c6132424fceb6a6");
            result.Name.ShouldBe("2ff58aeb7d9b4899a9aaf5c80883b44423ff7063c2c5408892aa8085ab8d7f5dfbde40ead774496fbcf91cdcf30");
            result.Level.ShouldBe(4);
            result.IsRoute.ShouldBe(true);
            result.IsSellingZone.ShouldBe(true);
            result.HierarchyCode.ShouldBe("6c5839759aa54255a5ba20fefdee82c4fed2526d0eeb49109a748f920a5c652a85a1d830dcb249ab9800a2");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new SalesOrgHierarchyUpdateDto()
            {
                Code = "7f278db70138475495fe",
                Name = "312e450b134b4a3e9",
                Level = 2,
                IsRoute = true,
                IsSellingZone = true,
                HierarchyCode = "7078621caad843fd9125e5a16c7e",
                Active = true,
                SalesOrgHeaderId = Guid.Parse("df3df657-5875-4172-b267-cc75c79376a9"),

            };

            // Act
            var serviceResult = await _salesOrgHierarchiesAppService.UpdateAsync(Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"), input);

            // Assert
            var result = await _salesOrgHierarchyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("7f278db70138475495fe");
            result.Name.ShouldBe("312e450b134b4a3e9");
            result.Level.ShouldBe(2);
            result.IsRoute.ShouldBe(true);
            result.IsSellingZone.ShouldBe(true);
            result.HierarchyCode.ShouldBe("7078621caad843fd9125e5a16c7e");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _salesOrgHierarchiesAppService.DeleteAsync(Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"));

            // Assert
            var result = await _salesOrgHierarchyRepository.FindAsync(c => c.Id == Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"));

            result.ShouldBeNull();
        }
    }
}