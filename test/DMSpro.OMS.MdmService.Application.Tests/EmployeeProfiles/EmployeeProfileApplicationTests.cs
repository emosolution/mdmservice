using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    public class EmployeeProfilesAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IEmployeeProfilesAppService _employeeProfilesAppService;
        private readonly IRepository<EmployeeProfile, Guid> _employeeProfileRepository;

        public EmployeeProfilesAppServiceTests()
        {
            _employeeProfilesAppService = GetRequiredService<IEmployeeProfilesAppService>();
            _employeeProfileRepository = GetRequiredService<IRepository<EmployeeProfile, Guid>>();
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _employeeProfilesAppService.GetAsync(Guid.Parse("3fc2a4ad-9179-4e93-a189-3ea230977844"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("3fc2a4ad-9179-4e93-a189-3ea230977844"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new EmployeeProfileCreateDto
            {
                Code = "df7ea45425054e4a8ff5",
                ERPCode = "6426de0460c54abea157",
                FirstName = "e50c4eae3a424611addceadf1b90bdfa0d4c37e873f9491b8d2313e57245826a2822a1d1e0b94d02bafc4456e461438bd4d836be39fd43b5a678121d0aab7d3f26ac3edf2ab64b2c8bf4a8b6b5c361fdcc28eb89b7ad4f32a1289ff7ce40482ea0872341ed0848de9f41e90d1a3014b87d4f2da100364fbaba8a822022dcb2d",
                LastName = "cceb371bb88b40a08d3a896de0520f2aa1162555f6c949d08f2d61a6d24ffc509cc0f8ff87244b4bac60a447584fb618ffa33b1ee3274a58870ba1aa1504fb20281b74b070364f5d9bb93666a85bda932550883b510b4a228055042d47c2fcdd7dc29ae29dad416aadd9ae5f5a5ae0bd31f4f7fa2a344c2193fa89818bc066f",
                DateOfBirth = new DateTime(2012, 5, 19),
                IdCardNumber = "4896e72aa8b94d56aeb974239b6420ebba7c76f91a694cbdada6ad74f5de40af265a7397e95940428288c5de1018ef9f3cba73f1b9104832b5b718eec3f13f6fd2ba095b5e674cea880ae5fb2cefc24ee49e335d3047494a9f29886630af493a517d89357659424cb18c92e0c0b073447b0e6037faac4b63b8c590eb960285e",
                Email = "8571cd3347764c3f99f10c8304eadc91aaa134d44aba4b5db5ab02eee5c5f924e51c332f2796486fbdbf000ce0bb2ab8f17ae6b1ba4d4afb860217d68d49@51b957e0bf434fb9bd2e8b7da1b3546322caac3806c74f4ab4fc4a236ac4d28b1d3307bdbf804dca91e936bf220ed41191038891fca443a0b3c8601943e1.com",
                Phone = "910d77394a8d460fb96571544fb63b2b9f75e6944aac4393825f2a856dcda3848dd28f2531de4e11a646e1ab8138782356cf6ac3c4ae481189609e7e69c0e21b55df3e7a0ce64a538113b46c1ef914b28fafba675b7246e7aede06463ef4ad8080e0524dd2364a60a85d6cd485ad2daac7cf7c661a3e4efc9df012b5691d6c4",
                Address = "36cdaf6a471649889f4ff1acd5a3f425ca99d09216164abd862cdb282b918d1228d0d12cd176422a8ff4056ac3336eeb8205c68356254608b726ffe3b7101288243005858b8843008629218d7c86d172b17bef8b9c254e6c8deeb84ccc8cd3ba0105636cce63423cb21b25758fd166212f1e9d58ff294c45a785ba89ce6c5266c90c70bac47c40f6a96009f304b3ee033f9d13bcb1344b1c9d3c977bb75d72646da579312d254a3f94bd4403471c807c8725ada5f51544178adb2bc6a69e5f33c6aed080cdc14c1095672755e47ef6ee42b9ea1e2138435682004963493047e2149c6691f0f94dfbaf7f1bea6ceebf564a66562914dd47999295",
                Active = true,
                EffectiveDate = new DateTime(2015, 9, 4),
                EndDate = new DateTime(2014, 7, 5),
                IdentityUserId = Guid.Parse("0e2862b9-c56a-47b9-8db6-e0974012bf11")
            };

            // Act
            var serviceResult = await _employeeProfilesAppService.CreateAsync(input);

            // Assert
            var result = await _employeeProfileRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("df7ea45425054e4a8ff5");
            result.ERPCode.ShouldBe("6426de0460c54abea157");
            result.FirstName.ShouldBe("e50c4eae3a424611addceadf1b90bdfa0d4c37e873f9491b8d2313e57245826a2822a1d1e0b94d02bafc4456e461438bd4d836be39fd43b5a678121d0aab7d3f26ac3edf2ab64b2c8bf4a8b6b5c361fdcc28eb89b7ad4f32a1289ff7ce40482ea0872341ed0848de9f41e90d1a3014b87d4f2da100364fbaba8a822022dcb2d");
            result.LastName.ShouldBe("cceb371bb88b40a08d3a896de0520f2aa1162555f6c949d08f2d61a6d24ffc509cc0f8ff87244b4bac60a447584fb618ffa33b1ee3274a58870ba1aa1504fb20281b74b070364f5d9bb93666a85bda932550883b510b4a228055042d47c2fcdd7dc29ae29dad416aadd9ae5f5a5ae0bd31f4f7fa2a344c2193fa89818bc066f");
            result.DateOfBirth.ShouldBe(new DateTime(2012, 5, 19));
            result.IdCardNumber.ShouldBe("4896e72aa8b94d56aeb974239b6420ebba7c76f91a694cbdada6ad74f5de40af265a7397e95940428288c5de1018ef9f3cba73f1b9104832b5b718eec3f13f6fd2ba095b5e674cea880ae5fb2cefc24ee49e335d3047494a9f29886630af493a517d89357659424cb18c92e0c0b073447b0e6037faac4b63b8c590eb960285e");
            result.Email.ShouldBe("8571cd3347764c3f99f10c8304eadc91aaa134d44aba4b5db5ab02eee5c5f924e51c332f2796486fbdbf000ce0bb2ab8f17ae6b1ba4d4afb860217d68d49@51b957e0bf434fb9bd2e8b7da1b3546322caac3806c74f4ab4fc4a236ac4d28b1d3307bdbf804dca91e936bf220ed41191038891fca443a0b3c8601943e1.com");
            result.Phone.ShouldBe("910d77394a8d460fb96571544fb63b2b9f75e6944aac4393825f2a856dcda3848dd28f2531de4e11a646e1ab8138782356cf6ac3c4ae481189609e7e69c0e21b55df3e7a0ce64a538113b46c1ef914b28fafba675b7246e7aede06463ef4ad8080e0524dd2364a60a85d6cd485ad2daac7cf7c661a3e4efc9df012b5691d6c4");
            result.Address.ShouldBe("36cdaf6a471649889f4ff1acd5a3f425ca99d09216164abd862cdb282b918d1228d0d12cd176422a8ff4056ac3336eeb8205c68356254608b726ffe3b7101288243005858b8843008629218d7c86d172b17bef8b9c254e6c8deeb84ccc8cd3ba0105636cce63423cb21b25758fd166212f1e9d58ff294c45a785ba89ce6c5266c90c70bac47c40f6a96009f304b3ee033f9d13bcb1344b1c9d3c977bb75d72646da579312d254a3f94bd4403471c807c8725ada5f51544178adb2bc6a69e5f33c6aed080cdc14c1095672755e47ef6ee42b9ea1e2138435682004963493047e2149c6691f0f94dfbaf7f1bea6ceebf564a66562914dd47999295");
            result.Active.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2015, 9, 4));
            result.EndDate.ShouldBe(new DateTime(2014, 7, 5));
            result.IdentityUserId.ShouldBe(Guid.Parse("0e2862b9-c56a-47b9-8db6-e0974012bf11"));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new EmployeeProfileUpdateDto()
            {
                Code = "4778e14d27434e3f9c34",
                ERPCode = "aced867cbe0d42af9b04",
                FirstName = "65fec8c642ea42e9bbcbe62180ef58a1d73c7aed5d41492f97e4f66280efb103923a68b2725e4091bf9315fecda4dd94869d467feb8240428fb6af9af86bdfa9d08eb21938604e9f89458de05c6d402b576f8d92441041f1ae1559f26446509cdf3462c02061452da9c0f37c49a5767ba66409a3775c4619afb84a3292e88c5",
                LastName = "193e522387754ae5b318299c76ce499e156eedc7180b4e89a2953c198f92ce405199bff9074a43dc9956ad2dd01de031fa31309165a546ee987a89650887185d28e2ddf7d7744c298fe0ffa71e7af8e1db607d0724014d00abe41bbdc3a8efe6fe07a4dfbbca4feb9c824272438e21f4edde65ff79984b44b1d251db2be990f",
                DateOfBirth = new DateTime(2013, 7, 5),
                IdCardNumber = "d8fe88b319f04936ae7f08282f01e20950f08d8dd4e646b384ab4a2e503f124923edaedc79a94ed499dc4dc2031beddc1be8244568a041f49abae8fb5fdc2860090bc986cee24ea98172b9cecdf6874e5c12b90691c7404e92c56ac4a08541994fb282cbbc8248988f6e2e41cff1d21fc5a640eda1904b0d9f45aca8fa4d8b0",
                Email = "e91c9549c9f342189aabc15cbe9c1dcf883e8c7a790c4f7bb1cddaf10aa5c21cf97c2d2f86c0495698eac9b62e45f2aa79f23b6846ee4207889bc0241bff@ba51882982ec4a6088d0f3fddb1454626a594f7eebc84b38950a7b550551569372ab2d62b13b464fbffe63498dfcc1ae540dd9e42a6942bcadb6f571fd9f.com",
                Phone = "4370ebd7679d4671810fbc3eee7b9c7bca9ad50ae2754d75b28dafb67541a5e267f71031778c414388a1cdc587d3c7f4ef8a04b0bf9543b788a064adb3fa7c0237e02786c624424785f0c109404b5dc753ac814c8f9f4052b0d1720fc00f8463cfb5374ce8b244629165e101cb0df7facf300f0775184ac0962b3c9581f4b80",
                Address = "f8ae5dd153fb4bfda6ee5653b4d6d7dc020493f098e041b7942f24d43f16cf6a37fd4eea15f645a9a5a9c14b1664e482d4ac901169b849a78857ffdd4d714c2e5e6e635fc98c4bcebbf27f4211a38a74b5abe287d7bb466f97f42b5cd35dc6e9a9b3086035c34a738ef3a6bf332f552e531b38ffe6cf4dfb89f7359ea5e5e5d6f79dc86fa2864f9e9026b18a971225bec2a49d735f68461182069e8aee4091e06852ea2a3fa74ca3896ca1051e9e07bd97706e67afbc4bacb23413b44c8cf9f2405004fbb7094887b51fd1b5f6bb52795ba579c0ad96431f923e83d5d1815750c7b0cbc386ce42ca945b68d21c7ca5a122ba9eaaa69a4ebeba1b",
                Active = true,
                EffectiveDate = new DateTime(2009, 4, 23),
                EndDate = new DateTime(2022, 1, 20),
                IdentityUserId = Guid.Parse("37945f86-a484-40a2-98dd-48b4d88d088d")
            };

            // Act
            var serviceResult = await _employeeProfilesAppService.UpdateAsync(Guid.Parse("3fc2a4ad-9179-4e93-a189-3ea230977844"), input);

            // Assert
            var result = await _employeeProfileRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("4778e14d27434e3f9c34");
            result.ERPCode.ShouldBe("aced867cbe0d42af9b04");
            result.FirstName.ShouldBe("65fec8c642ea42e9bbcbe62180ef58a1d73c7aed5d41492f97e4f66280efb103923a68b2725e4091bf9315fecda4dd94869d467feb8240428fb6af9af86bdfa9d08eb21938604e9f89458de05c6d402b576f8d92441041f1ae1559f26446509cdf3462c02061452da9c0f37c49a5767ba66409a3775c4619afb84a3292e88c5");
            result.LastName.ShouldBe("193e522387754ae5b318299c76ce499e156eedc7180b4e89a2953c198f92ce405199bff9074a43dc9956ad2dd01de031fa31309165a546ee987a89650887185d28e2ddf7d7744c298fe0ffa71e7af8e1db607d0724014d00abe41bbdc3a8efe6fe07a4dfbbca4feb9c824272438e21f4edde65ff79984b44b1d251db2be990f");
            result.DateOfBirth.ShouldBe(new DateTime(2013, 7, 5));
            result.IdCardNumber.ShouldBe("d8fe88b319f04936ae7f08282f01e20950f08d8dd4e646b384ab4a2e503f124923edaedc79a94ed499dc4dc2031beddc1be8244568a041f49abae8fb5fdc2860090bc986cee24ea98172b9cecdf6874e5c12b90691c7404e92c56ac4a08541994fb282cbbc8248988f6e2e41cff1d21fc5a640eda1904b0d9f45aca8fa4d8b0");
            result.Email.ShouldBe("e91c9549c9f342189aabc15cbe9c1dcf883e8c7a790c4f7bb1cddaf10aa5c21cf97c2d2f86c0495698eac9b62e45f2aa79f23b6846ee4207889bc0241bff@ba51882982ec4a6088d0f3fddb1454626a594f7eebc84b38950a7b550551569372ab2d62b13b464fbffe63498dfcc1ae540dd9e42a6942bcadb6f571fd9f.com");
            result.Phone.ShouldBe("4370ebd7679d4671810fbc3eee7b9c7bca9ad50ae2754d75b28dafb67541a5e267f71031778c414388a1cdc587d3c7f4ef8a04b0bf9543b788a064adb3fa7c0237e02786c624424785f0c109404b5dc753ac814c8f9f4052b0d1720fc00f8463cfb5374ce8b244629165e101cb0df7facf300f0775184ac0962b3c9581f4b80");
            result.Address.ShouldBe("f8ae5dd153fb4bfda6ee5653b4d6d7dc020493f098e041b7942f24d43f16cf6a37fd4eea15f645a9a5a9c14b1664e482d4ac901169b849a78857ffdd4d714c2e5e6e635fc98c4bcebbf27f4211a38a74b5abe287d7bb466f97f42b5cd35dc6e9a9b3086035c34a738ef3a6bf332f552e531b38ffe6cf4dfb89f7359ea5e5e5d6f79dc86fa2864f9e9026b18a971225bec2a49d735f68461182069e8aee4091e06852ea2a3fa74ca3896ca1051e9e07bd97706e67afbc4bacb23413b44c8cf9f2405004fbb7094887b51fd1b5f6bb52795ba579c0ad96431f923e83d5d1815750c7b0cbc386ce42ca945b68d21c7ca5a122ba9eaaa69a4ebeba1b");
            result.Active.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2009, 4, 23));
            result.EndDate.ShouldBe(new DateTime(2022, 1, 20));
            result.IdentityUserId.ShouldBe(Guid.Parse("37945f86-a484-40a2-98dd-48b4d88d088d"));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _employeeProfilesAppService.DeleteAsync(Guid.Parse("3fc2a4ad-9179-4e93-a189-3ea230977844"));

            // Assert
            var result = await _employeeProfileRepository.FindAsync(c => c.Id == Guid.Parse("3fc2a4ad-9179-4e93-a189-3ea230977844"));

            result.ShouldBeNull();
        }
    }
}