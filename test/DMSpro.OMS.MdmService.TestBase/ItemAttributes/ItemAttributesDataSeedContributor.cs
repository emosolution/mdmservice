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
                id: Guid.Parse("491c657a-c618-4c58-bde2-b5156e5728f3"),
                attrNo: "2cd9bbff3ab948e5a83e",
                attrName: "8ef91d393d6947069f1e",
                hierarchyLevel: 840953148,
                active: true,
                isSellingCategory: true
            ));

            await _itemAttributeRepository.InsertAsync(new ItemAttribute
            (
                id: Guid.Parse("308c03f7-7ce6-4f77-821c-601577c38001"),
                attrNo: "c61b88a8e7ec45ab8944",
                attrName: "7328332efef9415f93b7",
                hierarchyLevel: 270416606,
                active: true,
                isSellingCategory: true
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}