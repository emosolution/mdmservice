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
                    description: "18d5916224c84d029fd221e3d31604dc79e99f1e82b141c093586071025859ee547e81668551403380e89e1d0ca97d5defcb70c2a27d4543b7e58434b6a51fb487bd3bc0129e4cbe84b7d18721f73ef649105e1e10404cdbb28d2b72c527bb7b7652dc8987494d77b9583da152fa61e230b61839444c4d37bc949712a17430afc4f8d94545bc4b499bbf77bd2b8e6247484bf873cb9b452eb8448d9dcec43eebf34aaf435f094ba7b0620f53a6b63e5a5ccce0536d9946f3a19e30737efc8f7717b0ab2645064f0e873637ddef6591ee992f12664a10466c96a111cb0a9cec7bbcf882eafb1b433e90fff9c3002638be17c53ab6d1ca42c1a6a7",
                    url: "fee863ad6d1843dfb7e89e2b3d77cda28dc3f0bfa6bc45eb9891a45578538289ae210408be4746248dedbb08cf95cf3ef8248cfc068d4ab7b9122102200e297044925e6e672346fe8a45dcc3a0fbeb17da151903ef1e40a0ac411f45a9c78f42ba25fe5d19c64ebf96e34d4d8cc7452bbabfd9906524476fb4a7259d8094f496f1d50b162bcf48dea6d52e89cea181a52de89c9332084f8eb6a6f293b913b46f07cab3cc3be64782a377414423dac11f864241864f104ee0aae573c50686aa66067d092dfa7246a7a9b3f391bd00870a96e9ea8f386241c98e5824f53ac09948e6210f2725ad49eabac85c655b2e2af414d6984fe01c48ed95d7",
                    active: true,
                    fileId: Guid.Parse("101075be-f10d-477b-ac59-8db959af4c72")
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("66429276-e5ce-4854-8aac-ea6f7d15b25e"));
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
                    description: "cd353620c89e4d29b16ae4af58e4d6175a8aecc02a364dbfb6d0dc555fc4dfbbf44ab2b34ccf49828bba954e2ca966d06b6a37ffd97a40e4b9b34cdc75f863c4c0c84d0a13294497ae6b71ae003bcbc095d306a0c19d4cf8a9a7de731fb174eca477c120a848438494496a7a867e63163d7b607b698443f6b8ea83465d68980b77ece056c18640408dacb76a0d3912853ae806076073483da32fa7e4edf2789c62a651d88a4544e38d6a20be145f95ecefac6a0a6a6b45e0b7e8aadc608c75c380711444fdc74b038fa60de0e50e5c4250c8ab1b3d0c4f9fa3e751231412b24a43050f3dacca48a387f3bca86b323c0005e58c6719f44ea1baf1",
                    url: "b0cbb32b139947bf9d70af71bfd2dd198a3752f77bd148be914284403c45b6d8133efa6135c148efa88347bb6b1e0f6a887e4bdd088f48b08e90e43a31a7222059e82ea75d804a2bae08e252cf5da8e33ad1a24e56ba4d189bf4e976836a946c99785263dd96434fa9af97d51d59ade12d234c6ee8a849b8a4a441e27d7feadd7da4e946eb694965866034a404ccf791cc672b9ace734051a857d6eb449854ebd7b7bd1baac8446b970c5346a7ada17c0235284b6f5a4636808fa6f894598782e05b342e407342808b04b37eb415cb40e5c168a075d04c778c4c816cc1eb765435c67347c81b47ca82274a83a2bb3be6019e0a78b0e5414fa686",
                    active: true,
                    fileId: Guid.Parse("f630f75d-d068-4e4f-a8e9-938553c0643a")
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}