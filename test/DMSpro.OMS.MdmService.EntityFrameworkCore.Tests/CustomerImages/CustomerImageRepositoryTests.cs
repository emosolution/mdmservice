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
                    description: "18e90949114547cebaf069bbcddd4fcb9b0f5bd43fe8417ab004552933c4da450a84fb0275424f92b1d5ae0038ab05b97efa5cf800be42b7a66652ec1ed53ba9548f6a71bfc24e4c81b1d47225f2e1fd8999bd7ba117445985b2384d9460041c27d8c626f740444398a1f340f5266935ef60bac6c11745b29cc0d0d2b67ad2245bbdc1a2f0594d2e951059931e6d4a349d8752c619dd4d6b8bc0a1250907112c16fcb65a9eb548a1991d5a8a05215718a7f5bd7793c84dcb88b5dd280444d759ea47503ee4ce4ae78e5862689ccc1c2f1f30e754863f49a19d137052f983570cfbcbff6d0c264e75bd7ffd7b587466962b34e58c68ed48bb9843",
                    active: true,
                    isAvatar: true,
                    isPOSM: true,
                    fileId: Guid.Parse("e4b91f1c-6f38-4d18-a867-4ce03f604c93")
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("9b411435-eb93-46da-ab72-db79d0998a21"));
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
                    description: "2f8226390248493fac55a2f7d96c557c82c47b0a033647e5883022fa226c2849b29ee5b36f754276b96f7c690c4fb370b9f32e4c7ba244939f919f97800d150edc41c50fc3d54620a9f697af5e8a2025ea041309a3ec434ab525581560879e3bf867ea91735f4f5ab02c7b44ef8ea5eab82a173a6edd4ef8a93ecabce7a8c3bf8c86c8ad9c2646d9b01f89d6a8f53cca10bc3fa3c87d427e8bd3eab067e9cadd34052745b781494ea1227e62be7d3abcb463d8d7c669491b900d2a6f897948ad3538cc9feb7c4ce8a9b2a2136c77e0b1071c8a0663df4275b687b44488cfe286185db435d3ae443688b8858b5c6c28dbf34208ddd3cb4084b9fb",
                    active: true,
                    isAvatar: true,
                    isPOSM: true,
                    fileId: Guid.Parse("4adae6a9-58cd-4f2c-8022-7aad722a4340")
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}