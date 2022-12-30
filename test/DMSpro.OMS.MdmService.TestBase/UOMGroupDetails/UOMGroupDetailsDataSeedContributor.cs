using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.UOMGroups;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.UOMGroupDetails;

namespace DMSpro.OMS.MdmService.UOMGroupDetails
{
    public class UOMGroupDetailsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IUOMGroupDetailRepository _uOMGroupDetailRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly UOMGroupsDataSeedContributor _uOMGroupsDataSeedContributor;

        private readonly UOMsDataSeedContributor _altUOMsDataSeedContributor;

        private readonly UOMsDataSeedContributor _baseUOMsDataSeedContributor;

        public UOMGroupDetailsDataSeedContributor(IUOMGroupDetailRepository uOMGroupDetailRepository, IUnitOfWorkManager unitOfWorkManager,
            UOMGroupsDataSeedContributor uOMGroupsDataSeedContributor, UOMsDataSeedContributor altUOMsDataSeedContributor, UOMsDataSeedContributor baseUOMsDataSeedContributor)
        {
            _uOMGroupDetailRepository = uOMGroupDetailRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _uOMGroupsDataSeedContributor = uOMGroupsDataSeedContributor;
            _altUOMsDataSeedContributor = altUOMsDataSeedContributor;
            _baseUOMsDataSeedContributor = baseUOMsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _uOMGroupsDataSeedContributor.SeedAsync(context);
            await _altUOMsDataSeedContributor.SeedAsync(context);
            await _baseUOMsDataSeedContributor.SeedAsync(context);

            await _uOMGroupDetailRepository.InsertAsync(new UOMGroupDetail
            (
                id: Guid.Parse("dc465e6a-f105-4761-b8f2-8a6b21e85a62"),
                altQty: 1618281784,
                baseQty: 1347377494,
                active: true,
                uOMGroupId: Guid.Parse("f8b2ef88-6487-4c6a-9ea9-45604bc8756a"),
                altUOMId: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                baseUOMId: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65")
            ));

            await _uOMGroupDetailRepository.InsertAsync(new UOMGroupDetail
            (
                id: Guid.Parse("f852c863-a264-41ae-8658-d3d94df2bb22"),
                altQty: 724460206,
                baseQty: 1867278047,
                active: true,
                uOMGroupId: Guid.Parse("f8b2ef88-6487-4c6a-9ea9-45604bc8756a"),
                altUOMId: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65"),
                baseUOMId: Guid.Parse("805b2e46-7e18-44a4-8c46-20f77fc9de65")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}