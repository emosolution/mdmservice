using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.CustomerAttachments;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerAttachments
{
    public class CustomerAttachmentRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ICustomerAttachmentRepository _customerAttachmentRepository;

        public CustomerAttachmentRepositoryTests()
        {
            _customerAttachmentRepository = GetRequiredService<ICustomerAttachmentRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerAttachmentRepository.GetListAsync(
                    description: "9d11035",
                    active: true,
                    fileId: Guid.Parse("d66c8ccb-7637-469b-84c7-140847a4783a")
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("733bb553-ca74-4747-9468-9793cb07d1bd"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerAttachmentRepository.GetCountAsync(
                    description: "7299a06334fa",
                    active: true,
                    fileId: Guid.Parse("9addaadf-bd64-4374-a804-8f39fced702e")
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}