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
            result.Items.Any(x => x.ItemAttachment.Id == Guid.Parse("66429276-e5ce-4854-8aac-ea6f7d15b25e")).ShouldBe(true);
            result.Items.Any(x => x.ItemAttachment.Id == Guid.Parse("e7477fd8-535d-450f-9ef8-7c9281a15df5")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _itemAttachmentsAppService.GetAsync(Guid.Parse("66429276-e5ce-4854-8aac-ea6f7d15b25e"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("66429276-e5ce-4854-8aac-ea6f7d15b25e"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ItemAttachmentCreateDto
            {
                Description = "732161739f21478abd28df008c0404980442ebbd387b49d8937548453bfee58bb23cd9a0b5ee4318821c0c159f93f15a7f10043bab4b4ab68019a663fca4bc18fffe136984fc48dab1a7d79a556a04b920d24224ea9f4e8e8bcdb49d4f2c1421a57dbfd29d3c4e588ce84e5a4a0fb796deac39b5a2584c8e9432888018c47af674f1fee207354922963d137cdfb1d332241e3348b92d47a78b4cb7f51b2c8167ec29d560dd1a494d9976939df85ef7cde41f24df11ed4544bdc49924aa01cbd4c8e5377f61d74226a58592f512ae9dcc7b57047af06e4929999ae3a3022c17165b1822920e4544fcb5d702457bdbeef3694f65b636124bf4872f",
                Url = "e73db72766eb47eca4c550ac9fcf6f336e8f49699e4548ada28d90b069f19bb8a3bd5f4e917940a3ad8b140496d899c1ce4c1d3e30f24c4a96c0af444b0a691d378cae962eaa45e39f9ced07e4c9cc4a825dc38ad286407d83791d234926d11091c110597989483eae16648bdb51ea69086b45f917d54b65b706e5b700a625e49ba5dc7e5d3e4bd59767b556ac691047a0d4ac5a90c94cc7941d3740809344ac7883e54b38fe4b8a997c91f39d194a8343ea2fba91da4c0081cfce68c6979c5c7876bde63cde47f9a10a0e09c16d14c1edb111845cc848d7a0cbd9ca2ab58f663037a3cf51624942b81f7c5d10b65a640bb8866c5dca45a29908",
                Active = true,
                FileId = Guid.Parse("bbd77c32-b1dc-4c97-972f-b0dfac9e7a46"),
                ItemId = Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5")
            };

            // Act
            var serviceResult = await _itemAttachmentsAppService.CreateAsync(input);

            // Assert
            var result = await _itemAttachmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("732161739f21478abd28df008c0404980442ebbd387b49d8937548453bfee58bb23cd9a0b5ee4318821c0c159f93f15a7f10043bab4b4ab68019a663fca4bc18fffe136984fc48dab1a7d79a556a04b920d24224ea9f4e8e8bcdb49d4f2c1421a57dbfd29d3c4e588ce84e5a4a0fb796deac39b5a2584c8e9432888018c47af674f1fee207354922963d137cdfb1d332241e3348b92d47a78b4cb7f51b2c8167ec29d560dd1a494d9976939df85ef7cde41f24df11ed4544bdc49924aa01cbd4c8e5377f61d74226a58592f512ae9dcc7b57047af06e4929999ae3a3022c17165b1822920e4544fcb5d702457bdbeef3694f65b636124bf4872f");
            result.Url.ShouldBe("e73db72766eb47eca4c550ac9fcf6f336e8f49699e4548ada28d90b069f19bb8a3bd5f4e917940a3ad8b140496d899c1ce4c1d3e30f24c4a96c0af444b0a691d378cae962eaa45e39f9ced07e4c9cc4a825dc38ad286407d83791d234926d11091c110597989483eae16648bdb51ea69086b45f917d54b65b706e5b700a625e49ba5dc7e5d3e4bd59767b556ac691047a0d4ac5a90c94cc7941d3740809344ac7883e54b38fe4b8a997c91f39d194a8343ea2fba91da4c0081cfce68c6979c5c7876bde63cde47f9a10a0e09c16d14c1edb111845cc848d7a0cbd9ca2ab58f663037a3cf51624942b81f7c5d10b65a640bb8866c5dca45a29908");
            result.Active.ShouldBe(true);
            result.FileId.ShouldBe(Guid.Parse("bbd77c32-b1dc-4c97-972f-b0dfac9e7a46"));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ItemAttachmentUpdateDto()
            {
                Description = "8a28292079de4bc3b34e684362cdfd767a127fea97da4452a3fa23ca7f0d75ea8c128751f7c948939d307304aed37e31e3bd88283a05449d8092f5ca81cb421c7639373510f045a8a484abd2b27a0ea2fb24d49bdd974cce8dc251199ab61305bfd7523b22494948b6893ed4351dfdc14cbb0477cb494dc48b21f7cbb3956acefbba308208d54d4db4193cef98650a452fbd90ec67b34081b957eb45b0603df7065709079cad4113aaa695321c9460ce29ed6cb6fb2d41b288a8fccd60239f5ea757b61846f743ccb04bac043bd9de31f54bc633945f4b969ee4dcf194053db0cc4149ca41464860a2274db568a93bbf9f82da199b27459fb288",
                Url = "2bb4168ba6cf4fb49f38ee947c12ab673b44841f7f9847f19f44f007da00255a7df3ffde6fb149d88634dca9b6254a24b116209e923142d49eb1aa9ab9d429ddd7a417aa44f14b50a4c4b2723c44d91b1e57b9805ca141499c28b0ff2dbbedd9f32a7633018040bbb1eee2eee1425856763909759b8345088b69822b6a996238157599a2a6534698abc495b5bef4cd1e59367c73219c40829fc8a235cc7c117dadd2ff5651504a6fb5f5e9173dd7b01e7632628bf2a54667a156e799da1a1b2f46277b021e9246fe85146c549593a5c6de64671f371040dbb19be77bce367455b5f3b9b8f4c14849a6c85c16d7a5c67aa1482f4adf5549dd9ed6",
                Active = true,
                FileId = Guid.Parse("4db7b0fc-2813-49ae-90e2-15e3d5a3b20b"),
                ItemId = Guid.Parse("d318ea89-992c-4d36-bef0-2b12495d19e5")
            };

            // Act
            var serviceResult = await _itemAttachmentsAppService.UpdateAsync(Guid.Parse("66429276-e5ce-4854-8aac-ea6f7d15b25e"), input);

            // Assert
            var result = await _itemAttachmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Description.ShouldBe("8a28292079de4bc3b34e684362cdfd767a127fea97da4452a3fa23ca7f0d75ea8c128751f7c948939d307304aed37e31e3bd88283a05449d8092f5ca81cb421c7639373510f045a8a484abd2b27a0ea2fb24d49bdd974cce8dc251199ab61305bfd7523b22494948b6893ed4351dfdc14cbb0477cb494dc48b21f7cbb3956acefbba308208d54d4db4193cef98650a452fbd90ec67b34081b957eb45b0603df7065709079cad4113aaa695321c9460ce29ed6cb6fb2d41b288a8fccd60239f5ea757b61846f743ccb04bac043bd9de31f54bc633945f4b969ee4dcf194053db0cc4149ca41464860a2274db568a93bbf9f82da199b27459fb288");
            result.Url.ShouldBe("2bb4168ba6cf4fb49f38ee947c12ab673b44841f7f9847f19f44f007da00255a7df3ffde6fb149d88634dca9b6254a24b116209e923142d49eb1aa9ab9d429ddd7a417aa44f14b50a4c4b2723c44d91b1e57b9805ca141499c28b0ff2dbbedd9f32a7633018040bbb1eee2eee1425856763909759b8345088b69822b6a996238157599a2a6534698abc495b5bef4cd1e59367c73219c40829fc8a235cc7c117dadd2ff5651504a6fb5f5e9173dd7b01e7632628bf2a54667a156e799da1a1b2f46277b021e9246fe85146c549593a5c6de64671f371040dbb19be77bce367455b5f3b9b8f4c14849a6c85c16d7a5c67aa1482f4adf5549dd9ed6");
            result.Active.ShouldBe(true);
            result.FileId.ShouldBe(Guid.Parse("4db7b0fc-2813-49ae-90e2-15e3d5a3b20b"));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _itemAttachmentsAppService.DeleteAsync(Guid.Parse("66429276-e5ce-4854-8aac-ea6f7d15b25e"));

            // Assert
            var result = await _itemAttachmentRepository.FindAsync(c => c.Id == Guid.Parse("66429276-e5ce-4854-8aac-ea6f7d15b25e"));

            result.ShouldBeNull();
        }
    }
}