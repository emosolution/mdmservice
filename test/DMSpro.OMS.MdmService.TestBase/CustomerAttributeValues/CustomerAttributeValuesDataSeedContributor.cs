using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerAttributes;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.CustomerAttributeValues;

namespace DMSpro.OMS.MdmService.CustomerAttributeValues
{
    public class CustomerAttributeValuesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICustomerAttributeValueRepository _customerAttributeValueRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CustomerAttributesDataSeedContributor _customerAttributesDataSeedContributor;

        private readonly CustomerAttributeValuesDataSeedContributor _customerAttributeValuesDataSeedContributor;

        public CustomerAttributeValuesDataSeedContributor(ICustomerAttributeValueRepository customerAttributeValueRepository, IUnitOfWorkManager unitOfWorkManager, CustomerAttributesDataSeedContributor customerAttributesDataSeedContributor, CustomerAttributeValuesDataSeedContributor customerAttributeValuesDataSeedContributor)
        {
            _customerAttributeValueRepository = customerAttributeValueRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _customerAttributesDataSeedContributor = customerAttributesDataSeedContributor; _customerAttributeValuesDataSeedContributor = customerAttributeValuesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _customerAttributesDataSeedContributor.SeedAsync(context);
            await _customerAttributeValuesDataSeedContributor.SeedAsync(context);

            await _customerAttributeValueRepository.InsertAsync(new CustomerAttributeValue
            (
                id: Guid.Parse("7a403e16-730c-47c5-9270-abb66c8bd8e3"),
                code: "2f9abca954a144b49ada",
                attrValName: "9afd223be50045268ad02e464f82006168b4bd10bf194934895fc8c7a610f0f14ccee26370f642019d56b2e499e52ac615a34d99d66d4006baf0b68316c9b441994b89ff9c3e49d4aa765afa54f3caac239e190af3da41b0817ea500c5cd3bc942e8b823d5974f9aa92d62130c34fb16327d1adcd4614bf8ac2a332650ef411",
                customerAttributeId: Guid.Parse("14d12cb7-01d4-4f12-ace1-bce02dcebeae"),
                parentId: null
            ));

            await _customerAttributeValueRepository.InsertAsync(new CustomerAttributeValue
            (
                id: Guid.Parse("659fbec7-099e-4d96-a838-22b2da8c2b10"),
                code: "7367dfad08e24718b587",
                attrValName: "04a64f9a72c849738f9d259591136eb3245495df241a49cc9d7c0b2f66be873a9f88ec213fba4da9bf5df2af56a0305c1dd5e548dbde460bb14cb7aa46bf763b60ea44f67c944e429603ee25ff18ede71fc39616ec3f4e41b3cc719d6cd3cac5df675ebe61e8464db2521fa122ae7c1e0fd243c1dc5741e7b8525d5a97be54a",
                customerAttributeId: Guid.Parse("14d12cb7-01d4-4f12-ace1-bce02dcebeae"),
                parentId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}