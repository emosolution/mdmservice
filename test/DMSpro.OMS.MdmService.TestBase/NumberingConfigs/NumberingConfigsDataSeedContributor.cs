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
                id: Guid.Parse("e4a8349f-e1bc-4217-a569-6829fb5322e3"),
                startNumber: 424152740,
                prefix: "e88fabc858b34177b150",
                suffix: "020e39c4c8de4d80aca5",
                length: 1230255079,
                active: true,
                systemDataId: null
            ));

            await _numberingConfigRepository.InsertAsync(new NumberingConfig
            (
                id: Guid.Parse("ed8dc997-c9c1-4312-80a6-3f858ade8e23"),
                startNumber: 1633873877,
                prefix: "522c941224c645139c05",
                suffix: "66ebea96bd524154a081",
                length: 1411223853,
                active: true,
                systemDataId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}