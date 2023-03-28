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
                id: Guid.Parse("486c2977-cc43-4b0d-8018-cfe1bdd6188c"),
                attrNo: 4,
                attrName: "8bd5f05b8ea241ae9c16",
                hierarchyLevel: 1482070413,
                active: true
            ));

            await _itemAttributeRepository.InsertAsync(new ItemAttribute
            (
                id: Guid.Parse("034e167b-d081-4290-bd89-8f61afe77ad6"),
                attrNo: 7,
                attrName: "a7875aca46cc4993b9ad",
                hierarchyLevel: 556463913,
                active: true
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}