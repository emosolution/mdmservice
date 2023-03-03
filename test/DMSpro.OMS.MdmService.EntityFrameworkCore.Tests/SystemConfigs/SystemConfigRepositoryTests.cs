using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.SystemConfigs;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.SystemConfigs
{
    public class SystemConfigRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ISystemConfigRepository _systemConfigRepository;

        public SystemConfigRepositoryTests()
        {
            _systemConfigRepository = GetRequiredService<ISystemConfigRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _systemConfigRepository.GetListAsync(
                    code: "4ebfe30e5eb944338bbf",
                    description: "044ca571b74943bda67f78f3cc719508bc6f094044c54d2f82c5771be1e9435dd9be5063e1e348d7b24243608ec1a47ee5e476749c7e417cacd1e4a39b71f19b938cf8eb4fb948d48cdd6b063ee139c96e9479f6138547e9a9917d3b0747fd976750cd65c4c446d8a001f26f505416ebb2b414b44d1e465592599c55a0417966c31dd28340e5469aadb48e837c2cc6cb8170d8ee71fc4ec7ba08366fdbfadc8dbe3f149a4a424267a318c18be43b808c6ff458c715c549c09397bea2b0a1b07cd91ab6b88f2948a4a3e5e93756eead73e346f6bb373e415b8de4e0bd1fccb0920fcd6d9c371d4fcb8ac0306df3f55a8a5fb10c1a9e3a4191b98c",
                    value: "e39f0eab4c2d4d73b088587d27b632e9343c221bc21f48c99d3a53048d7e051af7145e473bcc4b9a865f01f10564fa5dc1ea541519d545799644b99370e9006acb5adca1a71f40428553bbcaabb97bd22b231a49cf3f40e3a985dadaf707352d24af8053081f4e5d97788bb174fba53d4229a393f4804c71966a79ec8f6de08",
                    defaultValue: "77b9c25ea96f424ab82cb103af80e5b461ada53f423f417cbed45568a529bd02c71a6552eedd412c8fcc5c40c1a6bfe814ca7ff51ff64e8e9004bc8c738452bda467d2ae8f1d4506ade4efbbdd2b981dd3978b97987a4dabad8176f6bd454fd2899ff61f258b4e0b9c1a50a36ff3f1f844a20dbb49424c9396adef50a3fbdfc",
                    editableByTenant: true,
                    controlType: default,
                    dataSource: "21e8d3fe000a4a458bfcf26948fae6afe34b165e744241758589b23235f1d581e74a1739b9904876a9cce650ef0afb29b83781e9782a4818aa6c7e37d36da910e4128b5c3da04e4c8680fb5a9e4d5cd8b052a9412cf641ff8a475a891a8be34e63bacdb609544533aaa1c8f1342195504f372ba7b66e480894b43800d7a65017b8afd17d5d724724bb7ed400b33d75cb1d89cb36339e4c67af4097457608c5e8f259583fd722467fb012c63c4019097db5bc93db9e7f4785b5ec324b94b962f80343916ac21d4d72934e6630a31dbbb310ad502e10b54d60b669e7928ff552aaca6c8e8aed04456facbb744e782344c4df21630c64564eccbedc075bf8a832fdece60a24724f4bb38c1d82bca69a18efe0991919f0aa492784faebe9cd872f3859cf972b5e1841ba856959b087fd58faa614715931484205b7ae080939bd7b1c59e4620f98614599b21a7ed27c54505568a12b5388a7440bb8460f431434af25e058c498061940408b0eb199d40ea5b695ee28a9c955453ca6f79fc5e84e215c6df255c350c748c885dc40bddc33c5197dc1710ca03b4b9b8dc3f04c81c035fba524776f07fd43e0abac724ded9ad8f18d6905f72efd4d7981d3a97f1efed1d652e1a5a0ffd3492f8a63671a3f94645e4e6c092929e64b3a97db2fdb288a6db1c81ef366119b443c8f60b74ca699ece9f0c1c040"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("86430f1c-0f4f-418a-a607-a3030971f95c"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _systemConfigRepository.GetCountAsync(
                    code: "61d8462902154034a43c",
                    description: "81fc351e19ca44929c2d2f94cb99fa7d3c52d809ad584755b3b992cb42b2f84010a2056b6e6d423aa21a87deb7f4ce69ec31662f47d54aec9543b2659433573c52ebc1935fdf43c5be1a1ec8b6a60a9151d88f14de274ffaac32da8a3a38511d66796dd83cea40a2b7d10edd38c5bf0635e2c34504054fbc8cb7996e76fd32161fbd6aa449eb4271a246f9473fa72dfe059e67e946ee4cc488f9225cf41bafab3d78fbcb85f54e049ff6a91e1850fd84e85a61f51a7041fa92ae95c981237c7de090422e10454a5d9963d966565d42af3396ec09017b4c28a24e13ed0320d9fc90d2f87d8b5c4084b0b1da0f0da37d4f94baba65a3a34067a904",
                    value: "8b5fa1dce6f64e2a9a325779108ae400d482ce8be2c5406184e7c7b161c1dad9c6987e645bc44e0893d6d29b72c2e07490f4d1156fa3404f81064e2528b76cafcfdde2c7b2964c1e8a355dd13bfec2be3e638a8c7b684e60b297d61a14f676f99e96b86a74054ac593e3244cb2522668c32a3d51ea004cbebf6901220ebbad9",
                    defaultValue: "b426a172104940ea9b77799931f0a8b1a291bfd2a6754445a9464ea732e39b2f1d2025b599464ae8848acccd939f757a2ecc0e160efe4b4e97df3000bf2eb1d68d4b921e261b47b7b3eb445c2a301c57f064fd7d410842b69f275f6e5af9a895160e9547bf7440b6b36d44441dc2a4e8beeafc40d6bc46f499ff19fac24cd10",
                    editableByTenant: true,
                    controlType: default,
                    dataSource: "bff370fb28104c23bd2b9ebb2b4093b8244ec13dbe014c7bb0db5601e459cf96180e9d30daa0441488620607fecee5f917ebc0a5c8fe433cbfcd65e7360546ffd35cd88957a64bcdbd95215ba4a6d5aa663d4b1cdd3a4fa7b9ac48696486f2f18cc74e9f9f7a47559a43d20dcc228e0b450dd4c90c8c4d84b0bba3932e3ed8c76a569e09e24c4dfe914e73979c69e2966f25f018f838439da4295da645e51a3fcc09f8ea467f4733af4cb196eb2f78b587fe5cdd0211453fb842afc2d19ec62873255ce15d2d40a4a3fd61030c4e7521cb012af275a34d9f97578deb895bfab4d29068e8754d480394af1cbbca2a9b7dfdccef3385cb4034928ef65c4957b6d2e0846ff869a948058fc8015931ea4df0b3c527de2f1048a2b298d2305a50301b5a16af764abb44d3bdd543320774b59f392aa20742d1443b960c4164c276b77084aef52024a84182832b32b423513a6cc9905546d3af44df91b1028641284bf294f3b8b01b8645cd9ced1ba870d07ff93ccb42ee6d704f6fb22c9b7b8a99d0f0171af88d0fbc4d94b24fc61b5c450c307b02b9df1fe849e2a2bb828878725f7dfbb387420bac4544992440a0202b0b2426460a96b517402fb09df0954985da2b61f23d85a43e4275b72564c5f8ea3ca4b7a3f11b0e3445d0937738e310dbbb1e8fbc8c8c10564db697181080602e6f380c8a3614"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}