using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.EmployeeImages
{
    public class EmployeeImagesAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IEmployeeImagesAppService _employeeImagesAppService;
        private readonly IRepository<EmployeeImage, Guid> _employeeImageRepository;

        public EmployeeImagesAppServiceTests()
        {
            _employeeImagesAppService = GetRequiredService<IEmployeeImagesAppService>();
            _employeeImageRepository = GetRequiredService<IRepository<EmployeeImage, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _employeeImagesAppService.GetListAsync(new GetEmployeeImagesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.EmployeeImage.Id == Guid.Parse("8edf5e2e-2a59-410d-962c-2275a580468b")).ShouldBe(true);
            result.Items.Any(x => x.EmployeeImage.Id == Guid.Parse("d117d5ff-356c-4418-beeb-529d98ce0e36")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _employeeImagesAppService.GetAsync(Guid.Parse("8edf5e2e-2a59-410d-962c-2275a580468b"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("8edf5e2e-2a59-410d-962c-2275a580468b"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new EmployeeImageCreateDto
            {
                Description = "6a5a25dc0aa542aea2d4dfb4e7e57a8cdb3e2ea6675247e498b26496a79402459ca1b46861244b11954a58b1c5",
                url = "7f9bce1dbfa746ffb14bc6e2946b5a75f87a2a9e",
                Active = true,
                IsAvatar = true,
                EmployeeProfileId = Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            };

            // Act
            var serviceResult = await _employeeImagesAppService.CreateAsync(input);

            // Assert
            var result = await _employeeImageRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("6a5a25dc0aa542aea2d4dfb4e7e57a8cdb3e2ea6675247e498b26496a79402459ca1b46861244b11954a58b1c5");
            result.url.ShouldBe("7f9bce1dbfa746ffb14bc6e2946b5a75f87a2a9e");
            result.Active.ShouldBe(true);
            result.IsAvatar.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new EmployeeImageUpdateDto()
            {
                Description = "2e3d56a47c0744e881a152a79d5fc9d88b4ed293d1f74803aeae28cf6774ef487857214807ee4",
                url = "d3221097ea7a4601aa27a322b105e782bd0e07eea1fb4f158f00fcc48813a8793780f",
                Active = true,
                IsAvatar = true,
                EmployeeProfileId = Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            };

            // Act
            var serviceResult = await _employeeImagesAppService.UpdateAsync(Guid.Parse("8edf5e2e-2a59-410d-962c-2275a580468b"), input);

            // Assert
            var result = await _employeeImageRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("2e3d56a47c0744e881a152a79d5fc9d88b4ed293d1f74803aeae28cf6774ef487857214807ee4");
            result.url.ShouldBe("d3221097ea7a4601aa27a322b105e782bd0e07eea1fb4f158f00fcc48813a8793780f");
            result.Active.ShouldBe(true);
            result.IsAvatar.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _employeeImagesAppService.DeleteAsync(Guid.Parse("8edf5e2e-2a59-410d-962c-2275a580468b"));

            // Assert
            var result = await _employeeImageRepository.FindAsync(c => c.Id == Guid.Parse("8edf5e2e-2a59-410d-962c-2275a580468b"));

            result.ShouldBeNull();
        }
    }
}