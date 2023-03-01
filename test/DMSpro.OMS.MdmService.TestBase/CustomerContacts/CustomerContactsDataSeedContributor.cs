using DMSpro.OMS.MdmService.Customers;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.CustomerContacts;

namespace DMSpro.OMS.MdmService.CustomerContacts
{
    public class CustomerContactsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICustomerContactRepository _customerContactRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CustomersDataSeedContributor _customersDataSeedContributor;

        public CustomerContactsDataSeedContributor(ICustomerContactRepository customerContactRepository, IUnitOfWorkManager unitOfWorkManager, CustomersDataSeedContributor customersDataSeedContributor)
        {
            _customerContactRepository = customerContactRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _customersDataSeedContributor = customersDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _customersDataSeedContributor.SeedAsync(context);

            await _customerContactRepository.InsertAsync(new CustomerContact
            (
                id: Guid.Parse("bc44d94d-009d-4374-9e32-c6bdefb96e2f"),
                title: default,
                firstName: "1943a0c3227c42f494cdf77a3c877494ab382758a58149dd8776a285f96c02a34dcf1c1d4a3149a8b7852a58a6aa3dbd4d2c959f700849a29728aead8279dea8ea6d817a1aba46bfbf5ea16d0d66de85b4e1289b63dc41a3ad418b7606cae2dd3bc46f5d700847fc8d202fe1b6c7f336bf357937141048649b14cb9619e22a2",
                lastName: "616a921e0e124d92ad998d643638cfa7ce7f4884a91e4fc0aa7ce677171d1a13769a12d9a72445f3942d8f6c25197d6cff424f10f48f4757bf023ce08db3804203e9ce55252e4a6bad06483298eab14f88ece8d983e64ac3a57a583646b7a745a422ff5c7d01474da0350f5a28d2409ae48ff1de521844e2953534ad4863696",
                gender: default,
                dateOfBirth: new DateTime(2017, 4, 15),
                phone: "dcfde9da4d914607931ec6ff34a13e1990947385c16249f1af6aa98e90f8d860525bac476bf84d58942f67e9170f025bb9c47dd5d903492f9a9d1673aa1a0c00ab61bf37c2304ae0904c4d5fe67e817a6a4157ec67a342cfa8df71f555c6c1cb3c69c7106c3846afa811ee1f3c8473432aaa029992e443fe80748d5a8929493",
                email: "bc39c9fce1b1433ab66f7cabe3a1c2ebb640c3bd6ca342969bfa87a7589fd8370e842dd44ae54521a49beccbde6cdebb020a4d9a612d4c3bac404ce12e2c@7db6062ee1394fea878d1683148bc83c1e9f9c20ff9f449cb1fff02a9dfb81e1d54e0229f93649d0b585f0ee6e421a39b2afcda214da4397865c1a3f31f0.com",
                address: "7f04c570f5dc4d9d878f59bf8cb65935a4a5358c89e247d2880c0cd24c512c4e578f58037571420d83502ebeb0eb1c503759cdbc68594a31ac6de746587d0ed2fb063b2e07e44874b882654297eb504ca05b46207b954f6789356054aef540b709824c42042e4792b5cc02943d7836c949627ef201ea47aea444a0729627d22f6aae85dd34e44f99b61374b91a3cee3bfc38e7d9855847e685d913ebc3c70b14328c2c235f08499b8497dc1e4510edeab68b9664be414c669c9ea0b085a098d791a8a1ca98264504bafcab94446e069efe6a1849645d46578394c40a15e6bb9a89f75f56c5954e9bae33a9dd80aa214945dcd6eb3a064c839e65",
                identityNumber: "e27ef5b6110942f7b8c1491efea9906622c2aba44a3b455fb1b979e65f7082c63197146c7d2a4194a2d1493b373a3b54c52d9f404055482ea41555db540e1ca8e211150246504a46b683c89b3d83a60db0584b69949d4372bba60f3e30b5e337f92f8a51a7124127b0a7e29e101a7b5f02fd4fc668f04c748a0eb11b0421fd1",
                bankName: "a7a11764420a468ab6e943166838870bd7ca836b902948029924cade526c07551e6e54d211094f19a9e41bb85c297f0d4bf57acca65340ee847ed8d9a401960d816c62a0d85f4b9eabff7ea671bd680e637a455ecf094349b8216082846c90371a671bfca43c40a3bb37a0346a0604e5ddf9953652c84746a4fb9dfe66fc8d0",
                bankAccName: "3611850611fe4be393f48de81b4fdd93dc4cb2aa2da84171a878a02f0dc477e9bd137c00e54c470db79ebb1438d3c95b88eb6fbb16c84d61ad3d1a278b8780e1833e553d5a63408e9a4b57052890f5ed7d024ebe4d84474a9de054b1747366950abd1802d5ce4dd69979da2c6f700f038695cc4a17f449a386e417f966d0c33",
                bankAccNumber: "b265c5b5e8b642b585b1d89e873b23ab8b0928240d7445b1b116b086ca709af9c82e0b99ab5e4c2086158bb51cf9db7d139016562b9848eeaede2f245c51a2343359459a386b4dd78681ccbe2aa4abcec11bf556f454411ab733e13a610cd9b0b97c122b8a75480d87f01f682b59acdf0997e9501d1b4c30ad8d99d0e84c45c",
                customerId: Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51")
            ));

            await _customerContactRepository.InsertAsync(new CustomerContact
            (
                id: Guid.Parse("0c40741d-3e33-4437-88ac-67c86e3bf12f"),
                title: default,
                firstName: "3d37170a1db64f539459102859abdf6f9c46d2c440d74ae69cbac685a2b411d2c92b749d5f5245aa9694c2323d68ba558ef81bb7a07b47c1a246a4b383a4fd5c13ff5cd4b3d1444c8e0c937110f67f6a7e36abcbceb94f6b9af6766e6c7c3f5fbeb721cdfb4f494a9e7d96f5d2d50a9ee9a9b2386c2f475b99d021a1e2d9663",
                lastName: "479dd2307e1e440785674633207a6e2392ae88e9e1c04fbfb1809f530b370dddc3589672918645e394b5cb924a49874d85758536af7941e4a02d13583c237583b3fa7040bd7e4f258cb90454dc1af832df9430603c6e48a1b63ee0d5711c3059f6ddf189a121425d9018538510a363ac0d65e61fec9d444e879829b147d8e17",
                gender: default,
                dateOfBirth: new DateTime(2002, 6, 16),
                phone: "4cfc934f370f42c48e28c2daddc2a719ba2fc677aceb4f1bb6fee9ffaf72269ec3ad3d7b1aaf43888314b64befdfd6137f6f3109f54343dd83eb6c18b23ee1ffc242fe665f2949a7abefcb496b77c0a56350c3199b384e36bcbb79e17e6f3da36d9057cd83ec4fbfb2011e8a3854b9734ac5ec7bc35647f8a8e2afa9249abd4",
                email: "6a42cad992d74ee5ad150839cfc7142c11b5b4d7939e41bba35a5545bf918bab88d3ed16896a4c4c8e145bebb852e4007dec50e833cd4abbbcd562ffe48f@fa9235b048554cf683a7b73209cbc4b232e66c1f72ed42f094d345b118fc14b5464d11fa88a6402d85e84f4df9c95f69157cfe65e7ea470db27395aff353.com",
                address: "433cf0f87e3741e9a58f30346d42e6e1a5ff7eb196d84193adcbd1333f846970dc5595c44f684877b3243911219b49f5edcda37a8368431a9be57cce53832351d050218fe4b541369ac6974ed380880e1812828eb2414c6f9e13cc5b8256fe4a0946cdf5caad434a975a595bac51a33e44e75f6a3f4d433380e5fffc10ec2d46eecc46f9438046e3b44958628a40df39b49db26bed4b4e0b85c46d5669c4e627b3fef759a55b4b2482b84ba12087159a5edb4ed16d244541ba625dccd0eb878e769238989c8648689c67a5cb79a82a316b737fb568814ae9bb4e90eeca2a5d57d22fd0b8ab324fa8986b82e02a63076cf6170fea53de461abe19",
                identityNumber: "825dafd1bb8248e798c270ae8109f3d1bf4058497aa143ce93adec2d0328f7b0cd1f88c2316d4bf79f8669f582728259c3a43125a5044193aa623a75ece16526a0adb5bf8df74c148eb356d992adc7a7cb12fc912f3f4e4aa0c346a9e4be0687a868713366574186bcd86a16349279c70dd5db1c2b204e87839b4033d73c4da",
                bankName: "10884819c58e45179c87e6734e033e6a3722f3f264fc4fd1afd4f856505b0798e8ef1481e03d4aea9e5ac87cb74e2ff065b1056618e64d618576fc65f107fc858f3671cdd8154a50841e7be1224fe4091c244edda06e4e83ba3b118c716336e72e26776228364b82a6d4cdab8905f2ed8565945dd24c4f4fa37a554bcbeffc3",
                bankAccName: "ec7e2282db054f66b0e27c2f3f4987990bcf8aece42f496095701f614f2a21302ca4a057f7a843a38e369bdbc1b6478924652f1e27404b5593b30f73a776f1d9ed17e3780f5643c8b8b3b9e976c9558991174d22e4ee4fac8c0471ca2b33d1c85d54ef50f85d4ae29aa137e8dd5dca483e9434966bdf458886ba23016a6c592",
                bankAccNumber: "a05dd09889784948ad5154a458c43643dd738e1a6ba34942922268c7901937c4d93487061fc14b3082bbcf346255a435d18ca702876e4e4ca1f1295da86df237d3c6424e3b7e4147a9c382aa7e7825bdd0678938d5a540bb9917e89bc4933d7dc0e0956e4ba14cb79e0ea9bca0b22280b920261c5ed44ab1ad4a313427672dc",
                customerId: Guid.Parse("03de2fdd-eb64-4eb0-bdae-cc79b5ee1a51")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}