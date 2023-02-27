using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using Xunit;

namespace DMSpro.OMS.MdmService.Companies
{
    public class CompanyRepositoryTests : MdmServiceEntityFrameworkCoreTestBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyRepositoryTests()
        {
            _companyRepository = GetRequiredService<ICompanyRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _companyRepository.GetListAsync(
                    code: "11b06d7c40ac45cab7b1",
                    name: "b58edfc2e9fb4501ba428e939916559dc0b785846b5849c080d6b29b8d7c2d8662a81101297b433b97bc6976ffea2fbcc2f8",
                    street: "8f28d7bdc5b143f88bb055613323dd32d232c",
                    address: "e07d686bf53449949ea75ec88f8c26219e1523ddf5f24437b8f89273d188d0d137adea004af749f1b0287167e5484413a33d2776581f4bb5a9d8d123d19aa1a4127fce8635254222b0b7e368fb06a465ddcc27fdd7c344af8116328ee908a7dcd0276c856758494bac69b4070e2cd71b0c10e326df1a430aae714de465d5c3a770463e8cd23b43a7af6ad901ee4828e2fa1a1d2aca034a7e9935a34b2f2865915a04954d4ab74ee9b9d7a7c29ebd91b3f93478fb9ebb412c975e7bb2da47cd41d9435af729204b9299b896f5a366076447a5b7fbef424911adb41866f20d5fe901ca051e6f384a9b943ec338a776e3a19375dbba77f640b2a0c4c5949c381343ef006c865be9458b8e7229941ffc5cb56c06a02efa164f2083953aa1952bb8f944266408d1764dcf8679911a70d8e8dd21e388168409406082eeb8fe579b5940a340f0ca160d4037b7e29fdb22fbebe4345672e669714a95a8689b374529da34c083f8cc134d4a6fa23cb16c8eefe7967cb569b9d04743a88e273fbedb887b3a78a178781a894df7888579244176e0689cd2dcc2f226408eb3dd70ea33a31151a252871f7e784ca794705e4604cd5a7ffaee309bb61c4da2886f30f260f2f9500d4444bc6c214369be26f250b22520fe0eeafe224dd54e71b4df0438cb341a4512f9d49e870247de911a6f2f2fdb916bbd46e2c0",
                    phone: "57887cc5906247bcbe71",
                    license: "5a16e4884dba4df792114251cf4590c90b9ce395d7f44c3a9776e019f29369c393a493ea37354065acd577efd26f37662f77",
                    taxCode: "a359cc35f9be4ee39e7ab73073d60d1369462a4f511e43a4a9a80fe4dba9300480a02a8697534bcdb8d3aa05216c1b369f83",
                    vatName: "f0a49e2efb0c4dd4b3c246fa75e9ba6ab08ab18b59e84cb1b48f1ca445924699c7",
                    vatAddress: "f60461a6c2a34dc78dba5f78c0c3e727dbef2507c7e043c8b786474d20003c73e6d8d0f08c40429d9733ed00462531aba1cf4aff9e8f4a7ab0ddf4efd55ca7bcc10a86d6b6e64a8fb0b10124729c0fb55291767ccede4630ab15686c450a7d510198ba01b4c4443c8fd2c13d8c28e3a59061f7334c8c48d69d66fb3454f87ed7d4f82452ea854be9940cb90d0645e22b2513b537e36c492d862a5827c4a10cb79697772ff0fe4f2a88ec2c3e6f4e6537f8162da936e042c3b4e40b9399f5bd640fff01814c29426a8499074d651670b028286e2193894adfb432947b99a89b3522dccda12a114b3da05f250091e99b30bd740e5eca3349e48693",
                    erpCode: "82fcd40a281e47d691a0e85251be7128d4d2661a557f41a88d2676b2d52621d2b54be6f48fb24ae7af0d21ed49080c9aa9b9",
                    active: true,
                    isHO: true,
                    latitude: "32eea34f55d4491f9270bd97333f39444cb41f100f17421fa6af368e8b3c72d11660dd6d04e841",
                    longitude: "9a795dfb4c44427",
                    contactName: "7e31d7332a7f4a579f3e7b50a35cfba330672da89ac247d3801259826b6d65b2c1d1a",
                    contactPhone: "e26c293f94604c11acd83bc40d2a7a446c58ffc443ab461db99354e52aa4db9612ae9e689db1412d8b"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("d45111bf-4879-49ce-9cc6-313f51f8b197"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _companyRepository.GetCountAsync(
                    code: "0daceda9889441d6958c",
                    name: "c1efb355e5fc4e34bd2c31fc57ea26fc32f78a7b18ba49ff8f37bb93291a8c84ee46d7e3cbf546948dca66d39502702550f9",
                    street: "1fc7b83aaa134ce6954ebf63dc559819389d611449a642a99f674f0d590384f5642957669a04402f82ca",
                    address: "9a13298f03e74a889b3f57c8ff6d7b0564efdca310144bf68ed24b1a2f2c7442b3bd1a8455ef485c87407cee69919cb1853199a60d924fe1aad149fd58caaa6e661dd0b390534a4dafac38d5f8dcfa4e7d5ba9f3afa54a63bb2c4787308d19b87ac6b44ac2d74987b442fb970525ac1050f94716365c435ba928594cace342e22a29931f26814cabb316c88c9ea509caf669178a0e3644f1be7fc74cda22dd5a756c691e49b243a28b8fe71b7b6f807d1e576853f9fe45e0869d613e78214016f4ce8114ec8848408ca0f61b921233166f1660648d8c424f9c68ad2e41e10d3ffcbfb55380f7463b9a785ee5f1885352d0572beaa12142249a46c174cdebbe33c365f92d57204b14b385ac771822289dc3c39f7232a247dd8ede5c44f3b48c092847855774434b739b9a02cc13979d4e85ebcd5f91984a3a85c0fb8fada2fde06cf30e15ad7c4749906918d38745f2ac2af60ce94dde4a6596ca89262ffb0b03847bd7f3a26946ba90d1a248ce3f3784bc70a85866db4e588542e39101385deead8af2515a1c4b6f8c7f5bcddcb42f822d27406df4eb43dd93ec620e20d8bd1fc071f931cecf4e66be30b2eb9664db5900311fe2b0974e06b3c700bcda344521df0cf0ef83224a93b700c67738df777df152697911fa4243bf08871a1d2aa213bc6027b9d21644e8b7621dc373db9f33e40d9090",
                    phone: "38fe4a594c1e44869228",
                    license: "c346ec45cc5e408d95c0ff1bd5e2962c59ed6abc2b594af2b363da832fc1c6f524f7efb86c804900869463777b7027010631",
                    taxCode: "9e8ac31f7bac4cd1b07ab6dd1f190278dae3872271eb426db6fffe68ff23dd52ea45716e12124c2e8f4a8c396829eb63238a",
                    vatName: "5841d73c5ad2483898a65b6d38bdd4678c210fe8194e4cb5bc511088b9f9478",
                    vatAddress: "19802eb6493845cc89a051a8fa2d14f11ea0d373194e4484bf537dd7109cb34f79e496e0433741f88aaa85759c8a2ae47221dc3f0b7343999d5749cdcbb91690dc3b45a71a5442bc85890d1f0eb1e13859a4431fb9e646b998cb027356f7445da0b82f20a0684faea59401b6c1aaf426b4406eb7d881417dba7924eed008caccb602a0b59d5e42afb57784cb8e7c375068a17ebcf4ce453ea50e049a0d87bd5610a3aa91ddd44bd98dc36b2db18afff9da0aac311657442ca0b6bea4921f5218ef0cad1022d4462c8b71e69f89b31d1f95d919e00a1a4695948aab2bb54987b0aebc569d601a43d09da7a855dbc19e659a0fd56f8c1e4ec0a92f",
                    erpCode: "bcd1dd6deafe4a668c32efa3713f9e281a8a4a36d2a34ec1b648c699f930b7796eb88d8287174a2484d361f6185ed2e167fa",
                    active: true,
                    isHO: true,
                    latitude: "cfa9e1bad83b",
                    longitude: "8dfb3f8fcd0f41a0a304278ddfc8b25018230f936a4041aeabeeeb6074a9327d4769fe7bb2094e7b9a1",
                    contactName: "a9da710fd9fe4420b60ed391504",
                    contactPhone: "7167c6ede24c44a18dc143e184c5613a"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}