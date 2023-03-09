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
                id: Guid.Parse("a556dc21-77d6-433f-a939-7df8fbfa19fa"),
                startNumber: 1704252903,
                prefix: "0b213f7643da4d708d51",
                suffix: "5b8fa92949694ab69be0",
                length: 1942562675,
                active: true,
                description: "9ecd5e4fccd241d68575831b51618a3977d00ffb8dcc4b61be1586919dacae10e8d66b3927e74369b68ed3af6305591e8c4df0387b2f4d29bdc262637d697e0cdfaa46f36a284a72b70fe7ca417d80550b2b7aaf6c804debaee0d732df24e4a1f9da6f9ff7894ba5809183a607ef6c7226246c4c11974b2b9f5e3cd686c94c3",
                systemDataId: null
            ));

            await _numberingConfigRepository.InsertAsync(new NumberingConfig
            (
                id: Guid.Parse("56a87130-e867-4adc-8f16-cfae98c4a319"),
                startNumber: 999411878,
                prefix: "e988d203e2fc4e2a9bac",
                suffix: "5ff3d326937d42e9b8d6",
                length: 2077347358,
                active: true,
                description: "36fe5ac000fe4917af3fdbacf42751e4d577e247dca24e8895add2b3c4c723bd0a920079a1814b33a09c6a8664077c2f5f557b862efc4128be50349089f7eae14d1b354bbe8d47d8b3e8f258430a6142fd2f8296b94347409a0c9394a9e530db5532b393287f4d94ba67df0aba3be5df2576d8b1763746a2b79e990180284a1",
                systemDataId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}