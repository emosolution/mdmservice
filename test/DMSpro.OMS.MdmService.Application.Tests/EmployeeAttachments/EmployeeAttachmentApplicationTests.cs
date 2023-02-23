using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.EmployeeAttachments
{
    public class EmployeeAttachmentsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IEmployeeAttachmentsAppService _employeeAttachmentsAppService;
        private readonly IRepository<EmployeeAttachment, Guid> _employeeAttachmentRepository;

        public EmployeeAttachmentsAppServiceTests()
        {
            _employeeAttachmentsAppService = GetRequiredService<IEmployeeAttachmentsAppService>();
            _employeeAttachmentRepository = GetRequiredService<IRepository<EmployeeAttachment, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _employeeAttachmentsAppService.GetListAsync(new GetEmployeeAttachmentsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.EmployeeAttachment.Id == Guid.Parse("ecc88372-b838-46e7-acb9-35460da0b2ee")).ShouldBe(true);
            result.Items.Any(x => x.EmployeeAttachment.Id == Guid.Parse("16c313cf-ceb0-4326-88df-c18e28dd19c3")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _employeeAttachmentsAppService.GetAsync(Guid.Parse("ecc88372-b838-46e7-acb9-35460da0b2ee"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("ecc88372-b838-46e7-acb9-35460da0b2ee"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new EmployeeAttachmentCreateDto
            {
                Description = "156c65",
                Active = true,
                //FileId = Guid.Parse("922d6552-ec5e-4cb1-9d80-a5b755408e20"),
                EmployeeProfileId = Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            };

            // Act
            var serviceResult = await _employeeAttachmentsAppService.CreateAsync(input);

            // Assert
            var result = await _employeeAttachmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("156c65");
            result.Active.ShouldBe(true);
            result.FileId.ShouldBe(Guid.Parse("922d6552-ec5e-4cb1-9d80-a5b755408e20"));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new EmployeeAttachmentUpdateDto()
            {
                Description = "7eba38f5a1364abdb2f70b9c08be366",
                Active = true,
                //FileId = Guid.Parse("d1fa3251-03ce-4dbb-b918-145dc502d89a"),
                EmployeeProfileId = Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            };

            // Act
            var serviceResult = await _employeeAttachmentsAppService.UpdateAsync(Guid.Parse("ecc88372-b838-46e7-acb9-35460da0b2ee"), input);

            // Assert
            var result = await _employeeAttachmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("7eba38f5a1364abdb2f70b9c08be366");
            result.Active.ShouldBe(true);
            result.FileId.ShouldBe(Guid.Parse("d1fa3251-03ce-4dbb-b918-145dc502d89a"));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _employeeAttachmentsAppService.DeleteManyAsync(new List<Guid> { Guid.Parse("ecc88372-b838-46e7-acb9-35460da0b2ee") });

            // Assert
            var result = await _employeeAttachmentRepository.FindAsync(c => c.Id == Guid.Parse("ecc88372-b838-46e7-acb9-35460da0b2ee"));

            result.ShouldBeNull();
        }
    }
}