using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.Companies
{
    public class CompaniesAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ICompaniesAppService _companiesAppService;
        private readonly IRepository<Company, Guid> _companyRepository;

        public CompaniesAppServiceTests()
        {
            _companiesAppService = GetRequiredService<ICompaniesAppService>();
            _companyRepository = GetRequiredService<IRepository<Company, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _companiesAppService.GetListAsync(new GetCompaniesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Company.Id == Guid.Parse("5cf1d383-b77c-4d86-ae4e-1ceb0e5e0246")).ShouldBe(true);
            result.Items.Any(x => x.Company.Id == Guid.Parse("6a72cddb-40c4-4405-b648-3ba089a88f75")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _companiesAppService.GetAsync(Guid.Parse("5cf1d383-b77c-4d86-ae4e-1ceb0e5e0246"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("5cf1d383-b77c-4d86-ae4e-1ceb0e5e0246"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CompanyCreateDto
            {
                Code = "25916431787f4be8b21c",
                Name = "a99d34e9d1d94061898c4a0471bd3db776e3af74d4d74d7191c9b62396282c7542dbbd89e74e4be4aa70c4bd615f9a504cca",
                Street = "8b1c7635039b487fa126caa",
                Address = "0d0404bbf79940659d300c5ed58b249d2f879a093af24026a2536360737749526b64dd4c2fa449479d349dea6bf4602d96ce0ed243f84474aff2574de4856b58f06de31204254b93a2e60c040ae47fe7e21c5643435b44d1ad68c3018dc990e3a8203ddd972649dda76842d515f40db66a4c3457df7c417fb7fc44c9243a1ce06d21bc9e6588487189ac006ea31f8ace5aebc3eb4beb4fb99a016b43d20a77ac9dad9bfe73884c98b81f6188950e50f08f0e6482f8f64c9cb3ec259bf2d3c5648e34dd5e17244637881fa1aac46fa258e37cc88028a3423b9f36aad722df8b1982a8d52bb8c84839b3f7561fb25b27fa93150b49e52044f58f96e9cc7c2b15df17da3336189f447a94e13256a8d2d44ec3a31ade8c21455bb52c4c7737b7da05513a1e1ff84649f194aa0e97b4ef6a79fc6f59f99b244f54a48bfdb2d077e4f1e03774e6b41e448ebaba701a86e98aff7100763a3d1241759c424980c77d45a83f926e121e5642db9b6d959f8abfe1e66bc3c6d0135246de884dfeb9222a7e67f227685d3b7140e9a5ba0c96100d9eeb10250238472747719868dba4405e9e17e3847c6134a145a489bc311457651ec602d051e14e9846a9b280d1fd0ce3b38eec41d18ba2d846f6bda5a1e6b02a0b793ddb1ac909284279bc55e0852e29bb3072ed2d4b211a410298c275c43dfafbded6bd6b7c",
                Phone = "d956b1a419124b15aa40",
                License = "27ffe62b33a4400788667457b63f7f50235d772692ee4b9bbfb863c7fb8934330bc45f46d1a8407a9155b71c53cf2b4b5887",
                TaxCode = "30017e875aa34b3798b57a395389e1c94d80ac5af1ef42caae5786ec39273ef77305b8354bd548b493f34a9e31232332f806",
                VATName = "0570a7c2d1b74840b22eee02c484ce76495171f4cbf74caf9476e5a301f439791dff16c8f0b34a",
                VATAddress = "6a3bdd0be8ba4bc5aaab8dc2ca1a4204b1284fe",
                ERPCode = "08f9875c8dfe40a489a975f24ca95dd74631cd3a5be64536b4d5cd06235e5f25d32b2ac4e96d4ea19a3ac0a78e871210f761",
                Active = true,
                EffectiveDate = new DateTime(2007, 4, 9),
                EndDate = new DateTime(2020, 2, 8),
                IsHO = true,
                Latitude = "4045def8bd3043998e13804895ce9f2163dee9665a3146",
                Longitude = "264135d1b021415ba513e",
                ContactName = "35f8407ce1da4b52a14bff2f8e44e54284655be3edcc4bbd82104948b007d7d6d5b5a902351b4005aab2dbc6ca",
                ContactPhone = "bb5a08ba2e944b89931f537bbca2f"
            };

            // Act
            var serviceResult = await _companiesAppService.CreateAsync(input);

            // Assert
            var result = await _companyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("25916431787f4be8b21c");
            result.Name.ShouldBe("a99d34e9d1d94061898c4a0471bd3db776e3af74d4d74d7191c9b62396282c7542dbbd89e74e4be4aa70c4bd615f9a504cca");
            result.Street.ShouldBe("8b1c7635039b487fa126caa");
            result.Address.ShouldBe("0d0404bbf79940659d300c5ed58b249d2f879a093af24026a2536360737749526b64dd4c2fa449479d349dea6bf4602d96ce0ed243f84474aff2574de4856b58f06de31204254b93a2e60c040ae47fe7e21c5643435b44d1ad68c3018dc990e3a8203ddd972649dda76842d515f40db66a4c3457df7c417fb7fc44c9243a1ce06d21bc9e6588487189ac006ea31f8ace5aebc3eb4beb4fb99a016b43d20a77ac9dad9bfe73884c98b81f6188950e50f08f0e6482f8f64c9cb3ec259bf2d3c5648e34dd5e17244637881fa1aac46fa258e37cc88028a3423b9f36aad722df8b1982a8d52bb8c84839b3f7561fb25b27fa93150b49e52044f58f96e9cc7c2b15df17da3336189f447a94e13256a8d2d44ec3a31ade8c21455bb52c4c7737b7da05513a1e1ff84649f194aa0e97b4ef6a79fc6f59f99b244f54a48bfdb2d077e4f1e03774e6b41e448ebaba701a86e98aff7100763a3d1241759c424980c77d45a83f926e121e5642db9b6d959f8abfe1e66bc3c6d0135246de884dfeb9222a7e67f227685d3b7140e9a5ba0c96100d9eeb10250238472747719868dba4405e9e17e3847c6134a145a489bc311457651ec602d051e14e9846a9b280d1fd0ce3b38eec41d18ba2d846f6bda5a1e6b02a0b793ddb1ac909284279bc55e0852e29bb3072ed2d4b211a410298c275c43dfafbded6bd6b7c");
            result.Phone.ShouldBe("d956b1a419124b15aa40");
            result.License.ShouldBe("27ffe62b33a4400788667457b63f7f50235d772692ee4b9bbfb863c7fb8934330bc45f46d1a8407a9155b71c53cf2b4b5887");
            result.TaxCode.ShouldBe("30017e875aa34b3798b57a395389e1c94d80ac5af1ef42caae5786ec39273ef77305b8354bd548b493f34a9e31232332f806");
            result.VATName.ShouldBe("0570a7c2d1b74840b22eee02c484ce76495171f4cbf74caf9476e5a301f439791dff16c8f0b34a");
            result.VATAddress.ShouldBe("6a3bdd0be8ba4bc5aaab8dc2ca1a4204b1284fe");
            result.ERPCode.ShouldBe("08f9875c8dfe40a489a975f24ca95dd74631cd3a5be64536b4d5cd06235e5f25d32b2ac4e96d4ea19a3ac0a78e871210f761");
            result.Active.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2007, 4, 9));
            result.EndDate.ShouldBe(new DateTime(2020, 2, 8));
            result.IsHO.ShouldBe(true);
            result.Latitude.ShouldBe("4045def8bd3043998e13804895ce9f2163dee9665a3146");
            result.Longitude.ShouldBe("264135d1b021415ba513e");
            result.ContactName.ShouldBe("35f8407ce1da4b52a14bff2f8e44e54284655be3edcc4bbd82104948b007d7d6d5b5a902351b4005aab2dbc6ca");
            result.ContactPhone.ShouldBe("bb5a08ba2e944b89931f537bbca2f");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CompanyUpdateDto()
            {
                Code = "e7ff4e3d6b074787abf0",
                Name = "f28799bda91246c0baf260f2b4a402a3f1475796b51148049f659fcae43e8fed0428515ac9d84861bd12176bfc4bdf506a4a",
                Street = "5a83039ceb",
                Address = "ee1139b0c69043c88562191f9c7d116645a85fc68d16413794e716ccee424b243dcf4e878454480fa27cc2d83a184f39a2712eb2e3864b2f9ffc4f82916b2be4ae83c87f364942829e649d57d09a4fe4b0325c0f32964114be80fe1e8a6faf724e49b020c1a14c808d2b27883a999187368124cd69d9426284e1271df769141880410fad688c4feab33800a4a48e037b2b20f7b72d0b4a7c90cea654cc3ea0c7f3c43c081a6f4525bff8d324e6adad144c428389b8f24a2da1a516eb41e4a9b14ee444432eea4859868abd0ae4445b18f44a1fd6e9fc449ebac3eb10f54d9e6542b80cd6253441b898cac99fb7174d04feff8269ee334432b204d2ec376a03e94e38609a03fa46c2b8eac78db708365c295b1f5bc78d4096a4e7fe2f257ad627a26ea016b2b2465799a17e5b118fcce6241b752867124525bf9e846ac2b7eeef7cc69d1494ef4931ad1f708074b1c4c69e917cae7b094b5c9a66fbab79f8e52193d14eaece3e4088a4a463b3b2e5e145d59ed13d19cb47fbaaa692979a63a090e6b31e13750747469c7b18a2d60b686b418899c4326c4aa0a6b686253d1d48ebec1cf04f3e674c35a8fd62f00a7820f6af2762c6226d46b1b35a604a9846387fe92a1d04341b4db0a0edb50a7e311fdb7d95062dd082447e9df56e68d5f9e1f634c47f41a6a1463abb7ed4f39cfb7d7ff8476a20",
                Phone = "bf8aebc0cbac461b82f8",
                License = "a8db97b1733c46658067dfdbd9de4588eddb0f781a98440e91eeb9e5a82950625226f795950b4fa4a07588e8665f98a30add",
                TaxCode = "cf0928b4c9b3484991228e5cf3e282760b0be7b45ae44488aec35561c9a98009c211ad9068104705b28622c7527d28fd2abc",
                VATName = "c88a4a4bab484bebbfc5ac84afa32c4c9b38135e38e84005b3d18f4d88e5c5dca1e",
                VATAddress = "428f734cee3546669378392f4fbbbb60e4a9d198cd2240a2843c974d07",
                ERPCode = "1e39fdf3352b4212aac54c81530c5b15383b5cb77d224a11b9f89d08d0a8bbb52310bd7a18044f818165d873520488ef3934",
                Active = true,
                EffectiveDate = new DateTime(2021, 3, 18),
                EndDate = new DateTime(2006, 4, 24),
                IsHO = true,
                Latitude = "a1e0fcc9003e42d1a298c9fa35b4d3482fa20716a3a94e70915450aade0a47cfcff3306495c34f84a1af",
                Longitude = "4a8517f9c8ad458cbf5",
                ContactName = "fbc5d246d1524f9c827b6",
                ContactPhone = "743a5418ce46446fa852fa50a48e94caef9c655da4254cfdba0c4c2c2d23d985df1d076c85264e47bf294"
            };

            // Act
            var serviceResult = await _companiesAppService.UpdateAsync(Guid.Parse("5cf1d383-b77c-4d86-ae4e-1ceb0e5e0246"), input);

            // Assert
            var result = await _companyRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("e7ff4e3d6b074787abf0");
            result.Name.ShouldBe("f28799bda91246c0baf260f2b4a402a3f1475796b51148049f659fcae43e8fed0428515ac9d84861bd12176bfc4bdf506a4a");
            result.Street.ShouldBe("5a83039ceb");
            result.Address.ShouldBe("ee1139b0c69043c88562191f9c7d116645a85fc68d16413794e716ccee424b243dcf4e878454480fa27cc2d83a184f39a2712eb2e3864b2f9ffc4f82916b2be4ae83c87f364942829e649d57d09a4fe4b0325c0f32964114be80fe1e8a6faf724e49b020c1a14c808d2b27883a999187368124cd69d9426284e1271df769141880410fad688c4feab33800a4a48e037b2b20f7b72d0b4a7c90cea654cc3ea0c7f3c43c081a6f4525bff8d324e6adad144c428389b8f24a2da1a516eb41e4a9b14ee444432eea4859868abd0ae4445b18f44a1fd6e9fc449ebac3eb10f54d9e6542b80cd6253441b898cac99fb7174d04feff8269ee334432b204d2ec376a03e94e38609a03fa46c2b8eac78db708365c295b1f5bc78d4096a4e7fe2f257ad627a26ea016b2b2465799a17e5b118fcce6241b752867124525bf9e846ac2b7eeef7cc69d1494ef4931ad1f708074b1c4c69e917cae7b094b5c9a66fbab79f8e52193d14eaece3e4088a4a463b3b2e5e145d59ed13d19cb47fbaaa692979a63a090e6b31e13750747469c7b18a2d60b686b418899c4326c4aa0a6b686253d1d48ebec1cf04f3e674c35a8fd62f00a7820f6af2762c6226d46b1b35a604a9846387fe92a1d04341b4db0a0edb50a7e311fdb7d95062dd082447e9df56e68d5f9e1f634c47f41a6a1463abb7ed4f39cfb7d7ff8476a20");
            result.Phone.ShouldBe("bf8aebc0cbac461b82f8");
            result.License.ShouldBe("a8db97b1733c46658067dfdbd9de4588eddb0f781a98440e91eeb9e5a82950625226f795950b4fa4a07588e8665f98a30add");
            result.TaxCode.ShouldBe("cf0928b4c9b3484991228e5cf3e282760b0be7b45ae44488aec35561c9a98009c211ad9068104705b28622c7527d28fd2abc");
            result.VATName.ShouldBe("c88a4a4bab484bebbfc5ac84afa32c4c9b38135e38e84005b3d18f4d88e5c5dca1e");
            result.VATAddress.ShouldBe("428f734cee3546669378392f4fbbbb60e4a9d198cd2240a2843c974d07");
            result.ERPCode.ShouldBe("1e39fdf3352b4212aac54c81530c5b15383b5cb77d224a11b9f89d08d0a8bbb52310bd7a18044f818165d873520488ef3934");
            result.Active.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2021, 3, 18));
            result.EndDate.ShouldBe(new DateTime(2006, 4, 24));
            result.IsHO.ShouldBe(true);
            result.Latitude.ShouldBe("a1e0fcc9003e42d1a298c9fa35b4d3482fa20716a3a94e70915450aade0a47cfcff3306495c34f84a1af");
            result.Longitude.ShouldBe("4a8517f9c8ad458cbf5");
            result.ContactName.ShouldBe("fbc5d246d1524f9c827b6");
            result.ContactPhone.ShouldBe("743a5418ce46446fa852fa50a48e94caef9c655da4254cfdba0c4c2c2d23d985df1d076c85264e47bf294");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _companiesAppService.DeleteAsync(Guid.Parse("5cf1d383-b77c-4d86-ae4e-1ceb0e5e0246"));

            // Assert
            var result = await _companyRepository.FindAsync(c => c.Id == Guid.Parse("5cf1d383-b77c-4d86-ae4e-1ceb0e5e0246"));

            result.ShouldBeNull();
        }
    }
}