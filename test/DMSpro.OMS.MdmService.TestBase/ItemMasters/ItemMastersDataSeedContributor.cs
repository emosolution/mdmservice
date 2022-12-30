using DMSpro.OMS.MdmService.ProdAttributeValues;
using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.UOMGroups;
using DMSpro.OMS.MdmService.VATs;
using DMSpro.OMS.MdmService.SystemDatas;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.ItemMasters;

namespace DMSpro.OMS.MdmService.ItemMasters
{
    public class ItemMastersDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IItemMasterRepository _itemMasterRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly SystemDatasDataSeedContributor _systemDatasDataSeedContributor;

        private readonly VATsDataSeedContributor _vATsDataSeedContributor;

        private readonly UOMGroupsDataSeedContributor _uOMGroupsDataSeedContributor;

        private readonly UOMsDataSeedContributor _uOMsDataInvUnitSeedContributor;

        private readonly UOMsDataSeedContributor _uOMsDataPurUnitSeedContributor;

        private readonly UOMsDataSeedContributor _uOMsDataSalesUnitSeedContributor;


        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues0DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues1DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues2DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues3DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues4DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues5DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues6DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues7DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues8DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues9DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues10DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues11DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues12DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues13DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues14DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues15DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues16DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues17DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues18DataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValues19DataSeedContributor;

        public ItemMastersDataSeedContributor(IItemMasterRepository itemMasterRepository, IUnitOfWorkManager unitOfWorkManager, 
            SystemDatasDataSeedContributor systemDatasDataSeedContributor, VATsDataSeedContributor vATsDataSeedContributor, 
            UOMGroupsDataSeedContributor uOMGroupsDataSeedContributor, UOMsDataSeedContributor uOMsDataInvUnitSeedContributor, 
            UOMsDataSeedContributor uOMsDataPurUnitSeedContributor, UOMsDataSeedContributor uOMsDataSalesUnitSeedContributor, 
            ProdAttributeValuesDataSeedContributor prodAttributeValues0DataSeedContributor, ProdAttributeValuesDataSeedContributor prodAttributeValues1DataSeedContributor, 
            ProdAttributeValuesDataSeedContributor prodAttributeValues2DataSeedContributor, ProdAttributeValuesDataSeedContributor prodAttributeValues3DataSeedContributor, 
            ProdAttributeValuesDataSeedContributor prodAttributeValues4DataSeedContributor, ProdAttributeValuesDataSeedContributor prodAttributeValues5DataSeedContributor, 
            ProdAttributeValuesDataSeedContributor prodAttributeValues6DataSeedContributor, ProdAttributeValuesDataSeedContributor prodAttributeValues7DataSeedContributor, 
            ProdAttributeValuesDataSeedContributor prodAttributeValues8DataSeedContributor, ProdAttributeValuesDataSeedContributor prodAttributeValues9DataSeedContributor, 
            ProdAttributeValuesDataSeedContributor prodAttributeValues10DataSeedContributor, ProdAttributeValuesDataSeedContributor prodAttributeValues11DataSeedContributor, 
            ProdAttributeValuesDataSeedContributor prodAttributeValues12DataSeedContributor, ProdAttributeValuesDataSeedContributor prodAttributeValues13DataSeedContributor, 
            ProdAttributeValuesDataSeedContributor prodAttributeValues14DataSeedContributor, ProdAttributeValuesDataSeedContributor prodAttributeValues15DataSeedContributor, 
            ProdAttributeValuesDataSeedContributor prodAttributeValues16DataSeedContributor, ProdAttributeValuesDataSeedContributor prodAttributeValues17DataSeedContributor, 
            ProdAttributeValuesDataSeedContributor prodAttributeValues18DataSeedContributor, ProdAttributeValuesDataSeedContributor prodAttributeValues19DataSeedContributor
            )
        {
            _itemMasterRepository = itemMasterRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _systemDatasDataSeedContributor = systemDatasDataSeedContributor; _vATsDataSeedContributor = vATsDataSeedContributor;
            _uOMGroupsDataSeedContributor = uOMGroupsDataSeedContributor;
            _uOMsDataInvUnitSeedContributor = uOMsDataInvUnitSeedContributor;
            _uOMsDataPurUnitSeedContributor = uOMsDataPurUnitSeedContributor;
            _uOMsDataSalesUnitSeedContributor = uOMsDataSalesUnitSeedContributor;
            _prodAttributeValues0DataSeedContributor = prodAttributeValues0DataSeedContributor;
            _prodAttributeValues1DataSeedContributor = prodAttributeValues1DataSeedContributor;
            _prodAttributeValues2DataSeedContributor = prodAttributeValues2DataSeedContributor;
            _prodAttributeValues3DataSeedContributor = prodAttributeValues3DataSeedContributor;
            _prodAttributeValues4DataSeedContributor = prodAttributeValues4DataSeedContributor;
            _prodAttributeValues5DataSeedContributor = prodAttributeValues5DataSeedContributor;
            _prodAttributeValues6DataSeedContributor = prodAttributeValues6DataSeedContributor;
            _prodAttributeValues7DataSeedContributor = prodAttributeValues7DataSeedContributor;
            _prodAttributeValues8DataSeedContributor = prodAttributeValues8DataSeedContributor;
            _prodAttributeValues9DataSeedContributor = prodAttributeValues9DataSeedContributor;
            _prodAttributeValues10DataSeedContributor = prodAttributeValues10DataSeedContributor;
            _prodAttributeValues11DataSeedContributor = prodAttributeValues11DataSeedContributor;
            _prodAttributeValues12DataSeedContributor = prodAttributeValues12DataSeedContributor;
            _prodAttributeValues13DataSeedContributor = prodAttributeValues13DataSeedContributor;
            _prodAttributeValues14DataSeedContributor = prodAttributeValues14DataSeedContributor;
            _prodAttributeValues15DataSeedContributor = prodAttributeValues15DataSeedContributor;
            _prodAttributeValues16DataSeedContributor = prodAttributeValues16DataSeedContributor;
            _prodAttributeValues17DataSeedContributor = prodAttributeValues17DataSeedContributor;
            _prodAttributeValues18DataSeedContributor = prodAttributeValues18DataSeedContributor;
            _prodAttributeValues19DataSeedContributor = prodAttributeValues19DataSeedContributor;
            }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _systemDatasDataSeedContributor.SeedAsync(context);
            await _vATsDataSeedContributor.SeedAsync(context);
            await _uOMGroupsDataSeedContributor.SeedAsync(context);
            await _uOMsDataInvUnitSeedContributor.SeedAsync(context);
            await _uOMsDataPurUnitSeedContributor.SeedAsync(context);
            await _uOMsDataSalesUnitSeedContributor.SeedAsync(context);
            await _prodAttributeValues0DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues1DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues2DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues3DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues4DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues5DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues6DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues7DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues8DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues9DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues10DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues11DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues12DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues13DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues14DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues15DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues16DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues17DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues18DataSeedContributor.SeedAsync(context);
            await _prodAttributeValues19DataSeedContributor.SeedAsync(context);

            await _itemMasterRepository.InsertAsync(new ItemMaster
            (
                id: Guid.Parse("666f2923-445f-4a72-9068-7917e83b3464"),
                code: "439de29f65e24496bca3",
                name: "c5795ad41e60484c9c1c271b411e9f94e7e3a9ebc79940c2b64ac3197601ad187024296a564e4a909f7a5f02a51dda5baa384561d695494a9cb92002cabdfce90846dea92b6d41a7aa57f9b4d0936ed5ed34b7e1935b402cba7490559d996f4e9ea90cf384a04e78a352d6f532595233799b4ae4902346529ce59937f9a5787",
                shortName: "50449045cfbf4adcb88eb876185f5285ac16ddbdf82b4063b3329898ddc8c1097d597a24e58d441fa703be57142fe1a7537414997c714215a42dbbb7cfd817e42356b00d66dc4c17a22674a5d0e198f835a8cf8348314d5ab56c61f7c1aa0bec2222cb9754b74cc4bc1c8ec4b4e56461a702ebe04f9c48588327a3f45c7a6a7",
                erpCode: "582df895d75a4926bc26e06460000a95c9562c02f8e1417f90c12b0a064a9c0ab3753f3e512f455680ad9ee69f0b9d2cf4ef2798b91f4235b76d1da20ed2ecb83e5f1a6bb171449bb0b13040c3c18e93e3e7da9ee4ca4acfb2f20b9dfbca7d23a8f3f226eae845f4b1334b52044eba771290a14a993d49abb05dbfe4c92951a",
                barcode: "1238fb81640a46b1a1f92a8e3cd0b556c6bd988fbbed472a94449dc8fd4cf983fa73167c2ea24908afab402746f37bdef75c13004a384d54a67c20de69485bc0f7d7bd8d181e488c8305f7f94902e675bc7aad9909ef468a9dc9bd16b28dd28183fc58bf86bc4199801965a32af3851b7dba3c8b6a7f4206896efd7fc54fe8c",
                purchasble: true,
                saleable: true,
                inventoriable: true,
                active: true,
                manageType: default,
                expiredType: default,
                expiredValue: 16386477,
                issueMethod: default,
                canUpdate: true,
                basePrice: 229684128,
                itemTypeId: Guid.Parse("85a4dc75-61e7-42e3-87b7-da8004347d49"),
                vATId: Guid.Parse("4c99eb1e-38da-41e2-ac16-1914592fc547"),
                uOMGroupId: Guid.Parse("f8b2ef88-6487-4c6a-9ea9-45604bc8756a"),
                inventoryUnitId: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                purUnitId: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                salesUnit: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                attr0Id: null,
                attr1Id: null,
                attr2Id: null,
                attr3Id: null,
                attr4Id: null,
                attr5Id: null,
                attr6Id: null,
                attr7Id: null,
                attr8Id: null,
                attr9Id: null,
                attr10Id: null,
                attr11Id: null,
                attr12Id: null,
                attr13Id: null,
                attr14Id: null,
                attr15Id: null,
                attr16Id: null,
                attr17Id: null,
                attr18Id: null,
                attr19Id: null
            ));

            await _itemMasterRepository.InsertAsync(new ItemMaster
            (
                id: Guid.Parse("79ff2554-f670-4461-9400-fbc109666b2a"),
                code: "a66679ba9a294f7d83ef",
                name: "a8da0f78a89e4774986e4998370e99f971ede3920f7847f0a120f11d258285f6a7ff1447a28b4314b21d69e47f42d6c8aaf70fe128f74c019cd84c04efc8fdd951a07666f2fa47b891f103c3fe7921ef4a5faac9b3d84fd3b9c9b93822a47c1b9dd0627336474c3fa5b38805452cb90a1d4b1ad071f6454aa4150013c8341d7",
                shortName: "742a4ec32bea411b803e3a98376ed29e1588d0fb94cd4dd4a423e08ceecdefb7c327abe62b4d4ef8ba92ebbc47d8470ba5d044cfe06940c4ae5e8ceddb66fc36ade1db045dc34daabdfd0124f24aa4179882934c50a7492a8e4dad0e8d8733ebe3d49c7ec4e44386a8c6666128d49972743ee5b189b84104abca60b72122a48",
                erpCode: "636c4194e37343bb99d9d26d96b13cdc8478df7cd1ad4f13b8a7c8aea11760715ad5971898764decbf24e2fbd47c44e5eb92ea8aadfc449fada4ceb25193531bc7c286cdc4064d74b47f94223c71c7ba4249e6f1e99d491981018119b628bfe94ed9fe3f142041a8bac5e80f0bfc60f4e9756aa7d5f241c481713ec3db4dbb9",
                barcode: "59515adfdeff430685cba3754f9f58831ccfb722b8d34438b2b5a43811585c5d44877f7554f4446ea4f09c68f212b19af036dc64e79a47698cbfd029af292bf39c97981e218b40e29e6d8028a345109a639e02129f074bc9b598ab3ac257769db01374873f1f472d82656a2a9d93570119c983c2159c4e4eb51d2fa21349d2a",
                purchasble: true,
                saleable: true,
                inventoriable: true,
                active: true,
                manageType: default,
                expiredType: default,
                expiredValue: 1744743700,
                issueMethod: default,
                canUpdate: true,
                basePrice: 710476527,
                itemTypeId: Guid.Parse("85a4dc75-61e7-42e3-87b7-da8004347d49"),
                vATId: Guid.Parse("4c99eb1e-38da-41e2-ac16-1914592fc547"),
                uOMGroupId: Guid.Parse("f8b2ef88-6487-4c6a-9ea9-45604bc8756a"),
                inventoryUnitId: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                purUnitId: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                salesUnit: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                attr0Id: null,
                attr1Id: null,
                attr2Id: null,
                attr3Id: null,
                attr4Id: null,
                attr5Id: null,
                attr6Id: null,
                attr7Id: null,
                attr8Id: null,
                attr9Id: null,
                attr10Id: null,
                attr11Id: null,
                attr12Id: null,
                attr13Id: null,
                attr14Id: null,
                attr15Id: null,
                attr16Id: null,
                attr17Id: null,
                attr18Id: null,
                attr19Id: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}