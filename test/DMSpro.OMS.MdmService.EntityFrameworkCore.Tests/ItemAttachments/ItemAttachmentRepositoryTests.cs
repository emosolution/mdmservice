using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.ItemAttachments;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
    public class ItemAttachmentRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly IItemAttachmentRepository _itemAttachmentRepository;

        public ItemAttachmentRepositoryTests()
        {
            _itemAttachmentRepository = GetRequiredService<IItemAttachmentRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _itemAttachmentRepository.GetListAsync(
                    description: "36d36b0b79bd42339a70a25c29128ebb3d129bfdd4e8404f9a1950b7c5fce5305c39040a38244545adf307493398",
                    active: true,
                    uRL: "48a86c0ff0bd4781970730fcf88b79280737"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("0f8663b1-558e-4a85-aa76-6a1d1f6b3acf"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _itemAttachmentRepository.GetCountAsync(
                    description: "853c135e2f074a2f9922367046cf5d22e1ea9c599b",
                    active: true,
                    uRL: "fa9da426f51a4c4f9c2cbf620b2acb170fe16e0f5d8840cf84de8a7f451bcc44e4dec7e50a744ca0be83df6cfb1e74"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}