using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.PriceLists;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.PricelistAssignments;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
    public class PricelistAssignmentsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IPricelistAssignmentRepository _pricelistAssignmentRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly PriceListsDataSeedContributor _priceListsDataSeedContributor;

        private readonly CustomerGroupsDataSeedContributor _customerGroupsDataSeedContributor;

        public PricelistAssignmentsDataSeedContributor(IPricelistAssignmentRepository pricelistAssignmentRepository, IUnitOfWorkManager unitOfWorkManager, PriceListsDataSeedContributor priceListsDataSeedContributor, CustomerGroupsDataSeedContributor customerGroupsDataSeedContributor)
        {
            _pricelistAssignmentRepository = pricelistAssignmentRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _priceListsDataSeedContributor = priceListsDataSeedContributor; _customerGroupsDataSeedContributor = customerGroupsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _priceListsDataSeedContributor.SeedAsync(context);
            await _customerGroupsDataSeedContributor.SeedAsync(context);

            await _pricelistAssignmentRepository.InsertAsync(new PricelistAssignment
            (
                id: Guid.Parse("7f82a888-1742-4659-bedc-bd7220496c3d"),
                description: "572121251a7540978bcfdf96956dea791b5fdef28f0244b790e5f115073250bf",
                priceListId: Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),
                customerGroupId: Guid.Parse("df6f7159-5418-4582-80bc-f56cf232a8b6")
            ));

            await _pricelistAssignmentRepository.InsertAsync(new PricelistAssignment
            (
                id: Guid.Parse("200a0319-42e3-49bc-9b7d-699ed6a8304b"),
                description: "4ef22585ae7c423aac290324c6550a06f49170e913e94129b167fcbf9208114f5d9f",
                priceListId: Guid.Parse("8c8c5f33-b4f5-48e0-895d-60f857e7b1f5"),
                customerGroupId: Guid.Parse("df6f7159-5418-4582-80bc-f56cf232a8b6")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}