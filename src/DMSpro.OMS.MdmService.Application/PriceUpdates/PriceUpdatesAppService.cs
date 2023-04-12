using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using DMSpro.OMS.MdmService.Permissions;

namespace DMSpro.OMS.MdmService.PriceUpdates
{

    [Authorize(MdmServicePermissions.PriceUpdates.Default)]
    public partial class PriceUpdatesAppService
    {
        public virtual async Task<PriceUpdateDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<PriceUpdate, PriceUpdateDto>(await _priceUpdateRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.PriceUpdates.Edit)]
        public virtual async Task<PriceUpdateDto> CancelAsync(Guid id)
        {
            var priceUpdate = await _priceUpdateRepository.GetAsync(id);
            if (priceUpdate.Status != PriceUpdateStatus.OPEN)
            {
                throw new UserFriendlyException(message: L["Error:PriceUpdatesAppService:550"], code: "0");
            }
            priceUpdate.Status = PriceUpdateStatus.CANCELLED;
            priceUpdate.CancelledDate = DateTime.Now;
            await _priceUpdateRepository.UpdateAsync(priceUpdate);
            return ObjectMapper.Map<PriceUpdate, PriceUpdateDto>(priceUpdate);
        }

        [Authorize(MdmServicePermissions.PriceUpdates.Release)]
        public virtual async Task<PriceUpdateDto> ReleaseAsync(Guid id)
        {
            var priceUpdate = await _priceUpdateRepository.GetAsync(id);
            if (priceUpdate.Status != PriceUpdateStatus.OPEN)
            {
                throw new UserFriendlyException(message: L["Error:PriceUpdatesAppService:551",
                    "EntityFieldValue:MDMService:PriceUpdate:Status:OPEN"], code: "0");
            }
            DateTime now = DateTime.Now;
            await ExecutePriceUpdate(priceUpdate);
            priceUpdate.Status = PriceUpdateStatus.COMPLETED;
            priceUpdate.ReleasedDate = now;
            priceUpdate.CompleteDate = now;
            await _priceUpdateRepository.UpdateAsync(priceUpdate);
            return ObjectMapper.Map<PriceUpdate, PriceUpdateDto>(priceUpdate);
        }

        [Authorize(MdmServicePermissions.PriceUpdates.Create)]
        public virtual async Task<PriceUpdateDto> CreateAsync(PriceUpdateCreateDto input)
        {
            if (input.PriceListId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["PriceList"]]);
            }
            await CheckCodeUniqueness(input.Code);
            var priceUpdate = await _priceUpdateManager.CreateAsync(
                input.PriceListId, input.Code, input.Description, isScheduled: false);

            return ObjectMapper.Map<PriceUpdate, PriceUpdateDto>(priceUpdate);
        }

        [Authorize(MdmServicePermissions.PriceUpdates.Edit)]
        public virtual async Task<PriceUpdateDto> UpdateAsync(Guid id, PriceUpdateUpdateDto input)
        {
            var priceUpdate = await _priceUpdateRepository.GetAsync(id);
            if (priceUpdate.Status != PriceUpdateStatus.OPEN)
            {
                throw new UserFriendlyException(message: L["Error:PriceUpdatesAppService:552",
                    "EntityFieldValue:MDMService:PriceUpdate:Status:OPEN"], code: "0");
            }
            var record = await _priceUpdateManager.UpdateAsync(
                id,
                input.Description, isScheduled: false,
                effectiveDate: null, endDate: null,
                input.ConcurrencyStamp);

            return ObjectMapper.Map<PriceUpdate, PriceUpdateDto>(record);
        }

        private async Task ExecutePriceUpdate(PriceUpdate priceUpdate)
        {
            throw new NotImplementedException();
        }
    }
}