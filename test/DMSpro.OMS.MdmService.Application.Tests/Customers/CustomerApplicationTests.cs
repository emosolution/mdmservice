using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.Customers
{
    public class CustomersAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ICustomersAppService _customersAppService;
        private readonly IRepository<Customer, Guid> _customerRepository;

        public CustomersAppServiceTests()
        {
            _customersAppService = GetRequiredService<ICustomersAppService>();
            _customerRepository = GetRequiredService<IRepository<Customer, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _customersAppService.GetListAsync(new GetCustomersInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Customer.Id == Guid.Parse("ce6d421c-4cde-493f-b12f-e6fa307128be")).ShouldBe(true);
            result.Items.Any(x => x.Customer.Id == Guid.Parse("8565be3d-2c64-4899-97f6-f45fa01bdfb7")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customersAppService.GetAsync(Guid.Parse("ce6d421c-4cde-493f-b12f-e6fa307128be"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("ce6d421c-4cde-493f-b12f-e6fa307128be"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerCreateDto
            {
                Code = "1d245b30c7d14f2bbb08",
                Name = "0a463225f4e14d34b8d5cc2f5cd234306ba7b7d2d72c43d7953fabedaeef143b97da85ebe2644325a864eb880ed7a5b73fe521cd913547f381dbb37c81a48b9bd4f311eff8c34f9b96f12801174e183edc8db7721f1047d59568e72d011411a58c0d067dcf114a168d1934a42945e0463a1b0f86dffa4774a5f68ff812aac34",
                Phone1 = "b228c7e44e4d428fa0cb9965ab9017a6869d7d5702c94bb997",
                Phone2 = "533c45428e124dce85c8821a75670f513a8825cb310144fa92",
                erpCode = "08658c80b34740bb9796",
                License = "9ee37d9d0c1a42e99c6367df588a30d65a30311e75604f3ebc",
                TaxCode = "1186f13b2aa04b8db9d1e67bb8eed5610e12d978ca93442ca1",
                vatName = "6f845eb4a816472b94f5bfd6d0c9dc48fb6c825ce2db4a6ba23d9b379ade4456264747d4700441f591debc1defb588c6aa0e0fe799554f169da4006eeddd1f219a58601db9b845a9857612c568d7cd41dccc13545639473e855a98f70854f5d97e6db8dad2d641cfa7e513a1ffa45fb204f79a8a2b2f40b49522319ebaae17a",
                vatAddress = "2decf8942d1d40e68992023dcb2ad3cb3490ac3e28234fec87598abfff42a276a43b2b8e1c9d45e5b0bb851d7444da857a6bcc556bb64512add7d7e78d8a904e137b12575a89467b8ef4b0900e239008ef80429847d4478398e34adc8f46a9baae2b52d4daa24472a67d1926c166938cd5d150b1e8b0454089b9aa0c4e16f42dc2d85cdcc30c438a94ccd457f0e615c575d7e0378aa34f3e9b7dc132179fc853e95d3602a85c4c4a9b7c9618b6bf6707ff0fd0da173e457b9d972626d0ce2ea9a300d0184ae24227a9cacc7c7c60ebebc6da02867a8c444ba6c0248e4635f26ee09ec474f52e42ada99f7926720d40ac241cf9e167b04b81a5034151b8292b57a1911e1c25a64917a02e26882c84d12c93b63a56950e4c5281c323a652f3f8322153b7b339aa4d098e7b0780ef59132ac99e964184f544a1b6e04809f76060b58ade7bedf3344625873a5d13714fbabd0ad8be860b644c73a06cf76130eed126d07bbc8c57cb494b8ded47e4ad5596fb80676bdb0cd24e88b39938e8922e1ddf8995579e9b654a3abe336c342730c888faecfefcdbab4e4da389eb55628aaa99717255432e7446e2bfaf6b9c55073dcb8ce50b28881040c2bf83a8897ba31545984fc7b821384f5c9c125fd71cdbf81d55a3dae49f98422aa9dc24317103116ca5cf988b780846ceb9a660d4d32f647891bc9bed",
                Active = true,
                EffectiveDate = new DateTime(2020, 1, 25),
                EndDate = new DateTime(2005, 9, 27),
                CreditLimit = 1931096095,
                IsCompany = true,
                WarehouseId = Guid.Parse("2ec9c244-0289-47a5-acf5-ea83d602ddd7"),
                Street = "f46b7dc9935f412f9b3cdc5d656d95b5d40fbee810664575ae387b99b4c98e98ec416cee847e40559e720c77ca8d833dc030ecf4ad2142138bd8589e2a1d77521bb6930bccd349d784cb247a0b32a999ec4e00a60acd4891b25e0e922c7d021ce8b1340aa390460f8381332fe856e7a887efbd0c7e7f437bbdc30ad01448e57",
                Address = "b65697e8e8854c2ab5059426e907414c5b0aee2965034cdaab2477b59a58815895ba20ff94de407288613076e67de651994c4822d5f64e3d87c40e4ec9ec200dce062fe7eb5a4d29973b952eb30c87c95dabe0a7ac104397af09e2c5f45efaa6ff828d4f305941deae3e08c1c2b94ce76fdd85432f50418fb9b211f4bde1b1d99997f46af59940a6ac659eb5ba56335a051a854544cf458ab8f0271159cee78dbea96cb87aba4bdb8ad4178f53393db197667b04b3f5471a949fb4f1d44d9c6100ab9dfc2f4642cdb3725bb0c84bac70171aa6d01c3c4476b8e3eb8cda7ad7025104e23d04164da7a7301876e0f6afc906442560b8c44c20a0f6",
                Latitude = "6fd5764299ee4a77a77c80d01ec3d63b7cb1323cf6b74afbb5f9ab5e2dc72d52769fae6f28d74b2a901d8ce2f7d23b0bd27eae5d6c6944ae9ebc63600bfef40c7fe1c3fa2d364bd3923f628cb077c9e9176bd55fb8a7457c91d6263ab6bb46852fe3b23ceab04f6ba2f902fd4f3870dd87370129375c43d081b46cd49bfdd0a",
                Longitude = "d26df9ed0a384a32b0b2aed35d96692202ad58cfa541454cbf3f83348e3e310b28c14d00318c4bca9413d85b23419c2e5fc664d2b152464aa2841119f7e1c7fbfc6598b7bded4951b2fc1b875bad6636f5384ca9d5e1427d937b81f8cbdcfca2a5bd926fd4344c9ea0925d5725855adb4ff9ace469ff4ed7b9f0f26a6a4e2b7",
                SFACustomerCode = "2d08f51127d948f9911e",
                LastOrderDate = new DateTime(2008, 7, 10),
                PriceListId = Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),

            };

            // Act
            var serviceResult = await _customersAppService.CreateAsync(input);

            // Assert
            var result = await _customerRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("1d245b30c7d14f2bbb08");
            result.Name.ShouldBe("0a463225f4e14d34b8d5cc2f5cd234306ba7b7d2d72c43d7953fabedaeef143b97da85ebe2644325a864eb880ed7a5b73fe521cd913547f381dbb37c81a48b9bd4f311eff8c34f9b96f12801174e183edc8db7721f1047d59568e72d011411a58c0d067dcf114a168d1934a42945e0463a1b0f86dffa4774a5f68ff812aac34");
            result.Phone1.ShouldBe("b228c7e44e4d428fa0cb9965ab9017a6869d7d5702c94bb997");
            result.Phone2.ShouldBe("533c45428e124dce85c8821a75670f513a8825cb310144fa92");
            result.erpCode.ShouldBe("08658c80b34740bb9796");
            result.License.ShouldBe("9ee37d9d0c1a42e99c6367df588a30d65a30311e75604f3ebc");
            result.TaxCode.ShouldBe("1186f13b2aa04b8db9d1e67bb8eed5610e12d978ca93442ca1");
            result.vatName.ShouldBe("6f845eb4a816472b94f5bfd6d0c9dc48fb6c825ce2db4a6ba23d9b379ade4456264747d4700441f591debc1defb588c6aa0e0fe799554f169da4006eeddd1f219a58601db9b845a9857612c568d7cd41dccc13545639473e855a98f70854f5d97e6db8dad2d641cfa7e513a1ffa45fb204f79a8a2b2f40b49522319ebaae17a");
            result.vatAddress.ShouldBe("2decf8942d1d40e68992023dcb2ad3cb3490ac3e28234fec87598abfff42a276a43b2b8e1c9d45e5b0bb851d7444da857a6bcc556bb64512add7d7e78d8a904e137b12575a89467b8ef4b0900e239008ef80429847d4478398e34adc8f46a9baae2b52d4daa24472a67d1926c166938cd5d150b1e8b0454089b9aa0c4e16f42dc2d85cdcc30c438a94ccd457f0e615c575d7e0378aa34f3e9b7dc132179fc853e95d3602a85c4c4a9b7c9618b6bf6707ff0fd0da173e457b9d972626d0ce2ea9a300d0184ae24227a9cacc7c7c60ebebc6da02867a8c444ba6c0248e4635f26ee09ec474f52e42ada99f7926720d40ac241cf9e167b04b81a5034151b8292b57a1911e1c25a64917a02e26882c84d12c93b63a56950e4c5281c323a652f3f8322153b7b339aa4d098e7b0780ef59132ac99e964184f544a1b6e04809f76060b58ade7bedf3344625873a5d13714fbabd0ad8be860b644c73a06cf76130eed126d07bbc8c57cb494b8ded47e4ad5596fb80676bdb0cd24e88b39938e8922e1ddf8995579e9b654a3abe336c342730c888faecfefcdbab4e4da389eb55628aaa99717255432e7446e2bfaf6b9c55073dcb8ce50b28881040c2bf83a8897ba31545984fc7b821384f5c9c125fd71cdbf81d55a3dae49f98422aa9dc24317103116ca5cf988b780846ceb9a660d4d32f647891bc9bed");
            result.Active.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2020, 1, 25));
            result.EndDate.ShouldBe(new DateTime(2005, 9, 27));
            result.CreditLimit.ShouldBe(1931096095);
            result.IsCompany.ShouldBe(true);
            result.WarehouseId.ShouldBe(Guid.Parse("2ec9c244-0289-47a5-acf5-ea83d602ddd7"));
            result.Street.ShouldBe("f46b7dc9935f412f9b3cdc5d656d95b5d40fbee810664575ae387b99b4c98e98ec416cee847e40559e720c77ca8d833dc030ecf4ad2142138bd8589e2a1d77521bb6930bccd349d784cb247a0b32a999ec4e00a60acd4891b25e0e922c7d021ce8b1340aa390460f8381332fe856e7a887efbd0c7e7f437bbdc30ad01448e57");
            result.Address.ShouldBe("b65697e8e8854c2ab5059426e907414c5b0aee2965034cdaab2477b59a58815895ba20ff94de407288613076e67de651994c4822d5f64e3d87c40e4ec9ec200dce062fe7eb5a4d29973b952eb30c87c95dabe0a7ac104397af09e2c5f45efaa6ff828d4f305941deae3e08c1c2b94ce76fdd85432f50418fb9b211f4bde1b1d99997f46af59940a6ac659eb5ba56335a051a854544cf458ab8f0271159cee78dbea96cb87aba4bdb8ad4178f53393db197667b04b3f5471a949fb4f1d44d9c6100ab9dfc2f4642cdb3725bb0c84bac70171aa6d01c3c4476b8e3eb8cda7ad7025104e23d04164da7a7301876e0f6afc906442560b8c44c20a0f6");
            result.Latitude.ShouldBe("6fd5764299ee4a77a77c80d01ec3d63b7cb1323cf6b74afbb5f9ab5e2dc72d52769fae6f28d74b2a901d8ce2f7d23b0bd27eae5d6c6944ae9ebc63600bfef40c7fe1c3fa2d364bd3923f628cb077c9e9176bd55fb8a7457c91d6263ab6bb46852fe3b23ceab04f6ba2f902fd4f3870dd87370129375c43d081b46cd49bfdd0a");
            result.Longitude.ShouldBe("d26df9ed0a384a32b0b2aed35d96692202ad58cfa541454cbf3f83348e3e310b28c14d00318c4bca9413d85b23419c2e5fc664d2b152464aa2841119f7e1c7fbfc6598b7bded4951b2fc1b875bad6636f5384ca9d5e1427d937b81f8cbdcfca2a5bd926fd4344c9ea0925d5725855adb4ff9ace469ff4ed7b9f0f26a6a4e2b7");
            result.SFACustomerCode.ShouldBe("2d08f51127d948f9911e");
            result.LastOrderDate.ShouldBe(new DateTime(2008, 7, 10));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerUpdateDto()
            {
                Code = "d37a16d7f58a450aaeeb",
                Name = "4c6654c0f61c467593906ab22bae0a300b1ef845bd394b7b99850c8e185c2e14a274ac638e3f4593a0c1c1f6561e95cfaa292965e3e34e5897076dcd5d2aa7c60bfb21e7d8f5418fbbbe5b60db710b27bd84ca8873324e88be01b9bcfc029ba7d8b022e564324f2da5300ba0e0a11f606cec8209084342e7af2e943eb766fa9",
                Phone1 = "c920d56ff6674c71ac32c80c65659f17dade805d076d497b82",
                Phone2 = "a7d35c36a97547bda5104f242b58c7aeb4bc0b1380ef41da8c",
                erpCode = "0bf7c551fab843e8a47f",
                License = "234fb4330e53471e8da030b2fb42daecb522aaa89929483894",
                TaxCode = "de0a44bf76294de1856be4ffd7774acc02130761636c427eab",
                vatName = "d7be823f45664d598e01ff81cffedbe3229181ce38614ebe932828c99d2babb7e07896288acc4229a796c81561086eaec946d343854e4c1baed62ccbe345718e83ec7066c003456599cb48ff13328c0ef0fc76faecd5446c945b62fd1d1b298aaf3d8fcc8ba2465092d87f418698c514a9f10716d1734e2f868616f7f0ac0eb",
                vatAddress = "8a9d7cb59d9c4691b8725f73b4910e4f6a368a69c0194916b2e4cd0c84105c78c17e918fcc76471e874de414e8d7e094d07993a056ec422e95832a8a260051dfb00b2e5623824206b15ca4fe5b58b34b27683e7a3d574c7987a0307b556303552144b83c247046048b30a2c87b47d0b0f09731d3903a47708b80c65d5f39d2a99f03d6820d614ca6b724fcb42c5c809f787b66f2dfa5483d813da4bc787d322ce98229acbf33469d92012e6be52b4922f11b97d3d8bd4582887be0f96b8c6c032f0b0e5a59de407fb6cef75725199e9f3d6d8396f5784e9c82720ed1542acc42540f0524858e4c34b047916a2a5dad84a602480450a84bff83edafaaf33dcea7059cb363758148cface046afa57be1bcb4baa20af9a54214882b76399ed2d35eb2a7e72c42f54169a129e71ca9a6b18d7b1b37b7b3804df3b0db09a59f254d2a7bad405a049040e481c238f8f0270fa63ae4824abee84a60b687a38590a7cf5c1420d1f88a1c457cb1fa6584cf9d6e288f6016bb76024461a28a81a3a4cd64f4b94b746ed4254e1e900421fbeaf6043bd4e92d37d561446c9a39be4a7ab3d6ece20d0d505e284a0098ad47a3693ae5a910c82011655141aea96a18e2487120df2881da063dbf424b97df9337df437ca1a6d63832a7c84bae9bf88c594076c6b22073c057e40546899016839bcd8be09c4952852f",
                Active = true,
                EffectiveDate = new DateTime(2019, 7, 9),
                EndDate = new DateTime(2013, 2, 3),
                CreditLimit = 1791884550,
                IsCompany = true,
                WarehouseId = Guid.Parse("0874be63-d11f-4026-9fb9-5096b9432b55"),
                Street = "13c02942296f4f31afe312e43b1e8fb411bebf5668574e0793a7757ee60859f410250c657f05414d818bf30a17b9e4cc30347ac8e647447e81a8cffcacf6d9d70e1df02bf1ee42d2a625a45a08d11862ad43e635e41847d78c46f48f2ac5360fddc63b5fd06b430a8177fb6bde12f7766f775046f72b493e92719bd3e8f9198",
                Address = "4b47ff6c372b4b06bc77b15b75eeac4c3b46f4a9c4594e1e8f932bd588af3b028f945c9bdf8748329015962ad1fa62174b3c4a859d234cf8a3948b44c5a9021d346fa25f078b4a67a0124edb4ebf86daa09c7e30692e497a80dcf3ec5c67b310fa7686918b0a4da0ade9382822c58048703ee036368a4b2aa99d5ac3e6ff0e900575000a3cbf48b4983afad99e981980821ee2fe39e7449388afb21ffcba30f7ab0db4891fa6434783d342eefbb062a651d991a6ae4442feb021b67520eb9866668125408587431393dab2275732cb5b33d22db3f6cd4de59fff57e168528249aa5f99926a3049088c19e12b1c9d5ee77651998c5fd34e72aea3",
                Latitude = "407c3b208a1044118d652efb13757db3f4b628c17790484e83db1782b608855603bd5030a8bf4576bf6c0ecccc17da275caf33a2902b4b478b0984d511a9a2aa498aecb91862439dbeaac3ef9934fbffff69fd346691481ab2d968f43c98737e59549fbfdb114ca2aee796a18be42b5808352bc03ac9446d8d729b831dd4460",
                Longitude = "445b06fb7ade427fa466d9941c38ba57ad3d54703647460d9e139939ebd8797e88bff3a0b94c483a98e37251dc8c06b31e380c85b50f43a6920ed2847922085d23c98bc46f014b9abe3cdeb65db1fec94e5759a328254343b10bd0c6687f5991b865ab42c846448393baa00be446472d23528f8d498d402b9e2826892e8d293",
                SFACustomerCode = "d654f59994884929bb5a",
                LastOrderDate = new DateTime(2017, 1, 22),
                PriceListId = Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),

            };

            // Act
            var serviceResult = await _customersAppService.UpdateAsync(Guid.Parse("ce6d421c-4cde-493f-b12f-e6fa307128be"), input);

            // Assert
            var result = await _customerRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("d37a16d7f58a450aaeeb");
            result.Name.ShouldBe("4c6654c0f61c467593906ab22bae0a300b1ef845bd394b7b99850c8e185c2e14a274ac638e3f4593a0c1c1f6561e95cfaa292965e3e34e5897076dcd5d2aa7c60bfb21e7d8f5418fbbbe5b60db710b27bd84ca8873324e88be01b9bcfc029ba7d8b022e564324f2da5300ba0e0a11f606cec8209084342e7af2e943eb766fa9");
            result.Phone1.ShouldBe("c920d56ff6674c71ac32c80c65659f17dade805d076d497b82");
            result.Phone2.ShouldBe("a7d35c36a97547bda5104f242b58c7aeb4bc0b1380ef41da8c");
            result.erpCode.ShouldBe("0bf7c551fab843e8a47f");
            result.License.ShouldBe("234fb4330e53471e8da030b2fb42daecb522aaa89929483894");
            result.TaxCode.ShouldBe("de0a44bf76294de1856be4ffd7774acc02130761636c427eab");
            result.vatName.ShouldBe("d7be823f45664d598e01ff81cffedbe3229181ce38614ebe932828c99d2babb7e07896288acc4229a796c81561086eaec946d343854e4c1baed62ccbe345718e83ec7066c003456599cb48ff13328c0ef0fc76faecd5446c945b62fd1d1b298aaf3d8fcc8ba2465092d87f418698c514a9f10716d1734e2f868616f7f0ac0eb");
            result.vatAddress.ShouldBe("8a9d7cb59d9c4691b8725f73b4910e4f6a368a69c0194916b2e4cd0c84105c78c17e918fcc76471e874de414e8d7e094d07993a056ec422e95832a8a260051dfb00b2e5623824206b15ca4fe5b58b34b27683e7a3d574c7987a0307b556303552144b83c247046048b30a2c87b47d0b0f09731d3903a47708b80c65d5f39d2a99f03d6820d614ca6b724fcb42c5c809f787b66f2dfa5483d813da4bc787d322ce98229acbf33469d92012e6be52b4922f11b97d3d8bd4582887be0f96b8c6c032f0b0e5a59de407fb6cef75725199e9f3d6d8396f5784e9c82720ed1542acc42540f0524858e4c34b047916a2a5dad84a602480450a84bff83edafaaf33dcea7059cb363758148cface046afa57be1bcb4baa20af9a54214882b76399ed2d35eb2a7e72c42f54169a129e71ca9a6b18d7b1b37b7b3804df3b0db09a59f254d2a7bad405a049040e481c238f8f0270fa63ae4824abee84a60b687a38590a7cf5c1420d1f88a1c457cb1fa6584cf9d6e288f6016bb76024461a28a81a3a4cd64f4b94b746ed4254e1e900421fbeaf6043bd4e92d37d561446c9a39be4a7ab3d6ece20d0d505e284a0098ad47a3693ae5a910c82011655141aea96a18e2487120df2881da063dbf424b97df9337df437ca1a6d63832a7c84bae9bf88c594076c6b22073c057e40546899016839bcd8be09c4952852f");
            result.Active.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2019, 7, 9));
            result.EndDate.ShouldBe(new DateTime(2013, 2, 3));
            result.CreditLimit.ShouldBe(1791884550);
            result.IsCompany.ShouldBe(true);
            result.WarehouseId.ShouldBe(Guid.Parse("0874be63-d11f-4026-9fb9-5096b9432b55"));
            result.Street.ShouldBe("13c02942296f4f31afe312e43b1e8fb411bebf5668574e0793a7757ee60859f410250c657f05414d818bf30a17b9e4cc30347ac8e647447e81a8cffcacf6d9d70e1df02bf1ee42d2a625a45a08d11862ad43e635e41847d78c46f48f2ac5360fddc63b5fd06b430a8177fb6bde12f7766f775046f72b493e92719bd3e8f9198");
            result.Address.ShouldBe("4b47ff6c372b4b06bc77b15b75eeac4c3b46f4a9c4594e1e8f932bd588af3b028f945c9bdf8748329015962ad1fa62174b3c4a859d234cf8a3948b44c5a9021d346fa25f078b4a67a0124edb4ebf86daa09c7e30692e497a80dcf3ec5c67b310fa7686918b0a4da0ade9382822c58048703ee036368a4b2aa99d5ac3e6ff0e900575000a3cbf48b4983afad99e981980821ee2fe39e7449388afb21ffcba30f7ab0db4891fa6434783d342eefbb062a651d991a6ae4442feb021b67520eb9866668125408587431393dab2275732cb5b33d22db3f6cd4de59fff57e168528249aa5f99926a3049088c19e12b1c9d5ee77651998c5fd34e72aea3");
            result.Latitude.ShouldBe("407c3b208a1044118d652efb13757db3f4b628c17790484e83db1782b608855603bd5030a8bf4576bf6c0ecccc17da275caf33a2902b4b478b0984d511a9a2aa498aecb91862439dbeaac3ef9934fbffff69fd346691481ab2d968f43c98737e59549fbfdb114ca2aee796a18be42b5808352bc03ac9446d8d729b831dd4460");
            result.Longitude.ShouldBe("445b06fb7ade427fa466d9941c38ba57ad3d54703647460d9e139939ebd8797e88bff3a0b94c483a98e37251dc8c06b31e380c85b50f43a6920ed2847922085d23c98bc46f014b9abe3cdeb65db1fec94e5759a328254343b10bd0c6687f5991b865ab42c846448393baa00be446472d23528f8d498d402b9e2826892e8d293");
            result.SFACustomerCode.ShouldBe("d654f59994884929bb5a");
            result.LastOrderDate.ShouldBe(new DateTime(2017, 1, 22));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customersAppService.DeleteAsync(Guid.Parse("ce6d421c-4cde-493f-b12f-e6fa307128be"));

            // Assert
            var result = await _customerRepository.FindAsync(c => c.Id == Guid.Parse("ce6d421c-4cde-493f-b12f-e6fa307128be"));

            result.ShouldBeNull();
        }
    }
}