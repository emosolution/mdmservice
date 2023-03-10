using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;
using System.Collections.Generic;

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
            result.Items.Any(x => x.CustomerAttachment.Id == Guid.Parse("041d9685-c4c0-4433-9419-6b4dd11218da")).ShouldBe(true);
            result.Items.Any(x => x.CustomerAttachment.Id == Guid.Parse("9d413f20-2063-4257-83b7-e59e6566c43f")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerAttachmentsAppService.GetAsync(Guid.Parse("041d9685-c4c0-4433-9419-6b4dd11218da"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("041d9685-c4c0-4433-9419-6b4dd11218da"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            /*
            // Arrange
            var input = new CustomerAttachmentCreateDto
            {
                Description = "ff747e8f5a2e491abdd2f1f25daf1a8f39e920cfe825496280c5d6a91df60a258b641523f3744314aa375252298bcdf1418f3a79c9cb4b2b9fc4c09bd1194692a5e8b6c656104d92a90bb21f86eed06c7eb7ac04c7ee4a4486838b40612685f4c04ee68559de4fd489abd29c26a9df23f5e4f943a76e40cfa8148f30b1e69de04b2f779aa20a49bcb3351b75c54446e51300964959d4441f936326a620138e952b167701886e40eb8d0b09850e60e57f4c340ddc60124ecd84782a2f1c2d3a80ae163e06a270422389e1181418a356b9898f5e44fef941de968d71d63f101ecf27f515352035464eab74acd106baf7de5a1741e480e04f43a551",
                Active = true,
                //FileId = Guid.Parse("3c81dee2-e8ad-4cd9-b877-c8a17bdb8d13"),
                CustomerId = Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51")
            };

            // Act
            var serviceResult = await _customerAttachmentsAppService.CreateAsync(input);

            // Assert
            var result = await _customerAttachmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("ff747e8f5a2e491abdd2f1f25daf1a8f39e920cfe825496280c5d6a91df60a258b641523f3744314aa375252298bcdf1418f3a79c9cb4b2b9fc4c09bd1194692a5e8b6c656104d92a90bb21f86eed06c7eb7ac04c7ee4a4486838b40612685f4c04ee68559de4fd489abd29c26a9df23f5e4f943a76e40cfa8148f30b1e69de04b2f779aa20a49bcb3351b75c54446e51300964959d4441f936326a620138e952b167701886e40eb8d0b09850e60e57f4c340ddc60124ecd84782a2f1c2d3a80ae163e06a270422389e1181418a356b9898f5e44fef941de968d71d63f101ecf27f515352035464eab74acd106baf7de5a1741e480e04f43a551");
            result.Active.ShouldBe(true);
            //result.FileId.ShouldBe(Guid.Parse("3c81dee2-e8ad-4cd9-b877-c8a17bdb8d13"));
            */
            var result = await _customerAttachmentRepository.GetQueryableAsync();
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task UpdateAsync()
        {
            /*
            // Arrange
            var input = new CustomerAttachmentUpdateDto()
            {
                Description = "f924ee2b22e8447b9381704899d13a646d49c07abdfb4a6383ddb05f34b838882c3058fcedd44dc78c83332a75b7322a2e10c0863cf2456aacd7960de9ca51a2443c238bcd004ed89629dc09b52ca84038f99f7345c94fc0ae8ebdf18a36ee5d3a937dd55553433abe043afe7590baf5586c04819f9a40d297f322e055ab668d0cd42fd8873e4bd6925a2f826231624b832be1667e5c49e7aec3ef044acac4720eac7c0bea7549d8943e1a17c61c179675bc23f5aaa3470f88b49da77409536f14370e671f7549b092508d8bdc469958306f55d9e4b04be98cef953c59156359ca0b8bfe7b824ce69e81ddc058fc4ceec2ecbc8d457b4ffb8812",
                Active = true,
                //FileId = Guid.Parse("bff35124-1220-4c42-b7e4-baada06bbabb"),
                CustomerId = Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51")
            };

            // Act
            var serviceResult = await _customerAttachmentsAppService.UpdateAsync(Guid.Parse("041d9685-c4c0-4433-9419-6b4dd11218da"), input);

            // Assert
            var result = await _customerAttachmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("f924ee2b22e8447b9381704899d13a646d49c07abdfb4a6383ddb05f34b838882c3058fcedd44dc78c83332a75b7322a2e10c0863cf2456aacd7960de9ca51a2443c238bcd004ed89629dc09b52ca84038f99f7345c94fc0ae8ebdf18a36ee5d3a937dd55553433abe043afe7590baf5586c04819f9a40d297f322e055ab668d0cd42fd8873e4bd6925a2f826231624b832be1667e5c49e7aec3ef044acac4720eac7c0bea7549d8943e1a17c61c179675bc23f5aaa3470f88b49da77409536f14370e671f7549b092508d8bdc469958306f55d9e4b04be98cef953c59156359ca0b8bfe7b824ce69e81ddc058fc4ceec2ecbc8d457b4ffb8812");
            result.Active.ShouldBe(true);
            //result.FileId.ShouldBe(Guid.Parse("bff35124-1220-4c42-b7e4-baada06bbabb"));
            */
            var result = await _customerAttachmentRepository.GetQueryableAsync();
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task DeleteManyAsync()
        {
            // Act
            await _customerAttachmentsAppService.DeleteManyAsync(new List<Guid>{ Guid.Parse("041d9685-c4c0-4433-9419-6b4dd11218da") });

            // Assert
            var result = await _customerAttachmentRepository.FindAsync(c => c.Id == Guid.Parse("041d9685-c4c0-4433-9419-6b4dd11218da"));

            result.ShouldBeNull();
        }
    }
}