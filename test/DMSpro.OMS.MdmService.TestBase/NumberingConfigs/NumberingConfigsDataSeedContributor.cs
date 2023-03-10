using DMSpro.OMS.MdmService.SystemDatas;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.NumberingConfigs;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public class NumberingConfigsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly INumberingConfigRepository _numberingConfigRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly SystemDatasDataSeedContributor _systemDatasDataSeedContributor;

        public NumberingConfigsDataSeedContributor(INumberingConfigRepository numberingConfigRepository, IUnitOfWorkManager unitOfWorkManager, SystemDatasDataSeedContributor systemDatasDataSeedContributor)
        {
            _numberingConfigRepository = numberingConfigRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _systemDatasDataSeedContributor = systemDatasDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _systemDatasDataSeedContributor.SeedAsync(context);

            await _numberingConfigRepository.InsertAsync(new NumberingConfig
            (
                id: Guid.Parse("1f96f177-3168-49f6-9fdd-97d2c5776f0a"),
                prefix: "135b9e74c0944c8499ed",
                suffix: "d8cbaf40cc6a484a8f47",
                paddingZeroNumber: 849203903,
                description: "29e19cc86aa645eda6d93fc383a6ac07002d99050a544389914880017591ab7906151163b69d43beae4c379b625edabf04956acd7f794d86817da1024fa633ccc00fd4fd60b64f0e9b3b9edb8bf777dd5f84ac4d0af844ada8edfbc6aa44b40d2872ca651a834ae19ba758ffc5be1eb22901bf6e72514c7aa6575ac87cbac2e",
                systemDataId: null
            ));

            await _numberingConfigRepository.InsertAsync(new NumberingConfig
            (
                id: Guid.Parse("0766bc5f-e627-4487-b6fe-f0c7b70568f4"),
                prefix: "de5fffeeebf943098aac",
                suffix: "cc1ec3971cf04b2fa8af",
                paddingZeroNumber: 665356493,
                description: "69f8d5291eba4ba0bbdcbf992fa3cef74168476ed58c443bb9bffec5dbb9dc23870c76ca3fb94344ad29f61d2faac5fe35e51f4864ac40c3b9d46bd862866896f362051a29604be192409fc637e6d7dcfeaebdab91044f5c82341900b5492073a90d9364176a46c495f0e32d0fe7ba7119ecbe21414a4adb89e23e39720dbe8",
                systemDataId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}