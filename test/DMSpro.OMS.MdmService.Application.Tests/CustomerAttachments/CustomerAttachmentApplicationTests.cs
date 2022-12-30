using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerAttachments
{
    public class CustomerAttachmentsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ICustomerAttachmentsAppService _customerAttachmentsAppService;
        private readonly IRepository<CustomerAttachment, Guid> _customerAttachmentRepository;

        public CustomerAttachmentsAppServiceTests()
        {
            _customerAttachmentsAppService = GetRequiredService<ICustomerAttachmentsAppService>();
            _customerAttachmentRepository = GetRequiredService<IRepository<CustomerAttachment, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _customerAttachmentsAppService.GetListAsync(new GetCustomerAttachmentsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.CustomerAttachment.Id == Guid.Parse("8db6de0b-2715-41b7-85b1-4157f3778be4")).ShouldBe(true);
            result.Items.Any(x => x.CustomerAttachment.Id == Guid.Parse("f65f3c2d-f9b1-4814-a236-b7989fc07ced")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerAttachmentsAppService.GetAsync(Guid.Parse("8db6de0b-2715-41b7-85b1-4157f3778be4"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("8db6de0b-2715-41b7-85b1-4157f3778be4"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerAttachmentCreateDto
            {
                url = "18fd897588e0",
                Description = "7efd7bd3442d4bacabd1d1e0961f84bd627d12bf5e1c424c8d59d1c28a5fa45da3d4d706ce8146c3bf0c151a",
                Active = true,
                CustomerId = Guid.Parse("4db13257-b7dd-482e-80ba-4e2854cea781")
            };

            // Act
            var serviceResult = await _customerAttachmentsAppService.CreateAsync(input);

            // Assert
            var result = await _customerAttachmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.url.ShouldBe("18fd897588e0");
            result.Description.ShouldBe("7efd7bd3442d4bacabd1d1e0961f84bd627d12bf5e1c424c8d59d1c28a5fa45da3d4d706ce8146c3bf0c151a");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerAttachmentUpdateDto()
            {
                url = "01978e34221a4ba1858550852bf72a7a64186374f3d0483597992cf2bf4d8f7ee7ab0fd280e44beb818a593adf281582b0b",
                Description = "e857ba29c59d46f5a227a69793cdf33c31b8ab727d70437e87d",
                Active = true,
                CustomerId = Guid.Parse("4db13257-b7dd-482e-80ba-4e2854cea781")
            };

            // Act
            var serviceResult = await _customerAttachmentsAppService.UpdateAsync(Guid.Parse("8db6de0b-2715-41b7-85b1-4157f3778be4"), input);

            // Assert
            var result = await _customerAttachmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.url.ShouldBe("01978e34221a4ba1858550852bf72a7a64186374f3d0483597992cf2bf4d8f7ee7ab0fd280e44beb818a593adf281582b0b");
            result.Description.ShouldBe("e857ba29c59d46f5a227a69793cdf33c31b8ab727d70437e87d");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerAttachmentsAppService.DeleteAsync(Guid.Parse("8db6de0b-2715-41b7-85b1-4157f3778be4"));

            // Assert
            var result = await _customerAttachmentRepository.FindAsync(c => c.Id == Guid.Parse("8db6de0b-2715-41b7-85b1-4157f3778be4"));

            result.ShouldBeNull();
        }
    }
}