using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;
using System.Collections.Generic;

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
            result.Items.Any(x => x.EmployeeImage.Id == Guid.Parse("cc8b6322-3ee7-4a46-bd95-7ba6da256a12")).ShouldBe(true);
            result.Items.Any(x => x.EmployeeImage.Id == Guid.Parse("2b8951d4-26ce-444d-8457-8f584b4344ee")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _employeeImagesAppService.GetAsync(Guid.Parse("cc8b6322-3ee7-4a46-bd95-7ba6da256a12"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("cc8b6322-3ee7-4a46-bd95-7ba6da256a12"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new EmployeeImageCreateDto
            {
                Description = "c4c6c9e6820e4",
                Active = true,
                IsAvatar = true,
                //FileId = Guid.Parse("dca4b8d3-7fd3-4949-b562-f75e5b55c4bd"),
                EmployeeProfileId = Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            };

            // Act
            var serviceResult = await _employeeImagesAppService.CreateAsync(input);

            // Assert
            var result = await _employeeImageRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("c4c6c9e6820e4");
            result.Active.ShouldBe(true);
            result.IsAvatar.ShouldBe(true);
            result.FileId.ShouldBe(Guid.Parse("dca4b8d3-7fd3-4949-b562-f75e5b55c4bd"));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new EmployeeImageUpdateDto()
            {
                Description = "b488272ec3f44bfb967abaf785310dd063a30300ab12445fa6be8af",
                Active = true,
                IsAvatar = true,
                //FileId = Guid.Parse("5bb764d7-ce5e-4561-964f-cc0dea2b3bcf"),
                EmployeeProfileId = Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            };

            // Act
            var serviceResult = await _employeeImagesAppService.UpdateAsync(Guid.Parse("cc8b6322-3ee7-4a46-bd95-7ba6da256a12"), input);

            // Assert
            var result = await _employeeImageRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("b488272ec3f44bfb967abaf785310dd063a30300ab12445fa6be8af");
            result.Active.ShouldBe(true);
            result.IsAvatar.ShouldBe(true);
            result.FileId.ShouldBe(Guid.Parse("5bb764d7-ce5e-4561-964f-cc0dea2b3bcf"));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _employeeImagesAppService.DeleteManyAsync(new List<Guid> { Guid.Parse("cc8b6322-3ee7-4a46-bd95-7ba6da256a12") });

            // Assert
            var result = await _employeeImageRepository.FindAsync(c => c.Id == Guid.Parse("cc8b6322-3ee7-4a46-bd95-7ba6da256a12"));

            result.ShouldBeNull();
        }
    }
}