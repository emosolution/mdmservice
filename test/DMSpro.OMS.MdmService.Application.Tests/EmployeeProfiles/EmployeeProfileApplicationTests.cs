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
        public async Task GetListAsync()
        {
            // Act
            var result = await _employeeProfilesAppService.GetListAsync(new GetEmployeeProfilesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.EmployeeProfile.Id == Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")).ShouldBe(true);
            result.Items.Any(x => x.EmployeeProfile.Id == Guid.Parse("914b814c-0616-470d-a337-dcfa64e6f06b")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _employeeProfilesAppService.GetAsync(Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new EmployeeProfileCreateDto
            {
                Code = "5f239bd94a234c8e9c14",
                ERPCode = "fa39048fb25d4a9cab46ee4b13f8940a4e5af4ba4fe",
                FirstName = "8eaa22ed1bb64a8eb29e490c133ec401d0ed06dc23ee4341b98b210b18d7de8cc44bead4950644ada3b64c960d2f6a065838533ba4b94d0181ccd015917a3217c314a793f0b5413093a7930d3922424799bf22212c164960b5f7940281b4e2d2ebf8d27161f141abb612852bb60aa411617275fa5d33417c91668f9dd318db9",
                LastName = "5d9e71eb5a1940bfb286ca154b3b3ff5ad1d2618f18d4318a1594ed1b8d46bf4812e03888a7e4407ad92c41",
                DateOfBirth = new DateTime(2014, 4, 12),
                IdCardNumber = "04432071bc104823b957645c6ce9724bb57f52362c35445da9a427df17b25",
                Email = "a4a8e50bf4aa42c3996b65a678c337d130eeefe5b5814d989c437",
                Phone = "0a0c5617671e4e5a87e4cd23c7eb4fd354cbc739b22c4b2a8c8a381f",
                Address = "b4e54d8fb13d4186b5308dc9392274959db05fe7ac7548808ca4bf6c425396d20cb2f29bffe44c2189cc",
                Active = true,
                EffectiveDate = new DateTime(2011, 5, 13),
                EndDate = new DateTime(2013, 1, 5),
                IdentityUserId = Guid.Parse("60c28ac7-5237-446d-a3e4-cfb96db0add1")
            };

            // Act
            var serviceResult = await _employeeProfilesAppService.CreateAsync(input);

            // Assert
            var result = await _employeeProfileRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("5f239bd94a234c8e9c14");
            result.ERPCode.ShouldBe("fa39048fb25d4a9cab46ee4b13f8940a4e5af4ba4fe");
            result.FirstName.ShouldBe("8eaa22ed1bb64a8eb29e490c133ec401d0ed06dc23ee4341b98b210b18d7de8cc44bead4950644ada3b64c960d2f6a065838533ba4b94d0181ccd015917a3217c314a793f0b5413093a7930d3922424799bf22212c164960b5f7940281b4e2d2ebf8d27161f141abb612852bb60aa411617275fa5d33417c91668f9dd318db9");
            result.LastName.ShouldBe("5d9e71eb5a1940bfb286ca154b3b3ff5ad1d2618f18d4318a1594ed1b8d46bf4812e03888a7e4407ad92c41");
            result.DateOfBirth.ShouldBe(new DateTime(2014, 4, 12));
            result.IdCardNumber.ShouldBe("04432071bc104823b957645c6ce9724bb57f52362c35445da9a427df17b25");
            result.Email.ShouldBe("a4a8e50bf4aa42c3996b65a678c337d130eeefe5b5814d989c437");
            result.Phone.ShouldBe("0a0c5617671e4e5a87e4cd23c7eb4fd354cbc739b22c4b2a8c8a381f");
            result.Address.ShouldBe("b4e54d8fb13d4186b5308dc9392274959db05fe7ac7548808ca4bf6c425396d20cb2f29bffe44c2189cc");
            result.Active.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2011, 5, 13));
            result.EndDate.ShouldBe(new DateTime(2013, 1, 5));
            result.IdentityUserId.ShouldBe(Guid.Parse("60c28ac7-5237-446d-a3e4-cfb96db0add1"));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new EmployeeProfileUpdateDto()
            {
                Code = "0e64189990ab47e9849a",
                ERPCode = "0e2657d29a3d425fa29e298cf585ee5d7c05e54d92de4415",
                FirstName = "27d7a86816cf4cd99e4262c1e8f67e69081e70d31ee842a3b832144890dc07d556ab1856a1c74ceda4bc39478a8f4fced528a5d23e354e74a4136874e44b70871d3f3b3aa94545218c781500f8bc59634f443075379e46359782e3c351e00a7159221112f4bb444ebb05f86d30ef11ac5db7323ecb0943bc907573ee8180102",
                LastName = "454b4b90e6804e8e95a438f8f3c283d7b20eceb2733645b2aa1a80b299fc00188f08c2aa587748e0b",
                DateOfBirth = new DateTime(2018, 11, 6),
                IdCardNumber = "9ccc013ec77c441c9423da451dba42105d75cb4726154dbcb23032bfdd3cc353fc7f3d271",
                Email = "3cdc0516a9bc4d9982c192a8ac01d935a261f2c8a59346579a7e750c4188c34079489c",
                Phone = "0f0db2c0be59446abafe8ca9180b8e1c",
                Address = "b93168ef67c34dbda09a74ec442b9c048c12157d46b54d4dadce12b498c8",
                Active = true,
                EffectiveDate = new DateTime(2019, 10, 17),
                EndDate = new DateTime(2010, 6, 27),
                IdentityUserId = Guid.Parse("f9dbd7e5-e82a-45d7-a23b-ff42a7b2e9e4")
            };

            // Act
            var serviceResult = await _employeeProfilesAppService.UpdateAsync(Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072"), input);

            // Assert
            var result = await _employeeProfileRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("0e64189990ab47e9849a");
            result.ERPCode.ShouldBe("0e2657d29a3d425fa29e298cf585ee5d7c05e54d92de4415");
            result.FirstName.ShouldBe("27d7a86816cf4cd99e4262c1e8f67e69081e70d31ee842a3b832144890dc07d556ab1856a1c74ceda4bc39478a8f4fced528a5d23e354e74a4136874e44b70871d3f3b3aa94545218c781500f8bc59634f443075379e46359782e3c351e00a7159221112f4bb444ebb05f86d30ef11ac5db7323ecb0943bc907573ee8180102");
            result.LastName.ShouldBe("454b4b90e6804e8e95a438f8f3c283d7b20eceb2733645b2aa1a80b299fc00188f08c2aa587748e0b");
            result.DateOfBirth.ShouldBe(new DateTime(2018, 11, 6));
            result.IdCardNumber.ShouldBe("9ccc013ec77c441c9423da451dba42105d75cb4726154dbcb23032bfdd3cc353fc7f3d271");
            result.Email.ShouldBe("3cdc0516a9bc4d9982c192a8ac01d935a261f2c8a59346579a7e750c4188c34079489c");
            result.Phone.ShouldBe("0f0db2c0be59446abafe8ca9180b8e1c");
            result.Address.ShouldBe("b93168ef67c34dbda09a74ec442b9c048c12157d46b54d4dadce12b498c8");
            result.Active.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2019, 10, 17));
            result.EndDate.ShouldBe(new DateTime(2010, 6, 27));
            result.IdentityUserId.ShouldBe(Guid.Parse("f9dbd7e5-e82a-45d7-a23b-ff42a7b2e9e4"));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _employeeProfilesAppService.DeleteAsync(Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072"));

            // Assert
            var result = await _employeeProfileRepository.FindAsync(c => c.Id == Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072"));

            result.ShouldBeNull();
        }
    }
}