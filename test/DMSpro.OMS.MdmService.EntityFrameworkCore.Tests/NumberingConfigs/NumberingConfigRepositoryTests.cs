using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.NumberingConfigs;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public class NumberingConfigRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly INumberingConfigRepository _numberingConfigRepository;

        public NumberingConfigRepositoryTests()
        {
            _numberingConfigRepository = GetRequiredService<INumberingConfigRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _numberingConfigRepository.GetListAsync(x => x.Prefix == "135b9e74c0944c8499ed" &&
                    x.Suffix == "d8cbaf40cc6a484a8f47" &&
                    x.Description == "29e19cc86aa645eda6d93fc383a6ac07002d99050a544389914880017591ab7906151163b69d43beae4c379b625edabf04956acd7f794d86817da1024fa633ccc00fd4fd60b64f0e9b3b9edb8bf777dd5f84ac4d0af844ada8edfbc6aa44b40d2872ca651a834ae19ba758ffc5be1eb22901bf6e72514c7aa6575ac87cbac2e"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("1f96f177-3168-49f6-9fdd-97d2c5776f0a"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _numberingConfigRepository.GetListAsync(x => x.Prefix == "de5fffeeebf943098aac" &&
                    x.Suffix == "cc1ec3971cf04b2fa8af" &&
                    x.Description == "69f8d5291eba4ba0bbdcbf992fa3cef74168476ed58c443bb9bffec5dbb9dc23870c76ca3fb94344ad29f61d2faac5fe35e51f4864ac40c3b9d46bd862866896f362051a29604be192409fc637e6d7dcfeaebdab91044f5c82341900b5492073a90d9364176a46c495f0e32d0fe7ba7119ecbe21414a4adb89e23e39720dbe8"
                );

                // Assert
                result.Count().ShouldBe(1);
            });
        }
    }
}