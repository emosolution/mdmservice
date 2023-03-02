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
                id: Guid.Parse("4308f81c-1cb1-418e-b180-430d2e91fdbf"),
                code: "e305d36f333646b5842a",
                name: "3297f6947f424f80aeb22b3dde2f0891ffd31a2553494b9e8e95757770ef602b3652c7ee7a424daa8437342097e84f9a7349e650e9634cc4a438c67f39966b79c51edce3c0af47ca8daa5e1101291e7c24183db2d464421db2873e70e524ccab095b2227b0564666bbe90e75bd9861b59b6630747c33436e88ea606a3aead0b",
                active: true
            ));

            await _salesOrgHeaderRepository.InsertAsync(new SalesOrgHeader
            (
                id: Guid.Parse("7753baa0-bd90-49f3-87ac-4cc3c687e075"),
                code: "7f596c4a8dfc495bb94f",
                name: "aec38aa60f534493af8d629925275c0b615ec763ccda4322b2b4a390eafc2e0acde6a03d60c64a37813de39cfeebd2d1e061288fd3c84ec38cccc7582b9448e41be5a7794efd4947b0b4bbd67dcf202262a8d0778c714a4591592c60559909b07d07d87dfad745cc889d986f2760f66af8c0b51c9493449c828dd4def4163e8",
                active: true
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}