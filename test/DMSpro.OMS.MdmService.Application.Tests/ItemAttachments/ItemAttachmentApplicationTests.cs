using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;
using System.Collections.Generic;

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
            result.Items.Any(x => x.ItemAttachment.Id == Guid.Parse("6ca6e468-ab4f-43c5-b376-dd94d92b830a")).ShouldBe(true);
            result.Items.Any(x => x.ItemAttachment.Id == Guid.Parse("0af5b120-f029-44a4-b0b5-bb4fad5baf6b")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _itemAttachmentsAppService.GetAsync(Guid.Parse("6ca6e468-ab4f-43c5-b376-dd94d92b830a"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("6ca6e468-ab4f-43c5-b376-dd94d92b830a"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ItemAttachmentCreateDto
            {
                Description = "abfeb0769efa4feb88a9ed1d9ed9bbd026a486a4423f4ac2914f9584cfcf70492586f27840a2403b85ec6419a3b3c2b22328383a3fbd4e68811faaf79fe4b6d6cf5353a9685b4f3f90a6dcad20f71c28e2c1398aa15f4d95b7ab951c3f5047fa85a5977a695f482280883dbe455b6bf674052aedd9c84555bf6db1b05741352307a461fb5ee2471fb8da7bb10e38a392e8633794417f4ce38bae3c2654bcc9fb944cdaa03ffb4b00b7a8a4e6f420da9ba5eff3793de24811886834b6f255dfe516bd26bd8a334f07bc98826400f9510f573b501d00df489bb585ea412c21b38f7e3b158640c940acaea6655646e23c6ce7acda3d86b34218ae9a",
                Active = true,
                //FileId = Guid.Parse("db9f57ef-8196-46b0-8185-66fcb25d4525"),
                ItemId = Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5")
            };

            // Act
            var serviceResult = await _itemAttachmentsAppService.CreateAsync(input);

            // Assert
            var result = await _itemAttachmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("abfeb0769efa4feb88a9ed1d9ed9bbd026a486a4423f4ac2914f9584cfcf70492586f27840a2403b85ec6419a3b3c2b22328383a3fbd4e68811faaf79fe4b6d6cf5353a9685b4f3f90a6dcad20f71c28e2c1398aa15f4d95b7ab951c3f5047fa85a5977a695f482280883dbe455b6bf674052aedd9c84555bf6db1b05741352307a461fb5ee2471fb8da7bb10e38a392e8633794417f4ce38bae3c2654bcc9fb944cdaa03ffb4b00b7a8a4e6f420da9ba5eff3793de24811886834b6f255dfe516bd26bd8a334f07bc98826400f9510f573b501d00df489bb585ea412c21b38f7e3b158640c940acaea6655646e23c6ce7acda3d86b34218ae9a");
            result.Active.ShouldBe(true);
            //result.FileId.ShouldBe(Guid.Parse("db9f57ef-8196-46b0-8185-66fcb25d4525"));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ItemAttachmentUpdateDto()
            {
                Description = "4619441313a84a43afb92b91b13c741f1f22da3cb9a943368cd601a60720c44e22e9963be01544e5906c1acdd816285c78e3b3c089224df28e7a19732ff8cca78a65a1f73f6e46ada56a03dbcf7a4ff9d8ce62c5e7ea4c7db07469254597c8c84408353df8ad44c0ac57e90fb3409cf765ea3584372f451a89611cbb5de3c44b00ffd0360f3b4ea4b5fc733322a1725b654865251d7b4645ab77f8e12298c6f9d7405142bd844e1fb1af85c4062ee972bc822b4a3d6746e4b7d110ca4fbc7175ebce277e862b4dd7855548cf5737e745ba56e64e4e6a4cb88cd86babdb7eb5c135965b1b07724edc800eb03303a2823ebe507d9d7cd34db99420",
                Active = true,
                //FileId = Guid.Parse("40f7a17a-dc2b-43ae-813e-af1eb0c34c2f"),
                ItemId = Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5")
            };

            // Act
            var serviceResult = await _itemAttachmentsAppService.UpdateAsync(Guid.Parse("6ca6e468-ab4f-43c5-b376-dd94d92b830a"), input);

            // Assert
            var result = await _itemAttachmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("4619441313a84a43afb92b91b13c741f1f22da3cb9a943368cd601a60720c44e22e9963be01544e5906c1acdd816285c78e3b3c089224df28e7a19732ff8cca78a65a1f73f6e46ada56a03dbcf7a4ff9d8ce62c5e7ea4c7db07469254597c8c84408353df8ad44c0ac57e90fb3409cf765ea3584372f451a89611cbb5de3c44b00ffd0360f3b4ea4b5fc733322a1725b654865251d7b4645ab77f8e12298c6f9d7405142bd844e1fb1af85c4062ee972bc822b4a3d6746e4b7d110ca4fbc7175ebce277e862b4dd7855548cf5737e745ba56e64e4e6a4cb88cd86babdb7eb5c135965b1b07724edc800eb03303a2823ebe507d9d7cd34db99420");
            result.Active.ShouldBe(true);
            //result.FileId.ShouldBe(Guid.Parse("40f7a17a-dc2b-43ae-813e-af1eb0c34c2f"));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            List<Guid> ids = new()
            {
                {Guid.Parse("6ca6e468-ab4f-43c5-b376-dd94d92b830a") }
            };
            await _itemAttachmentsAppService.DeleteManyAsync(ids);

            // Assert
            var result = await _itemAttachmentRepository.FindAsync(c => c.Id == Guid.Parse("6ca6e468-ab4f-43c5-b376-dd94d92b830a"));

            result.ShouldBeNull();
        }
    }
}