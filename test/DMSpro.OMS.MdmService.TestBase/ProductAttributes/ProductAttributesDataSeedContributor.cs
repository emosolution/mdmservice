using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.ProductAttributes;

namespace DMSpro.OMS.MdmService.ProductAttributes
{
    public class ProductAttributesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IProductAttributeRepository _productAttributeRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ProductAttributesDataSeedContributor(IProductAttributeRepository productAttributeRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _productAttributeRepository = productAttributeRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _productAttributeRepository.InsertAsync(new ProductAttribute
            (
                id: Guid.Parse("c7702e2b-8e29-45a0-8439-c081bbe27418"),
                attrNo: 13,
                attrName: "36fe4c206296495aa5cf2fe33a195721c0b24c92c337491085ec5d80ac7f39ff1a947e20f9ae498d8c5f1cf346244aed95c2",
                hierarchyLevel: 16,
                active: true,
                isProductCategory: true
            ));

            await _productAttributeRepository.InsertAsync(new ProductAttribute
            (
                id: Guid.Parse("377d6a4f-2571-4641-9ad3-2590585fa6a6"),
                attrNo: 4,
                attrName: "be720ecb7bb84dfdbd39c1faabf861904cec6df44c314bbdb4887981012ff12b5732db15045942d4b3da404616489e4a5753",
                hierarchyLevel: 4,
                active: true,
                isProductCategory: true
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}