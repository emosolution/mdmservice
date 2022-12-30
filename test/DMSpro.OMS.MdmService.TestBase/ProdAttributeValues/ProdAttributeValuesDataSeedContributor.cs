using DMSpro.OMS.MdmService.ProdAttributeValues;
using DMSpro.OMS.MdmService.ProductAttributes;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace DMSpro.OMS.MdmService.ProdAttributeValues
{
    public class ProdAttributeValuesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IProdAttributeValueRepository _prodAttributeValueRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ProductAttributesDataSeedContributor _productAttributesDataSeedContributor;

        private readonly ProdAttributeValuesDataSeedContributor _prodAttributeValuesDataSeedContributor;

        public ProdAttributeValuesDataSeedContributor(IProdAttributeValueRepository prodAttributeValueRepository, IUnitOfWorkManager unitOfWorkManager, ProductAttributesDataSeedContributor productAttributesDataSeedContributor, ProdAttributeValuesDataSeedContributor prodAttributeValuesDataSeedContributor)
        {
            _prodAttributeValueRepository = prodAttributeValueRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _productAttributesDataSeedContributor = productAttributesDataSeedContributor; _prodAttributeValuesDataSeedContributor = prodAttributeValuesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _productAttributesDataSeedContributor.SeedAsync(context);
            await _prodAttributeValuesDataSeedContributor.SeedAsync(context);

            await _prodAttributeValueRepository.InsertAsync(new ProdAttributeValue
            (
                id: Guid.Parse("814ec6a6-0bc4-48d5-82a7-e22b2b98411c"),
                attrValName: "fa20e780362a476eb80a7c3304a7d9fc943c48253cb24af78aaaed5f65bee2826561110677f74d07b466a531c0099addb2c7",
                prodAttributeId: Guid.Parse("6048be4f-07df-4015-a5ee-f886ec2077a8"),
                parentProdAttributeValueId: null
            ));

            await _prodAttributeValueRepository.InsertAsync(new ProdAttributeValue
            (
                id: Guid.Parse("7f69737f-d2f8-4bc1-bff0-265e3be7e88e"),
                attrValName: "fb1ff3d3a41a4b62a8652ba025af83c0b23298f4f8f94ea0aebf2c1d0708f675e129e7c97fbb4433832acf5f373af0fb96dc",
                prodAttributeId: Guid.Parse("6048be4f-07df-4015-a5ee-f886ec2077a8"),
                parentProdAttributeValueId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}