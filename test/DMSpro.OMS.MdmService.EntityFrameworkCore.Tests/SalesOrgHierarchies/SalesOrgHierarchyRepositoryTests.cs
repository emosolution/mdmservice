using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public class SalesOrgHierarchyRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;

        public SalesOrgHierarchyRepositoryTests()
        {
            _salesOrgHierarchyRepository = GetRequiredService<ISalesOrgHierarchyRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _salesOrgHierarchyRepository.GetListAsync(
                    code: "afeb4dce5b37425286b0",
                    name: "77bb8ffbb5c74b8daf7b65d446f064046d9fe0e0047f46fda67026477271f7e5d7b9dfd95be546bb9af",
                    isRoute: true,
                    isSellingZone: true,
                    hierarchyCode: "6398f174ed204fdf979e19e2fb038ea36426410745",
                    active: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("b481dbc7-677d-4199-9065-4da2e69641c5"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _salesOrgHierarchyRepository.GetCountAsync(
                    code: "1e28dc6c259d4134932c",
                    name: "66c0eb441a9d4418a9db5875078f6960de97fb57b04a4cbcaa23451a445104ce10f7a693f7b945048e",
                    isRoute: true,
                    isSellingZone: true,
                    hierarchyCode: "a43863e13a5f40aa8a1e91ef5179031bb5249513479e49a8ae555ff",
                    active: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}