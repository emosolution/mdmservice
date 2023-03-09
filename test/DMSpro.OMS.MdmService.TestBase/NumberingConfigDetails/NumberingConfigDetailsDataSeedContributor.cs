using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.NumberingConfigs;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.NumberingConfigDetails;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public class NumberingConfigDetailsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly INumberingConfigDetailRepository _numberingConfigDetailRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly NumberingConfigsDataSeedContributor _numberingConfigsDataSeedContributor;

        private readonly CompaniesDataSeedContributor _companiesDataSeedContributor;

        public NumberingConfigDetailsDataSeedContributor(INumberingConfigDetailRepository numberingConfigDetailRepository, IUnitOfWorkManager unitOfWorkManager, NumberingConfigsDataSeedContributor numberingConfigsDataSeedContributor, CompaniesDataSeedContributor companiesDataSeedContributor)
        {
            _numberingConfigDetailRepository = numberingConfigDetailRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _numberingConfigsDataSeedContributor = numberingConfigsDataSeedContributor; _companiesDataSeedContributor = companiesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _numberingConfigsDataSeedContributor.SeedAsync(context);
            await _companiesDataSeedContributor.SeedAsync(context);

            await _numberingConfigDetailRepository.InsertAsync(new NumberingConfigDetail
            (
                id: Guid.Parse("d9aa889f-a795-4808-a7b7-8388d8a85a7d"),
                active: true,
                description: "92e10b91f7e34bdfa50900f8afd3cc0e270cd6a8f3ec45e5a5a20f4dfa713124b11d11353b5f4a958da2f47cc831ddc1e75f0cc1dd0e4ad784335cb0611b9cbd4ddc22e36b354436a6e2736d42e9c7f1bcf351d71e6a49cda484ab914885862ea0b4a99da2d4402696ff8853366c4d950b21f456c71149828d7ed507052e9d1",
                numberingConfigId: Guid.Parse("a556dc21-77d6-433f-a939-7df8fbfa19fa"),
                companyId: Guid.Parse("b0aca71d-adf2-47c1-a39a-b1e2ff33dcfc")
            ));

            await _numberingConfigDetailRepository.InsertAsync(new NumberingConfigDetail
            (
                id: Guid.Parse("3d05a31b-7f2b-45cc-9107-9ad156da00da"),
                active: true,
                description: "6a3b7f00a8304577b8dae685691f440710c0af59b8af4ba19b56859d38e9b8624e688b8297314e70b5282cf346baf0e733f134edc30148b8a087b4997f7040c7881cd3100ef7423f869207cf71fe4e93e26b55a5d4764f3088849cfa5d7d960c33d67ebc313b42bdb853231f635adbc1de2e025c2e0f48598d00a2b31fdd4b5",
                numberingConfigId: Guid.Parse("a556dc21-77d6-433f-a939-7df8fbfa19fa"),
                companyId: Guid.Parse("b0aca71d-adf2-47c1-a39a-b1e2ff33dcfc")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}