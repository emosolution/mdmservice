using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.CustomerGroups;

namespace DMSpro.OMS.MdmService.CustomerGroups
{
    public class CustomerGroupsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICustomerGroupRepository _customerGroupRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public CustomerGroupsDataSeedContributor(ICustomerGroupRepository customerGroupRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _customerGroupRepository = customerGroupRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _customerGroupRepository.InsertAsync(new CustomerGroup
            (
                id: Guid.Parse("a53045fa-bd3b-449b-97f1-8e265b1358f4"),
                code: "da5d436cd5954d88a90e",
                name: "784ea625b4f5411e9a00e4c3bf99c095936f4c5865e648898c3cf820c10cab9328e1a3cfebec4b9d96172c4b4",
                active: true,
                effectiveDate: new DateTime(2000, 1, 10),
                groupBy: default,
                status: default
            ));

            await _customerGroupRepository.InsertAsync(new CustomerGroup
            (
                id: Guid.Parse("e80e8de3-f2ab-4d55-a329-bf3cbc47e66a"),
                code: "4e439fb805ab45d09292",
                name: "838838978fc84b31be44077763b283e0dc2d41ee27dd4947a589d4927796c486ccd72c2da37f4aa3a8b088d29",
                active: true,
                effectiveDate: new DateTime(2010, 1, 18),
                groupBy: default,
                status: default
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}