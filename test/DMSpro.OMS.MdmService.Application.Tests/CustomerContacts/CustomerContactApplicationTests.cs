using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DMSpro.OMS.MdmService.CustomerContacts
{
    public class CustomerContactsAppServiceTests : MdmServiceApplicationTestBase
    {
        private readonly ICustomerContactsAppService _customerContactsAppService;
        private readonly IRepository<CustomerContact, Guid> _customerContactRepository;

        public CustomerContactsAppServiceTests()
        {
            _customerContactsAppService = GetRequiredService<ICustomerContactsAppService>();
            _customerContactRepository = GetRequiredService<IRepository<CustomerContact, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _customerContactsAppService.GetListAsync(new GetCustomerContactsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.CustomerContact.Id == Guid.Parse("bc44d94d-009d-4374-9e32-c6bdefb96e2f")).ShouldBe(true);
            result.Items.Any(x => x.CustomerContact.Id == Guid.Parse("0c40741d-3e33-4437-88ac-67c86e3bf12f")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _customerContactsAppService.GetAsync(Guid.Parse("bc44d94d-009d-4374-9e32-c6bdefb96e2f"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("bc44d94d-009d-4374-9e32-c6bdefb96e2f"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CustomerContactCreateDto
            {
                Title = default,
                FirstName = "9ae77094b85549f29a1a45dd495ba2a424d2af23808441219e9dca32ed1795e87db2a3ed171743a0aca97fc6e7927de5dfdf92224bdc4116959b26da91a9562c91babddfa7c34fae861592c4b8779b3c6888ef7b0cd44815bacbc0748600395d944cca31a4734a2fb249f88fee20967c6f43b6dab0164015b51465cd9067c79",
                LastName = "46a8d8696df742b8b082ab7e803a7197a760bbb383934381897b2ef3a146ac13cdf2fc2fc03f488489e9fa421de8381ea798a293376144a88e70917df4c500837f8e2d956b8c4e03838880b48f3949422346e12c8428488db1ab74f3d08278dcf54c86d66471459d92aa4d2129c1f083937f5bac7e4146b3a38411d09b2471d",
                Gender = default,
                DateOfBirth = new DateTime(2010, 4, 7),
                Phone = "e6a330e605a7432eb9423daf1228d28cf9e65893c0df458fae9c49783aba10e34522bd8ab08545c1919550f2df3024a0b1b0813f5ebe49b0a70354038ec6e6905eb128efe0c6421197e50c2bddef0ee029305943cb0f4b36aa5764e0f52e4441cb1ba7b5aa9d49d889cbf5d8e0c419eaf74c2fe02a5445cd83e571f6873a88c",
                Email = "c1f177287d814ec1aa440411ad00abfb37ae32e7680d492892aea638eb9b80a2a44d794512a1455aa6a7afa467e70102f4607d05e98b4f09b17d5e435f9b@426e827341fd48bebefae6a78dab7bb898fc2f8720dd4e6c8a19b66531e38a306cba6922c40546479fea48c60eb49c4d5d0ae397a2674dc99e1f36d55dc6.com",
                Address = "d26271bc738d4b5cba4b14acf7eae8bc179b2d4dfb4349eaafefc6d1157548d96ae0afbc03104c99912c3b4e6ebb791b8a4ca38221c94a22bf7e57dfba1dfaf75880a3586b6a4fc3b33963ab225f627e0d11b7634b374782a72508a8033941c1a930c475b5bd4a47ad7c71aff9eed920508cb83a693d4b89bac6633dffd6b954bbf0bc5c251e4021967b7bbcc0f17abcdaea4f2c156941ce9c01eecdd1d26e4e13f53124cf1a45a9b8f00b6f6fc4dc5768efa936ea5347b5a846a2e7b23d60f327db02da9bb847aa80c0a1eacf5a81e0e13ff30ade554a889f8289f27b2c0f0823cb62a07bb141e3873876521d3b850289a172138ace4e1cba85",
                IdentityNumber = "b25cd859ed274b62ae51ec7f14ce418ea04adaa68a904b00bbbff244bac5753b605fc46e0ac84f55ab6bcd5d0255d62fae9d1d48377b44f2a31abb17f9ac007b5c940185d07a445a8f7a1a5f12b601d7df84ad2fb3db4f84a9d0c14f7e13a3138f6a2e9a716e4d7c87a45d04761b135dbec0bcfb37714158ae7dd093609bbf3",
                BankName = "7c6246cb6fc14db8b0cf464d0d211f7cc8980c2909a44a869e74dd37563e98147055f39aa7624bb0b33753e2871da1dc5862c8744ff14acfa29f56711c9d8a17c77927e9d309454184b1eae90c5325d2c73c47188f364f6c89d61a4b5f22ca09c2428518fbce4ff7b0f0d784c98ad0cb7765e71c4ece471cb3d2a8a40719d9c",
                BankAccName = "6cdc368b2ed743b5964a9da57094343db69f32bb03644e26a21ac4f28f9c34db894eddb3eef04625addb313d0c25f266d570218f97744d2bbf78d517941a8337a3eedff2af0a402d820806b06a9a1672918b0451a22d48958b9a9fda6b8cdd48012a29e334a947b3badae90238efd23693ad87240eab4ce89f7a4256b255409",
                BankAccNumber = "178d510b3b624586a056a79860632bc13ed35c9c2c2147c48518f7c4b54590d776ff539938024d4d91589084c62847716f449391329f4c09aa335a77ef653517f21a5fef35b648cc931790ad8a6ac423807271c964704e2ab7fd015f08b6a9f546798c35995449929d277335827cef424f382d5ff7d34dcb810908f1357b9bf",
                CustomerId = Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51")
            };

            // Act
            var serviceResult = await _customerContactsAppService.CreateAsync(input);

            // Assert
            var result = await _customerContactRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Title.ShouldBe(default);
            result.FirstName.ShouldBe("9ae77094b85549f29a1a45dd495ba2a424d2af23808441219e9dca32ed1795e87db2a3ed171743a0aca97fc6e7927de5dfdf92224bdc4116959b26da91a9562c91babddfa7c34fae861592c4b8779b3c6888ef7b0cd44815bacbc0748600395d944cca31a4734a2fb249f88fee20967c6f43b6dab0164015b51465cd9067c79");
            result.LastName.ShouldBe("46a8d8696df742b8b082ab7e803a7197a760bbb383934381897b2ef3a146ac13cdf2fc2fc03f488489e9fa421de8381ea798a293376144a88e70917df4c500837f8e2d956b8c4e03838880b48f3949422346e12c8428488db1ab74f3d08278dcf54c86d66471459d92aa4d2129c1f083937f5bac7e4146b3a38411d09b2471d");
            result.Gender.ShouldBe(default);
            result.DateOfBirth.ShouldBe(new DateTime(2010, 4, 7));
            result.Phone.ShouldBe("e6a330e605a7432eb9423daf1228d28cf9e65893c0df458fae9c49783aba10e34522bd8ab08545c1919550f2df3024a0b1b0813f5ebe49b0a70354038ec6e6905eb128efe0c6421197e50c2bddef0ee029305943cb0f4b36aa5764e0f52e4441cb1ba7b5aa9d49d889cbf5d8e0c419eaf74c2fe02a5445cd83e571f6873a88c");
            result.Email.ShouldBe("c1f177287d814ec1aa440411ad00abfb37ae32e7680d492892aea638eb9b80a2a44d794512a1455aa6a7afa467e70102f4607d05e98b4f09b17d5e435f9b@426e827341fd48bebefae6a78dab7bb898fc2f8720dd4e6c8a19b66531e38a306cba6922c40546479fea48c60eb49c4d5d0ae397a2674dc99e1f36d55dc6.com");
            result.Address.ShouldBe("d26271bc738d4b5cba4b14acf7eae8bc179b2d4dfb4349eaafefc6d1157548d96ae0afbc03104c99912c3b4e6ebb791b8a4ca38221c94a22bf7e57dfba1dfaf75880a3586b6a4fc3b33963ab225f627e0d11b7634b374782a72508a8033941c1a930c475b5bd4a47ad7c71aff9eed920508cb83a693d4b89bac6633dffd6b954bbf0bc5c251e4021967b7bbcc0f17abcdaea4f2c156941ce9c01eecdd1d26e4e13f53124cf1a45a9b8f00b6f6fc4dc5768efa936ea5347b5a846a2e7b23d60f327db02da9bb847aa80c0a1eacf5a81e0e13ff30ade554a889f8289f27b2c0f0823cb62a07bb141e3873876521d3b850289a172138ace4e1cba85");
            result.IdentityNumber.ShouldBe("b25cd859ed274b62ae51ec7f14ce418ea04adaa68a904b00bbbff244bac5753b605fc46e0ac84f55ab6bcd5d0255d62fae9d1d48377b44f2a31abb17f9ac007b5c940185d07a445a8f7a1a5f12b601d7df84ad2fb3db4f84a9d0c14f7e13a3138f6a2e9a716e4d7c87a45d04761b135dbec0bcfb37714158ae7dd093609bbf3");
            result.BankName.ShouldBe("7c6246cb6fc14db8b0cf464d0d211f7cc8980c2909a44a869e74dd37563e98147055f39aa7624bb0b33753e2871da1dc5862c8744ff14acfa29f56711c9d8a17c77927e9d309454184b1eae90c5325d2c73c47188f364f6c89d61a4b5f22ca09c2428518fbce4ff7b0f0d784c98ad0cb7765e71c4ece471cb3d2a8a40719d9c");
            result.BankAccName.ShouldBe("6cdc368b2ed743b5964a9da57094343db69f32bb03644e26a21ac4f28f9c34db894eddb3eef04625addb313d0c25f266d570218f97744d2bbf78d517941a8337a3eedff2af0a402d820806b06a9a1672918b0451a22d48958b9a9fda6b8cdd48012a29e334a947b3badae90238efd23693ad87240eab4ce89f7a4256b255409");
            result.BankAccNumber.ShouldBe("178d510b3b624586a056a79860632bc13ed35c9c2c2147c48518f7c4b54590d776ff539938024d4d91589084c62847716f449391329f4c09aa335a77ef653517f21a5fef35b648cc931790ad8a6ac423807271c964704e2ab7fd015f08b6a9f546798c35995449929d277335827cef424f382d5ff7d34dcb810908f1357b9bf");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CustomerContactUpdateDto()
            {
                Title = default,
                FirstName = "ff905a0f24414d6b8a5c75bc374984410b3c9545cd7843fa93c836c3044d125bddb99bde8aab44c5a306ea7eab35087b2e3cc34082984d359e1d80a3845346fa98c3002858a64832ae3f3c73caf60b2442fbd00f6b884b1fa1574f631ef1b353aca8cf13ecc24884a380942fa6829e411d502dee17284d7583c837810496171",
                LastName = "2cb7c3c993df4cffa0e802dda29714b7cdeb44482461416caf02fbdfbcf95848587bf18b453b4ac48c5e7a3f032b7f0303fd11afbc754de18c8e9132c12f47291ac10dd3697a4f0e88b4ac74f70d11620baadf3a30de4e949667085cb87c57f0ed04cdd896764c0eacee517971e35707aa7233a6e7a340a5ac0ca5953a5642d",
                Gender = default,
                DateOfBirth = new DateTime(2018, 4, 23),
                Phone = "801a7dc28f894202b34b0c8eb261a627e42cfa9bc5a24e30bc279adce49cca8d180fcd88ebda4632a9238cdab8b5f7c09adca0bb87b74b0491dbbdef06bb6eea2c39966f7f644615a955793d0dd3d6702fc3238c69de4dcfb1a029cd6eb7e8c9011f5eef412b456eb8ea5297dc7adc3ad50816db18b44fac88c0b4d9a4c30d8",
                Email = "dc68523112a54a348666d513b9c9adcb813b0933c377423e8e6e24181e5358f5625d9174ddca471a9ce1a0d6409bfc5b6f7fc4f278614a12b1cc7e4b3a30@1b72fd7d81094df1a0fb1528051fc0804badcbe5c1c248ceaffc4ecd6200468b994d3de1d93240e490712e0ef0b6b6111245e678b2b145358aa14aade486.com",
                Address = "e525530e4ca3436387254cd1f43dca401d2a7bb4ecd9483892627a86e80be28b08f716d450da416fb945f93b31c0f9d688db79405c704dcfb53f95cbd2977403836ec76ef15442929003434e7a92ed51fe6eb8e7422b44819e90671fffaf8c39225374962d8e47ff967442f6cb33a69b09615cb417ef47fe9df6b4f55c45288816ada38c33194869a3a776ff6016b33dbe46a23149104466b24be15e8a11882bfedd8da5300a428b9b14c0abf95d79058e72196900c544798522c3a3c7d42107fd587aabee074e0695663ad80337116a1cfb88f578f345beb54cde911bae0c3826631ff00ae8440d9cd0af4c42f46597769e1118132640cb9931",
                IdentityNumber = "fa1d9454eb064015b66a4713d98bbdd691a6b93e8b2b42a586d1e8a02138da7275bc74cad1cc439b9bc0cc55d9c1757812d68b27368d459d9a85386e37841945a23eb9eec611491c8f126f486c5fa23db5dc9e62d134488e87ccac9cc558f454cd5a70d7f052426ba048d20ef09eb36e0d8f2e762ca046679367e90597188c3",
                BankName = "37a19a9f31794a308e362542dcde42f7ac3c07af23ae4c8a930b07cddd18bcb8bb38588ed9904db39e0b1dc8203445ec2fbb9c8782c24c298b3c68a64ceff7269e769f466ca1496184a3c9ee3e541cfce2e315957f0744179c2b5b2b00581c0d4d38ab636c6f4551b7f5f54dbb3b2f974aae6f0f504e49cf814bffb91c9e3f9",
                BankAccName = "f34e9beb731947f2ae2a9bfb640d6efc66a9c51192b3446ab528ce82e7a0215a10b603202eea47d48948f9847767bc1fc643a6f5460c4483afd3173d47498129e98b6081ae24483089636130df1a4d815966ab815852435abf78532de309fc2bb15224bd19db4369b1aac26e87a9151507c9ac559a2a4146ae9ba49d39d623c",
                BankAccNumber = "5574a28a9d3147cd9eaa6c1b99c1a04d920237720e1e40e49ece1904818c5a76279856c310fd4efea13efb2555f18e4237a1ad67c4fb4b5b932ee5b72025bf94c1cb2f5e7bc149ac9d739fa448d59f13c70aae4be14d417493399a9313a28e871789cf2548a44aa58dfbd056270563cc591ea12a89094e17b59051159004d43",
                CustomerId = Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51")
            };

            // Act
            var serviceResult = await _customerContactsAppService.UpdateAsync(Guid.Parse("bc44d94d-009d-4374-9e32-c6bdefb96e2f"), input);

            // Assert
            var result = await _customerContactRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Title.ShouldBe(default);
            result.FirstName.ShouldBe("ff905a0f24414d6b8a5c75bc374984410b3c9545cd7843fa93c836c3044d125bddb99bde8aab44c5a306ea7eab35087b2e3cc34082984d359e1d80a3845346fa98c3002858a64832ae3f3c73caf60b2442fbd00f6b884b1fa1574f631ef1b353aca8cf13ecc24884a380942fa6829e411d502dee17284d7583c837810496171");
            result.LastName.ShouldBe("2cb7c3c993df4cffa0e802dda29714b7cdeb44482461416caf02fbdfbcf95848587bf18b453b4ac48c5e7a3f032b7f0303fd11afbc754de18c8e9132c12f47291ac10dd3697a4f0e88b4ac74f70d11620baadf3a30de4e949667085cb87c57f0ed04cdd896764c0eacee517971e35707aa7233a6e7a340a5ac0ca5953a5642d");
            result.Gender.ShouldBe(default);
            result.DateOfBirth.ShouldBe(new DateTime(2018, 4, 23));
            result.Phone.ShouldBe("801a7dc28f894202b34b0c8eb261a627e42cfa9bc5a24e30bc279adce49cca8d180fcd88ebda4632a9238cdab8b5f7c09adca0bb87b74b0491dbbdef06bb6eea2c39966f7f644615a955793d0dd3d6702fc3238c69de4dcfb1a029cd6eb7e8c9011f5eef412b456eb8ea5297dc7adc3ad50816db18b44fac88c0b4d9a4c30d8");
            result.Email.ShouldBe("dc68523112a54a348666d513b9c9adcb813b0933c377423e8e6e24181e5358f5625d9174ddca471a9ce1a0d6409bfc5b6f7fc4f278614a12b1cc7e4b3a30@1b72fd7d81094df1a0fb1528051fc0804badcbe5c1c248ceaffc4ecd6200468b994d3de1d93240e490712e0ef0b6b6111245e678b2b145358aa14aade486.com");
            result.Address.ShouldBe("e525530e4ca3436387254cd1f43dca401d2a7bb4ecd9483892627a86e80be28b08f716d450da416fb945f93b31c0f9d688db79405c704dcfb53f95cbd2977403836ec76ef15442929003434e7a92ed51fe6eb8e7422b44819e90671fffaf8c39225374962d8e47ff967442f6cb33a69b09615cb417ef47fe9df6b4f55c45288816ada38c33194869a3a776ff6016b33dbe46a23149104466b24be15e8a11882bfedd8da5300a428b9b14c0abf95d79058e72196900c544798522c3a3c7d42107fd587aabee074e0695663ad80337116a1cfb88f578f345beb54cde911bae0c3826631ff00ae8440d9cd0af4c42f46597769e1118132640cb9931");
            result.IdentityNumber.ShouldBe("fa1d9454eb064015b66a4713d98bbdd691a6b93e8b2b42a586d1e8a02138da7275bc74cad1cc439b9bc0cc55d9c1757812d68b27368d459d9a85386e37841945a23eb9eec611491c8f126f486c5fa23db5dc9e62d134488e87ccac9cc558f454cd5a70d7f052426ba048d20ef09eb36e0d8f2e762ca046679367e90597188c3");
            result.BankName.ShouldBe("37a19a9f31794a308e362542dcde42f7ac3c07af23ae4c8a930b07cddd18bcb8bb38588ed9904db39e0b1dc8203445ec2fbb9c8782c24c298b3c68a64ceff7269e769f466ca1496184a3c9ee3e541cfce2e315957f0744179c2b5b2b00581c0d4d38ab636c6f4551b7f5f54dbb3b2f974aae6f0f504e49cf814bffb91c9e3f9");
            result.BankAccName.ShouldBe("f34e9beb731947f2ae2a9bfb640d6efc66a9c51192b3446ab528ce82e7a0215a10b603202eea47d48948f9847767bc1fc643a6f5460c4483afd3173d47498129e98b6081ae24483089636130df1a4d815966ab815852435abf78532de309fc2bb15224bd19db4369b1aac26e87a9151507c9ac559a2a4146ae9ba49d39d623c");
            result.BankAccNumber.ShouldBe("5574a28a9d3147cd9eaa6c1b99c1a04d920237720e1e40e49ece1904818c5a76279856c310fd4efea13efb2555f18e4237a1ad67c4fb4b5b932ee5b72025bf94c1cb2f5e7bc149ac9d739fa448d59f13c70aae4be14d417493399a9313a28e871789cf2548a44aa58dfbd056270563cc591ea12a89094e17b59051159004d43");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _customerContactsAppService.DeleteAsync(Guid.Parse("bc44d94d-009d-4374-9e32-c6bdefb96e2f"));

            // Assert
            var result = await _customerContactRepository.FindAsync(c => c.Id == Guid.Parse("bc44d94d-009d-4374-9e32-c6bdefb96e2f"));

            result.ShouldBeNull();
        }
    }
}