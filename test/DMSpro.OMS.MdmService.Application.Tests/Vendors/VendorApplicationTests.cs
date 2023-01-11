using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.Vendors
{
    public class VendorsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IVendorsAppService _vendorsAppService;
        private readonly IRepository<Vendor, Guid> _vendorRepository;

        public VendorsAppServiceTests()
        {
            _vendorsAppService = GetRequiredService<IVendorsAppService>();
            _vendorRepository = GetRequiredService<IRepository<Vendor, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _vendorsAppService.GetListAsync(new GetVendorsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Vendor.Id == Guid.Parse("e19c23e6-f052-4267-bae1-7c923cd2d7db")).ShouldBe(true);
            result.Items.Any(x => x.Vendor.Id == Guid.Parse("fdb433da-2f47-466d-a83e-ae779c0cc364")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _vendorsAppService.GetAsync(Guid.Parse("e19c23e6-f052-4267-bae1-7c923cd2d7db"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("e19c23e6-f052-4267-bae1-7c923cd2d7db"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new VendorCreateDto
            {
                Code = "6cf79d7cc3bd40d0b976",
                Name = "7b6135d5b6bc44bfaf12a34fe3fefc9925ac866ea6284bb3a5706705abbdc2ab3dee1d475a9d4c63bc9f47e22d96cb211565fd366f394314bb6781646122dc6aa90a945516404e08b957f11b893a272ee229a16d72634fbb8edabb95b0aaad6d5e91890d",
                ShortName = "1ddf9535a3964d299286beb67c6b75b48f91cb7783ad4eed9b0850f2f9ae7485200a9be03fd14eb5aac3e27830a8407a40400786523941cd98abbd4528dc50236b849e26551e43aab0effc48c2f74269cc1d279f27104f508f1dfcc443aeb65aec5e1efd",
                Phone1 = "fe4665f9ce574f91a77b83642840fa98a63ca85441aa4854bc96611f1298969c989d3310e3664acc96b1dfcdf8f360",
                Phone2 = "44677742462a41f49762",
                ERPCode = "243c965",
                Active = true,
                EndDate = new DateTime(2004, 9, 3),
                LinkedCompany = "5131172c376f4d8587f9",
                WarehouseId = Guid.Parse("4823f242-d2ce-4725-b0ec-523b9c7f651a"),
                Street = "f97e296294c4444eb8f22f6d161ae24f2a1b3b7c4948492c969c32bc27dcc",
                Address = "5b70370d728b4ae08dc57bf07a4cbb9203033",
                Latitude = "191f7f3e9ce14103a0d5aecdaef1f2f60dc85a50834c4a28bc27204",
                Longitude = "8cc3d8e8f82f419789364432fc583f20457e673ec7984377ba",
                PriceListId = Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),
                CompanyId = Guid.Parse("5cf1d383-b77c-4d86-ae4e-1ceb0e5e0246")
            };

            // Act
            var serviceResult = await _vendorsAppService.CreateAsync(input);

            // Assert
            var result = await _vendorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("6cf79d7cc3bd40d0b976");
            result.Name.ShouldBe("7b6135d5b6bc44bfaf12a34fe3fefc9925ac866ea6284bb3a5706705abbdc2ab3dee1d475a9d4c63bc9f47e22d96cb211565fd366f394314bb6781646122dc6aa90a945516404e08b957f11b893a272ee229a16d72634fbb8edabb95b0aaad6d5e91890d");
            result.ShortName.ShouldBe("1ddf9535a3964d299286beb67c6b75b48f91cb7783ad4eed9b0850f2f9ae7485200a9be03fd14eb5aac3e27830a8407a40400786523941cd98abbd4528dc50236b849e26551e43aab0effc48c2f74269cc1d279f27104f508f1dfcc443aeb65aec5e1efd");
            result.Phone1.ShouldBe("fe4665f9ce574f91a77b83642840fa98a63ca85441aa4854bc96611f1298969c989d3310e3664acc96b1dfcdf8f360");
            result.Phone2.ShouldBe("44677742462a41f49762");
            result.ERPCode.ShouldBe("243c965");
            result.Active.ShouldBe(true);
            result.EndDate.ShouldBe(new DateTime(2004, 9, 3));
            result.LinkedCompany.ShouldBe("5131172c376f4d8587f9");
            result.WarehouseId.ShouldBe(Guid.Parse("4823f242-d2ce-4725-b0ec-523b9c7f651a"));
            result.Street.ShouldBe("f97e296294c4444eb8f22f6d161ae24f2a1b3b7c4948492c969c32bc27dcc");
            result.Address.ShouldBe("5b70370d728b4ae08dc57bf07a4cbb9203033");
            result.Latitude.ShouldBe("191f7f3e9ce14103a0d5aecdaef1f2f60dc85a50834c4a28bc27204");
            result.Longitude.ShouldBe("8cc3d8e8f82f419789364432fc583f20457e673ec7984377ba");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new VendorUpdateDto()
            {
                Code = "ee34c8e515aa4a71a71a",
                Name = "a4478395ccf8466694eaf4e0f4f7a87deca364baff9942939f3bf30efa6d398ace4af4c19cd54a52a14ad0e614642e5674dac4e1e1424e4c889c8c6848f276e1b6f8886da6c745c3b575f4a77712e9ec2d4e6f97a13149768d3ad0393d86aa7af6f16248",
                ShortName = "0d5198bc64014f7ca074148707a67c0f91eb42471fa44c6ba5594a5a889189aa3e8ad2484b8d41a8ad6e785866e2354fa0c1a1f95c064db8a77b49f352429eccf2b515d9c4c7437db04479c9681f9841dfa3ec19b17d4d438124ff3d6b4c95d5ea0dcc22",
                Phone1 = "cf6c3530e49d40f68db9f4b7829000570823168aff86463380afa1f3844ff169ac35b93c714b4196a6eb",
                Phone2 = "3c9ea2f2007a4a888981916e8a93a4076df898aae6ce41b088d73deeb612c5025cdc823042",
                ERPCode = "7173a625ae5349809",
                Active = true,
                EndDate = new DateTime(2003, 2, 19),
                LinkedCompany = "ddae8188792b4c45aa07",
                WarehouseId = Guid.Parse("e655cda1-d7b1-4101-b242-85469acb652a"),
                Street = "5590bd6c7d094ea891f7a5e631cabf6c64e4d4d13fca4d3ab6b6429e6b04a5031f04130cbc",
                Address = "8323e9c2929546a2b41c2b2732b346f7e52",
                Latitude = "012b33c2b0ea41deaadd37d2314fc55622aba414d1e04bbb9e80786274dbd36b7306f",
                Longitude = "ed264847fab042f5a6",
                PriceListId = Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),
                CompanyId = Guid.Parse("5cf1d383-b77c-4d86-ae4e-1ceb0e5e0246")
            };

            // Act
            var serviceResult = await _vendorsAppService.UpdateAsync(Guid.Parse("e19c23e6-f052-4267-bae1-7c923cd2d7db"), input);

            // Assert
            var result = await _vendorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("ee34c8e515aa4a71a71a");
            result.Name.ShouldBe("a4478395ccf8466694eaf4e0f4f7a87deca364baff9942939f3bf30efa6d398ace4af4c19cd54a52a14ad0e614642e5674dac4e1e1424e4c889c8c6848f276e1b6f8886da6c745c3b575f4a77712e9ec2d4e6f97a13149768d3ad0393d86aa7af6f16248");
            result.ShortName.ShouldBe("0d5198bc64014f7ca074148707a67c0f91eb42471fa44c6ba5594a5a889189aa3e8ad2484b8d41a8ad6e785866e2354fa0c1a1f95c064db8a77b49f352429eccf2b515d9c4c7437db04479c9681f9841dfa3ec19b17d4d438124ff3d6b4c95d5ea0dcc22");
            result.Phone1.ShouldBe("cf6c3530e49d40f68db9f4b7829000570823168aff86463380afa1f3844ff169ac35b93c714b4196a6eb");
            result.Phone2.ShouldBe("3c9ea2f2007a4a888981916e8a93a4076df898aae6ce41b088d73deeb612c5025cdc823042");
            result.ERPCode.ShouldBe("7173a625ae5349809");
            result.Active.ShouldBe(true);
            result.EndDate.ShouldBe(new DateTime(2003, 2, 19));
            result.LinkedCompany.ShouldBe("ddae8188792b4c45aa07");
            result.WarehouseId.ShouldBe(Guid.Parse("e655cda1-d7b1-4101-b242-85469acb652a"));
            result.Street.ShouldBe("5590bd6c7d094ea891f7a5e631cabf6c64e4d4d13fca4d3ab6b6429e6b04a5031f04130cbc");
            result.Address.ShouldBe("8323e9c2929546a2b41c2b2732b346f7e52");
            result.Latitude.ShouldBe("012b33c2b0ea41deaadd37d2314fc55622aba414d1e04bbb9e80786274dbd36b7306f");
            result.Longitude.ShouldBe("ed264847fab042f5a6");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _vendorsAppService.DeleteAsync(Guid.Parse("e19c23e6-f052-4267-bae1-7c923cd2d7db"));

            // Assert
            var result = await _vendorRepository.FindAsync(c => c.Id == Guid.Parse("e19c23e6-f052-4267-bae1-7c923cd2d7db"));

            result.ShouldBeNull();
        }
    }
}