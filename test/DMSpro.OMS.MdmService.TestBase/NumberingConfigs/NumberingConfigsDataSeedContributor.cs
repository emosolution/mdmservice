using DMSpro.OMS.MdmService.SystemDatas;
using DMSpro.OMS.MdmService.Companies;
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
        private readonly CompaniesDataSeedContributor _companiesDataSeedContributor;

        private readonly SystemDatasDataSeedContributor _systemDatasDataSeedContributor;

        public NumberingConfigsDataSeedContributor(INumberingConfigRepository numberingConfigRepository, IUnitOfWorkManager unitOfWorkManager, CompaniesDataSeedContributor companiesDataSeedContributor, SystemDatasDataSeedContributor systemDatasDataSeedContributor)
        {
            _numberingConfigRepository = numberingConfigRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _companiesDataSeedContributor = companiesDataSeedContributor; _systemDatasDataSeedContributor = systemDatasDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _companiesDataSeedContributor.SeedAsync(context);
            await _systemDatasDataSeedContributor.SeedAsync(context);

            await _numberingConfigRepository.InsertAsync(new NumberingConfig
            (
                id: Guid.Parse("92d02adb-9081-4fbb-9c12-9da8c78d2b99"),
                startNumber: 487928377,
                prefix: "1b5c519893704503a0cc48f9967a20dfa681d0b9c",
                suffix: "8e219193e12d4828822202f61229e8e57ae95c59e93342ef98c90893e3d3975f2509ee8e1e854e92",
                length: 769435237,
                companyId: null,
                systemDataId: null
            ));

            await _numberingConfigRepository.InsertAsync(new NumberingConfig
            (
                id: Guid.Parse("e4cff639-7b5a-4b93-9419-ac80032fdbcd"),
                startNumber: 1984986498,
                prefix: "18fdec2d7b23403e8913f8f9af7c164597fc0ed8e43c4383b2ba028f4",
                suffix: "81edc3cfd4ff4e8da68126914391c39f30041051520343258791d",
                length: 240440849,
                companyId: null,
                systemDataId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}