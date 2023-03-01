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
                id: Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f"),
                code: "acab52ffbc854143ac04",
                name: "6beb579cea5f435c94847fb9a8829f5662f6485a4bc84d92bc6fa0f2f02a0bec525aa3710f754b68824fdeceec1cc8a3cc4606ff3c204f7b8f0cd0d34a79821866d156bc319d4d1b998ec6cd12ba86de64cecda113bb40739d21a6278d6d942f1022bb9ea7a9414f847ba4a5ab2d8e3ff454b73fd73b4c7694b26bf5478256b",
                active: true,
                effectiveDate: new DateTime(2018, 1, 5),
                groupBy: default,
                status: default
            ));

            await _customerGroupRepository.InsertAsync(new CustomerGroup
            (
                id: Guid.Parse("f28f4bcc-161b-4637-82f9-478e41abb83c"),
                code: "ee24015727c94765acf5",
                name: "9e0df906e2eb4d68a02d8763cdae3ca5bd5c940d94ba433da2fedb32464a0f2fc8f41ab68c624e0da5b36874eb2685fa458a623e5f7c49fc8d63566cb1e649272f265e3f64b94f28bc7dce1cc25edf438b6e5b223d1240cf85b8d411361b9b075a925de5c3ec44ec982b5c135add07ef5a13d7c4a98944d4ba9d71e648dde72",
                active: true,
                effectiveDate: new DateTime(2001, 5, 18),
                groupBy: default,
                status: default
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}