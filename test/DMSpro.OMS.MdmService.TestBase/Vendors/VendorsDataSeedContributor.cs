using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.Companies;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.Vendors;

namespace DMSpro.OMS.MdmService.Vendors
{
    public class VendorsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IVendorRepository _vendorRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly PriceListsDataSeedContributor _priceListsDataSeedContributor;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor0;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor1;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor2;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor3;

        private readonly GeoMastersDataSeedContributor _geoMastersDataSeedContributor4;

        private readonly CompaniesDataSeedContributor _companiesDataSeedContributor0;

        private readonly CompaniesDataSeedContributor _companiesDataSeedContributor1;

        public VendorsDataSeedContributor(IVendorRepository vendorRepository, 
            IUnitOfWorkManager unitOfWorkManager, 
            PriceListsDataSeedContributor priceListsDataSeedContributor, 
            GeoMastersDataSeedContributor geoMastersDataSeedContributor0, 
            GeoMastersDataSeedContributor geoMastersDataSeedContributor1, 
            GeoMastersDataSeedContributor geoMastersDataSeedContributor2, 
            GeoMastersDataSeedContributor geoMastersDataSeedContributor3, 
            GeoMastersDataSeedContributor geoMastersDataSeedContributor4, 
            CompaniesDataSeedContributor companiesDataSeedContributor0, 
            CompaniesDataSeedContributor companiesDataSeedContributor1)
        {
            _vendorRepository = vendorRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _priceListsDataSeedContributor = priceListsDataSeedContributor; 
            _geoMastersDataSeedContributor0 = geoMastersDataSeedContributor0; 
            _geoMastersDataSeedContributor1 = geoMastersDataSeedContributor1; 
            _geoMastersDataSeedContributor2 = geoMastersDataSeedContributor2; 
            _geoMastersDataSeedContributor3 = geoMastersDataSeedContributor3; 
            _geoMastersDataSeedContributor4 = geoMastersDataSeedContributor4; 
            _companiesDataSeedContributor0 = companiesDataSeedContributor0; 
            _companiesDataSeedContributor1 = companiesDataSeedContributor1;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _priceListsDataSeedContributor.SeedAsync(context);
            await _geoMastersDataSeedContributor0.SeedAsync(context);
            await _geoMastersDataSeedContributor1.SeedAsync(context);
            await _geoMastersDataSeedContributor2.SeedAsync(context);
            await _geoMastersDataSeedContributor3.SeedAsync(context);
            await _geoMastersDataSeedContributor4.SeedAsync(context);
            await _companiesDataSeedContributor0.SeedAsync(context);
            await _companiesDataSeedContributor1.SeedAsync(context);

            await _vendorRepository.InsertAsync(new Vendor
            (
                id: Guid.Parse("1f7f0feb-4286-440d-86d0-3e8dba6e75fa"),
                code: "dd9eb362fae14ee8a717",
                name: "deddb2738ee547f2837201973a4aafeb0b126acd4d7040febdfcfd2a747b8d271ded8c5251d2479ca6d758f2c1c217708e4f877d639a4fb0ad0d2f77352e5d4788ab7381cb9949b9a9765b7f686c285f8f4850196562456598346483be51f4edb09bff8c",
                shortName: "68cf7ec173c54907aedad183d2be0c3e8c8d35f31e6d4a88b790f8ef485f788849bbef59f5744fe7ae6cf77aab190ae4a6c0d7b40ddc41a69c2b46dba5385ed641acdcc17ecd455c944ea0be83c2aee6c3ffdd28c438439784bd41db565720a965bcb4e9",
                phone1: "8c100812322745c190fd20f5131e0cf989e42ec53b02448993b6af9e3f6106aac3daba35b15b4a28be28c6a6f16b505a64821c26ecf94e3f936abf66263de70c01b344edbef84ae4a4f29e1da84a5e1e112b69d3e4e54744b7de75304bcf79bdb5a7f5c9c7914b27b0c06c2a518abe75c2d4132028b84d5596607a16336697d",
                phone2: "509a02fbda104abcac7de5848422d8e0f37d81cbabcc4c10b6737a1c9ebe40b1f22357e58d334cbc8503380d6ed81af49eba4193304e4a3ab4322cab100891bcf64b8f9da3b7446eaa6498ebe8380762b9e28c9eb146477d8cf5bae97b1f8298939aee196ea84039b5dc51c8ff37defc6a7681925e164579a2c6dd9ca8fa360",
                erpCode: "a9d7f4aa70634b39b00188c8da5d4d353e0a92d79c4f407fb93ad7a0b3244d8b73b7d8c3d62c424091443edc2235a0bf51ce8e5f0a0b40a5804da268feac707d7a43a4a1ed24403a999dea018d9193f6ab70e625d2c04fb784b90531192fb222c686fd099b4941f6b83f9f2bab77a5716095239125e3414bad557d726550040",
                active: true,
                endDate: new DateTime(2000, 6, 24),
                street: "b56ec0ae08a9430faf0e8a7e27576ed5b3631e0f808b4452a11d08eea864bfc0c6c13be2a0cb4d38a878f0ca0f41b0092279c6ef72dc422d9e36cee25cc2cc0e25931b3b2bf2436bb62332f9179fd98d502d363bbacf48f188ea1250cd40201aa8bc65894cd24f2e9fc851642a360b0f7ecbdc5f4ba84e42891dfbf4aad8a86",
                address: "e2c369ae8d7c4640ab07575a342dcb512a39820ee30e4e60941addb782298edc0461b49025f249568d9be2a2d69bf1219c7ddb231f30455683126b28675108d1dc74e8a7927e49919c6a6e913c6087067df06fd831e947fdbfd25964ab9e41adb0916b45be0f456a872d2e095587f1d0eed88d5cfd904319a0961a766af5b778455cfe4cf0484bf387ada088c4de0a3c81139736ca41461fb7923a4ec2edd21a164ce24126e74eb79558f725dfdd276d78f82b9684f24819b99abc5cd0825a4aeaa3549ebea84095b737f468a6915646c0593bf383c843b8b66342b1b51e73565eef945892bb4cdba1fca2d19c19d88511de7b0ca44e448ca1e7",
                latitude: "6756d267e14542b18f46bfe6b48c67886a5feccb5766456d92a010f8ff76a49ddcc3d2dee8174469a7cf613d24d5c75f79b21a5dfa3d46689d06e418a713d0f7d81564fffc7d470cb27a32d9ae1ce3ee64447cf367374a059cb52b6cd5034603e2f05dd748404bd6bd50b503b598c6ae426f21cffa98442abbaf5569c13e448",
                longitude: "8e047cda24dd4fa18dc058bc5fd4e6ada36e45acf38d43f08fa533dbbb29ab0c01c1fc8299aa4d47827a8ea65a9d09e5cc0678dcbb95496694ffa9ac6f5e5eb7ce61f5cc90d34bdaa73760d9911d9484af375ba7a34c474680acab74b81c36f9d20a7a3df8a841c691287063bbb91bde8a25a5ee74c64fac91be1dbf27bf3c1",
                priceListId: Guid.Parse("587b2afd-c04a-4dda-bcb1-759beb5e3a41"),
                geoMaster0Id: null,
                geoMaster1Id: null,
                geoMaster2Id: null,
                geoMaster3Id: null,
                geoMaster4Id: null,
                companyId: Guid.Parse("b0aca71d-adf2-47c1-a39a-b1e2ff33dcfc"),
                linkedCompanyId: null
            ));

            await _vendorRepository.InsertAsync(new Vendor
            (
                id: Guid.Parse("2341d664-3a18-4717-96e4-fc7326ea3bac"),
                code: "54678076804d49c2bd7b",
                name: "e975ee149731419f82bf0a2014be4eb45af9060948be446b9794a5dc0002e3eb8116e9651e6947d6adacbfec901bec2b287ae10b52d3456c8bbb2ff26985c500b530b9fff6594db290f956940601cb629709264e1d7a4a9ba94a5563b113c914010dff10",
                shortName: "e41d6362c0864accb33f4d5aed1c2b88d4e2d0a64b8d4b04a707a6b491e0d91194af30b38ceb42c589c79382229dcbe916a653ce96e1426a9e93a6478e41e173fc5d3bbf75144af5917686e2cf4c56dd0d010d55a0634a679f00da5639298051cf66e78f",
                phone1: "6524077a079744fd8ae8c1f0bfecc825e5505e662e944b36832e4a215e8d5b7398b9db9da93d46418dbea8a4313072a7c306a97baeff4758b3f73c5e840b5aaa1ee3c9baf2aa4abf81242195d7372ff758098806b8774d269912b1ff2fb755c9ddc22dac688e45d286450a165dfc6bbc266d3fc2fc18401a9174f63e24d5f05",
                phone2: "a12a6aeb694d4ad195989fdb1f25fbc02c9a55458fd3429dbc6ed8f5f0d9e448e9a219732013470da07be013913ccaac0aade8134ebd4612b27c07b9fced3dd618e0e2aceeaf425fb6cafa4004a12b970334e2678527482a9017539954c189b8a2b78884f52b47e382032470d001b2c696cae83639514a4f8663c3bd9e3061f",
                erpCode: "22173d8595c04500a434e89be694c8d83d23d5fd9f0e4e8bb40a6db8d59ff9c7a535f0764b034188b8fcc73bfa2fbf2928ad4e3103704b9094359e8826f3adad04717ea746944521a0ff0743864a18f3afa5d2fa22ac4486a425a678ac471d25fb48b04fac5a43148ddfa9d006061644be92a54ae0f54ca0bf0458c47f71e2e",
                active: true,
                endDate: new DateTime(2011, 7, 26),
                street: "cab271bccf1848f1b3f8bf0ae67e4a1154f63e8ccc6141718d5a61fa3faa3f9b4fc44535a82b4dd6961a4d93e625a974354827c5720d40d5b491cac1364dbc0aa287f5c2879f49e7aef12f2806045aa064a54915026843bfbb265c66a1013e0ffa650801142242a2be7ff62c9d9b4e5e86c3848f9f2d4ebd8321a65ccfa4258",
                address: "bf767d3e65f94d05bb3c794e7e02d57a81b6bd89f94e49cda8aae10d325da63ea362834515cd44b78cc6aaeb68c966a852ca17190ea048989246d90d4b11407441e1f22f6f44413abca067a4074f64685018db44bb514d5099efa5c49504561989cfa6bab1984d10b944f5a76bbccc05b01997d4907c4747aa2d326e3327c0ff9a2cdc7b325c48fc9620d699d3f0190373b03be67c6b4204b1368a433b4da5bb85d090e2b64544d1be8abd16f2021a8e524618054ad542dc84cbadc9942eacf25e6fdb3886c1428680390091143b4fbfc0f8b14b60b948059378da434c8ed34041bfb2ab24924125867c8deb74385d108e6d88c5385b436390e4",
                latitude: "466e742314c14f7ebc2f9a02dd042be605e428c478ef414b90ff6e42bf6548b92ac4850698ce4168b7edce1546a8848ddea543110db140ee8984a40c58820ee53f29d01277504735b6efbcd7eb88d8ae60504d0c6847482b8bb904f7c6ca03aac8a5ba7318794d28a3443aad57de98ee06884f7e34254a5b9e283a34dac62b1",
                longitude: "f9aa6be3dbb74c6f866bbbe29907df141340219a89304ba1af691067c90a316a86b1393512f643e98a73448be280a6457bede0aecc824454a19a4beeda4175fcbaa3d75ff56c42cf99c2fb58aef5d1764a3b373e4e134f45849243a9ce03a4d2ec7a7fc17281484f9bce8b60ca73ee561be9a06dc84641dbb33a7902bf450a1",
                priceListId: Guid.Parse("587b2afd-c04a-4dda-bcb1-759beb5e3a41"),
                geoMaster0Id: null,
                geoMaster1Id: null,
                geoMaster2Id: null,
                geoMaster3Id: null,
                geoMaster4Id: null,
                companyId: Guid.Parse("b0aca71d-adf2-47c1-a39a-b1e2ff33dcfc"),
                linkedCompanyId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}