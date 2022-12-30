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
            result.Items.Any(x => x.Customer.Id == Guid.Parse("e9c6f102-3d37-46de-b979-3e039dd965dc")).ShouldBe(true);
            result.Items.Any(x => x.Customer.Id == Guid.Parse("78cc889b-a12c-4b3c-978e-182b7eda591d")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customersAppService.GetAsync(Guid.Parse("e9c6f102-3d37-46de-b979-3e039dd965dc"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("e9c6f102-3d37-46de-b979-3e039dd965dc"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerCreateDto
            {
                Code = "a28f63eed9c3416ebd36",
                Name = "0f4a0dabfae24833820c9ffcd838c46ae44e29fae5a94ef5a8b182cad87cfa6c9228326bf8e8427996a8f97b6e3ad2b6f2bca0efce6c4fb0906e2d2b6a0bac625d5ee6214cdc4322b6e5c457f67beba8f3772432bb6a42de9297295bb930e27d94efa89b7dba4053b5aab891aa428d0555014f34e9b941c0b5c41a48a10eaec",
                Phone1 = "23cffb369d35473a97612869a9ad8a839761ecca35d94304b4",
                Phone2 = "0e6ec99e7d8c43c78e4d023bacf9d8f0f912e17afc44432aa3",
                erpCode = "90295b8e27204b7a8e1a",
                License = "0027b7445dc042a2a9e474949ebd69cb032b510f88cc4b09bf",
                TaxCode = "c420dfbb7a2545b9b4a7e357b15a0fa3c1ff8c7a0cf240a6be",
                vatName = "3496efe80d0b4281b694a7dd3e111545404298f2e72e452996d240b8e5bf1adf982fcf4a32d04b5cb6e0c5d9fda37faa08bf1ce766244f87a6aa813f888dbe56771820eeafbe4781bd469f737d05d90bd08523a3f69d4fda934fab9791bb05c04f456e32138349f791d9142ca7e73c97a38b3d45678249f3a9ace05306dce90",
                vatAddress = "8e6d3f2fae0f4576ac72b8f343835a8410813579e66f4756b9bcd6679d22b7b420ff8f5285d54e68a448da89946f61541b055f5d4d7b48f9b9d04b759a1b67165b20b2f0de3942c49781989afb110a8c975c432d28134018a7c80537777c57f98c646d5c068d41c9b28c9102a2b3760b7bc84ec3b62c4f7388384a52086d1675bb8eefdec2194a0f855bd819c7748780c8fd86a22cae4ab2ba65431e5296cf7e61cfb328e11f4b3a9fef2af60c2543492b3b1f5e089341d2a8c5ba006310e35bd91595380c374bf28645df0c936b962a2ecf3a41c8284481bff6ccc4e46b3272aa6257397fc045428bcb80c03369fec7c15f523e468148fdaed90c463525c1a069576a1970044a88bfcbe618a08ab52ad7300dea52364acf88e13ab463ad4684234f015a2d9c4c83bc751f1309dec7196e2ede2a967d495899274addc32ddaefccfe976108d54f8baa40aa839e0782f1e77e5f23d781442fb3de26510a210d96dbc5dc8e170d4475bdfee5f1b8278706d8cc36bedf9643a2b6c2a92b6f2af51fcec89f43dcaf42ef9c42d279dd881d363e0f9ff769534ab99f0d0c0221b076a8047f10c03dff4913ab588e0502b662eb1efc582f01ba4086b0ba0b2399dfef0c3fb92acf3cc149928fbcf5137aac72d347cbbcd7a5a64a7eb82a02bf9203651344c8e1ae580548ecbb8d4c3757df4d54c28acbd5",
                Active = true,
                EffectiveDate = new DateTime(2015, 2, 5),
                EndDate = new DateTime(2016, 10, 25),
                CreditLimit = 116412938,
                IsCompany = true,
                WarehouseId = Guid.Parse("d40e8ba0-6c53-4bc5-82a2-8b99d03eb3c6"),
                Street = "b56598dd6eb943afa92abe3c435129a5000294ab7c4c45e8b1856972a8d5325e78a61524f6ae4e6692284cc8c3d284bd9c5ce6a31b7647618ece439a1592ebb48f961421f1024aef907bafee100ad61f8c09d97a061b41cf9e067a9da184c52f2b3f5b74afc84c7fb73c14c397d617cbcd2fd61e0c0b480eb6f2d1de09f7535",
                Address = "f003b6b10c31461eb57bace864682a9f17fd963cac434692857e110dfe0bc92df65f9e95d05d409cb78436c111b5ebe6221a0638775c47b0ac3cb0ecfd89011b28775c35372c4ddeb48587af1fa67739cf82805c23e24f6984053471930b09c8ec975354a47149ddbac599b9b4e555b5ede4b72b9c124ea098c1cacd407c59abd94bee441fbb4306806a9e9c67d8382a35c35a9444444914942550e9a32ec7e2d36c71ea44434ec7845eb955460d757658ca84c76cdf421397677bac46b5aba500a3e2a2e3f54cab9c583d64cc42ef8d802f416a9482440bab98194247e3cb56ed4bb1f7ba134a85be3913035b33d80fc605f924fd1c43578d17",
                Latitude = "e72c8ecc344041b0afb8ea1811c3d3edd7be52f0a1d9486db8f4c75dd0250afd62debf17e25941c9aea8413a57cf43104b00b5e5f21a4f0788b2d05f502a10657fc0d90d8db24ddbb2a44b1cd59459525ce8b656be164150bc6f7448d092aefc865fc250dd5243db8bb622b440f77199dbdb72f89f2f458d8fe5b0e43312d62",
                Longitude = "8e5e0e30165a4859a752609a9a0bbd35c05976fb41ab4abc9ad2da70329de778375b72f168e844ec8dfcc48f7112e98693b1815a94c149f9b5efb9057830d4891917cc45b0214b92909dcc3dec7bc63c98e76192735540de901b1208a8d7f84b0e84286278234918b42ab31fa31231082d1bb785d4fc4ee184631c5c9fddd9b",
                SFACustomerCode = "e44eafe5a7e94cd98b4b",
                LastOrderDate = new DateTime(2018, 4, 23),
                PriceListId = Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),

            };

            // Act
            var serviceResult = await _customersAppService.CreateAsync(input);

            // Assert
            var result = await _customerRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("a28f63eed9c3416ebd36");
            result.Name.ShouldBe("0f4a0dabfae24833820c9ffcd838c46ae44e29fae5a94ef5a8b182cad87cfa6c9228326bf8e8427996a8f97b6e3ad2b6f2bca0efce6c4fb0906e2d2b6a0bac625d5ee6214cdc4322b6e5c457f67beba8f3772432bb6a42de9297295bb930e27d94efa89b7dba4053b5aab891aa428d0555014f34e9b941c0b5c41a48a10eaec");
            result.Phone1.ShouldBe("23cffb369d35473a97612869a9ad8a839761ecca35d94304b4");
            result.Phone2.ShouldBe("0e6ec99e7d8c43c78e4d023bacf9d8f0f912e17afc44432aa3");
            result.erpCode.ShouldBe("90295b8e27204b7a8e1a");
            result.License.ShouldBe("0027b7445dc042a2a9e474949ebd69cb032b510f88cc4b09bf");
            result.TaxCode.ShouldBe("c420dfbb7a2545b9b4a7e357b15a0fa3c1ff8c7a0cf240a6be");
            result.vatName.ShouldBe("3496efe80d0b4281b694a7dd3e111545404298f2e72e452996d240b8e5bf1adf982fcf4a32d04b5cb6e0c5d9fda37faa08bf1ce766244f87a6aa813f888dbe56771820eeafbe4781bd469f737d05d90bd08523a3f69d4fda934fab9791bb05c04f456e32138349f791d9142ca7e73c97a38b3d45678249f3a9ace05306dce90");
            result.vatAddress.ShouldBe("8e6d3f2fae0f4576ac72b8f343835a8410813579e66f4756b9bcd6679d22b7b420ff8f5285d54e68a448da89946f61541b055f5d4d7b48f9b9d04b759a1b67165b20b2f0de3942c49781989afb110a8c975c432d28134018a7c80537777c57f98c646d5c068d41c9b28c9102a2b3760b7bc84ec3b62c4f7388384a52086d1675bb8eefdec2194a0f855bd819c7748780c8fd86a22cae4ab2ba65431e5296cf7e61cfb328e11f4b3a9fef2af60c2543492b3b1f5e089341d2a8c5ba006310e35bd91595380c374bf28645df0c936b962a2ecf3a41c8284481bff6ccc4e46b3272aa6257397fc045428bcb80c03369fec7c15f523e468148fdaed90c463525c1a069576a1970044a88bfcbe618a08ab52ad7300dea52364acf88e13ab463ad4684234f015a2d9c4c83bc751f1309dec7196e2ede2a967d495899274addc32ddaefccfe976108d54f8baa40aa839e0782f1e77e5f23d781442fb3de26510a210d96dbc5dc8e170d4475bdfee5f1b8278706d8cc36bedf9643a2b6c2a92b6f2af51fcec89f43dcaf42ef9c42d279dd881d363e0f9ff769534ab99f0d0c0221b076a8047f10c03dff4913ab588e0502b662eb1efc582f01ba4086b0ba0b2399dfef0c3fb92acf3cc149928fbcf5137aac72d347cbbcd7a5a64a7eb82a02bf9203651344c8e1ae580548ecbb8d4c3757df4d54c28acbd5");
            result.Active.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2015, 2, 5));
            result.EndDate.ShouldBe(new DateTime(2016, 10, 25));
            result.CreditLimit.ShouldBe(116412938);
            result.IsCompany.ShouldBe(true);
            result.WarehouseId.ShouldBe(Guid.Parse("d40e8ba0-6c53-4bc5-82a2-8b99d03eb3c6"));
            result.Street.ShouldBe("b56598dd6eb943afa92abe3c435129a5000294ab7c4c45e8b1856972a8d5325e78a61524f6ae4e6692284cc8c3d284bd9c5ce6a31b7647618ece439a1592ebb48f961421f1024aef907bafee100ad61f8c09d97a061b41cf9e067a9da184c52f2b3f5b74afc84c7fb73c14c397d617cbcd2fd61e0c0b480eb6f2d1de09f7535");
            result.Address.ShouldBe("f003b6b10c31461eb57bace864682a9f17fd963cac434692857e110dfe0bc92df65f9e95d05d409cb78436c111b5ebe6221a0638775c47b0ac3cb0ecfd89011b28775c35372c4ddeb48587af1fa67739cf82805c23e24f6984053471930b09c8ec975354a47149ddbac599b9b4e555b5ede4b72b9c124ea098c1cacd407c59abd94bee441fbb4306806a9e9c67d8382a35c35a9444444914942550e9a32ec7e2d36c71ea44434ec7845eb955460d757658ca84c76cdf421397677bac46b5aba500a3e2a2e3f54cab9c583d64cc42ef8d802f416a9482440bab98194247e3cb56ed4bb1f7ba134a85be3913035b33d80fc605f924fd1c43578d17");
            result.Latitude.ShouldBe("e72c8ecc344041b0afb8ea1811c3d3edd7be52f0a1d9486db8f4c75dd0250afd62debf17e25941c9aea8413a57cf43104b00b5e5f21a4f0788b2d05f502a10657fc0d90d8db24ddbb2a44b1cd59459525ce8b656be164150bc6f7448d092aefc865fc250dd5243db8bb622b440f77199dbdb72f89f2f458d8fe5b0e43312d62");
            result.Longitude.ShouldBe("8e5e0e30165a4859a752609a9a0bbd35c05976fb41ab4abc9ad2da70329de778375b72f168e844ec8dfcc48f7112e98693b1815a94c149f9b5efb9057830d4891917cc45b0214b92909dcc3dec7bc63c98e76192735540de901b1208a8d7f84b0e84286278234918b42ab31fa31231082d1bb785d4fc4ee184631c5c9fddd9b");
            result.SFACustomerCode.ShouldBe("e44eafe5a7e94cd98b4b");
            result.LastOrderDate.ShouldBe(new DateTime(2018, 4, 23));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerUpdateDto()
            {
                Code = "3567db6c02674f6e8a8a",
                Name = "c5255f157d944482b0f3c3769e8bf3c0f8c3160a656d4e209db0c4b8914dd3f42a6289a3afa3442796f3e77ac5ed7cb7b30d9df16866421e8bbc6f83e196301b4308e915346849fa852f9841f1b38584336ee3b478a046a284dd48460aad14662ddce20b65f14ad98f01a098c39b3cbba3a8783e051549338cc645b0abc770b",
                Phone1 = "9a87ca76eaff4e36b1cf7be96da067fbda0dcc305e8e4892a9",
                Phone2 = "3c2802db92e242fe997fdba6c4ed0c83f7ce89da02e343fc9f",
                erpCode = "c37ef3b9d97743ecb94b",
                License = "db60de05d3d1461d8fc9fb44499716ab6873cdf8336247d4aa",
                TaxCode = "f3d76ce7aab84027b767809e3a675959ba1884cf2fd244379a",
                vatName = "9472cfd4a3b144ac87328c3e51aecc31be5ff945bc9143729b62f49bcd7f1a09d5866101280a4df3ac6ff9e6e4d2449e545f204f02fa49519636034bbdeb569bf2d70ad647774f72a67059dcf376f5b008cdc06d44eb4eb2a4f5e1cb7687e1e9a4fba6c20fdc475284f10f02691d1a91d86ed8a0914a41769f90422dccc947c",
                vatAddress = "fa17bd2b11234eda96cf0b992c1f051ac8133892da7b410bb7f706165083085f1682075507ee4fbca6ada45024c58400d527691efacc46e8ac59d3f614ea704ff697b6d771fa411887d6c2251f6064aedd7529e410a14efbb14d62238bf1334fe7531e0be61c4953b3486ba5a0979aa5a2dda1e040d341e2a1b778b6054eecceaeee68f09abe4f40bee7a1bceadbecff283bd549161c40f2877009b3d9f73472d246056f2756494ebc8fec05f150b5d025562eee9c21412db6d015845ebb9a42d09896cba34c4e838e1336e17fb737ed3715c041c4824ca391d7dd5c7b7df7fe7a0dd51d8f774e41ba9a21b2c50f97868a6a21f4ca15430cbf9e14a366e3a03a5f4c1c30640d4052b8c9b8e442e72b15f09eb20e59a94c8392509881086c023064e5e0105dbf4295b5eb40a7e3efb1f3cbb157779ff24a74a6ea7b2ff051b8e09ef86e2ad06a42268042da3872c64c59b7049b50dccc447ca80c4cd76423a6a8288cffe8688644fca8ec5bc4c1fa41a99cfb16960856432eac0ec46977824eeeafb81a8c14a04886b6bcf232caaf42f58b5e21d1b37c4ff0aa3c6cd147c2a832f53ed5f598e04f77bb6fa267048d8ff235ecc6d341e247a8bc2e4ada9d1983a511ea47b513ee4fce9df9c1ff70b6679a0f9cc281bff9484981c1e361134f7a34c23c23339fef4a5e97c002c80e5df9b2b9a762c8",
                Active = true,
                EffectiveDate = new DateTime(2019, 9, 23),
                EndDate = new DateTime(2007, 11, 23),
                CreditLimit = 997715824,
                IsCompany = true,
                WarehouseId = Guid.Parse("cb0ee242-1528-46c4-b374-a1bc8b53f2f4"),
                Street = "2cf4e959dfd34bccbd6f7035c671d7bf8bceff01013044b0a7a4418328099ccfc49f365cdd424109847e0393c5c767a0cf0e8dc2998f4fc78224e4b5a2c14a271e62bc3f67b641b18cd94d39dba84335da30b2b7883e406bb41b93fccd5b08e81df6bf01cd5d4ed39e4392d8364bf95d901e606180f64f79bd034fc228be1ee",
                Address = "a05737ff5a884bb2946fbc81e626c61c0080f0929a574e7490edcc669d743f92bb41771c14f74a94907b59a7100d19d8ff14ef88c1aa4e61b31a32cacd1f1433b3c8c95f336a4a7d8134c6655142a5cfafd9b7a4254d414b8b9f3d2c7a14d08296fc3db55930413c946be345b6d8c490f0c1876e958c4021aac1340184e8d8f962de850cdd264fd48f5134bcadcadd228f1b78f9684c4c5681d4ec8eb4971824974e694bca474d8cbb9db0cbd9f32f835e512e9be8aa4f2ca16b446bc58bdd5419c983f11ad542efb78b85de3f1647b1dac1d32152c44b078b6426508634e42d9f9c127909894df2bda0349b4468c319b57fbaea63814e34b46c",
                Latitude = "25e53f7c8b2843fab5a0e295002c249062b607ef29d743678ee8b59a24b4e7e0053d10beef124d718d0060d9203076eea51e05c5d4854545a90310ad2987b4202a9d4f748cf24a8aba3a1b7f4d9f7d65c046d85c067f4b00b0e961a0e44165a8514cef881dc44065a609466a1518115922acfd1a21f94d45932709be874938d",
                Longitude = "540ca64b7cbe443fbc52511c74cee2157bf66d41b33141a7b110e329f92007220333d15e3cdb4831bde7ed38104ddad46701ec705ec549e98bfd9954d156b130b149fd566a1e46a092d8ec33c5168e68d7556cff061e4cca997ccdb7bd74808febb01d87b42042358be90374f59d106fe0f2420095354539b3bc5c573b5ed7d",
                SFACustomerCode = "f6c85e9cb0ab4e4a927b",
                LastOrderDate = new DateTime(2000, 6, 21),
                PriceListId = Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),

            };

            // Act
            var serviceResult = await _customersAppService.UpdateAsync(Guid.Parse("e9c6f102-3d37-46de-b979-3e039dd965dc"), input);

            // Assert
            var result = await _customerRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Code.ShouldBe("3567db6c02674f6e8a8a");
            result.Name.ShouldBe("c5255f157d944482b0f3c3769e8bf3c0f8c3160a656d4e209db0c4b8914dd3f42a6289a3afa3442796f3e77ac5ed7cb7b30d9df16866421e8bbc6f83e196301b4308e915346849fa852f9841f1b38584336ee3b478a046a284dd48460aad14662ddce20b65f14ad98f01a098c39b3cbba3a8783e051549338cc645b0abc770b");
            result.Phone1.ShouldBe("9a87ca76eaff4e36b1cf7be96da067fbda0dcc305e8e4892a9");
            result.Phone2.ShouldBe("3c2802db92e242fe997fdba6c4ed0c83f7ce89da02e343fc9f");
            result.erpCode.ShouldBe("c37ef3b9d97743ecb94b");
            result.License.ShouldBe("db60de05d3d1461d8fc9fb44499716ab6873cdf8336247d4aa");
            result.TaxCode.ShouldBe("f3d76ce7aab84027b767809e3a675959ba1884cf2fd244379a");
            result.vatName.ShouldBe("9472cfd4a3b144ac87328c3e51aecc31be5ff945bc9143729b62f49bcd7f1a09d5866101280a4df3ac6ff9e6e4d2449e545f204f02fa49519636034bbdeb569bf2d70ad647774f72a67059dcf376f5b008cdc06d44eb4eb2a4f5e1cb7687e1e9a4fba6c20fdc475284f10f02691d1a91d86ed8a0914a41769f90422dccc947c");
            result.vatAddress.ShouldBe("fa17bd2b11234eda96cf0b992c1f051ac8133892da7b410bb7f706165083085f1682075507ee4fbca6ada45024c58400d527691efacc46e8ac59d3f614ea704ff697b6d771fa411887d6c2251f6064aedd7529e410a14efbb14d62238bf1334fe7531e0be61c4953b3486ba5a0979aa5a2dda1e040d341e2a1b778b6054eecceaeee68f09abe4f40bee7a1bceadbecff283bd549161c40f2877009b3d9f73472d246056f2756494ebc8fec05f150b5d025562eee9c21412db6d015845ebb9a42d09896cba34c4e838e1336e17fb737ed3715c041c4824ca391d7dd5c7b7df7fe7a0dd51d8f774e41ba9a21b2c50f97868a6a21f4ca15430cbf9e14a366e3a03a5f4c1c30640d4052b8c9b8e442e72b15f09eb20e59a94c8392509881086c023064e5e0105dbf4295b5eb40a7e3efb1f3cbb157779ff24a74a6ea7b2ff051b8e09ef86e2ad06a42268042da3872c64c59b7049b50dccc447ca80c4cd76423a6a8288cffe8688644fca8ec5bc4c1fa41a99cfb16960856432eac0ec46977824eeeafb81a8c14a04886b6bcf232caaf42f58b5e21d1b37c4ff0aa3c6cd147c2a832f53ed5f598e04f77bb6fa267048d8ff235ecc6d341e247a8bc2e4ada9d1983a511ea47b513ee4fce9df9c1ff70b6679a0f9cc281bff9484981c1e361134f7a34c23c23339fef4a5e97c002c80e5df9b2b9a762c8");
            result.Active.ShouldBe(true);
            result.EffectiveDate.ShouldBe(new DateTime(2019, 9, 23));
            result.EndDate.ShouldBe(new DateTime(2007, 11, 23));
            result.CreditLimit.ShouldBe(997715824);
            result.IsCompany.ShouldBe(true);
            result.WarehouseId.ShouldBe(Guid.Parse("cb0ee242-1528-46c4-b374-a1bc8b53f2f4"));
            result.Street.ShouldBe("2cf4e959dfd34bccbd6f7035c671d7bf8bceff01013044b0a7a4418328099ccfc49f365cdd424109847e0393c5c767a0cf0e8dc2998f4fc78224e4b5a2c14a271e62bc3f67b641b18cd94d39dba84335da30b2b7883e406bb41b93fccd5b08e81df6bf01cd5d4ed39e4392d8364bf95d901e606180f64f79bd034fc228be1ee");
            result.Address.ShouldBe("a05737ff5a884bb2946fbc81e626c61c0080f0929a574e7490edcc669d743f92bb41771c14f74a94907b59a7100d19d8ff14ef88c1aa4e61b31a32cacd1f1433b3c8c95f336a4a7d8134c6655142a5cfafd9b7a4254d414b8b9f3d2c7a14d08296fc3db55930413c946be345b6d8c490f0c1876e958c4021aac1340184e8d8f962de850cdd264fd48f5134bcadcadd228f1b78f9684c4c5681d4ec8eb4971824974e694bca474d8cbb9db0cbd9f32f835e512e9be8aa4f2ca16b446bc58bdd5419c983f11ad542efb78b85de3f1647b1dac1d32152c44b078b6426508634e42d9f9c127909894df2bda0349b4468c319b57fbaea63814e34b46c");
            result.Latitude.ShouldBe("25e53f7c8b2843fab5a0e295002c249062b607ef29d743678ee8b59a24b4e7e0053d10beef124d718d0060d9203076eea51e05c5d4854545a90310ad2987b4202a9d4f748cf24a8aba3a1b7f4d9f7d65c046d85c067f4b00b0e961a0e44165a8514cef881dc44065a609466a1518115922acfd1a21f94d45932709be874938d");
            result.Longitude.ShouldBe("540ca64b7cbe443fbc52511c74cee2157bf66d41b33141a7b110e329f92007220333d15e3cdb4831bde7ed38104ddad46701ec705ec549e98bfd9954d156b130b149fd566a1e46a092d8ec33c5168e68d7556cff061e4cca997ccdb7bd74808febb01d87b42042358be90374f59d106fe0f2420095354539b3bc5c573b5ed7d");
            result.SFACustomerCode.ShouldBe("f6c85e9cb0ab4e4a927b");
            result.LastOrderDate.ShouldBe(new DateTime(2000, 6, 21));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customersAppService.DeleteAsync(Guid.Parse("e9c6f102-3d37-46de-b979-3e039dd965dc"));

            // Assert
            var result = await _customerRepository.FindAsync(c => c.Id == Guid.Parse("e9c6f102-3d37-46de-b979-3e039dd965dc"));

            result.ShouldBeNull();
        }
    }
}