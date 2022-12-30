using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.SalesOrgHeaders;

namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{
    public class SalesOrgHeadersDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ISalesOrgHeaderRepository _salesOrgHeaderRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public SalesOrgHeadersDataSeedContributor(ISalesOrgHeaderRepository salesOrgHeaderRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _salesOrgHeaderRepository = salesOrgHeaderRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _salesOrgHeaderRepository.InsertAsync(new SalesOrgHeader
            (
                id: Guid.Parse("df3df657-5875-4172-b267-cc75c79376a9"),
                code: "7efb90e3416f417d8b32",
                name: "a5b3e11d58e04aec8c177017c7cf1dc424c29a1f02574944a7d1c6f758e3b7a19fa52c8",
                active: true
            ));

            await _salesOrgHeaderRepository.InsertAsync(new SalesOrgHeader
            (
                id: Guid.Parse("ba3324d2-dfa8-40fa-99da-4b4207f1f6ad"),
                code: "c1ed60a7dee949ce9441",
                name: "854ee42703ce4e29863ce9691dcca6dc71379046b2e04f3d91bba8f71c62c44ad86781c8300e46ecac4f99",
                active: true
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}