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
                id: Guid.Parse("6e78fef6-50e3-40a1-b9fb-3124691bbd21"),
                code: "d0cfcea721d84862b45a",
                name: "ba69ba2ea77648378bcc4c0ae7bcab69253b3bc3c72a449780328c04450a673ddc2cceaee9fe4d72b4280c4f1519e1db2f83837e36aa4222a993aba0bb91687373949992c4794f5fb5bbfc694046365c14ce5afc258e4b37bd2fe306f50299f037d36bcb502a431e92283303e515e3efd3e8bd821af144c796e53336b83fff5",
                selectable: true,
                groupBy: default,
                status: default,
                description: "8e7031dd716745fcb47df0c7e4eb28da51fc8acdb5f04c10bfd0fe8d6d9e1a95e8e9a2d59d74433abdb78a7c0f9b117cd5eb2f7ec11d4f2f9e373d68a83c17291f4dec7ef31d4d898f6b2bbb64bb90114c4534cac236482ea00cf85d525cdc2b7001c844bf2147a6a633abf866754504b24aee5711234908b68b46176f084d9"
            ));

            await _customerGroupRepository.InsertAsync(new CustomerGroup
            (
                id: Guid.Parse("95fce899-bd21-4400-9f35-12c36d113f82"),
                code: "9c55e8f8fdd44c3a8523",
                name: "16162777e69f47208d6e0eddb1521a039203c53c07ac464087d2d67e8975deb91b2ef70988884a43b79b8e62bf896f7a3c77790e22ce4183b38dd591e8adec50252c35b159174d1bb22d7001fe390c3828ba058df8d543698bba4640bdeff9e90b3b3be84a454682866605449e23739accd173b2671844ad8d0077c863a843d",
                selectable: true,
                groupBy: default,
                status: default,
                description: "8be4d02409844e739937b4fc2d6c4fe35e5d2fdc9c034aa7b346513e78b888da52885809e81d451d8e75b432b45e003e6f3ec82a253242da838b530117a9838a9c1b1f002c6b4e45909fce9a27e77c3ead720bd944ea4eadae013245aef549bdc48d20393f544a59b8ce804e88199fe7bf5856d75e7a4436b13b44c21b05594"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}