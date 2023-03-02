using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.SystemConfigs
{
    public class SystemConfigsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ISystemConfigsAppService _systemConfigsAppService;
        private readonly IRepository<SystemConfig, Guid> _systemConfigRepository;

        public SystemConfigsAppServiceTests()
        {
            _systemConfigsAppService = GetRequiredService<ISystemConfigsAppService>();
            _systemConfigRepository = GetRequiredService<IRepository<SystemConfig, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _systemConfigsAppService.GetListAsync(new GetSystemConfigsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("86430f1c-0f4f-418a-a607-a3030971f95c")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("72d36d51-225b-4ecf-8a7d-37dcba537458")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _systemConfigsAppService.GetAsync(Guid.Parse("86430f1c-0f4f-418a-a607-a3030971f95c"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("86430f1c-0f4f-418a-a607-a3030971f95c"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new SystemConfigCreateDto
            {
                Code = "ae5638e9315c4ca3850c",
                Description = "738f36d9faa64683bec62b606ec9f121e26d8a6a422a45d4b9abd688b9d6994518ac571dd515458494bc881514b2165cbff3cc21cd614565acc4d16008a1b54ad5fb431f21354af28fead1a331d066ccd3bd76ba443041928eca0467abf2faf6a0e1285d2dc8482184acea7df3b2e62f493c64c18ebb4e5b9b10417716638fd92c40cd9b84be48e58de73c0eb263842e2b7e9e38a2f7419986f60b043320a9f73c5ae7a401a74171bac367847baa5218d8ced0fec5d14e599b50c2b2808d3c888b54738d4d15467ab649dfc1350e8384ced3141dd95448a59c24c472d87571e49aa6876c4dfd408789477f9ffe61daca107f100163cb4d258984",
                Value = "dfa3add538e2463d9d7079bd907c4c7fae20a428da11407fb976e503b6c389a89d114ff81d7c4787bdd43afac103ba1cb021667699bf49e7a35f8b36bdbdfc4709c81c2da789430f86df805c01f0595be834ea891d9646b1a9017bdcbbd14f348f3f035fb5514200bb9b920eec96e081e05eba0ed6f646d79634733540683b7",
                DefaultValue = "4112208e02d34420aa147d7ac06a0586f0347d350d3c4824ae842fadebdeb0d03d96f855c46948b5bb97000da27d7ad0780eb39ccf19414591eac70aa96dd1834930b8e270154a16aba7d5b78712b4cd390aedbe41554fb888c852403f608db76a3b7e3b598541e59d67bc76b74b39627886ca0486194bd59a2454ec0e7104a",
                EditableByTenant = true,
                ControlType = default,
                DataSource = "922ef3be4d1249be818135174e6598a6b9eebf5a8daa4d1ea41ef13aafd26c467595500177a9473fb96a7e983c0d390da71536a0589841a1ba5fc01f4bcb4afe62953952153240fdabd76a673a9e24a1107d92ff47f34b40a869d2c1f7fc6ead8d7005b1fca24c78ac4186674e6d24b5de4dc4d228654a84bd54976f164b99190b41743db13d491d95c02725050cea206e706e407501498b9001b06109ca7b75a1f1c1d51c644cfbb7b2e0b9f9eba5cac1d46f59a19f48e6b460f86bd328ffb59a96ca57c87c4d3e8c4a8b912a003143dacd125b9ebd483da3e4bd094d31eb1f796fe23aad4044a08e637b6472db1896232f39d17c3941718d939140a339700bd7c8e9bb7dca49c1858078493220dd4453e7fefc6641429d9a75ed80808d4a444df6a9c7f96c45aa98edec79ef5772675d0ca75f80654aa6af7d83ae0d7b62ba854c1abd29bc4fb8842bba28f85126fefdeb6195404e4c94820dada2389921102de81dfbb60b4ad4a531bb6dd46ddea9a130269a8cd74f72bc70d42e3220284b6c9a5512b1dc45148ac6a26ca096b927df895e6b8f5d4f8a84053798c55b17befe0db2e24a654971b55f3e865aa4cd50d17ee0e87d2640faa269d86c95f7bdecbcea2e39863b4fe69311a279201cd9e8a09932474c5d4326a29f07a903d4db856031caefc71f448e96d48b533911689e45086a3a"
            };

            // Act
            var serviceResult = await _systemConfigsAppService.CreateAsync(input);

            // Assert
            var result = await _systemConfigRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("ae5638e9315c4ca3850c");
            result.Description.ShouldBe("738f36d9faa64683bec62b606ec9f121e26d8a6a422a45d4b9abd688b9d6994518ac571dd515458494bc881514b2165cbff3cc21cd614565acc4d16008a1b54ad5fb431f21354af28fead1a331d066ccd3bd76ba443041928eca0467abf2faf6a0e1285d2dc8482184acea7df3b2e62f493c64c18ebb4e5b9b10417716638fd92c40cd9b84be48e58de73c0eb263842e2b7e9e38a2f7419986f60b043320a9f73c5ae7a401a74171bac367847baa5218d8ced0fec5d14e599b50c2b2808d3c888b54738d4d15467ab649dfc1350e8384ced3141dd95448a59c24c472d87571e49aa6876c4dfd408789477f9ffe61daca107f100163cb4d258984");
            result.Value.ShouldBe("dfa3add538e2463d9d7079bd907c4c7fae20a428da11407fb976e503b6c389a89d114ff81d7c4787bdd43afac103ba1cb021667699bf49e7a35f8b36bdbdfc4709c81c2da789430f86df805c01f0595be834ea891d9646b1a9017bdcbbd14f348f3f035fb5514200bb9b920eec96e081e05eba0ed6f646d79634733540683b7");
            result.DefaultValue.ShouldBe("4112208e02d34420aa147d7ac06a0586f0347d350d3c4824ae842fadebdeb0d03d96f855c46948b5bb97000da27d7ad0780eb39ccf19414591eac70aa96dd1834930b8e270154a16aba7d5b78712b4cd390aedbe41554fb888c852403f608db76a3b7e3b598541e59d67bc76b74b39627886ca0486194bd59a2454ec0e7104a");
            result.EditableByTenant.ShouldBe(true);
            result.ControlType.ShouldBe(default);
            result.DataSource.ShouldBe("922ef3be4d1249be818135174e6598a6b9eebf5a8daa4d1ea41ef13aafd26c467595500177a9473fb96a7e983c0d390da71536a0589841a1ba5fc01f4bcb4afe62953952153240fdabd76a673a9e24a1107d92ff47f34b40a869d2c1f7fc6ead8d7005b1fca24c78ac4186674e6d24b5de4dc4d228654a84bd54976f164b99190b41743db13d491d95c02725050cea206e706e407501498b9001b06109ca7b75a1f1c1d51c644cfbb7b2e0b9f9eba5cac1d46f59a19f48e6b460f86bd328ffb59a96ca57c87c4d3e8c4a8b912a003143dacd125b9ebd483da3e4bd094d31eb1f796fe23aad4044a08e637b6472db1896232f39d17c3941718d939140a339700bd7c8e9bb7dca49c1858078493220dd4453e7fefc6641429d9a75ed80808d4a444df6a9c7f96c45aa98edec79ef5772675d0ca75f80654aa6af7d83ae0d7b62ba854c1abd29bc4fb8842bba28f85126fefdeb6195404e4c94820dada2389921102de81dfbb60b4ad4a531bb6dd46ddea9a130269a8cd74f72bc70d42e3220284b6c9a5512b1dc45148ac6a26ca096b927df895e6b8f5d4f8a84053798c55b17befe0db2e24a654971b55f3e865aa4cd50d17ee0e87d2640faa269d86c95f7bdecbcea2e39863b4fe69311a279201cd9e8a09932474c5d4326a29f07a903d4db856031caefc71f448e96d48b533911689e45086a3a");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new SystemConfigUpdateDto()
            {
                Code = "d69b745934e94d389116",
                Description = "ba2146ef493d44bfb446dcb616842aa9374e9b386ecd4cc6a47acedd330baa25a37ef82c6d60451c97814b693770985e7153927480de411497ef638d125825894e4de3eab24346a69e4da4afbee74e97d71b9b182d444d5d8dddfcb74ecc140f5a672d6b0134479eb22429c2ddc059e979fa914c976f4486b2b5b98017a1796cc13782f2e8c8421881697af42b7390d8425cf00022844287bafabc4312bcf96d2e99b2e1c0ab49b0bcad3b13c5b2ff4fc2e9f7c7259c4f99a41982a3641a456bdb5e6735beba42c68a1655bc17cea0a664e7178661ea41c8a779d1a0d0722dbaa29b83ca08794ea98bb01aaf8ad6df0525be5a0c2e45473c92e9",
                Value = "97f12c90dda14744bee33080a25b96badbccd8b00b2b4740a2964dc5f1ebcbd19debdc82b85d4195bd7242f4e4228a66206308a0aa324da9a6a609f07b609181515a25c60bcd4e03af79c95c539428ced8df64e760454620b75ebda9b1971d9159d728e806a74929a224e079a7764604480f4e3c073c44598c0368d4b66d234",
                DefaultValue = "41a1250afa9647e1850812ef48073533473c545dbf9e4e01a9dad75ce9ad561da582c389e20244c1aea4bb7fadc016a4d6f3ad8c03f54c748e6727405712d10623790e8526e246f0a47c404a52652a648168daf91db64746b584620f6c45d2024995a36dc0704581a41832a515c37055ac545987d9b54149880c2f554b848b6",
                EditableByTenant = true,
                ControlType = default,
                DataSource = "2de1f40962a3472394c5370a0c679b2ebe40e1ada43a4a819ac0752d95ee0ffa15ccfe7589eb482b9f2870da1994665bb2c4a6645e154478b83693e08ba7be591d435c1579834f8f8df1f630a0f8eafb6d94deea7f46422997f9a27a95c2727f5fe26198e14d494399bd2002eeb5cd87abbd5558e90240d1a19796a09806997c9c5a595920434442a906cff28493fe34418daa85c2784ce194bc1f21adabaaa135828998dd634bccac646f357c85f7d49b0585785c4245d8b454c2bbfd0a250a787ceae6f9e2477a9210173456152a4749c04e34d236424f9c13f1e6a4ae28c56bc2d5c9707c4e558cd210afc9ce62117f11ff6970df48cca274d718eff391ccc2140e4bece84220b0ec9270d1641b0355af9f3821734cfdb26a8ba3da2b79af05610a8aeed7468f96ed87738e9cc8b3c0f6002ed3db4301b2eb23393a2b94f3d26b63d26b964f4b8b4644d44da6b701a85190c4a8f2460e940a09a014bf7399856d0ca5d50d48eebd1513aa283aabd9080714f4ffd148819df2754672c310154b5e972e878a40a7ad4bbccc35fc357acabb99ec7ae74d22adb41acf49cc966861d7d54f004b4e4da490ad905a7aadc8bd627e093ecd4b4f85110a36e371003af2f0dcea131c450e8e83806d294a472464702597e5c64e16bc91ace8c633b2a565658d7c3df64bf6a7dadcd2cae80b34741d4795"
            };

            // Act
            var serviceResult = await _systemConfigsAppService.UpdateAsync(Guid.Parse("86430f1c-0f4f-418a-a607-a3030971f95c"), input);

            // Assert
            var result = await _systemConfigRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("d69b745934e94d389116");
            result.Description.ShouldBe("ba2146ef493d44bfb446dcb616842aa9374e9b386ecd4cc6a47acedd330baa25a37ef82c6d60451c97814b693770985e7153927480de411497ef638d125825894e4de3eab24346a69e4da4afbee74e97d71b9b182d444d5d8dddfcb74ecc140f5a672d6b0134479eb22429c2ddc059e979fa914c976f4486b2b5b98017a1796cc13782f2e8c8421881697af42b7390d8425cf00022844287bafabc4312bcf96d2e99b2e1c0ab49b0bcad3b13c5b2ff4fc2e9f7c7259c4f99a41982a3641a456bdb5e6735beba42c68a1655bc17cea0a664e7178661ea41c8a779d1a0d0722dbaa29b83ca08794ea98bb01aaf8ad6df0525be5a0c2e45473c92e9");
            result.Value.ShouldBe("97f12c90dda14744bee33080a25b96badbccd8b00b2b4740a2964dc5f1ebcbd19debdc82b85d4195bd7242f4e4228a66206308a0aa324da9a6a609f07b609181515a25c60bcd4e03af79c95c539428ced8df64e760454620b75ebda9b1971d9159d728e806a74929a224e079a7764604480f4e3c073c44598c0368d4b66d234");
            result.DefaultValue.ShouldBe("41a1250afa9647e1850812ef48073533473c545dbf9e4e01a9dad75ce9ad561da582c389e20244c1aea4bb7fadc016a4d6f3ad8c03f54c748e6727405712d10623790e8526e246f0a47c404a52652a648168daf91db64746b584620f6c45d2024995a36dc0704581a41832a515c37055ac545987d9b54149880c2f554b848b6");
            result.EditableByTenant.ShouldBe(true);
            result.ControlType.ShouldBe(default);
            result.DataSource.ShouldBe("2de1f40962a3472394c5370a0c679b2ebe40e1ada43a4a819ac0752d95ee0ffa15ccfe7589eb482b9f2870da1994665bb2c4a6645e154478b83693e08ba7be591d435c1579834f8f8df1f630a0f8eafb6d94deea7f46422997f9a27a95c2727f5fe26198e14d494399bd2002eeb5cd87abbd5558e90240d1a19796a09806997c9c5a595920434442a906cff28493fe34418daa85c2784ce194bc1f21adabaaa135828998dd634bccac646f357c85f7d49b0585785c4245d8b454c2bbfd0a250a787ceae6f9e2477a9210173456152a4749c04e34d236424f9c13f1e6a4ae28c56bc2d5c9707c4e558cd210afc9ce62117f11ff6970df48cca274d718eff391ccc2140e4bece84220b0ec9270d1641b0355af9f3821734cfdb26a8ba3da2b79af05610a8aeed7468f96ed87738e9cc8b3c0f6002ed3db4301b2eb23393a2b94f3d26b63d26b964f4b8b4644d44da6b701a85190c4a8f2460e940a09a014bf7399856d0ca5d50d48eebd1513aa283aabd9080714f4ffd148819df2754672c310154b5e972e878a40a7ad4bbccc35fc357acabb99ec7ae74d22adb41acf49cc966861d7d54f004b4e4da490ad905a7aadc8bd627e093ecd4b4f85110a36e371003af2f0dcea131c450e8e83806d294a472464702597e5c64e16bc91ace8c633b2a565658d7c3df64bf6a7dadcd2cae80b34741d4795");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _systemConfigsAppService.DeleteAsync(Guid.Parse("86430f1c-0f4f-418a-a607-a3030971f95c"));

            // Assert
            var result = await _systemConfigRepository.FindAsync(c => c.Id == Guid.Parse("86430f1c-0f4f-418a-a607-a3030971f95c"));

            result.ShouldBeNull();
        }
    }
}