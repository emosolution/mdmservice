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
            result.Items.Any(x => x.CustomerAttachment.Id == Guid.Parse("733bb553-ca74-4747-9468-9793cb07d1bd")).ShouldBe(true);
            result.Items.Any(x => x.CustomerAttachment.Id == Guid.Parse("3ed21074-9c0e-46b3-83ce-c05e55dc6fd6")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerAttachmentsAppService.GetAsync(Guid.Parse("733bb553-ca74-4747-9468-9793cb07d1bd"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("733bb553-ca74-4747-9468-9793cb07d1bd"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerAttachmentCreateDto
            {
                Description = "de5f1169357f4fb4a565820bc7f99ef0636c79d",
                Active = true,
                FileId = Guid.Parse("f4a98615-7776-43f4-8bfb-021b8c18a60e"),
                CustomerId = Guid.Parse("ce6d421c-4cde-493f-b12f-e6fa307128be")
            };

            // Act
            var serviceResult = await _customerAttachmentsAppService.CreateAsync(input);

            // Assert
            var result = await _customerAttachmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("de5f1169357f4fb4a565820bc7f99ef0636c79d");
            result.Active.ShouldBe(true);
            result.FileId.ShouldBe(Guid.Parse("f4a98615-7776-43f4-8bfb-021b8c18a60e"));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerAttachmentUpdateDto()
            {
                Description = "561da1fac16a40fba71a01ada10c91a93e386ef",
                Active = true,
                FileId = Guid.Parse("727b8d93-998a-48cd-93c7-4b30266e0deb"),
                CustomerId = Guid.Parse("ce6d421c-4cde-493f-b12f-e6fa307128be")
            };

            // Act
            var serviceResult = await _customerAttachmentsAppService.UpdateAsync(Guid.Parse("733bb553-ca74-4747-9468-9793cb07d1bd"), input);

            // Assert
            var result = await _customerAttachmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("561da1fac16a40fba71a01ada10c91a93e386ef");
            result.Active.ShouldBe(true);
            result.FileId.ShouldBe(Guid.Parse("727b8d93-998a-48cd-93c7-4b30266e0deb"));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerAttachmentsAppService.DeleteAsync(Guid.Parse("733bb553-ca74-4747-9468-9793cb07d1bd"));

            // Assert
            var result = await _customerAttachmentRepository.FindAsync(c => c.Id == Guid.Parse("733bb553-ca74-4747-9468-9793cb07d1bd"));

            result.ShouldBeNull();
        }
    }
}