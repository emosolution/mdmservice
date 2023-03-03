using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.CustomerImages;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerImages
{
    public class CustomerImageRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ICustomerImageRepository _customerImageRepository;

        public CustomerImageRepositoryTests()
        {
            _customerImageRepository = GetRequiredService<ICustomerImageRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerImageRepository.GetListAsync(
                    description: "66985881031043fda8b377e72d8515d86c391bda0c3a41d4aa12a7a3712a179b65b83ce84fba4d27837246c4e31d4d72790f2124eb3a4005bd944357ec1de7d80db357bd99594937be0ef426c8a25dca2f049a4c331944e6a43ed28c775a718c37f8cd3251d44136bb8eafa2a52bd3e21d59d3df121d44528158dcdf05138d15b4ce000f1a984e808c2fbc11206962ad6de935c45f744df7996c07c239082b89573f1eac70404004b5876cc750c3bd4c888f615b3e6445038ee5a7ecf8957ebb67f1b46a0e2348f5a081abe2dc04396b00558e6d1e744ea0b2c9b94d063584b1655674d02fb248b389227fa8427fbac9debc89d5296240c2821d",
                    active: true,
                    isAvatar: true,
                    isPOSM: true,
                    fileId: Guid.Parse("22d2f561-9c76-4888-b8b7-4a634900d6f1")
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("d3f948bb-4a2d-4cf8-9186-042dc2120986"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerImageRepository.GetCountAsync(
                    description: "a221d995f7754d8b9ecb50bae6f8e1bb166ce6a2317d4afeac412ec7a7e840b026a36c5366294d808d9e29a8e8d05d8bc0e73cd7ffde490ab5f1ede3340e85cc2adf4a8c4726463a92c9f01654ac2306964b4ce8e59f419c8a1c167f5f5e67f22983707abfc543d890b67dea692b69779434f4d5b3154691abc3b4f0f44a30d3052c9056bb6142e78689c507c220c667fab38c2af2f640af827f074572c64d060c455e77f9b942bc8a5170d6d98e7037e180d5c4a50048f1b692325babeefa299dd9586af6a443e5b1788cd510ae462d0d18b6634c86462e9e6aa984b07161ac99e77942820b4817ac5775f208ef8f3273adc2f89edb4f1b8481",
                    active: true,
                    isAvatar: true,
                    isPOSM: true,
                    fileId: Guid.Parse("b439329b-0aa8-4d92-87cf-72ed362b987c")
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}