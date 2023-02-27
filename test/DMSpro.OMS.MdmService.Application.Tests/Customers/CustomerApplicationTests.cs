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
            result.Items.Any(x => x.Customer.Id == Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51")).ShouldBe(true);
            result.Items.Any(x => x.Customer.Id == Guid.Parse("0fe1132a-a470-49af-976c-0132f54e3aa7")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customersAppService.GetAsync(Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerCreateDto
            {
                Code = "6bdf9666db5845caad8a",
                Name = "50761a01bc244078aa3d3f57a0e3229a06acee5d518e475da691edfb8823c6ec707a9f052b7c4c5c9697394964221b59798ddacaed234b0db6e09e7459bf44f336dc9422a18f4208acb84f100ded54633b8d3dbcf7dc40a0b3b83df9c7074aa5d455004cbebe4c1cbd84f37ba00cefedd6b9b10fdbbc47f7905ecf3fdd3631d",
                Phone1 = "78fe3acabba54806b7d50556440d988ea7fe73684a964f91b3",
                Phone2 = "8e4946e60aa24dfca4e25eccc9b49e5fe311170ec8d9496a89",
                erpCode = "b1e5a740bcfc46838108",
                License = "010005bb4c214179b9077dd696cbb3823a362f0c26ef49bb92",
                TaxCode = "72950fada06a4c25a84317e210d764d6d11573d958b04feb92",
                vatName = "97de4d57672a4f2cbeb6112d98563d5d396c86739c46446fa61a0fd19a388c69b2ec8008a7f74cb8ae7002329fb32f3aae180ea63ffe40d2b897723d8ee7d3cb05f219a7db78455a8938fa12c6b5984ca56678f4c3e14b1fa4aa57dc2e1cdd43233c7ac978f44bbb9dd0b9dfeb80a28ed777032644a94110adb54fc57fa1855",
                vatAddress = "3c6141cb498047d5b53ca1019c642827bf86871c16cc4eeea9d441e8d280a9f0ae88093582e843c99c65680dedf14bbdd470cea965d048d08b20a93d08d7d950c2e12e84600447baad3bab28dc25e984f22d06982a41468f91ccc7228819ea5fbd96be6fef5f40e3baac58165105a56149347fc3babf4fabb031e94a5a58e91183ea5554ebc24708b3930a4c9043dff74c4b40b274454e55914f0a9cc644bfa7fac20ae5168b4aa89068fac9c71ce967bc04ad5f3b3243dfa67b534b052652f3edd05ae26da94716ba246f1ffc17176297cef746c22044c6849f36009fc443170c3a9e32406a490fac7483a63a491b525e72bfdd5d4f43a6ad623b4fc863a6fb186dba95d5ff48b9875607a626b11106b8379d0256bd49edafe3a5d1ded54ead8a1958afbb054b93b85f65d60ffae84cd553cc0d118b4b0fabdfb29ffcc5b2be79689c86bf5b49438e51c00a6df440c396a4f291859e4de9b1255709db7022a421fb3c57691844fc9760f81e2f960ca2c0f2edb62bb94dbcabecb3e8af4373becccbb4625d4345eba6c8af48c8c176080098451f666c4881a795a89ee0a9567ee212b338b6104eafa2a643afc24bed9bcc514e4f78a6436c997d7928f1445c5e3aa429134b6d48f9be8ae03fb2ce087ed9f4c847841c4328bb9a947c475f44f7ca64e7cd25864ea8a1426f167ca8b369066c009c",
                Active = true,
                EffectiveDate = new DateTime(2016, 6, 22),
                EndDate = new DateTime(2015, 6, 1),
                CreditLimit = 683669612,
                IsCompany = true,
                WarehouseId = Guid.Parse("d892180b-315d-4bc6-8fce-f14ab555eb8e"),
                Street = "f7ea1e52b27f4808a008dec10aebcd8f27aa4b447a494fb1a1c26fdcef2628fc5fa47c4055c2487f8bd002387c3590232c4230b165da49c6babe4d941cfa560e10621edb0fdc4bcd9b28e5976d0fb4663c08ee9608ab4846b5a31190ebcd56d2c9ac70ef2b7f491b8d01d257d37c5c245e9f9307c00a499a94d5ffc94599573",
                Address = "76157d5e155c484380698966f19838c28667d763281742a1b94c1c3b82666c5d8ada8bfec1844e1e971f9ec7a2bfc63b6fd9890669474d378ad6d12a8518a558976f12e7ed4b4d85879182764880f25d4a65ea6400be484a945c9305aad878ad093a439103e84338bc4c332609279890fd6f6d1143e9469aa9a30930d2c27846cbe2c4769d994113bd43eaa73574e979ca698f6aac4e4d5ba9cba2cb5ad5cfaebb4052d41acc42888c623c2a65470d04b73973636ed34ee0bed526432917b8dae943a142d90440cf97be036322b28125e7830fca63c447a1be19ccfcd01ae97a65c45186c0ed43dabf4a3cedff3fa9079c3b423796144076823b",
                Latitude = "95d5878c32ba4800853405f4800a257ccb17f3d30f69406083ff3a54bbd02e16f14d8c7dec8a4581a1838cd23da7c879765121c54ed6403dbda989bf4d937b6bc94486c89b8744449d2993f6425439e2406403bcfe1544f79245e7831cf09489212558c5654f42f8b454a398af9945ab761d82cc79304a7faadea231fadafa2",
                Longitude = "89ddb5ffd8da443a9bac2fe4567ecac4b506c0fb68db4af58f201e71c3920a74440961f829e14673a763958ad817561705c0a70fd4794093bf512a261acbd0fd57db39b273d5420ebdc6027c4289f25da551673e43eb466e9d779e2eae309302fb87636958ed44e897350cc06894ce923b0be10fdea84d3695a1f5921def030",
                SFACustomerCode = "6970747f0f564ac59590",
                LastOrderDate = new DateTime(2020, 2, 11),
                PriceListId = Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),

            };

            // Act
            var serviceResult = await _customersAppService.CreateAsync(input);

            // Assert
            var result = await _customerRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("6bdf9666db5845caad8a");
            result.Name.ShouldBe("50761a01bc244078aa3d3f57a0e3229a06acee5d518e475da691edfb8823c6ec707a9f052b7c4c5c9697394964221b59798ddacaed234b0db6e09e7459bf44f336dc9422a18f4208acb84f100ded54633b8d3dbcf7dc40a0b3b83df9c7074aa5d455004cbebe4c1cbd84f37ba00cefedd6b9b10fdbbc47f7905ecf3fdd3631d");
            result.Phone1.ShouldBe("78fe3acabba54806b7d50556440d988ea7fe73684a964f91b3");
            result.Phone2.ShouldBe("8e4946e60aa24dfca4e25eccc9b49e5fe311170ec8d9496a89");
            result.erpCode.ShouldBe("b1e5a740bcfc46838108");
            result.License.ShouldBe("010005bb4c214179b9077dd696cbb3823a362f0c26ef49bb92");
            result.TaxCode.ShouldBe("72950fada06a4c25a84317e210d764d6d11573d958b04feb92");
            result.vatName.ShouldBe("97de4d57672a4f2cbeb6112d98563d5d396c86739c46446fa61a0fd19a388c69b2ec8008a7f74cb8ae7002329fb32f3aae180ea63ffe40d2b897723d8ee7d3cb05f219a7db78455a8938fa12c6b5984ca56678f4c3e14b1fa4aa57dc2e1cdd43233c7ac978f44bbb9dd0b9dfeb80a28ed777032644a94110adb54fc57fa1855");
            result.vatAddress.ShouldBe("3c6141cb498047d5b53ca1019c642827bf86871c16cc4eeea9d441e8d280a9f0ae88093582e843c99c65680dedf14bbdd470cea965d048d08b20a93d08d7d950c2e12e84600447baad3bab28dc25e984f22d06982a41468f91ccc7228819ea5fbd96be6fef5f40e3baac58165105a56149347fc3babf4fabb031e94a5a58e91183ea5554ebc24708b3930a4c9043dff74c4b40b274454e55914f0a9cc644bfa7fac20ae5168b4aa89068fac9c71ce967bc04ad5f3b3243dfa67b534b052652f3edd05ae26da94716ba246f1ffc17176297cef746c22044c6849f36009fc443170c3a9e32406a490fac7483a63a491b525e72bfdd5d4f43a6ad623b4fc863a6fb186dba95d5ff48b9875607a626b11106b8379d0256bd49edafe3a5d1ded54ead8a1958afbb054b93b85f65d60ffae84cd553cc0d118b4b0fabdfb29ffcc5b2be79689c86bf5b49438e51c00a6df440c396a4f291859e4de9b1255709db7022a421fb3c57691844fc9760f81e2f960ca2c0f2edb62bb94dbcabecb3e8af4373becccbb4625d4345eba6c8af48c8c176080098451f666c4881a795a89ee0a9567ee212b338b6104eafa2a643afc24bed9bcc514e4f78a6436c997d7928f1445c5e3aa429134b6d48f9be8ae03fb2ce087ed9f4c847841c4328bb9a947c475f44f7ca64e7cd25864ea8a1426f167ca8b369066c009c");
            result.Active.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2016, 6, 22));
            result.EndDate.ShouldBe(new DateTime(2015, 6, 1));
            result.CreditLimit.ShouldBe(683669612);
            result.IsCompany.ShouldBe(true);
            result.WarehouseId.ShouldBe(Guid.Parse("d892180b-315d-4bc6-8fce-f14ab555eb8e"));
            result.Street.ShouldBe("f7ea1e52b27f4808a008dec10aebcd8f27aa4b447a494fb1a1c26fdcef2628fc5fa47c4055c2487f8bd002387c3590232c4230b165da49c6babe4d941cfa560e10621edb0fdc4bcd9b28e5976d0fb4663c08ee9608ab4846b5a31190ebcd56d2c9ac70ef2b7f491b8d01d257d37c5c245e9f9307c00a499a94d5ffc94599573");
            result.Address.ShouldBe("76157d5e155c484380698966f19838c28667d763281742a1b94c1c3b82666c5d8ada8bfec1844e1e971f9ec7a2bfc63b6fd9890669474d378ad6d12a8518a558976f12e7ed4b4d85879182764880f25d4a65ea6400be484a945c9305aad878ad093a439103e84338bc4c332609279890fd6f6d1143e9469aa9a30930d2c27846cbe2c4769d994113bd43eaa73574e979ca698f6aac4e4d5ba9cba2cb5ad5cfaebb4052d41acc42888c623c2a65470d04b73973636ed34ee0bed526432917b8dae943a142d90440cf97be036322b28125e7830fca63c447a1be19ccfcd01ae97a65c45186c0ed43dabf4a3cedff3fa9079c3b423796144076823b");
            result.Latitude.ShouldBe("95d5878c32ba4800853405f4800a257ccb17f3d30f69406083ff3a54bbd02e16f14d8c7dec8a4581a1838cd23da7c879765121c54ed6403dbda989bf4d937b6bc94486c89b8744449d2993f6425439e2406403bcfe1544f79245e7831cf09489212558c5654f42f8b454a398af9945ab761d82cc79304a7faadea231fadafa2");
            result.Longitude.ShouldBe("89ddb5ffd8da443a9bac2fe4567ecac4b506c0fb68db4af58f201e71c3920a74440961f829e14673a763958ad817561705c0a70fd4794093bf512a261acbd0fd57db39b273d5420ebdc6027c4289f25da551673e43eb466e9d779e2eae309302fb87636958ed44e897350cc06894ce923b0be10fdea84d3695a1f5921def030");
            result.SFACustomerCode.ShouldBe("6970747f0f564ac59590");
            result.LastOrderDate.ShouldBe(new DateTime(2020, 2, 11));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerUpdateDto()
            {
                Code = "0da992f1ceac414c9b8d",
                Name = "279b47b6f4c34fcbbd8171f082f1982a4388968a95514efca7d5df00ed78d700fc9be734cb10491a8c6e948628081491d8f1e799482546b89e49f0b978754b64f0030983ca254fd191a4d1f544f3191680022934e19a4b19bf7b97b7ff3ddcd136458ae359d34c029e84bd5adc935626087afaecd0084156a988dd841d460d7",
                Phone1 = "4edbfd0f53964050889b4cc967c0ffe74674a9b5760542889d",
                Phone2 = "4efe487f47284ae491382e354f6a0688d5fb3aac6c94463ea5",
                erpCode = "2adc030b7f394de5a972",
                License = "b7f7ae22906f4b9faf1a8503b132de149410cad80800459882",
                TaxCode = "8b4034ddd0744f5cbecc713fe72b8e58393d90eb53fb467fb5",
                vatName = "930d0eb76f694466a6459fcb2a95af404fa12e34133040cbaae8901f573d720c019fe6e869004864bc1c30a20fca99d7ebdd72759e964daaa2c7435b349f4c09e9eeeb30e9544727abfab36957d8e043b04e03786a2e444f9eb231fc1b391cab2fb92e43943742aeb191a4f5876b0171e175c5c0554543c5b8aa3757685eb4f",
                vatAddress = "fe73a99f16e946c7a664e1dc03ee6c42a60966b53b3243fdba464a1df6b593b9f4840d3d891f49eab80cde1bb8569bef1dcac96eefa9408da63d199c27189df7e3e0b56311164a39ba56bf3613e0294b85d49deaebbb4fc3a67de0ca963a2d16fa80b4bee29c4e5b847e3970ffb6647a276d765311cb4f4892d815ea1addc0cb87f33e52343d48ccac55530187bcb707c2a0c6e666294345b5d879eac0968cc46107dda3f0e24c43963b54c09db50d9ce6529031383f428d88ea4cf3bba67fb1b70a1974bbdd490b97a5c9debb4be6206d84936a5d944422a2bf46ba62cd1a524238dfd02c98403cba4576c1228c0ad0227a41e434ad40d4bbb00c7c91089a2f4a8a5bbc664e492dba991c346053aeec5d69967f204043c6800d31975b768db65554f354edd44a2baad7ae5bc439e4aadaaea1dc09bb4492819127c1a5da47bb4a2842c3d2b542ce96fdd4dc883bd084ddab65edc03c46a485dee934716f81dc9311d18eba6740a0a4dd151d0997c4fd530555a2d34945419f7ea014b14e6f37568775aedafa456ba19b98a01629818431e42c9b7ee64c96a49fa6ad15ce34dc97934c41b2684c10b091ca687760a4d00d6630806c774ba59567ea74fee067dbc412983b50c24d43af668264e1444dbbb22e714664cb4b8f831c52abca77dbe892c7b6398bb44de0905519490ffcecd32395d534",
                Active = true,
                EffectiveDate = new DateTime(2004, 3, 16),
                EndDate = new DateTime(2022, 9, 20),
                CreditLimit = 375231229,
                IsCompany = true,
                WarehouseId = Guid.Parse("90c413ac-16ba-425b-801b-20d5bd037401"),
                Street = "f13a29fd38c74441abdd1cf76bf8b1296cb3bc131752413ba61c6d06897f71d53572a6f7e863475b99669a391151e4452a0a2ba346ff48199c449bcbd9825271d184378dbbba421a807a3ea143938923078f67d7489c4f7ea6a601bc7aac109352dcafc700024e4e82c4efcb6e536bc8a7e3753f402447af863bc88cd5a2644",
                Address = "2eff402fcff040c8a850aa0d0c1ba263ea0edae2abb34248af122f1de5ed5cceadb9db92f13e4b6096092a69be2735e2b65b5183cb534d83b303c00783167ad31c59da6d49e64fdda520fe448fe6c119c0d9d5cbc9be446592ba8f7d3b86ade0a109d093795c4b59ac1a8a263076034dbcd5a3e666514f4298eb6260b789035f5627e8af0f964a709085b56799b24992a71036d211604467962235dc82276377d50f5878264d4c22bf9c5b29139f0d5cb02ea36baaa04c9bbec662b30ae99a34dc817c782b51449082a07f6db0d562ba371762948e364f2b82a1889b0534423e15f51a8d9a2044808b97f1bd188c26524e1e3d88033141df875c",
                Latitude = "900e36323b4b493ea908e7a5f37ab93b3ec9d7051833468a8180098d8f072da5789731ba108a479c881105ddc8630591ad46bb03751a41cd93875a8687072ef172fd3fb25d32435c9159567886fdc5b1c8b580e8cc92432f87c95fc87729a5b03b09d9baefc74c278a78dac4711185470627f147f0ff4a9e9aa2a8f70929e82",
                Longitude = "591c8dc78b4848ab966e30e3b59b52dd7c640a5e2d5a40d6b9877f8d8e08840123abbb6e5f8544c98ff7fb6bd9ae43645e8e6ceb99964a2faaf51dc818509cfdc4cd0673dcac42fcbe11100d88946a5be58a5a580eb444f4a39ae8bfa9fe3322a20c6ac269ce4298a978be4dc8d4bc85ef841ceeb5d04297a041f3737d3ba74",
                SFACustomerCode = "137c6a0651c44c3a9a9c",
                LastOrderDate = new DateTime(2001, 9, 10),
                PriceListId = Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),

            };

            // Act
            var serviceResult = await _customersAppService.UpdateAsync(Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51"), input);

            // Assert
            var result = await _customerRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("0da992f1ceac414c9b8d");
            result.Name.ShouldBe("279b47b6f4c34fcbbd8171f082f1982a4388968a95514efca7d5df00ed78d700fc9be734cb10491a8c6e948628081491d8f1e799482546b89e49f0b978754b64f0030983ca254fd191a4d1f544f3191680022934e19a4b19bf7b97b7ff3ddcd136458ae359d34c029e84bd5adc935626087afaecd0084156a988dd841d460d7");
            result.Phone1.ShouldBe("4edbfd0f53964050889b4cc967c0ffe74674a9b5760542889d");
            result.Phone2.ShouldBe("4efe487f47284ae491382e354f6a0688d5fb3aac6c94463ea5");
            result.erpCode.ShouldBe("2adc030b7f394de5a972");
            result.License.ShouldBe("b7f7ae22906f4b9faf1a8503b132de149410cad80800459882");
            result.TaxCode.ShouldBe("8b4034ddd0744f5cbecc713fe72b8e58393d90eb53fb467fb5");
            result.vatName.ShouldBe("930d0eb76f694466a6459fcb2a95af404fa12e34133040cbaae8901f573d720c019fe6e869004864bc1c30a20fca99d7ebdd72759e964daaa2c7435b349f4c09e9eeeb30e9544727abfab36957d8e043b04e03786a2e444f9eb231fc1b391cab2fb92e43943742aeb191a4f5876b0171e175c5c0554543c5b8aa3757685eb4f");
            result.vatAddress.ShouldBe("fe73a99f16e946c7a664e1dc03ee6c42a60966b53b3243fdba464a1df6b593b9f4840d3d891f49eab80cde1bb8569bef1dcac96eefa9408da63d199c27189df7e3e0b56311164a39ba56bf3613e0294b85d49deaebbb4fc3a67de0ca963a2d16fa80b4bee29c4e5b847e3970ffb6647a276d765311cb4f4892d815ea1addc0cb87f33e52343d48ccac55530187bcb707c2a0c6e666294345b5d879eac0968cc46107dda3f0e24c43963b54c09db50d9ce6529031383f428d88ea4cf3bba67fb1b70a1974bbdd490b97a5c9debb4be6206d84936a5d944422a2bf46ba62cd1a524238dfd02c98403cba4576c1228c0ad0227a41e434ad40d4bbb00c7c91089a2f4a8a5bbc664e492dba991c346053aeec5d69967f204043c6800d31975b768db65554f354edd44a2baad7ae5bc439e4aadaaea1dc09bb4492819127c1a5da47bb4a2842c3d2b542ce96fdd4dc883bd084ddab65edc03c46a485dee934716f81dc9311d18eba6740a0a4dd151d0997c4fd530555a2d34945419f7ea014b14e6f37568775aedafa456ba19b98a01629818431e42c9b7ee64c96a49fa6ad15ce34dc97934c41b2684c10b091ca687760a4d00d6630806c774ba59567ea74fee067dbc412983b50c24d43af668264e1444dbbb22e714664cb4b8f831c52abca77dbe892c7b6398bb44de0905519490ffcecd32395d534");
            result.Active.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2004, 3, 16));
            result.EndDate.ShouldBe(new DateTime(2022, 9, 20));
            result.CreditLimit.ShouldBe(375231229);
            result.IsCompany.ShouldBe(true);
            result.WarehouseId.ShouldBe(Guid.Parse("90c413ac-16ba-425b-801b-20d5bd037401"));
            result.Street.ShouldBe("f13a29fd38c74441abdd1cf76bf8b1296cb3bc131752413ba61c6d06897f71d53572a6f7e863475b99669a391151e4452a0a2ba346ff48199c449bcbd9825271d184378dbbba421a807a3ea143938923078f67d7489c4f7ea6a601bc7aac109352dcafc700024e4e82c4efcb6e536bc8a7e3753f402447af863bc88cd5a2644");
            result.Address.ShouldBe("2eff402fcff040c8a850aa0d0c1ba263ea0edae2abb34248af122f1de5ed5cceadb9db92f13e4b6096092a69be2735e2b65b5183cb534d83b303c00783167ad31c59da6d49e64fdda520fe448fe6c119c0d9d5cbc9be446592ba8f7d3b86ade0a109d093795c4b59ac1a8a263076034dbcd5a3e666514f4298eb6260b789035f5627e8af0f964a709085b56799b24992a71036d211604467962235dc82276377d50f5878264d4c22bf9c5b29139f0d5cb02ea36baaa04c9bbec662b30ae99a34dc817c782b51449082a07f6db0d562ba371762948e364f2b82a1889b0534423e15f51a8d9a2044808b97f1bd188c26524e1e3d88033141df875c");
            result.Latitude.ShouldBe("900e36323b4b493ea908e7a5f37ab93b3ec9d7051833468a8180098d8f072da5789731ba108a479c881105ddc8630591ad46bb03751a41cd93875a8687072ef172fd3fb25d32435c9159567886fdc5b1c8b580e8cc92432f87c95fc87729a5b03b09d9baefc74c278a78dac4711185470627f147f0ff4a9e9aa2a8f70929e82");
            result.Longitude.ShouldBe("591c8dc78b4848ab966e30e3b59b52dd7c640a5e2d5a40d6b9877f8d8e08840123abbb6e5f8544c98ff7fb6bd9ae43645e8e6ceb99964a2faaf51dc818509cfdc4cd0673dcac42fcbe11100d88946a5be58a5a580eb444f4a39ae8bfa9fe3322a20c6ac269ce4298a978be4dc8d4bc85ef841ceeb5d04297a041f3737d3ba74");
            result.SFACustomerCode.ShouldBe("137c6a0651c44c3a9a9c");
            result.LastOrderDate.ShouldBe(new DateTime(2001, 9, 10));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customersAppService.DeleteAsync(Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51"));

            // Assert
            var result = await _customerRepository.FindAsync(c => c.Id == Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51"));

            result.ShouldBeNull();
        }
    }
}