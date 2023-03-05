using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.ItemGroupInZones
{
    public class ItemGroupInZoneManager : DomainService
    {
        private readonly IItemGroupInZoneRepository _itemGroupInZoneRepository;

        public ItemGroupInZoneManager(IItemGroupInZoneRepository itemGroupInZoneRepository)
        {
            _itemGroupInZoneRepository = itemGroupInZoneRepository;
        }

        public async Task<ItemGroupInZone> CreateAsync(
        Guid sellingZoneId, Guid itemGroupId, DateTime effectiveDate, bool active, string description, DateTime? endDate = null)
        {
            Check.NotNull(sellingZoneId, nameof(sellingZoneId));
            Check.NotNull(itemGroupId, nameof(itemGroupId));
            Check.NotNull(effectiveDate, nameof(effectiveDate));
            Check.Length(description, nameof(description), ItemGroupInZoneConsts.DescriptionMaxLength);

            var itemGroupInZone = new ItemGroupInZone(
             GuidGenerator.Create(),
             sellingZoneId, itemGroupId, effectiveDate, active, description, endDate
             );

            return await _itemGroupInZoneRepository.InsertAsync(itemGroupInZone);
        }

        public async Task<ItemGroupInZone> UpdateAsync(
            Guid id,
            Guid sellingZoneId, Guid itemGroupId, DateTime effectiveDate, bool active, string description, DateTime? endDate = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(sellingZoneId, nameof(sellingZoneId));
            Check.NotNull(itemGroupId, nameof(itemGroupId));
            Check.NotNull(effectiveDate, nameof(effectiveDate));
            Check.Length(description, nameof(description), ItemGroupInZoneConsts.DescriptionMaxLength);

            var itemGroupInZone = await _itemGroupInZoneRepository.GetAsync(id);

            itemGroupInZone.SellingZoneId = sellingZoneId;
            itemGroupInZone.ItemGroupId = itemGroupId;
            itemGroupInZone.EffectiveDate = effectiveDate;
            itemGroupInZone.Active = active;
            itemGroupInZone.Description = description;
            itemGroupInZone.EndDate = endDate;

            itemGroupInZone.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _itemGroupInZoneRepository.UpdateAsync(itemGroupInZone);
        }

    }
}