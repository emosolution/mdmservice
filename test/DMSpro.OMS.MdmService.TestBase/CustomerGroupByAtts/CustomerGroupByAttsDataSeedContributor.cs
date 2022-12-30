using DMSpro.OMS.MdmService.CusAttributeValues;
using DMSpro.OMS.MdmService.CustomerGroups;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.CustomerGroupByAtts;

namespace DMSpro.OMS.MdmService.CustomerGroupByAtts
{
    public class CustomerGroupByAttsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICustomerGroupByAttRepository _customerGroupByAttRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CustomerGroupsDataSeedContributor _customerGroupsDataSeedContributor;

        private readonly CusAttributeValuesDataSeedContributor _cusAttributeValuesDataSeedContributor;

        public CustomerGroupByAttsDataSeedContributor(ICustomerGroupByAttRepository customerGroupByAttRepository, IUnitOfWorkManager unitOfWorkManager, CustomerGroupsDataSeedContributor customerGroupsDataSeedContributor, CusAttributeValuesDataSeedContributor cusAttributeValuesDataSeedContributor)
        {
            _customerGroupByAttRepository = customerGroupByAttRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _customerGroupsDataSeedContributor = customerGroupsDataSeedContributor; _cusAttributeValuesDataSeedContributor = cusAttributeValuesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _customerGroupsDataSeedContributor.SeedAsync(context);
            await _cusAttributeValuesDataSeedContributor.SeedAsync(context);

            await _customerGroupByAttRepository.InsertAsync(new CustomerGroupByAtt
            (
                id: Guid.Parse("b5051aef-4045-4c45-9a11-1023a689c744"),
                valueCode: "0c27a455d2b44e0f935cd5e8438dcb1118bca223a38a4cbfb",
                valueName: "d6c4d9c3679347da8fa350412c8e76f60a5e4fc8405e49e4973067b9b8f46cc465426f395e154e6d847ef57ab4",
                customerGroupId: Guid.Parse("62485c9b-4efb-48f4-a7e3-63a6ab70dfe9"),
                cusAttributeValueId: Guid.Parse("fad43f42-f0a7-4a2d-912a-605efeb104bd")
            ));

            await _customerGroupByAttRepository.InsertAsync(new CustomerGroupByAtt
            (
                id: Guid.Parse("6e1edfb3-8b46-4d26-8da6-84b3137c4319"),
                valueCode: "9a63d55654cc435cb2ec",
                valueName: "ddd3058befd54d96be4cce5360",
                customerGroupId: Guid.Parse("62485c9b-4efb-48f4-a7e3-63a6ab70dfe9"),
                cusAttributeValueId: Guid.Parse("fad43f42-f0a7-4a2d-912a-605efeb104bd")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}