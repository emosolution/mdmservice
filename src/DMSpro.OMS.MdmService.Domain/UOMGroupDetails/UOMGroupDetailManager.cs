using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.UOMGroupDetails
{
    public class UOMGroupDetailManager : DomainService
    {
        private readonly IUOMGroupDetailRepository _uOMGroupDetailRepository;

        public UOMGroupDetailManager(IUOMGroupDetailRepository uOMGroupDetailRepository)
        {
            _uOMGroupDetailRepository = uOMGroupDetailRepository;
        }

        public async Task<UOMGroupDetail> CreateAsync(
        Guid uOMGroupId, Guid altUOMId, Guid baseUOMId, uint altQty, uint baseQty, bool active)
        {
            Check.NotNull(uOMGroupId, nameof(uOMGroupId));
            Check.NotNull(altUOMId, nameof(altUOMId));
            Check.NotNull(baseUOMId, nameof(baseUOMId));

            var uOMGroupDetail = new UOMGroupDetail(
             GuidGenerator.Create(),
             uOMGroupId, altUOMId, baseUOMId, altQty, baseQty, active
             );

            return await _uOMGroupDetailRepository.InsertAsync(uOMGroupDetail);
        }

        public async Task<UOMGroupDetail> UpdateAsync(
            Guid id,
            Guid uOMGroupId, Guid altUOMId, Guid baseUOMId, uint altQty, uint baseQty, bool active, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(uOMGroupId, nameof(uOMGroupId));
            Check.NotNull(altUOMId, nameof(altUOMId));
            Check.NotNull(baseUOMId, nameof(baseUOMId));

            var queryable = await _uOMGroupDetailRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var uOMGroupDetail = await AsyncExecuter.FirstOrDefaultAsync(query);

            uOMGroupDetail.UOMGroupId = uOMGroupId;
            uOMGroupDetail.AltUOMId = altUOMId;
            uOMGroupDetail.BaseUOMId = baseUOMId;
            uOMGroupDetail.AltQty = altQty;
            uOMGroupDetail.BaseQty = baseQty;
            uOMGroupDetail.Active = active;

            uOMGroupDetail.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _uOMGroupDetailRepository.UpdateAsync(uOMGroupDetail);
        }

    }
}