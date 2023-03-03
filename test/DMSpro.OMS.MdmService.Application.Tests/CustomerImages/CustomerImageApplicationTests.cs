using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerImages
{
    public class CustomerImagesAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ICustomerImagesAppService _customerImagesAppService;
        private readonly IRepository<CustomerImage, Guid> _customerImageRepository;

        public CustomerImagesAppServiceTests()
        {
            _customerImagesAppService = GetRequiredService<ICustomerImagesAppService>();
            _customerImageRepository = GetRequiredService<IRepository<CustomerImage, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _customerImagesAppService.GetListAsync(new GetCustomerImagesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.CustomerImage.Id == Guid.Parse("9b411435-eb93-46da-ab72-db79d0998a21")).ShouldBe(true);
            result.Items.Any(x => x.CustomerImage.Id == Guid.Parse("2027d989-7c9a-4673-8045-05ce24eb7d95")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerImagesAppService.GetAsync(Guid.Parse("9b411435-eb93-46da-ab72-db79d0998a21"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("9b411435-eb93-46da-ab72-db79d0998a21"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerImageCreateDto
            {
                Description = "e501b8b2b18146eba71485d7f796cec76c9d49b7f5024d2e982159a9985117e698a75e25721b44c9b8c954f2c29617d43f3a81881893445e9655f60f2502f40cf0921251a5774092802972fd82256bc4b1e5a025860f443281a2f3875cfbe1094d2c4d9e95ca44a88105a98840320b2d22b25add9a374c39b181465a7dcb2020dd7ff97b784c4e989165923db06f27462e71da63e77e472c9cf1f8fe75ff4794c373869dbcf2411a9563c75447203f13a3ed14c8401043fca512f900f37ce7dc8bdc73a54f144b5792c7f31616b06805f8a62f717cbb4a3596b83fba6b61adb36451c55a549344a1beadb66e526bd2df78ecb909ecc04679bbd4",
                Active = true,
                IsAvatar = true,
                IsPOSM = true,
                FileId = Guid.Parse("ca56ad62-7384-43f9-b1ce-495c7a37cf49"),
                CustomerId = Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51")
            };

            // Act
            var serviceResult = await _customerImagesAppService.CreateAsync(input);

            // Assert
            var result = await _customerImageRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("e501b8b2b18146eba71485d7f796cec76c9d49b7f5024d2e982159a9985117e698a75e25721b44c9b8c954f2c29617d43f3a81881893445e9655f60f2502f40cf0921251a5774092802972fd82256bc4b1e5a025860f443281a2f3875cfbe1094d2c4d9e95ca44a88105a98840320b2d22b25add9a374c39b181465a7dcb2020dd7ff97b784c4e989165923db06f27462e71da63e77e472c9cf1f8fe75ff4794c373869dbcf2411a9563c75447203f13a3ed14c8401043fca512f900f37ce7dc8bdc73a54f144b5792c7f31616b06805f8a62f717cbb4a3596b83fba6b61adb36451c55a549344a1beadb66e526bd2df78ecb909ecc04679bbd4");
            result.Active.ShouldBe(true);
            result.IsAvatar.ShouldBe(true);
            result.IsPOSM.ShouldBe(true);
            result.FileId.ShouldBe(Guid.Parse("ca56ad62-7384-43f9-b1ce-495c7a37cf49"));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerImageUpdateDto()
            {
                Description = "a9dc9a66eeb74efe9294eb7bdbf8800aace205c5d8ac44dab34c39fe5112169a6bd2a71f36ea4afdb2b832f1484d61883e0626653d0a47be8d050b449bb259984bdf0144deef47d28aabdab9c3b2c595911d224fb4ac4636b5069b13e05101ee45ff7680800a418c84ba98d5231caead3fd64148e4314acf96e0e2745f332baad4ef1a02f16b481baba437bbf342350eb43782fbbe0a4d34bbc10af583e11d374194a705733c4b0cb610d648248ecff7219582ec271f44a3940fa0d86065429585c2c42fdaa7434da90a4d3970f8d5b3be0ea73a94f8435395b578aae529b1d8d72375b0376d42b0bc124ef59ce2e43f2b663faa304c4a119a4a",
                Active = true,
                IsAvatar = true,
                IsPOSM = true,
                FileId = Guid.Parse("5aa2a313-7af1-4dbe-8827-208bbeb8c192"),
                CustomerId = Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51")
            };

            // Act
            var serviceResult = await _customerImagesAppService.UpdateAsync(Guid.Parse("9b411435-eb93-46da-ab72-db79d0998a21"), input);

            // Assert
            var result = await _customerImageRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("a9dc9a66eeb74efe9294eb7bdbf8800aace205c5d8ac44dab34c39fe5112169a6bd2a71f36ea4afdb2b832f1484d61883e0626653d0a47be8d050b449bb259984bdf0144deef47d28aabdab9c3b2c595911d224fb4ac4636b5069b13e05101ee45ff7680800a418c84ba98d5231caead3fd64148e4314acf96e0e2745f332baad4ef1a02f16b481baba437bbf342350eb43782fbbe0a4d34bbc10af583e11d374194a705733c4b0cb610d648248ecff7219582ec271f44a3940fa0d86065429585c2c42fdaa7434da90a4d3970f8d5b3be0ea73a94f8435395b578aae529b1d8d72375b0376d42b0bc124ef59ce2e43f2b663faa304c4a119a4a");
            result.Active.ShouldBe(true);
            result.IsAvatar.ShouldBe(true);
            result.IsPOSM.ShouldBe(true);
            result.FileId.ShouldBe(Guid.Parse("5aa2a313-7af1-4dbe-8827-208bbeb8c192"));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerImagesAppService.DeleteAsync(Guid.Parse("9b411435-eb93-46da-ab72-db79d0998a21"));

            // Assert
            var result = await _customerImageRepository.FindAsync(c => c.Id == Guid.Parse("9b411435-eb93-46da-ab72-db79d0998a21"));

            result.ShouldBeNull();
        }
    }
}