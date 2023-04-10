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
            var result = await _employeeProfilesAppService.GetAsync(Guid.Parse("1fb0435c-53b9-4183-a98c-c656337727cf"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("1fb0435c-53b9-4183-a98c-c656337727cf"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new EmployeeProfileCreateDto
            {
                ERPCode = "a413c0bae09e410e9a94",
                FirstName = "0e5a6044d4bd4763ab2a7e3acd182e9ad73de7d5e78f4db7b15bd782a1460180139bee5647cc448dbf6d6f47ba25fbf0d925761897ba414dad32669a46682519dc30fd8176c94a56a9f10f12a73c00fd57ac249233cb439eb38084336d9edd12e779974495fa453f8b9b99a84e7689d454b5f4e3b0cb4f39bc0b3176c222719",
                LastName = "9d0ce69c610f40f4902f7efc728afeff6be5860353de42d782a4c9614ac7f23299b65b2cafe24ee58249c87881d01916f58c75561fea49dcbe5718742a14b282620bcc743896454eb98d5f4c218439da5477daf614a042769a6ab303f7f41f54f9fcdcaa24aa404db27ec6fe1034888ebce66d894a3644fb877c9ed87695028",
                DateOfBirth = new DateTime(2008, 1, 13),
                IdCardNumber = "e6917249f51542e88e5bf7cacff4b0e3556a71e566e148a4b2d15d6ea37012e6a69c42e89065498d85c0405c1e6bda61d0f4465f8e48474bb15b008c6e455534c0e4889e90e44ab4b92e660535397dd3eb83e4a491564e2cb3f9af891ec46599a35122c6dbb94015b7ca50f51c2438d23e25db3d2f39492582b957a1bff9610",
                Email = "4994108ee72248349d5ff3602a7d7fa36df3cb18238b45bb97461a34ead7ab7a8b9ff058fd884a6a8dacdad96d54ca9968256efdfbcd4ed79259b38fbe0c@4f1bc68982d341f686e9eccf11e6a0d73a5875d18cc646e48f8ae63c9478c89973093af4b20b48a9955f3f7d1768bf74b5722398895c4eb684542dcd50ab.com",
                Phone = "5ae964cbc54a4b3ba58fbfd29e6f32b2cfe5386f3a3a419a95e5f24060bfe4ab7976e8949c9245b2bdb6b159c878481429af5ed09b854427b8a9e66c22a9d1948cace84dad9c41cebe762c0dd254820cc917362103714760bd47317b672ffd9e6f55ca0375bb45ba821f42b7248353a59181a06ab8b944e188fc504849d7715",
                Address = "57c507553b4b476aab19065f007a8b2c56886c4f399f46f09864b7ad00cca08efb8ce233d6cd40f89f6061c10f755daea08ccb11889c4f7c81ed9b9023736b5770aca675894045f2a190e7d12964c8abbd860c252d5148a4af14785f01355b89725f866793ed4313b137e646d99b8a25efd8c4b941704a419463ae98d9123423273ed9094f6749abb9bd108528604173853762bf63e749999e7c74d2866dc93ebc08c61ba47747949de6c5187ffc39de12f7224a6ff84f15afc3ac432cb37970d662a40db9fe462991e95f981719ad3e1cff63b7875f43fda0d53e3071b13bec1a5b08509bb04ed8865a6991c99e779342d190b5c7ce43728626",
                Active = true,
                EffectiveDate = new DateTime(2000, 6, 24),
                EndDate = new DateTime(2004, 9, 24),
                IdentityUserId = Guid.Parse("c8b4399f-0414-4ad8-bbe6-d993d3017bd7"),
                EmployeeType = default
            };

            // Act
            var serviceResult = await _employeeProfilesAppService.CreateAsync(input);

            // Assert
            var result = await _employeeProfileRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ERPCode.ShouldBe("a413c0bae09e410e9a94");
            result.FirstName.ShouldBe("0e5a6044d4bd4763ab2a7e3acd182e9ad73de7d5e78f4db7b15bd782a1460180139bee5647cc448dbf6d6f47ba25fbf0d925761897ba414dad32669a46682519dc30fd8176c94a56a9f10f12a73c00fd57ac249233cb439eb38084336d9edd12e779974495fa453f8b9b99a84e7689d454b5f4e3b0cb4f39bc0b3176c222719");
            result.LastName.ShouldBe("9d0ce69c610f40f4902f7efc728afeff6be5860353de42d782a4c9614ac7f23299b65b2cafe24ee58249c87881d01916f58c75561fea49dcbe5718742a14b282620bcc743896454eb98d5f4c218439da5477daf614a042769a6ab303f7f41f54f9fcdcaa24aa404db27ec6fe1034888ebce66d894a3644fb877c9ed87695028");
            result.DateOfBirth.ShouldBe(new DateTime(2008, 1, 13));
            result.IdCardNumber.ShouldBe("e6917249f51542e88e5bf7cacff4b0e3556a71e566e148a4b2d15d6ea37012e6a69c42e89065498d85c0405c1e6bda61d0f4465f8e48474bb15b008c6e455534c0e4889e90e44ab4b92e660535397dd3eb83e4a491564e2cb3f9af891ec46599a35122c6dbb94015b7ca50f51c2438d23e25db3d2f39492582b957a1bff9610");
            result.Email.ShouldBe("4994108ee72248349d5ff3602a7d7fa36df3cb18238b45bb97461a34ead7ab7a8b9ff058fd884a6a8dacdad96d54ca9968256efdfbcd4ed79259b38fbe0c@4f1bc68982d341f686e9eccf11e6a0d73a5875d18cc646e48f8ae63c9478c89973093af4b20b48a9955f3f7d1768bf74b5722398895c4eb684542dcd50ab.com");
            result.Phone.ShouldBe("5ae964cbc54a4b3ba58fbfd29e6f32b2cfe5386f3a3a419a95e5f24060bfe4ab7976e8949c9245b2bdb6b159c878481429af5ed09b854427b8a9e66c22a9d1948cace84dad9c41cebe762c0dd254820cc917362103714760bd47317b672ffd9e6f55ca0375bb45ba821f42b7248353a59181a06ab8b944e188fc504849d7715");
            result.Address.ShouldBe("57c507553b4b476aab19065f007a8b2c56886c4f399f46f09864b7ad00cca08efb8ce233d6cd40f89f6061c10f755daea08ccb11889c4f7c81ed9b9023736b5770aca675894045f2a190e7d12964c8abbd860c252d5148a4af14785f01355b89725f866793ed4313b137e646d99b8a25efd8c4b941704a419463ae98d9123423273ed9094f6749abb9bd108528604173853762bf63e749999e7c74d2866dc93ebc08c61ba47747949de6c5187ffc39de12f7224a6ff84f15afc3ac432cb37970d662a40db9fe462991e95f981719ad3e1cff63b7875f43fda0d53e3071b13bec1a5b08509bb04ed8865a6991c99e779342d190b5c7ce43728626");
            result.Active.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2000, 6, 24));
            result.EndDate.ShouldBe(new DateTime(2004, 9, 24));
            result.IdentityUserId.ShouldBe(Guid.Parse("c8b4399f-0414-4ad8-bbe6-d993d3017bd7"));
            result.EmployeeType.ShouldBe(default);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new EmployeeProfileUpdateDto()
            {
                ERPCode = "a469f5ff261b4624a230",
                FirstName = "f12b50a10a374cfb937699591df46ae8b20cc4b8e79149c1aebcd853b82f6e7dd34c0b0f289644c7a267a0b2db0db1922d179940ac864381b349d6fd991165bbb432b454e84f40009cb3dc1b8f4bc4014cd9df3b17e94542a09873bfb8773b5fa1010a7ac4cf4f43a8835e4fd5490169c2e8d8e56c0b4e91b1ad01b39e053f4",
                LastName = "fae4f3e45c134151807fabb7af014e7c0a9af77ca74941a7a4c40ceac5a9a5a63e59e6d85252448c94b3558c4bfa7483da5b0b540599492db8dca05856a7f50f854d29e595e6473d8687b548ec72caf5331931e0bef14f3aa31cc0f20ddae9a475881715ec3f41a7937ca42ed5fac9b71689f2ac97fa46099e0728b8f5e0955",
                DateOfBirth = new DateTime(2004, 10, 19),
                IdCardNumber = "207a4a0ec7c54ae6bb98b4c8d962d8591c11827e4f1c4fd2bdeb9e0d24b58215d5a4c9649a384ad3b395820a5f0e93fedc9e244c469a4b1dbb728a1bd624c4effe5e0522c0c44d5d818909e11343f98d1ed57e3cba364a4a97495360540416aa711309672b9446e69d9a2fbfaa1f041b4ba2c120b065434eaa1c766f85fb433",
                Email = "d1ee902657f34419b6219e5430cc8b6bfdb2d4413b8c4920ac44247ccd4a9e6264b9ba3054c44b5a94afa04f3abb947459e0d838e3d64acc80ae78fd6f9b@f674ad0e1ddd4911b1242857036858b0b244fe5a6fd8465aa98370812ff4e21bb88f50e35a3e4b3391d3438a397c0005e14d82b8c9804e02a39e9d581a4b.com",
                Phone = "07a5a89f31c8484eb44754d0bd7e3da3ddbeec13b9094d1ca9f05a73fdf82b68e9d4ba7daa924a5ab36b776c1e4f67d659e61e0a020e45b9a20cf8569d63b4facf77f68875314d798663855502aa34b02034b0dffbd74ee7b36bb323a949791a91a90a86f2a74d1c9840ec407b54e2c4e634f6c0b3bd47749cc2b6ef3b74c54",
                Address = "4a81333393b24c31854f426de65882c480e5d28471b94160a3dd4121ce41b19807d6caddf9f14c1ba978e5e03d5df1d1896710b12562492fafd6b1c9454317d6e6a07e3dccac45bdb3f50d3072781d2a333262e17c424e9ab72ed4de5ae20941239d0dc0471c44fb861d94f5caa0e885341a29b9d4a040aa9add4c1be12d2fedc77c9eb21ba341499faf47411ae1970abcee002b88fc476ca317a07e40aac609872fc6a3c049458491855f6fc9d52a7297433b188b6a4815a799f508559f36a54fb4560c177341f69206d6729d5d195b45e483b5af46488f92a5af07b81f2be43c12ec739d8047b7bd7e1526e61bf45a5ea8be940c5d43dd92f9",
                Active = true,
                EffectiveDate = new DateTime(2007, 2, 23),
                EndDate = new DateTime(2022, 2, 16),
                IdentityUserId = Guid.Parse("58acc427-1396-4180-ac9b-83a7bf824e89"),
                EmployeeType = default
            };

            // Act
            var serviceResult = await _employeeProfilesAppService.UpdateAsync(Guid.Parse("1fb0435c-53b9-4183-a98c-c656337727cf"), input);

            // Assert
            var result = await _employeeProfileRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ERPCode.ShouldBe("a469f5ff261b4624a230");
            result.FirstName.ShouldBe("f12b50a10a374cfb937699591df46ae8b20cc4b8e79149c1aebcd853b82f6e7dd34c0b0f289644c7a267a0b2db0db1922d179940ac864381b349d6fd991165bbb432b454e84f40009cb3dc1b8f4bc4014cd9df3b17e94542a09873bfb8773b5fa1010a7ac4cf4f43a8835e4fd5490169c2e8d8e56c0b4e91b1ad01b39e053f4");
            result.LastName.ShouldBe("fae4f3e45c134151807fabb7af014e7c0a9af77ca74941a7a4c40ceac5a9a5a63e59e6d85252448c94b3558c4bfa7483da5b0b540599492db8dca05856a7f50f854d29e595e6473d8687b548ec72caf5331931e0bef14f3aa31cc0f20ddae9a475881715ec3f41a7937ca42ed5fac9b71689f2ac97fa46099e0728b8f5e0955");
            result.DateOfBirth.ShouldBe(new DateTime(2004, 10, 19));
            result.IdCardNumber.ShouldBe("207a4a0ec7c54ae6bb98b4c8d962d8591c11827e4f1c4fd2bdeb9e0d24b58215d5a4c9649a384ad3b395820a5f0e93fedc9e244c469a4b1dbb728a1bd624c4effe5e0522c0c44d5d818909e11343f98d1ed57e3cba364a4a97495360540416aa711309672b9446e69d9a2fbfaa1f041b4ba2c120b065434eaa1c766f85fb433");
            result.Email.ShouldBe("d1ee902657f34419b6219e5430cc8b6bfdb2d4413b8c4920ac44247ccd4a9e6264b9ba3054c44b5a94afa04f3abb947459e0d838e3d64acc80ae78fd6f9b@f674ad0e1ddd4911b1242857036858b0b244fe5a6fd8465aa98370812ff4e21bb88f50e35a3e4b3391d3438a397c0005e14d82b8c9804e02a39e9d581a4b.com");
            result.Phone.ShouldBe("07a5a89f31c8484eb44754d0bd7e3da3ddbeec13b9094d1ca9f05a73fdf82b68e9d4ba7daa924a5ab36b776c1e4f67d659e61e0a020e45b9a20cf8569d63b4facf77f68875314d798663855502aa34b02034b0dffbd74ee7b36bb323a949791a91a90a86f2a74d1c9840ec407b54e2c4e634f6c0b3bd47749cc2b6ef3b74c54");
            result.Address.ShouldBe("4a81333393b24c31854f426de65882c480e5d28471b94160a3dd4121ce41b19807d6caddf9f14c1ba978e5e03d5df1d1896710b12562492fafd6b1c9454317d6e6a07e3dccac45bdb3f50d3072781d2a333262e17c424e9ab72ed4de5ae20941239d0dc0471c44fb861d94f5caa0e885341a29b9d4a040aa9add4c1be12d2fedc77c9eb21ba341499faf47411ae1970abcee002b88fc476ca317a07e40aac609872fc6a3c049458491855f6fc9d52a7297433b188b6a4815a799f508559f36a54fb4560c177341f69206d6729d5d195b45e483b5af46488f92a5af07b81f2be43c12ec739d8047b7bd7e1526e61bf45a5ea8be940c5d43dd92f9");
            result.Active.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2007, 2, 23));
            result.EndDate.ShouldBe(new DateTime(2022, 2, 16));
            result.IdentityUserId.ShouldBe(Guid.Parse("58acc427-1396-4180-ac9b-83a7bf824e89"));
            result.EmployeeType.ShouldBe(default);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _employeeProfilesAppService.DeleteAsync(Guid.Parse("1fb0435c-53b9-4183-a98c-c656337727cf"));

            // Assert
            var result = await _employeeProfileRepository.FindAsync(c => c.Id == Guid.Parse("1fb0435c-53b9-4183-a98c-c656337727cf"));

            result.ShouldBeNull();
        }
    }
}