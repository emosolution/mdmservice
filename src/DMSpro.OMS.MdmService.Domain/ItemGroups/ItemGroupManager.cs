using DMSpro.OMS.MdmService.ItemGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
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
        string code, string name, string description, GroupType type, GroupStatus status)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), ItemGroupConsts.CodeMaxLength, ItemGroupConsts.CodeMinLength);
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(description, nameof(description), ItemGroupConsts.DescriptionMaxLength, ItemGroupConsts.DescriptionMinLength);
            Check.NotNull(type, nameof(type));
            Check.NotNull(status, nameof(status));

            var itemGroup = new ItemGroup(
             GuidGenerator.Create(),
             code, name, description, type, status
             );

            return await _itemGroupRepository.InsertAsync(itemGroup);
        }

        public async Task<ItemGroup> UpdateAsync(
            Guid id,
            string code, string name, string description, GroupType type, GroupStatus status, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), ItemGroupConsts.CodeMaxLength, ItemGroupConsts.CodeMinLength);
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(description, nameof(description), ItemGroupConsts.DescriptionMaxLength, ItemGroupConsts.DescriptionMinLength);
            Check.NotNull(type, nameof(type));
            Check.NotNull(status, nameof(status));

            var queryable = await _itemGroupRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var itemGroup = await AsyncExecuter.FirstOrDefaultAsync(query);

            itemGroup.Code = code;
            itemGroup.Name = name;
            itemGroup.Description = description;
            itemGroup.Type = type;
            itemGroup.Status = status;

            itemGroup.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _itemGroupRepository.UpdateAsync(itemGroup);
        }

    }
}