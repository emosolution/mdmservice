using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.PriceUpdates;
using Volo.Abp.Domain.Repositories;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DMSpro.OMS.Shared.Lib.Parser;
using System.Collections.Generic;
using System.Linq;

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{

    [Authorize(MdmServicePermissions.PriceUpdates.Default)]
    public partial class PriceUpdateDetailsAppService
    {
        public virtual async Task<PriceUpdateDetailDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<PriceUpdateDetail, PriceUpdateDetailDto>(await _priceUpdateDetailRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.PriceUpdates.Edit)]
        public virtual async Task DeleteAsync(Guid id)
        {
            var detail = await _priceUpdateDetailRepository.GetAsync(id);
            await CheckPriceUpdate(detail.PriceUpdateId);
            await _priceUpdateDetailRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.PriceUpdates.Create)]
        public virtual async Task<PriceUpdateDetailDto> CreateAsync(PriceUpdateDetailCreateDto input)
        {
            if (input.PriceUpdateId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["PriceUpdate"]]);
            }
            if (input.PriceListDetailId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["PriceListDetail"]]);
            }
            var header = await CheckPriceUpdate(input.PriceUpdateId);
            if (await _priceUpdateDetailRepository.AnyAsync(
                x => x.PriceListDetailId == input.PriceListDetailId &&
                x.PriceUpdateId == input.PriceUpdateId))
            {
                throw new UserFriendlyException(message: L["Error:PriceUpdateDetailsAppService:551"], code: "1");
            }
            var priceListDetail = await _priceListDetailRepository.GetAsync(input.PriceListDetailId);
            if (header.PriceListId != priceListDetail.PriceListId)
            {
                throw new UserFriendlyException(message: L["Error:PriceUpdateDetailsAppService:552"], code: "1");
            }
            var priceBeforeUpdate = priceListDetail.Price;

            var priceUpdateDetail = await _priceUpdateDetailManager.CreateAsync(
                input.PriceUpdateId, input.PriceListDetailId, priceBeforeUpdate, input.NewPrice);

            return ObjectMapper.Map<PriceUpdateDetail, PriceUpdateDetailDto>(priceUpdateDetail);
        }

        [Authorize(MdmServicePermissions.PriceUpdates.Edit)]
        public virtual async Task<PriceUpdateDetailDto> UpdateAsync(Guid id, PriceUpdateDetailUpdateDto input)
        {
            var detail = await _priceUpdateDetailRepository.GetAsync(id);
            await CheckPriceUpdate(detail.PriceUpdateId);
            var record = await _priceUpdateDetailManager.UpdateAsync(
                id,
                input.NewPrice,
                input.ConcurrencyStamp);

            return ObjectMapper.Map<PriceUpdateDetail, PriceUpdateDetailDto>(record);
        }

        private async Task<PriceUpdate> CheckPriceUpdate(Guid headerId)
        {
            var header = await _priceUpdateRepository.GetAsync(headerId);
            if (header.Status != PriceUpdateStatus.OPEN)
            {
                throw new UserFriendlyException(message: L["Error:PriceUpdateDetailsAppService:550"], code: "1");
            }
            return header;
        }

        public override async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            await CheckPermission();
            var items = await _repository.WithDetailsAsync();
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption, inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);
            var details = results.data.Cast<PriceUpdateDetail>().ToList();
            if (inputDev.Group == null)
            {
                results.data = ObjectMapper.Map<List<PriceUpdateDetail>,
                    List<PriceUpdateDetailWithDetailsDto>>(details);
            }
            var priceListDetails = details.Select(x => x.PriceListDetail).ToList();
            var uomIds = priceListDetails.Select(x => x.UOMId).Distinct().ToList();
            var itemIds = priceListDetails.Select(x => x.ItemId).Distinct().ToList();
            var uomsInDetails = await _uOMRepository.GetListAsync(x => uomIds.Contains(x.Id));
            var itemsInDetails = await _itemRepository.GetListAsync(x => itemIds.Contains(x.Id));
            var uomDictionary = uomsInDetails.ToDictionary(x => x.Id, x => new
            {
                x.Id,
                x.Code,
                x.Name,
            });
            var itemDictionary = itemsInDetails.ToDictionary(x => x.Id, x => new
            {
                x.Id,
                x.Code,
                x.Name,
                x.ShortName,
            });
            var dictionaries = new
            {
                UomDictionary = uomDictionary,
                ItemDictionary = itemDictionary,
            };
            results.summary = new object[] { dictionaries };
            return results;
        }
    }
}