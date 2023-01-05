using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.ItemAttributes;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
    public class ItemAttributesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IItemAttributeRepository _itemAttributeRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ItemAttributesDataSeedContributor(IItemAttributeRepository itemAttributeRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _itemAttributeRepository = itemAttributeRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _itemAttributeRepository.InsertAsync(new ItemAttribute
            (
                id: Guid.Parse("42932fe9-fb80-4320-ba30-24dcef522f8f"),
                attrNo: 18,
                attrName: "601d6242fcbb447b8d0b",
                hierarchyLevel: 1831578934,
                active: true,
                isSellingCategory: true
            ));

            await _itemAttributeRepository.InsertAsync(new ItemAttribute
            (
                id: Guid.Parse("d3b859c6-5b84-454c-a914-68680e430af5"),
                attrNo: 3,
                attrName: "e0f2827c1ba846ad8edf",
                hierarchyLevel: 1841818253,
                active: true,
                isSellingCategory: true
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}