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
            result.Items.Any(x => x.ItemAttachment.Id == Guid.Parse("085e25a9-91ba-48b8-9d01-2d28a6f69d72")).ShouldBe(true);
            result.Items.Any(x => x.ItemAttachment.Id == Guid.Parse("cf6c4f27-336d-4daa-af74-ec751e244a7b")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _itemAttachmentsAppService.GetAsync(Guid.Parse("085e25a9-91ba-48b8-9d01-2d28a6f69d72"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("085e25a9-91ba-48b8-9d01-2d28a6f69d72"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ItemAttachmentCreateDto
            {
                Description = "3e279843a9cf4517b67fa1806e93ea4680e940bc096b4aa8b08c8d12ab46dfefed73115235b94627b97e551c58fb9e3b64624d6daa264319914dfd99977cf0864dc518f0dfb14f9f98d96f6140f0ddd9c04e1728a40c4090811da729a7f4b82b922cda16dbd7411a8d47bb587e0339137aa0d312fe43474094c0677727e6c5b176725e5b2c4c46b98b2f88d21d298bf9b4c1cd88626e44ae832d927eac89327fcac8757c49644fc38dd1774ecc7c939d19bc4d596437418f90ad996ae1b0d180c71ef1ac932045fb92c6e85261c1b6e5924a59dfc4504cb7b7817396b891e930f81cc352e17e41a69ef9bdc4ba1526ca047f9bb574d641e48b3b",
                Url = "81639765c98b4f9e830a902c61f84b02d5a93aa84fa54cd29132ea35f60e6d00c97671c256354136a674ad4c10484c94d34b4c4a6e714be292c99e9f84f940233cbed6143f934cf48177ecc061fc518b44f53ad449864c9d979671a5011aade75513644eac314cd795f71b9a49afb82c3050462cadcb4e2c9a25c37857bd376d03e0c1deefe24efaaf63bb7041aae1c485247c59a4fd4ebe938d3c02aa441422c444033a88304dea89d97362e72e2a5fa68aad26c5814ec6b9a1693e8144789347b5d1ee65e748de95b46754baddb99825aaf67911a44f05811324c6bdaa95a925023f71aa4049d3b9461701d80536dacd3e077de3474ab0a09c",
                Active = true,
                ItemId = Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5")
            };

            // Act
            var serviceResult = await _itemAttachmentsAppService.CreateAsync(input);

            // Assert
            var result = await _itemAttachmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("3e279843a9cf4517b67fa1806e93ea4680e940bc096b4aa8b08c8d12ab46dfefed73115235b94627b97e551c58fb9e3b64624d6daa264319914dfd99977cf0864dc518f0dfb14f9f98d96f6140f0ddd9c04e1728a40c4090811da729a7f4b82b922cda16dbd7411a8d47bb587e0339137aa0d312fe43474094c0677727e6c5b176725e5b2c4c46b98b2f88d21d298bf9b4c1cd88626e44ae832d927eac89327fcac8757c49644fc38dd1774ecc7c939d19bc4d596437418f90ad996ae1b0d180c71ef1ac932045fb92c6e85261c1b6e5924a59dfc4504cb7b7817396b891e930f81cc352e17e41a69ef9bdc4ba1526ca047f9bb574d641e48b3b");
            result.Url.ShouldBe("81639765c98b4f9e830a902c61f84b02d5a93aa84fa54cd29132ea35f60e6d00c97671c256354136a674ad4c10484c94d34b4c4a6e714be292c99e9f84f940233cbed6143f934cf48177ecc061fc518b44f53ad449864c9d979671a5011aade75513644eac314cd795f71b9a49afb82c3050462cadcb4e2c9a25c37857bd376d03e0c1deefe24efaaf63bb7041aae1c485247c59a4fd4ebe938d3c02aa441422c444033a88304dea89d97362e72e2a5fa68aad26c5814ec6b9a1693e8144789347b5d1ee65e748de95b46754baddb99825aaf67911a44f05811324c6bdaa95a925023f71aa4049d3b9461701d80536dacd3e077de3474ab0a09c");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ItemAttachmentUpdateDto()
            {
                Description = "02eaea5c104e49db92d864bf838c9a938d1c26c8eb3f4cc98e9388c7a0fb2e5c692bcca8973f4001907a6899e04e909a54b1b90657d74ebcabf35a3557a9be5326f78f28e68d452ab3e66ca00e3e6b57e9882fa12f1b4cd8a7d625578490f9b7e658fc78ce7d4895a074a2b243ea12239be41290187445bba0026cd6f2be2cc88112762fb1a049ea94c6963bb81f45d1b07d878ffd474a21923b6de0cc3ce3fc71d09b03837b4767b2a47fcf716c1a2abe252afdef4f453e8fc7702f391ebd6cd31b2af0e3344eb1a095802c1286740c63834333753e41038bb67b2922b66a841c7b4880752c4b4aa72ec77f9847cc76f347c8497389470fa47e",
                Url = "cd2d3867cda44db8bcee443364dce85aadcec131cb5740be8484a6cc8af5d71e08513e88c8d247858ef8e09e490af57d5a8863564fb549519793175f4692001ee9ee864f702d4eabaea7c9f0ec434204e653061f45bd4b4e9f839f50a2845b1e3eb8c963864a4c149ee73dbbbaa781819cfd7b72b8e348689401ca6d31890da62b61557949734b77860348ca09c6b2c6899f4ff5c59744d3a8864c0ae69b36c4e21f6e07f6d04227a1d7299476cda8c3fc5d1e6903e54178915acb84293299a007ce0cd72b284be1a8f0d0d3641872fcb5fca9851eed4a64b0c741c369066cd5fe6bda73b89946c8b6126abe65c1e4647e9329cf90ae40178a01",
                Active = true,
                ItemId = Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5")
            };

            // Act
            var serviceResult = await _itemAttachmentsAppService.UpdateAsync(Guid.Parse("085e25a9-91ba-48b8-9d01-2d28a6f69d72"), input);

            // Assert
            var result = await _itemAttachmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("02eaea5c104e49db92d864bf838c9a938d1c26c8eb3f4cc98e9388c7a0fb2e5c692bcca8973f4001907a6899e04e909a54b1b90657d74ebcabf35a3557a9be5326f78f28e68d452ab3e66ca00e3e6b57e9882fa12f1b4cd8a7d625578490f9b7e658fc78ce7d4895a074a2b243ea12239be41290187445bba0026cd6f2be2cc88112762fb1a049ea94c6963bb81f45d1b07d878ffd474a21923b6de0cc3ce3fc71d09b03837b4767b2a47fcf716c1a2abe252afdef4f453e8fc7702f391ebd6cd31b2af0e3344eb1a095802c1286740c63834333753e41038bb67b2922b66a841c7b4880752c4b4aa72ec77f9847cc76f347c8497389470fa47e");
            result.Url.ShouldBe("cd2d3867cda44db8bcee443364dce85aadcec131cb5740be8484a6cc8af5d71e08513e88c8d247858ef8e09e490af57d5a8863564fb549519793175f4692001ee9ee864f702d4eabaea7c9f0ec434204e653061f45bd4b4e9f839f50a2845b1e3eb8c963864a4c149ee73dbbbaa781819cfd7b72b8e348689401ca6d31890da62b61557949734b77860348ca09c6b2c6899f4ff5c59744d3a8864c0ae69b36c4e21f6e07f6d04227a1d7299476cda8c3fc5d1e6903e54178915acb84293299a007ce0cd72b284be1a8f0d0d3641872fcb5fca9851eed4a64b0c741c369066cd5fe6bda73b89946c8b6126abe65c1e4647e9329cf90ae40178a01");
            result.Active.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _itemAttachmentsAppService.DeleteAsync(Guid.Parse("085e25a9-91ba-48b8-9d01-2d28a6f69d72"));

            // Assert
            var result = await _itemAttachmentRepository.FindAsync(c => c.Id == Guid.Parse("085e25a9-91ba-48b8-9d01-2d28a6f69d72"));

            result.ShouldBeNull();
        }
    }
}