using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerGroups;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace DMSpro.OMS.MdmService.CustomerGroupAttributes
{
    public class CustomerGroupAttributesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICustomerGroupAttributeRepository _customerGroupAttributeRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CustomerGroupsDataSeedContributor _customerGroupsDataSeedContributor;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor0;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor1;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor2;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor3;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor4;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor5;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor6;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor7;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor8;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor9;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor10;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor11;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor12;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor13;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor14;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor15;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor16;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor17;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor18;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor19;

        public CustomerGroupAttributesDataSeedContributor(ICustomerGroupAttributeRepository customerGroupAttributeRepository, IUnitOfWorkManager unitOfWorkManager, 
            CustomerGroupsDataSeedContributor customerGroupsDataSeedContributor, 
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor0, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor1, 
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor2, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor3, 
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor4, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor5, 
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor6, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor7, 
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor8, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor9,
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor10, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor11,
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor12, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor13,
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor14, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor15,
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor16, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor17,
            CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor18, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor19)
        {
            _customerGroupAttributeRepository = customerGroupAttributeRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _customerGroupsDataSeedContributor = customerGroupsDataSeedContributor;
            _customerAttributeValuesDataSeedContributor0 = customerAttributeValuesDataSeedContributor0;
            _customerAttributeValuesDataSeedContributor1 = customerAttributeValuesDataSeedContributor1; 
            _customerAttributeValuesDataSeedContributor2 = customerAttributeValuesDataSeedContributor2; 
            _customerAttributeValuesDataSeedContributor3 = customerAttributeValuesDataSeedContributor3; 
            _customerAttributeValuesDataSeedContributor4 = customerAttributeValuesDataSeedContributor4;
            _customerAttributeValuesDataSeedContributor5 = customerAttributeValuesDataSeedContributor5;
            _customerAttributeValuesDataSeedContributor6 = customerAttributeValuesDataSeedContributor6;
            _customerAttributeValuesDataSeedContributor7 = customerAttributeValuesDataSeedContributor7;
            _customerAttributeValuesDataSeedContributor8 = customerAttributeValuesDataSeedContributor8;
            _customerAttributeValuesDataSeedContributor9 = customerAttributeValuesDataSeedContributor9;
            _customerAttributeValuesDataSeedContributor10 = customerAttributeValuesDataSeedContributor10;
            _customerAttributeValuesDataSeedContributor11 = customerAttributeValuesDataSeedContributor11;
            _customerAttributeValuesDataSeedContributor12 = customerAttributeValuesDataSeedContributor12;
            _customerAttributeValuesDataSeedContributor13 = customerAttributeValuesDataSeedContributor13;
            _customerAttributeValuesDataSeedContributor14 = customerAttributeValuesDataSeedContributor14;
            _customerAttributeValuesDataSeedContributor15 = customerAttributeValuesDataSeedContributor15;
            _customerAttributeValuesDataSeedContributor16 = customerAttributeValuesDataSeedContributor16;
            _customerAttributeValuesDataSeedContributor17 = customerAttributeValuesDataSeedContributor17;
            _customerAttributeValuesDataSeedContributor18 = customerAttributeValuesDataSeedContributor18;
            _customerAttributeValuesDataSeedContributor19 = customerAttributeValuesDataSeedContributor19;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _customerGroupsDataSeedContributor.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor0.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor1.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor2.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor3.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor4.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor5.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor6.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor7.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor8.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor9.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor10.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor11.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor12.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor13.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor14.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor15.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor16.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor17.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor18.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor19.SeedAsync(context);

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