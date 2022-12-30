using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

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
            result.Items.Any(x => x.EmployeeAttachment.Id == Guid.Parse("5b7a3183-f7e9-4c78-b345-63ad7f123f61")).ShouldBe(true);
            result.Items.Any(x => x.EmployeeAttachment.Id == Guid.Parse("d96a176e-8c9c-4e13-835e-278d09fd5edc")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _employeeAttachmentsAppService.GetAsync(Guid.Parse("5b7a3183-f7e9-4c78-b345-63ad7f123f61"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("5b7a3183-f7e9-4c78-b345-63ad7f123f61"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new EmployeeAttachmentCreateDto
            {
                url = "5216d164951c4c74a5d5a17df610cd7acc17da6b17f2462b87a2ac",
                Description = "29df2d7037e74fa6a49ca23fc03f18aad3d37a8739cf4e49a3d57e3d1a47378435c2b",
                Active = true,
                EmployeeProfileId = Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            };

            // Act
            var serviceResult = await _employeeAttachmentsAppService.CreateAsync(input);

            // Assert
            var result = await _employeeAttachmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.url.ShouldBe("5216d164951c4c74a5d5a17df610cd7acc17da6b17f2462b87a2ac");
            result.Description.ShouldBe("29df2d7037e74fa6a49ca23fc03f18aad3d37a8739cf4e49a3d57e3d1a47378435c2b");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new EmployeeAttachmentUpdateDto()
            {
                url = "3c17a21369c94889bf0023c8b52266355005d099fee74aa7a972d47ef582ba0f742f2c947cc8444780603ca566",
                Description = "30ba4db520914d36a973706057b13b1b5312020ac1",
                Active = true,
                EmployeeProfileId = Guid.Parse("b582d913-b271-48f8-ae8b-93fc32c81072")
            };

            // Act
            var serviceResult = await _employeeAttachmentsAppService.UpdateAsync(Guid.Parse("5b7a3183-f7e9-4c78-b345-63ad7f123f61"), input);

            // Assert
            var result = await _employeeAttachmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.url.ShouldBe("3c17a21369c94889bf0023c8b52266355005d099fee74aa7a972d47ef582ba0f742f2c947cc8444780603ca566");
            result.Description.ShouldBe("30ba4db520914d36a973706057b13b1b5312020ac1");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _employeeAttachmentsAppService.DeleteAsync(Guid.Parse("5b7a3183-f7e9-4c78-b345-63ad7f123f61"));

            // Assert
            var result = await _employeeAttachmentRepository.FindAsync(c => c.Id == Guid.Parse("5b7a3183-f7e9-4c78-b345-63ad7f123f61"));

            result.ShouldBeNull();
        }
    }
}