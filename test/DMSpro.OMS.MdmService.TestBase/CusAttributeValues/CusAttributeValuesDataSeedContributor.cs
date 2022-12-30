using DMSpro.OMS.MdmService.CustomerAttributes;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace DMSpro.OMS.MdmService.CusAttributeValues
{
    public class CusAttributeValuesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICusAttributeValueRepository _cusAttributeValueRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CustomerAttributesDataSeedContributor _customerAttributesDataSeedContributor;

        private readonly CusAttributeValuesDataSeedContributor _cusAttributeValuesDataSeedContributor;

        public CusAttributeValuesDataSeedContributor(ICusAttributeValueRepository cusAttributeValueRepository, IUnitOfWorkManager unitOfWorkManager, CustomerAttributesDataSeedContributor customerAttributesDataSeedContributor, CusAttributeValuesDataSeedContributor cusAttributeValuesDataSeedContributor)
        {
            _cusAttributeValueRepository = cusAttributeValueRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _customerAttributesDataSeedContributor = customerAttributesDataSeedContributor; _cusAttributeValuesDataSeedContributor = cusAttributeValuesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _customerAttributesDataSeedContributor.SeedAsync(context);
            await _cusAttributeValuesDataSeedContributor.SeedAsync(context);

            await _cusAttributeValueRepository.InsertAsync(new CusAttributeValue
            (
                id: Guid.Parse("fad43f42-f0a7-4a2d-912a-605efeb104bd"),
                attrValName: "c3cf714f47274117b852c75a89de421aa76e42709b874776ad5ea2ce92ee13440d5af6f004f14aa18aafca8506631c261e46",
                customerAttributeId: Guid.Parse("14d12cb7-01d4-4f12-ace1-bce02dcebeae"),
                parentCusAttributeValueId: null
            ));

            await _cusAttributeValueRepository.InsertAsync(new CusAttributeValue
            (
                id: Guid.Parse("14db2331-958b-4622-8856-4e1949cdc323"),
                attrValName: "3fa7f94e627749eebfec64616286a7e19f56f7d3f2d64da9b911858eb796ede35cf1afd108064f989196c7fb10f173e73f47",
                customerAttributeId: Guid.Parse("14d12cb7-01d4-4f12-ace1-bce02dcebeae"),
                parentCusAttributeValueId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}