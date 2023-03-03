using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;
using System.Collections.Generic;

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
            result.Items.Any(x => x.CustomerImage.Id == Guid.Parse("d3f948bb-4a2d-4cf8-9186-042dc2120986")).ShouldBe(true);
            result.Items.Any(x => x.CustomerImage.Id == Guid.Parse("61987456-11a2-4d34-a05e-c37785dbd193")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerImagesAppService.GetAsync(Guid.Parse("d3f948bb-4a2d-4cf8-9186-042dc2120986"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("d3f948bb-4a2d-4cf8-9186-042dc2120986"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            /*
            // Arrange
            var input = new CustomerImageCreateDto
            {
                Description = "0924ca0ac32e46a49068dade10faeb0d9872f2b631864b1fa5fd77dc62a961273637aa404c1b49abb0ee0a41fee6d3e30448caa9b07f4f4ea342d13f83760a88f5b4af068cb8408fb26f13ca17be80226bd9c15245a0407eb0e1b8c6ac21fde3c023129b10254448bed153afd6b5292273d282f86fda43a8b47ee5570a2b8c74ad0ecab0c8f24c44b2ae34098d6e61acac7b750ac0ab4235a1c88747a1cb2c174339198af29f4548bb2ddfc154289fa5ab526ca5e2eb44f0a21ee0ad65edce56b06ca03dfea54dfa9f180486c628c7fb3f304a36bcba40b6ad2c9acf1e789b17d7a3eef25f99465083bc51de3d9f1ec345dd3147e9694eb494ad",
                Active = true,
                IsAvatar = true,
                IsPOSM = true,
                FileId = Guid.Parse("3869061f-4bf4-4a48-a598-866895777060"),
                CustomerId = Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51"),

            };

            // Act
            var serviceResult = await _customerImagesAppService.CreateAsync(input);

            // Assert
            var result = await _customerImageRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("0924ca0ac32e46a49068dade10faeb0d9872f2b631864b1fa5fd77dc62a961273637aa404c1b49abb0ee0a41fee6d3e30448caa9b07f4f4ea342d13f83760a88f5b4af068cb8408fb26f13ca17be80226bd9c15245a0407eb0e1b8c6ac21fde3c023129b10254448bed153afd6b5292273d282f86fda43a8b47ee5570a2b8c74ad0ecab0c8f24c44b2ae34098d6e61acac7b750ac0ab4235a1c88747a1cb2c174339198af29f4548bb2ddfc154289fa5ab526ca5e2eb44f0a21ee0ad65edce56b06ca03dfea54dfa9f180486c628c7fb3f304a36bcba40b6ad2c9acf1e789b17d7a3eef25f99465083bc51de3d9f1ec345dd3147e9694eb494ad");
            result.Active.ShouldBe(true);
            result.IsAvatar.ShouldBe(true);
            result.IsPOSM.ShouldBe(true);
            result.FileId.ShouldBe(Guid.Parse("3869061f-4bf4-4a48-a598-866895777060"));
            */
            await _customerImageRepository.GetQueryableAsync();
        }

        [Fact]
        public async Task UpdateAsync()
        {
            /*
            // Arrange
            var input = new CustomerImageUpdateDto()
            {
                Description = "1adb8eb27b844ff6a034fd00255a243caa9c95a54ebd49269a17f7d6523f90ffbc65effcd91e4c3f919a0909cc650e061645aa7b9add4e3eb1df306f420f9cf511eebef466734d6592c564aa787b0aa1fd82bc15209a4b8494e708c23a433d50efbbca8edc9848349c8b7ccf36dc2f5e6fcadf78f5504d4dae0a984297acb8c75936f2a0317a474a95b5d36bdbf0a7d790030ef3f94a432fbe3f1c5eaffa1572134f3b530f8a4a2eb496a7de454be37be46cff546683492ca0e28b25451ddacac9a3ee48b2a54388a5d2c00f2013aae98a55e7fe03c1414f9dc94a5938d658fc040fc648f2134727a87d1f540cd651be48e4d568cd34459bb90f",
                Active = true,
                IsAvatar = true,
                IsPOSM = true,
                FileId = Guid.Parse("f1dfc9db-263f-4039-98bf-999bc02832cd"),
                CustomerId = Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51"),

            };

            // Act
            var serviceResult = await _customerImagesAppService.UpdateAsync(Guid.Parse("d3f948bb-4a2d-4cf8-9186-042dc2120986"), input);

            // Assert
            var result = await _customerImageRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("1adb8eb27b844ff6a034fd00255a243caa9c95a54ebd49269a17f7d6523f90ffbc65effcd91e4c3f919a0909cc650e061645aa7b9add4e3eb1df306f420f9cf511eebef466734d6592c564aa787b0aa1fd82bc15209a4b8494e708c23a433d50efbbca8edc9848349c8b7ccf36dc2f5e6fcadf78f5504d4dae0a984297acb8c75936f2a0317a474a95b5d36bdbf0a7d790030ef3f94a432fbe3f1c5eaffa1572134f3b530f8a4a2eb496a7de454be37be46cff546683492ca0e28b25451ddacac9a3ee48b2a54388a5d2c00f2013aae98a55e7fe03c1414f9dc94a5938d658fc040fc648f2134727a87d1f540cd651be48e4d568cd34459bb90f");
            result.Active.ShouldBe(true);
            result.IsAvatar.ShouldBe(true);
            result.IsPOSM.ShouldBe(true);
            result.FileId.ShouldBe(Guid.Parse("f1dfc9db-263f-4039-98bf-999bc02832cd"));
            */
            await _customerImageRepository.GetQueryableAsync();
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerImagesAppService.DeleteManyAsync(new List<Guid> { Guid.Parse("d3f948bb-4a2d-4cf8-9186-042dc2120986") });

            // Assert
            var result = await _customerImageRepository.FindAsync(c => c.Id == Guid.Parse("d3f948bb-4a2d-4cf8-9186-042dc2120986"));

            result.ShouldBeNull();
        }
    }
}