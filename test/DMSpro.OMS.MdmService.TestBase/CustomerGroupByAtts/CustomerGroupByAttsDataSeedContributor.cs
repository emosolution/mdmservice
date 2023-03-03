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
                id: Guid.Parse("461ea009-5f7b-4e83-a5e1-35e0c69a231d"),
                valueCode: "eaa533bcf8fe444bbbe5",
                valueName: "364356d9dc754b99bc838d5dc7942d4b8476081c435147938ee086010e531c3d79ca5fe7b720449fb666d361ebdc6862907a4c98de9940489ea73d02a1457ac437a18c763fc44d528b5d8e335cfb1a1d7fd8c609f38f48e18d4f3f6df1cdb8f31679d9252fd449f29a8d725809bf0d9751f7a6ef8f434521bcab489c03445da",
                customerGroupId: Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f"),
                cusAttributeValueId: Guid.Parse("fad43f42-f0a7-4a2d-912a-605efeb104bd")
            ));

            await _customerGroupByAttRepository.InsertAsync(new CustomerGroupByAtt
            (
                id: Guid.Parse("4d7f0e31-431c-4200-aa08-b7b2ed05f27a"),
                valueCode: "c9460c857f6d47eb88ac",
                valueName: "ff6451e7ff914fc485144e50bc8f4416497a5be47505421794ced6ab6b9a68d51be0f29ac2ba41a28fd08fb6634e5d0cf2aee96532244a7d8bb36e71428e39c55195c0b497d5442d8750ece2e2c539a2e8146676cf184fe7abdb123ffbdd9f39b666e9ef063448cd8fc32a6ae0e2d81fad1d6632e5fd4209b19aac1db35081a",
                customerGroupId: Guid.Parse("a0ff5319-aa04-4e71-b0d6-05b8800ed64f"),
                cusAttributeValueId: Guid.Parse("fad43f42-f0a7-4a2d-912a-605efeb104bd")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}