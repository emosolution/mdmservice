using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerGroups;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.CustomerGroupAttributes;

namespace DMSpro.OMS.MdmService.CustomerGroupAttributes
{
    public class CustomerGroupAttributesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICustomerGroupAttributeRepository _customerGroupAttributeRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CustomerGroupsDataSeedContributor _customerGroupsDataSeedContributor;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor;

        public CustomerGroupAttributesDataSeedContributor(ICustomerGroupAttributeRepository customerGroupAttributeRepository, IUnitOfWorkManager unitOfWorkManager, CustomerGroupsDataSeedContributor customerGroupsDataSeedContributor, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor)
        {
            _customerGroupAttributeRepository = customerGroupAttributeRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _customerGroupsDataSeedContributor = customerGroupsDataSeedContributor; _customerAttributeValuesDataSeedContributor = customerAttributeValuesDataSeedContributor; _customerAttributeValuesDataSeedContributor = customerAttributeValuesDataSeedContributor; _customerAttributeValuesDataSeedContributor = customerAttributeValuesDataSeedContributor; _customerAttributeValuesDataSeedContributor = customerAttributeValuesDataSeedContributor; _customerAttributeValuesDataSeedContributor = customerAttributeValuesDataSeedContributor; _customerAttributeValuesDataSeedContributor = customerAttributeValuesDataSeedContributor; _customerAttributeValuesDataSeedContributor = customerAttributeValuesDataSeedContributor; _customerAttributeValuesDataSeedContributor = customerAttributeValuesDataSeedContributor; _customerAttributeValuesDataSeedContributor = customerAttributeValuesDataSeedContributor; _customerAttributeValuesDataSeedContributor = customerAttributeValuesDataSeedContributor; _customerAttributeValuesDataSeedContributor = customerAttributeValuesDataSeedContributor; _customerAttributeValuesDataSeedContributor = customerAttributeValuesDataSeedContributor; _customerAttributeValuesDataSeedContributor = customerAttributeValuesDataSeedContributor; _customerAttributeValuesDataSeedContributor = customerAttributeValuesDataSeedContributor; _customerAttributeValuesDataSeedContributor = customerAttributeValuesDataSeedContributor; _customerAttributeValuesDataSeedContributor = customerAttributeValuesDataSeedContributor; _customerAttributeValuesDataSeedContributor = customerAttributeValuesDataSeedContributor; _customerAttributeValuesDataSeedContributor = customerAttributeValuesDataSeedContributor; _customerAttributeValuesDataSeedContributor = customerAttributeValuesDataSeedContributor; _customerAttributeValuesDataSeedContributor = customerAttributeValuesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _customerGroupsDataSeedContributor.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor.SeedAsync(context);

            await _customerGroupAttributeRepository.InsertAsync(new CustomerGroupAttribute
            (
                id: Guid.Parse("92e68d29-550e-4915-a91e-8e7bfae5acaa"),
                description: "f236584e67d64e88941ac898345d28ede0dd53708cd141ec98db4a34fed5a1595374c67cf9e24eb9a4d021030382c18dc8c0498748a6477db38eb49df566306c0e20cb6509734293919a67b7ece9fdc3ae38a842b0054f05b1ad918ac44daa5a63504448ed444233bdafe9d44e35b77479df729a887344049923992e49ff40b",
                customerGroupId: Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f"),
                attr0Id: null,
                attr1Id: null,
                attr2Id: null,
                attr3Id: null,
                attr4Id: null,
                attr5Id: null,
                attr6Id: null,
                attr7Id: null,
                attr8Id: null,
                attr9Id: null,
                attr10Id: null,
                attr11Id: null,
                attr12Id: null,
                attr13Id: null,
                attr14Id: null,
                attr15Id: null,
                attr16Id: null,
                attr17Id: null,
                attr18Id: null,
                attr19Id: null
            ));

            await _customerGroupAttributeRepository.InsertAsync(new CustomerGroupAttribute
            (
                id: Guid.Parse("635debef-ac63-477b-8826-cb029d906217"),
                description: "33c25055df434447bc4b8f2fbced745f424bf0f91df44c77939703faea6368fa50e8fbfb037b47f7af024e6ff16a0a6c413e05773c22484e8119969249f0bb8d71fdadba48244d55975f34077c11fe408e7f3dfd55084fe0bb6e3fec1e0484586980f6b908eb4e56960b2aaedd266adfb4c52fa449ed4f84a3f2d889017ab59",
                customerGroupId: Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f"),
                attr0Id: null,
                attr1Id: null,
                attr2Id: null,
                attr3Id: null,
                attr4Id: null,
                attr5Id: null,
                attr6Id: null,
                attr7Id: null,
                attr8Id: null,
                attr9Id: null,
                attr10Id: null,
                attr11Id: null,
                attr12Id: null,
                attr13Id: null,
                attr14Id: null,
                attr15Id: null,
                attr16Id: null,
                attr17Id: null,
                attr18Id: null,
                attr19Id: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}