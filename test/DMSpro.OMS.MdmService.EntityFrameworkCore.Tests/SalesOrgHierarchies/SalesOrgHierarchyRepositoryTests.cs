using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public class SalesOrgHierarchyRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;

        public SalesOrgHierarchyRepositoryTests()
        {
            _salesOrgHierarchyRepository = GetRequiredService<ISalesOrgHierarchyRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _salesOrgHierarchyRepository.GetListAsync(
                    code: "b90415bb92404ed49fed",
                    name: "04638fbe56dc47e0b2658e028d3e061197ed3cbf86d44d73ae3d1abcbaf859a1f5ee9056ef2042139d774142145c0af323f2a827ca3e487d802f306cbbbcdac1cfe16cddea974cff8b56ca63d425aacd64a85d477ce2453fb5aa6e4578662f89ac5a74ef2a42431090630cc11cfc92d8d20a9b3b4dd04cedb380bd58b94a00c",
                    isRoute: true,
                    isSellingZone: true,
                    hierarchyCode: "3fd1c1498653426cb8d658f7851c6fd026c1ad30e8f34214bce551033bfec62e52fd209812af41d89c9ce8cf14a728c4f5cf125d411a45d681bb7fc01bc283c08c506188b8684013a6a106b5a3eac56612c5abb7e478401f81af0d8b3b20431823bd75c445334dff95ad85c727ee05eaaa155a72bccd41069f4958a447f718f81528e62203544e36ba2210cfeebcd3a59546bfa655ed4657b94f8bddf94b13b60205ca7da0204288af5593c081664660a4c4c1aa0d284cf7aea21cd0cfbf74d2e4d6c7de7fa44d9dafe88fca8a45a979d985b8b7801642b48f1e6b0005b30f68683505815ffc4c7dba66a2abd48d2709673e5d08502d4b0fa6f5",
                    active: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("357a4424-f5c6-494d-b44d-cd180adc87cb"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _salesOrgHierarchyRepository.GetCountAsync(
                    code: "e5c933120c504e0182fb",
                    name: "e15e15e9008e4967ade760cd91f3654ee38db17afbb44b4d8ea1129d59c1e756904096dc787248f393fa753106c12ceab09011ffc5dc45dd81ee769041dd730fe68fe3332c17457bbf9e8fa4ac134681d2ffbd13ce954ed9b3de12311d7080a42a36ad4119ce42d08c6d807313b59ebf327b1c94cf264e8da6ab92bd868b577",
                    isRoute: true,
                    isSellingZone: true,
                    hierarchyCode: "3937f5f62b9944c093e84e0582bc30acc607d03db0f143aa9a88342b06338dff3e3a1a3530184f9c8e2db4edcbe0710a8bcdd77920be4b7f95221dda3a2b451f3cf5400484cb438bb9b1c56cafdbc2f001d44290c54c41a8ba48103efce613a22be66de16c8246f8934b5d9bc7b2bd117f766946bfb844f19067308b58f43c50f7c917a60c8d41b1bb29ad36f42dcb41c6d36d757aca47b5a28eabab78a6f949bf15bc692e8a4c9290613147e15151039c43b5312d4344568dcb9573641f7b96042d6633f6284d4e8c1038dcf249d9bf7cac4e9d36ea44d7920d1c72fee88b6a478f89bdfb0a4a7cbe70950ef2b84036934d69bd51d34f118fb4",
                    active: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}