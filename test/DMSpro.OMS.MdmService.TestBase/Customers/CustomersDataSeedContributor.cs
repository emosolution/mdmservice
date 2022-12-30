using DMSpro.OMS.MdmService.CusAttributeValues;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.SystemDatas;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace DMSpro.OMS.MdmService.Customers
{
    public class CustomersDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly SystemDatasDataSeedContributor _systemDatasDataSeedContributor;

        private readonly CompaniesDataSeedContributor _companiesDataSeedContributor;

        private readonly PriceListsDataSeedContributor _priceListsDataSeedContributor;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor0;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor1;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor2;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor3;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor4;

        private readonly CusAttributeValuesDataSeedContributor _cusAttributeValuesDataSeedContributor0;

        private readonly CusAttributeValuesDataSeedContributor _cusAttributeValuesDataSeedContributor1;

        private readonly CusAttributeValuesDataSeedContributor _cusAttributeValuesDataSeedContributor2;

        private readonly CusAttributeValuesDataSeedContributor _cusAttributeValuesDataSeedContributor3;

        private readonly CusAttributeValuesDataSeedContributor _cusAttributeValuesDataSeedContributor4;

        private readonly CusAttributeValuesDataSeedContributor _cusAttributeValuesDataSeedContributor5;

        private readonly CusAttributeValuesDataSeedContributor _cusAttributeValuesDataSeedContributor6;

        private readonly CusAttributeValuesDataSeedContributor _cusAttributeValuesDataSeedContributor7;

        private readonly CusAttributeValuesDataSeedContributor _cusAttributeValuesDataSeedContributor8;

        private readonly CusAttributeValuesDataSeedContributor _cusAttributeValuesDataSeedContributor9;

        private readonly CusAttributeValuesDataSeedContributor _cusAttributeValuesDataSeedContributor10;

        private readonly CusAttributeValuesDataSeedContributor _cusAttributeValuesDataSeedContributor11;

        private readonly CusAttributeValuesDataSeedContributor _cusAttributeValuesDataSeedContributor12;

        private readonly CusAttributeValuesDataSeedContributor _cusAttributeValuesDataSeedContributor13;

        private readonly CusAttributeValuesDataSeedContributor _cusAttributeValuesDataSeedContributor14;

        private readonly CusAttributeValuesDataSeedContributor _cusAttributeValuesDataSeedContributor15;

        private readonly CusAttributeValuesDataSeedContributor _cusAttributeValuesDataSeedContributor16;

        private readonly CusAttributeValuesDataSeedContributor _cusAttributeValuesDataSeedContributor17;

        private readonly CusAttributeValuesDataSeedContributor _cusAttributeValuesDataSeedContributor18;

        private readonly CusAttributeValuesDataSeedContributor _cusAttributeValuesDataSeedContributor19;

        private readonly CustomersDataSeedContributor _customersDataSeedContributor;

        public CustomersDataSeedContributor(ICustomerRepository customerRepository, IUnitOfWorkManager unitOfWorkManager, SystemDatasDataSeedContributor systemDatasDataSeedContributor, 
            CompaniesDataSeedContributor companiesDataSeedContributor, PriceListsDataSeedContributor priceListsDataSeedContributor, 
            GeoMastersDataSeedContributor geoMastersDataSeedContributor0, GeoMastersDataSeedContributor geoMastersDataSeedContributor1,
            GeoMastersDataSeedContributor geoMastersDataSeedContributor2, GeoMastersDataSeedContributor geoMastersDataSeedContributor3, 
            GeoMastersDataSeedContributor geoMastersDataSeedContributor4, 
            CusAttributeValuesDataSeedContributor cusAttributeValuesDataSeedContributor0, 
            CusAttributeValuesDataSeedContributor cusAttributeValuesDataSeedContributor1, 
            CusAttributeValuesDataSeedContributor cusAttributeValuesDataSeedContributor2, 
            CusAttributeValuesDataSeedContributor cusAttributeValuesDataSeedContributor3, 
            CusAttributeValuesDataSeedContributor cusAttributeValuesDataSeedContributor4, 
            CusAttributeValuesDataSeedContributor cusAttributeValuesDataSeedContributor5, 
            CusAttributeValuesDataSeedContributor cusAttributeValuesDataSeedContributor6, 
            CusAttributeValuesDataSeedContributor cusAttributeValuesDataSeedContributor7,
            CusAttributeValuesDataSeedContributor cusAttributeValuesDataSeedContributor8, 
            CusAttributeValuesDataSeedContributor cusAttributeValuesDataSeedContributor9, 
            CusAttributeValuesDataSeedContributor cusAttributeValuesDataSeedContributor10, 
            CusAttributeValuesDataSeedContributor cusAttributeValuesDataSeedContributor11,
            CusAttributeValuesDataSeedContributor cusAttributeValuesDataSeedContributor12, 
            CusAttributeValuesDataSeedContributor cusAttributeValuesDataSeedContributor13,
            CusAttributeValuesDataSeedContributor cusAttributeValuesDataSeedContributor14,
            CusAttributeValuesDataSeedContributor cusAttributeValuesDataSeedContributor15, 
            CusAttributeValuesDataSeedContributor cusAttributeValuesDataSeedContributor16, 
            CusAttributeValuesDataSeedContributor cusAttributeValuesDataSeedContributor17, 
            CusAttributeValuesDataSeedContributor cusAttributeValuesDataSeedContributor18, 
            CusAttributeValuesDataSeedContributor cusAttributeValuesDataSeedContributor19, 
            CustomersDataSeedContributor customersDataSeedContributor)
        {
            _customerRepository = customerRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _systemDatasDataSeedContributor = systemDatasDataSeedContributor; _companiesDataSeedContributor = companiesDataSeedContributor; _priceListsDataSeedContributor = priceListsDataSeedContributor; 
            _geoMastersDataSeedContributor0 = geoMastersDataSeedContributor0; 
            _geoMastersDataSeedContributor1 = geoMastersDataSeedContributor1; 
            _geoMastersDataSeedContributor2 = geoMastersDataSeedContributor2; 
            _geoMastersDataSeedContributor3 = geoMastersDataSeedContributor3; 
            _geoMastersDataSeedContributor4 = geoMastersDataSeedContributor4; 
            _cusAttributeValuesDataSeedContributor0 = cusAttributeValuesDataSeedContributor0; _cusAttributeValuesDataSeedContributor1 = cusAttributeValuesDataSeedContributor1; 
            _cusAttributeValuesDataSeedContributor2 = cusAttributeValuesDataSeedContributor2; _cusAttributeValuesDataSeedContributor3 = cusAttributeValuesDataSeedContributor3; 
            _cusAttributeValuesDataSeedContributor4 = cusAttributeValuesDataSeedContributor4; _cusAttributeValuesDataSeedContributor5 = cusAttributeValuesDataSeedContributor5; 
            _cusAttributeValuesDataSeedContributor6 = cusAttributeValuesDataSeedContributor6; _cusAttributeValuesDataSeedContributor7 = cusAttributeValuesDataSeedContributor7; 
            _cusAttributeValuesDataSeedContributor8 = cusAttributeValuesDataSeedContributor8; _cusAttributeValuesDataSeedContributor9 = cusAttributeValuesDataSeedContributor9; 
            _cusAttributeValuesDataSeedContributor10 = cusAttributeValuesDataSeedContributor10; _cusAttributeValuesDataSeedContributor11 = cusAttributeValuesDataSeedContributor11; 
            _cusAttributeValuesDataSeedContributor12 = cusAttributeValuesDataSeedContributor12; _cusAttributeValuesDataSeedContributor13 = cusAttributeValuesDataSeedContributor13; 
            _cusAttributeValuesDataSeedContributor14 = cusAttributeValuesDataSeedContributor14; _cusAttributeValuesDataSeedContributor15 = cusAttributeValuesDataSeedContributor15; 
            _cusAttributeValuesDataSeedContributor16 = cusAttributeValuesDataSeedContributor16; _cusAttributeValuesDataSeedContributor17 = cusAttributeValuesDataSeedContributor17; 
            _cusAttributeValuesDataSeedContributor18 = cusAttributeValuesDataSeedContributor18; _cusAttributeValuesDataSeedContributor19 = cusAttributeValuesDataSeedContributor19;
            _customersDataSeedContributor = customersDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _systemDatasDataSeedContributor.SeedAsync(context);
            await _companiesDataSeedContributor.SeedAsync(context);
            await _priceListsDataSeedContributor.SeedAsync(context);
            await _geoMastersDataSeedContributor1.SeedAsync(context);
            await _geoMastersDataSeedContributor2.SeedAsync(context);
            await _geoMastersDataSeedContributor3.SeedAsync(context);
            await _geoMastersDataSeedContributor4.SeedAsync(context);
            await _geoMastersDataSeedContributor0.SeedAsync(context);
            await _cusAttributeValuesDataSeedContributor1.SeedAsync(context);
            await _cusAttributeValuesDataSeedContributor2.SeedAsync(context);
            await _cusAttributeValuesDataSeedContributor3.SeedAsync(context);
            await _cusAttributeValuesDataSeedContributor4.SeedAsync(context);
            await _cusAttributeValuesDataSeedContributor5.SeedAsync(context);
            await _cusAttributeValuesDataSeedContributor6.SeedAsync(context);
            await _cusAttributeValuesDataSeedContributor7.SeedAsync(context);
            await _cusAttributeValuesDataSeedContributor8.SeedAsync(context);
            await _cusAttributeValuesDataSeedContributor9.SeedAsync(context);
            await _cusAttributeValuesDataSeedContributor10.SeedAsync(context);
            await _cusAttributeValuesDataSeedContributor11.SeedAsync(context);
            await _cusAttributeValuesDataSeedContributor12.SeedAsync(context);
            await _cusAttributeValuesDataSeedContributor13.SeedAsync(context);
            await _cusAttributeValuesDataSeedContributor14.SeedAsync(context);
            await _cusAttributeValuesDataSeedContributor15.SeedAsync(context);
            await _cusAttributeValuesDataSeedContributor16.SeedAsync(context);
            await _cusAttributeValuesDataSeedContributor17.SeedAsync(context);
            await _cusAttributeValuesDataSeedContributor18.SeedAsync(context);
            await _cusAttributeValuesDataSeedContributor19.SeedAsync(context);
            await _cusAttributeValuesDataSeedContributor0.SeedAsync(context);
            await _customersDataSeedContributor.SeedAsync(context);

            await _customerRepository.InsertAsync(new Customer
            (
                id: Guid.Parse("e9c6f102-3d37-46de-b979-3e039dd965dc"),
                code: "7fa2dc79268848e4b299",
                name: "e559752dd4054d4ea14b586d50be65b521b9334480da4122b62acabf08e17e0688046d84df86421c88063439523a46c1d20d46011a5a4985b87afa128ab0d7a2330b308738ff4c2bbab9cb73311b4988efb8f2cfe5654cbebff1602df2214cd281b29eb40b8e485cb93a835d258f9c77cd146805002743aea2a9a6fc8b1e87c",
                phone1: "162454a654664e07af18ef958cfc71c80e328eb81a1f4e638d",
                phone2: "287f019809274b8f952f49e2401259e48dc961fc7bd74b8393",
                erpCode: "0d4f0649cb4a4aa3877c",
                license: "87031e3ceb2d4d088f996b55d154553abe1435c8d6814099b8",
                taxCode: "22edb7ec3242487fb3e07996001541b892c8e57272d24c14a8",
                vatName: "6e5355f65764470dbffea7a13c4e01cc6087bdbc032440c5a57cf9517938a503e1210345a4324a51a5ffe685b542da6b851ffebbb5c44e4cb21401f589dc22789ed09937d9c84a7f9ba4aa032f473b0f006da1cc1ff9455899feb12d8023300d7ad28db56ef74f24b18762e9de36b93bd7670fc2bbd0411794f0f3c37c0709e",
                vatAddress: "a9faa55f3cf9479bbb375ea32ebaa804744ae65de9c248d1a14840e1011fc9d88f80b367e6a342afa9fa22219127c006001eb2fed8114872a8a2204a66950bbf6c1762843bc34b4cb53ee42b2b16e3d3e56d483d009f4537893cc991f91e6ca3df224795f353408ab46655bd7d232c3807ea1f27d0ea49c2949531098d66c294d4d6dc1d5e4e4416bda10433922ae1350bbd73055dc64151834355c01591b6924a805a448cea4bb496feb7c3ae6e7079687443a18656496880ff6a4bad9df5456422378ba45c4fcca4d59f333b70ca0a29961e5ac29945fa81122fe670971f2c5a88f29fc4304b9cb9843c785da7603c8579be7e9978469db87ba4c0b9d952fde0f4c534ab27475290792224e6efed1359f247aa5f2f46a2b9c24128b8835c7a687a5b7bc03a4092bd5cf42f6842afcae85316a59df54ce58e0cd53c23ae0519df154a73a2be43e1bece642b7e5ad84e668039138c9346379d8bb9efb7ac9512466877bd77f448e2823f9199b18b63e5d993db0e995e4fdd9a003151e4d765200cfbe0a2338b438f8609a5c34329d6c5b75085d16ffc4b2ab1f99f6032dd99ad5ade6fc428f94c9290014f046d583e3e23914e79dc964f5b854454ab4f49c42720521b7d8490410b94750c090d871e659b1fce2e76c34b7abe02ad46d4eb1dc7982cee4032964125b57819dcaa4a82b46f4b8383",
                active: true,
                effectiveDate: new DateTime(2013, 11, 1),
                endDate: new DateTime(2003, 2, 7),
                creditLimit: 967018517,
                isCompany: true,
                warehouseId: Guid.Parse("ef7c788a-2b81-49af-a585-92f7bdc2d0c3"),
                street: "f006733554c64823a916104b0affda63b3230e31fecd479ba631f8d2a262621976d527dc63e14a278ff5e2ef3d51d847b473d69c6be04ac3942420d0e63bde250962f3a2c00e487993113d0cce135aa97def752c90f346a3962257a51aae6285f3e7f5a352c346b49af61b2c4ce55da9c08e02efe10a4a3ab9520f5b1adba32",
                address: "ef1e330f3ef447b19c73a9851fcda9c25376f42801b44da6863f228b46c32c436781c20f901c4e12af038b7be5246581dd10ad8a7d2f4359965a204410009f1c63f5b01ed8a4422198a96cf2bf7d4f62fafcddf1b47e4fdbbb4921e0b46f64c0107018eb9e30440eb5bffe961670087a93f638147bab45baa168341b792a691e46ab184df9894613b07eeb2661e69d43422ab70bae86448eaca26e34b73f29e2604639ae39254e1b8df4bb799dff75fa8cbdc84c6c85480e8be23142ed73959e7068876a68de4c4ab3dac452081f49d4b94d1fbd58aa4c1598c1733d2f6fa951f1ec287112e742858297c81b89e8059b021698fd2cca4b5eaf35",
                latitude: "6511650760ab4ed784e93d651acbabd0e3d98b2c597e4e40a5cf48f8849e7ed423a40188c6334cb5941b7116b92562a13b6133e1638841079231b4c2c785252f67db2bf9888946aca71fd223e25b23ca5319f82d544f4111b3482133b2ca4ba73dfa9002acee4830b01d2fe933ddc788c0b12e3430d943c38d2047e5de35cd2",
                longitude: "4a2f9cdf95e24751918f1e5c79af5cc03ae830aac193484d834bdffa0f89ace733bab3c622e840e396b62cabe21336850506a21ab3db4698a3190688aaee4b6f9f11550e5751425e90fc5105b93d1923d2f776e9ecc54b5d8711d1d214ba5cf1828ce7423e8540a9a555c28f7bdffd29da0bd535210a4a5b982b189b8f6dbcf",
                sfaCustomerCode: "8c148b24598d4c39af02",
                lastOrderDate: new DateTime(2017, 5, 11),
                paymentTermId: null,
                linkedCompanyId: null,
                priceListId: Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),
                geoMaster0Id: null,
                geoMaster1Id: null,
                geoMaster2Id: null,
                geoMaster3Id: null,
                geoMaster4Id: null,
                attribute0Id: null,
                attribute1Id: null,
                attribute2Id: null,
                attribute3Id: null,
                attribute4Id: null,
                attribute5Id: null,
                attribute6Id: null,
                attribute7Id: null,
                attribute8Id: null,
                attribute9Id: null,
                attribute10Id: null,
                attribute11Id: null,
                attribute12Id: null,
                attribute13Id: null,
                attribute14Id: null,
                attribute15Id: null,
                attribute16Id: null,
                attribute1I7d: null,
                attribute18Id: null,
                attribute19Id: null,
                paymentId: null
            ));

            await _customerRepository.InsertAsync(new Customer
            (
                id: Guid.Parse("78cc889b-a12c-4b3c-978e-182b7eda591d"),
                code: "6c1799c7c0a6443189b5",
                name: "ba030628c96242bd9faa49a4c7844acd85a93de72832426ebd7a658215e27f01b81521f1a9524323898ad590f179b1ecb30c715caccb44a8b081baee63de422033002adf482e4b249cd976da96c9daa8226cdf84ad5f42f39028a3316def6985e69c46590d4a4949a716a8c62f294f3ff3e45bbd116241b78d24acf21eb5bbe",
                phone1: "09097ec969bf40d5a1b0cccdc5a9a7492c0785536ec84d7092",
                phone2: "cae168b0842c4dd8a87f9bab1c31e1afb2f26d74a5be46819f",
                erpCode: "8ea35ea870db45fc9cc8",
                license: "4b7c73086a2e43aeb016e378714353be0ca588ad5f574ccea8",
                taxCode: "8ab688367ad24eb2b5f1e8b9673182ff85ca6300ba7d409894",
                vatName: "e84658071d1c46d5bbf799de4b95a6b7ff3c614ed855473080c2f9fd52043197ae5b340d6265406aa780e86bc84331e102f82d95a3b44b2385f30cb5d55d009d12636c7b80a24cdc98b6f654936e49f9579608dafca2457680933eaeff6ceef1053bcd368e294bb2961122acc1b4f311406d0e8145d148158705fc3ba42a0ed",
                vatAddress: "698ad8cb77734df1bfa56a3550e4862d5f034a355a7748fdb313593f8d89ea2601a4ea30d61b4c9d97fc36fa3eb8cbab20594fc8636b47eba41b3d5ee074bb41c6f5a890f40b4007853ca46737ad0489fd0e84e35c4647178cad4a3879cc2ba9bbe45d6f7198495da92e097d6f4985808b567bddaf774ce5938f18ae6b01a862143912097b5c44c39d56d4116e27120a522294e675d84ad4bd7fea003a613e917b76a23fc3414e5b9e78b3fcc8ad66ed2fc3c2d0970347f58181eec9830f1bb6fae4224252e34f2fbfdba00c50e71557202fdfd3e6f646f1a470621991a73964281bf62b135e4917a9b2428f2b70d4278b602970e6de4d35b1e81a0bb34bc567e8cfcdb8759940d6bde5c2061b1785e0a385f4ba27b4493eb6497625029645c37645c3362ced43028b29220f49e4e8dffb57f6fbfcc2463ebfabae1422239319d7dfb8e262364276b6080acb23ef315147b6df9d7c624be78c59e473d6187bc9bbed78b25d09458e97922be80fc3b7dc1eac384a3de14ed3b5bcf3c3eafac827bd83ef3c7ce94562922e44a6dff95f0032d65289d45f44e9aa9d7872b258d55784c09db6e4b64a85bd08f575212a0657db0124da57f74bf490da22cccc64588ca97034cca3134c5cadbaafedf981c708f85679185c194aa38e5f005df9a2a2ebb86083e26b564f0d81b0f7d16593aad1d6ba3184",
                active: true,
                effectiveDate: new DateTime(2002, 7, 10),
                endDate: new DateTime(2021, 2, 17),
                creditLimit: 775928158,
                isCompany: true,
                warehouseId: Guid.Parse("282f7bef-85dc-4d1e-99df-c105e139f588"),
                street: "ab828a3400484c5f95949864c7a377500d2c9c3df818468086c70f00ade076a78556c4741c6347a1bde07e21ecae906af12a9e9c901f4cc7bb792aa4f611a343d3991648a5c142f8b569189390d965917bb5f1c9a3c940c08444d5682b79527c32c85a6940564577a451a2fd3b0f29f027ec8113617a4bb2a35e2c9b3beca16",
                address: "604e6ff88d4b4fc4b1bb142bcefc16aa3ec9305d754b4099ae7ff37c7e556d35b0065c51652b46febfe3a7537712605fe23733bdfd7d4398a2dc6565c615e44924840fc5b7184b39bec8f549be6fb22062a25cfe751c42a0a3a8e1ea74f211a058601ef63e934f8cab6cfe8f313f0daacb14a066bd6d4fc89d74a7325caa8574b0740a82c6054733b6b01deb61fc7a0c063b583bde52465788fd19180d39c3d9aa5491dfd60248b99b8d4cb5a8a89521b547a6148f8346aaba5877699996755df3764658f09f44bbb29978b48c4bd4321e3af5caa0874dc0b8e16286c742862909268c2a91164566a9de3d362a8debf2dedc76d738fd49ebb9f7",
                latitude: "75f924cea30a4c0c8ce449ffab8ae05dcd209be5c57541b7b91d8d66a99bdc9f3a3e6e85c04a4d1389509ad1b4657b0060fb96ed78af4925ad606e4eadb7f2e2d1b6f8ed2e5a4f19b207ebc5fa1b1d62cb6d533145ba4ff3b12708f28c2c3175f174a0f9423041a3a4196cb52290f2e29527afe96ddb4854ac51f3114c508a8",
                longitude: "610ac0269a704f19a162bef2c2f226fd0b19eda413844f519f69ab17da232a8e25c57463b63949fca39c508a1fbb63d9c2e5050f569e4c4eaa24d4f5b3bd229b89ac67cd7be64320a755f2a802dda8181a5bd26957854c0592b1c242181e2d3635ea97149f5b4c14801540e0fbc815d26cf9ff92ef81453fb48caa80351fa59",
                sfaCustomerCode: "1b83add35af94a738d83",
                lastOrderDate: new DateTime(2017, 6, 12),
                paymentTermId: null,
                linkedCompanyId: null,
                priceListId: Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),
                geoMaster0Id: null,
                geoMaster1Id: null,
                geoMaster2Id: null,
                geoMaster3Id: null,
                geoMaster4Id: null,
                attribute0Id: null,
                attribute1Id: null,
                attribute2Id: null,
                attribute3Id: null,
                attribute4Id: null,
                attribute5Id: null,
                attribute6Id: null,
                attribute7Id: null,
                attribute8Id: null,
                attribute9Id: null,
                attribute10Id: null,
                attribute11Id: null,
                attribute12Id: null,
                attribute13Id: null,
                attribute14Id: null,
                attribute15Id: null,
                attribute16Id: null,
                attribute1I7d: null,
                attribute18Id: null,
                attribute19Id: null,
                paymentId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}