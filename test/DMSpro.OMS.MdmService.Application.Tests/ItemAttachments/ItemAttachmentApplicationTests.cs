using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
    public class ItemAttachmentsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly IItemAttachmentsAppService _itemAttachmentsAppService;
        private readonly IRepository<ItemAttachment, Guid> _itemAttachmentRepository;

        public ItemAttachmentsAppServiceTests()
        {
            _itemAttachmentsAppService = GetRequiredService<IItemAttachmentsAppService>();
            _itemAttachmentRepository = GetRequiredService<IRepository<ItemAttachment, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _itemAttachmentsAppService.GetListAsync(new GetItemAttachmentsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.ItemAttachment.Id == Guid.Parse("0f8663b1-558e-4a85-aa76-6a1d1f6b3acf")).ShouldBe(true);
            result.Items.Any(x => x.ItemAttachment.Id == Guid.Parse("40854521-e646-431a-91a3-dc97e43da96f")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _itemAttachmentsAppService.GetAsync(Guid.Parse("0f8663b1-558e-4a85-aa76-6a1d1f6b3acf"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("0f8663b1-558e-4a85-aa76-6a1d1f6b3acf"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ItemAttachmentCreateDto
            {
                Description = "24fc602f53e3440cb29f0e602964c1c6",
                Active = true,
                URL = "004028222bb6414db222369c323ebfa45a6e3fec394e4b2dbf6fbe777a5613afea6cdaac85374d8",
                ItemId = Guid.Parse("846548f8-0a9b-4ff6-9831-bdc654dbdf64")
            };

            // Act
            var serviceResult = await _itemAttachmentsAppService.CreateAsync(input);

            // Assert
            var result = await _itemAttachmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("24fc602f53e3440cb29f0e602964c1c6");
            result.Active.ShouldBe(true);
            result.URL.ShouldBe("004028222bb6414db222369c323ebfa45a6e3fec394e4b2dbf6fbe777a5613afea6cdaac85374d8");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ItemAttachmentUpdateDto()
            {
                Description = "6905cf00970f46d9a41453e8b7d80527dce051dab82d425a98df84dcd4c2ca6c5e9c0f9a36024",
                Active = true,
                URL = "f83ebbee49c54198a0f879b7b747c68d1e71f15749",
                ItemId = Guid.Parse("846548f8-0a9b-4ff6-9831-bdc654dbdf64")
            };

            // Act
            var serviceResult = await _itemAttachmentsAppService.UpdateAsync(Guid.Parse("0f8663b1-558e-4a85-aa76-6a1d1f6b3acf"), input);

            // Assert
            var result = await _itemAttachmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("6905cf00970f46d9a41453e8b7d80527dce051dab82d425a98df84dcd4c2ca6c5e9c0f9a36024");
            result.Active.ShouldBe(true);
            result.URL.ShouldBe("f83ebbee49c54198a0f879b7b747c68d1e71f15749");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _itemAttachmentsAppService.DeleteAsync(Guid.Parse("0f8663b1-558e-4a85-aa76-6a1d1f6b3acf"));

            // Assert
            var result = await _itemAttachmentRepository.FindAsync(c => c.Id == Guid.Parse("0f8663b1-558e-4a85-aa76-6a1d1f6b3acf"));

            result.ShouldBeNull();
        }
    }
}