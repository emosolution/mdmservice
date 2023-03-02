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
            result.Items.Any(x => x.SalesOrgHierarchy.Id == Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb")).ShouldBe(true);
            result.Items.Any(x => x.SalesOrgHierarchy.Id == Guid.Parse("467c0025-c593-48ef-98e0-5e5f5c17d501")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _salesOrgHierarchiesAppService.GetAsync(Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new SalesOrgHierarchyCreateDto
            {
                Code = "c4e81b1c2d3e48c495fd",
                Name = "5472ae22f9d6402ebe4d264c0e0add4fd593dfa5665c4a4d89f379e157fa4656ce49a56c1a584d9eae8381afb7e8adb6a6c23d3951c049679971f119a3908743333018cf751346d08d3b9af3ced0cabad2d6568a566048559f3993f0828c3dd2688f2c542c5149a6b1f10986c09e84917f9a854759c04616afad0bb69493386",
                Level = 6,
                IsRoute = true,
                IsSellingZone = true,
                HierarchyCode = "01b3522dedc04212a2ffff5ddd367756ecea85bcca4a4adbb7a98c33343f9eac6653cec8949340b89c54cb5e3e8d5254309dce69476c48fb87a502d27ba735f3afc3158e2e464315b39fa7eeebac7fa31541bd530d4b46f78b2d2a62d23a54c9598e4e5d6c224200a530bd8ff8e5bd8c8c15c4652aa64ec6942752c7c497863df91c6f35d5ac4c27a19f107b984eada3f4db363db28b4bcab7dc7c20e187ce732d6b37b53ede497d9e469aea0748115588023bf0b6554a78917c44c3e0f128fa384c2412d4454e58b17487334502765f5a0709511e0c4900942655782b967a94571b71f5240c4b0aa3d70143f5a1179877286ac557e94a6d877d",
                Active = true,
                SalesOrgHeaderId = Guid.Parse("4308f81c-1cb1-418e-b180-430d2e91fdbf"),

            };

            // Act
            var serviceResult = await _salesOrgHierarchiesAppService.CreateAsync(input);

            // Assert
            var result = await _salesOrgHierarchyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("c4e81b1c2d3e48c495fd");
            result.Name.ShouldBe("5472ae22f9d6402ebe4d264c0e0add4fd593dfa5665c4a4d89f379e157fa4656ce49a56c1a584d9eae8381afb7e8adb6a6c23d3951c049679971f119a3908743333018cf751346d08d3b9af3ced0cabad2d6568a566048559f3993f0828c3dd2688f2c542c5149a6b1f10986c09e84917f9a854759c04616afad0bb69493386");
            result.Level.ShouldBe(6);
            result.IsRoute.ShouldBe(true);
            result.IsSellingZone.ShouldBe(true);
            result.HierarchyCode.ShouldBe("01b3522dedc04212a2ffff5ddd367756ecea85bcca4a4adbb7a98c33343f9eac6653cec8949340b89c54cb5e3e8d5254309dce69476c48fb87a502d27ba735f3afc3158e2e464315b39fa7eeebac7fa31541bd530d4b46f78b2d2a62d23a54c9598e4e5d6c224200a530bd8ff8e5bd8c8c15c4652aa64ec6942752c7c497863df91c6f35d5ac4c27a19f107b984eada3f4db363db28b4bcab7dc7c20e187ce732d6b37b53ede497d9e469aea0748115588023bf0b6554a78917c44c3e0f128fa384c2412d4454e58b17487334502765f5a0709511e0c4900942655782b967a94571b71f5240c4b0aa3d70143f5a1179877286ac557e94a6d877d");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new SalesOrgHierarchyUpdateDto()
            {
                Code = "c109e77f1e224b67aac5",
                Name = "cf0ff478a8984cbabb73c0e682fd897e8634293615e44aef8fe9772cb4f90621afcfd3c01bc4491da017458be2c36c02a21960b703ec47669982967fcaeadf2d3bba0409ea8e42709ef64c7f5994a2f5f771139ffc26452ab2d7c4f4cccf9bc2246d6a3baee14265bf2f95947e4e1137185a48cd683740419bf01e43b952562",
                Level = 7,
                IsRoute = true,
                IsSellingZone = true,
                HierarchyCode = "634baf89d4ff462ab568861a775bcdc9f9b4ca8578a84fcc84e65ef4d2d2ccc76fd2a584e2e54c03a287bbb5f871ffaf9f7a92fafdb043e1a37a9582b9c9cd197777ddd0f4574151b797bf9da112bda7bfd2b554d98d46d785d0a545fe24d609dd68cd37613846959c54e5a4357e3de9b7c90b540a4742f39b72dd9e38004d41e3800969ee8946e8916405df641b96e24cbd07df99ca475a9211696d2ae89c4be3a262912e7a45cebd9204e42ae6d931a6a12965211448e0958ac9431a044040aba30c40f6704bdda2ba3ac8a61ccafd2edeb33819684097848350000b2201eac1a72f51cb1745068fa12df2db1c74369b6657ce20e343d5ac16",
                Active = true,
                SalesOrgHeaderId = Guid.Parse("4308f81c-1cb1-418e-b180-430d2e91fdbf"),

            };

            // Act
            var serviceResult = await _salesOrgHierarchiesAppService.UpdateAsync(Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb"), input);

            // Assert
            var result = await _salesOrgHierarchyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("c109e77f1e224b67aac5");
            result.Name.ShouldBe("cf0ff478a8984cbabb73c0e682fd897e8634293615e44aef8fe9772cb4f90621afcfd3c01bc4491da017458be2c36c02a21960b703ec47669982967fcaeadf2d3bba0409ea8e42709ef64c7f5994a2f5f771139ffc26452ab2d7c4f4cccf9bc2246d6a3baee14265bf2f95947e4e1137185a48cd683740419bf01e43b952562");
            result.Level.ShouldBe(7);
            result.IsRoute.ShouldBe(true);
            result.IsSellingZone.ShouldBe(true);
            result.HierarchyCode.ShouldBe("634baf89d4ff462ab568861a775bcdc9f9b4ca8578a84fcc84e65ef4d2d2ccc76fd2a584e2e54c03a287bbb5f871ffaf9f7a92fafdb043e1a37a9582b9c9cd197777ddd0f4574151b797bf9da112bda7bfd2b554d98d46d785d0a545fe24d609dd68cd37613846959c54e5a4357e3de9b7c90b540a4742f39b72dd9e38004d41e3800969ee8946e8916405df641b96e24cbd07df99ca475a9211696d2ae89c4be3a262912e7a45cebd9204e42ae6d931a6a12965211448e0958ac9431a044040aba30c40f6704bdda2ba3ac8a61ccafd2edeb33819684097848350000b2201eac1a72f51cb1745068fa12df2db1c74369b6657ce20e343d5ac16");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _salesOrgHierarchiesAppService.DeleteAsync(Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb"));

            // Assert
            var result = await _salesOrgHierarchyRepository.FindAsync(c => c.Id == Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb"));

            result.ShouldBeNull();
        }
    }
}