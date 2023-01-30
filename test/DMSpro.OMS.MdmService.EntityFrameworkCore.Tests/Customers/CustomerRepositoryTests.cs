using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.Customers
{
    public class CustomerRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerRepositoryTests()
        {
            _customerRepository = GetRequiredService<ICustomerRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerRepository.GetListAsync(
                    code: "48aeae2d9a4f4243b8bf",
                    name: "330e00b6188f480a816fa0d9e61434eaab1742baeba248e587683f7d5db21f3b310936dbb47c4856817ed14338ff2d22787baed7bf474e2092c90071d9d3cdd006f7c4fd2bd3455ead7c8700aacb7a1e40149b8f88d04c3bbc8b78008863ae17c0f478242dd04c6eba7f1bbc72ffdc4474382acf39b642849c05932c61fa377",
                    phone1: "1a9081fe2c424a019e554b0bb63d3f9cc5debda6224b4bf9bd",
                    phone2: "a29d98f8d4eb4661908ebab9c088b784aeed918d0fd349c998",
                    erpCode: "4cf7ac57d8ed488cb941",
                    license: "f9a33c99c86d4a829fc622d475d041b43de44373d1a44dd596",
                    taxCode: "ea1c1eff545941a499cb8939f80d1b0a0acbf753bd3e4a6b91",
                    vatName: "fde739434fc7428caef1d2b28db598e434b3508b06d34ebd97aa43b52158e09fa37b200e6b974857806ddee3164a11470854ab76c6e54e8db4e417ac36c5a63a9af43bfb78d24aaca0211ec6a502f747d03e01ccc9324457965b3dd49b826c9f584d2ad6a7534d34a0f962d5110d9683ca5f94db366444e3b2fb2644ea82794",
                    vatAddress: "dbd8392f899040188a7818340c1b7f14554aab0f1430497db499b073fce6fe04839dd2ca11054195917dcbe884ab3682a87ccdb51aa148169b3393a874c984bc91da6f914f9e4f45befb46ebd30f35e10d1ad89743ce4efaabc2ac9741c3857599802e1aff324017b3e22d84242bdf77fa8a1ae66c684138ac6d8cf5bf1e10d9925f3a434fd6440caa4da377b246b591d5e50bab508c4f7f8474c5e22fc2f292500a64b1a5c542dd8e9a7e2641c801e9c7a4b2dad6b647968ff755ff71aae5e2f6b17320aab544b6b300a95fa2e99865659930200a02404daba3e87d78ccb6eff51aae618b43414e999238e043585999953272b676c14f3aa2632923006900c85c986b6f0fc543de9700cce2c9a58210b1d1cf56bb1b4a47a46c0e84156355d4f0b8eb17a4ed467f9ece1f6d50bdee45ec2696110c6a4b19bed8cd570a789e0715d97255edaf47a0bf8635bf494e74b699baa6627bb743b6bf13bdcc9e58b79addffb52c71864d3a9ec480e821ea7570dcaf4877d92c461aade7807c94c70089f1ff612983334f70b5f65166c045126e1864aa9d67674edaae6018f2ab74833ee8aac44e7b29490bace2009f852a1dd5796a58abdf8b4304b5b91d9f3a64bc98a6228844a997491981e1ad24f2705dee1f9bb4b611e24c62943d67ef19262d53863e89f3494d4c82863863b8c0479a3e09434ba3",
                    active: true,
                    isCompany: true,
                    warehouseId: Guid.Parse("fa00dce0-e8ac-4e26-b317-00ae19f68c28"),
                    street: "22f48a6f7849457ebfb5a0923a0f1cafd45d0eb53ce9477a9b854fb32f1a2a09bfe8d116f26b4679ac28dacc520243003dad68bd1b2f4fa48d887b3f0c27110f9b2709db388e44ca93ebfcfbd8c89fd11763c70b8feb4daaac6b23dcc2ceb3cbedf2f30c93e14669854ed5b5e1ec42c09faac56b3d1349d2a663669d198a2d7",
                    address: "beaa5963d53040129bd878fab015449fb2a4b7f3b3004ab983ac74a41703bb480125bfecf6e84284879ad15cd5d0a012b92907a09c80481b896c7d015e987ba215afa039a6e0452b98a941f90f6c14d095c7f1c2d6c9407ca294a45c5116fbc080521bae043e402a9c5a4e03b9884c71d08700b92dcf4b068496e5db339506128a93c2fa6dd64286b0a9e73ad88a17a0580019dcf90941ffa45c919067ad5953c106b36473c24fd5b79e8d12e32d5b1503a499612c7d400a8cc72d562b742df6fdbe40c1251743b8ac78101956403b13ccdcfde12e38433d9dc78182be3062155ac8334e829e45fd993c55c7795fa85147e8d90f3b784dbd8f2f",
                    latitude: "6ebefd81ccff4d9fa3d28dd439cc8ce05aaca28821a9469e9354d7c23a7d2d1ea86d9e656d8241d1807d5f68a871bdb5f637cd2389e24bfb9d7317288c0072f4ed57c77f24f94c18a5f12e5311f6ced15dcb7b5bd9e74645bc70c86eb27d8b3ff259be811d2b4225aae37fa5fbe06e864b45c3de66384adda5cc2259795c478",
                    longitude: "23583a1439f5404a9842317900e7a5cca80c0bf1e89b4d4c9da97b19050596161f1841ab89e444559d587f0a08ca940848def49debc043d2bee4cedf3069a22a285f201263d944f2be397b9f031e5c197a86e4cd40454cb8b6e690fe75125aa4191417bd9bea4085b32dce6ed727fee1fd925aed07d24b46b7ba66a59a9cd54",
                    sfaCustomerCode: "57dc860c68fa4df9b4c8"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("ce6d421c-4cde-493f-b12f-e6fa307128be"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerRepository.GetCountAsync(
                    code: "907c791148fe46bd8db3",
                    name: "98f3db8389a6406fbe897e78cbbde7d8ed8651de7d274174a53ece8deb49f71b59d49ed96fce4b7ea9005631c8ee6e24b4ebe580807e4ba2a93ebaf9954c35371ece3305b8614b6eb800ef7011c026bdbbb8a8e4f672451e8e8ba27dd54ec29f0495ca3b295542a2bcde36e5c6c290b5db69674961ea4447a3727cd7b75f9ff",
                    phone1: "07bb012f345742d3af8519abb87a148bef4296f5e64e46a08f",
                    phone2: "3234b2cb239b4c0abd085a7914eaf5f40932ba1cf555426393",
                    erpCode: "0f84513f53b34e38bcc1",
                    license: "08f5b03ab52f4b05ba55e3ab72645492dd2e9fa4625b4b0e8e",
                    taxCode: "df51f60a89e644749b47b439597ea39a7a05a127e367429386",
                    vatName: "f81b67255c4e4de3bf5757435f95309c3da5f8ccce474f0e95e07a620739e96ff08e8f07f37a44ec90bcdaf62bee63214c0cffcfbc084c168b4b376b853ac1d4bd6960f85f2744d0b2886e97693af09d9287f06543fc4642aa5225f80d3574aea520a7a4301a466e96fb655a195c76e12f703cb3b0a54123b6e616011c3d0a0",
                    vatAddress: "3fbed23d53164622b2715988ea511e0127d8354e87ec4699bed2f342a4abae4c6b3f6dd7967646a8804a1b66d5c9f83fab2d221c3f2343768d2bfef56a45c27163cfabacaf2747bea7700af1a6c71b5af82e48a9fa1a4e6ebd34bb491fcf22a2adbdf077326246769034683095a2a224270dc175f80f42fb9a0b94620b133dc33c0aca61d37d4fcdbe67d8ef868e365af86da6b06e194de4b96afa01e320e36969eac51120d84dbabf3beaaac13e54e2d27798776bd2444fb5d580aa94d5431bc506e048eefc41018e2f7f8215ed5696df43ee1129ac42b2baa65d09992b15784688925c6e4144cab103d58df21cf043df19449e3ffe43908a9280aade188dffcdd91daa90234c4785675a60b9ef6933f0fad026eb884f488adc63217784c2d5aef9cb2f0255421199e35373cb70048a7a3b3590bcf34ef5b78805f03a6d2ec8bfb7f340f5be499e8282c952515327b70c0d2b6f8f594ebcaaf76e1dcc7a22092b215ac695724588a61b4c6baef250fdcd76a2ba6dba446ba28e36afdaa9eedce48afbd61c1e4dcfa1aec71ac852e8eeb086cbd9a6fb4158900f5cbb2055201259c2c6681dca4932bd002a67c988b399c4e1c19f29e04c388d2ce142ae39c81536db4931fc284974acf16c713165c026e5fdab46f859416182ad3ac78db9e5899cd2f7c5f41a40a4bac57ec32d3fa99cb4abb55e",
                    active: true,
                    isCompany: true,
                    warehouseId: Guid.Parse("613da3e9-d992-4457-9f98-684316a0b479"),
                    street: "82f59ced853f4b6194b1fc424e74e278c9799cfc6b354d9db102db4cd701b3ac97eb5bf08205472ea1dd59ff393fa459f773f62e0d0a40cb9ddb166a8a6c0b80356f762ad47240b98ea6ee9a68f4a722c663d08ee5474341ab2f5bce68b8d3fe290c0fa253ae419c94382ebc2ef29170511fd5317ae1451bb4e5e89dc227a8d",
                    address: "06c2e118f5cc4e46b22435e6faf4741df4f850aeee2a4a9aafccd78d4b67b6e07ca0b94bf03c449bb92862a7990a5c81f784709cd70948a5af6f5253313dc8e1f7b08012c05143329d72f9643a7ce0b9a708193fe7f24b44b04efcba76efe36825348bdc2fc14862b1399b2aaf76bdd0d0ff16e56e7b46dd9988ebe8c2f459231c90cd0c666d4eec9ec775a475aba060bb18697560f9402bae9c12f393a050207458e11e7eb6470fb7f516ce62cc5ad5678b5347cea24f7f8d4b75161c6aba6d9c59e698546947be89af870a878832bb10911e06a33f46e1ac01e4a5451ec1287bd1ed535d7844b286e245e555e118176754cda0b48042d7abed",
                    latitude: "30844e31d05941b9b432669cdd4dcc7b7e85ac7b93ab42b7b13c296155b6e440f221fb0bbdb3403785852921cce100abb50e8b260a574085a66b684d50c3e28c2c94269bcf1e4df09ab57e1d6dc1ff411b6dbcffa6a949e39e296f7c990d9751d4ea31574e554988a8679f7f25ba8d1f83fdf4f75117413a826fe9022bc7bed",
                    longitude: "013d6095e9594269acfa1ec98a92ec53178cf8bd64fa4225bd9b5a585a6889ef6ce601bd64a84d878c0ed620ff8d1d220a34db21e2c44e3cbdd64cd5694ed95ff67f42ab56c14c399ff780df802c4d2632fdaf6a5328487dad3b9c0b53f904d1cf63adf550ee470f886c19c06eb7aa3fe19e671d81ea4ce68f241b55841d7dc",
                    sfaCustomerCode: "209eab4ae58842c4b81c"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}