using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.ItemGroups
{
    public class ItemGroupManager : DomainService
    {
        private readonly IItemGroupRepository _itemGroupRepository;

        public ItemGroupManager(IItemGroupRepository itemGroupRepository)
        {
            _itemGroupRepository = itemGroupRepository;
        }

        public async Task<ItemGroup> CreateAsync(
            string code, string name, string description,
            GroupType type, bool? selectable)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), ItemGroupConsts.CodeMaxLength, ItemGroupConsts.CodeMinLength);
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), ItemGroupConsts.NameMaxLength);
            Check.Length(description, nameof(description), ItemGroupConsts.DescriptionMaxLength);
            Check.NotNull(type, nameof(type));

            var itemGroup = new ItemGroup(
                 GuidGenerator.Create(),
                 code, name, description, type, GroupStatus.OPEN, selectable);

            return await _itemGroupRepository.InsertAsync(itemGroup);
        }

        public async Task<ItemGroup> UpdateAsync(
            Guid id,
            string name, string description, 
            GroupType type, bool? selectable,
            [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), ItemGroupConsts.NameMaxLength);
            Check.Length(description, nameof(description), ItemGroupConsts.DescriptionMaxLength);
            Check.NotNull(type, nameof(type));

            var itemGroup = await _itemGroupRepository.GetAsync(id);

            itemGroup.Name = name;
            itemGroup.Description = description;
            itemGroup.Type = type;
            itemGroup.Selectable = selectable;

            itemGroup.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _itemGroupRepository.UpdateAsync(itemGroup);
        }

    }
}