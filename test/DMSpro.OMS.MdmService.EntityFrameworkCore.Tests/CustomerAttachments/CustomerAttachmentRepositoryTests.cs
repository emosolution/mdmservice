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
                    url: "4b8ea87c9d204e29a98e9c10c",
                    description: "96fad43a6e6742c2ae70b2a267437064513233aeb8ad4eaab1e760fd7b5e543380940",
                    active: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("8db6de0b-2715-41b7-85b1-4157f3778be4"));
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
                    url: "9b1cb478e430434eb06d671136142c26b4a4ded20e384f47a9e7a249886e457f9356a9b3fdd240b3a89bf03",
                    description: "afe6c7616781466aad9a8bb977240f0b01e64c5ecd70412497687c5351fe752043543b285b9b42",
                    active: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}