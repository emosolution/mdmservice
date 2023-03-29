using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Domain.Repositories;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.PriceListDetails;

namespace DMSpro.OMS.MdmService.PriceLists
{

    [Authorize(MdmServicePermissions.PriceLists.Default)]
    public partial class PriceListsAppService 
    {
        public virtual async Task<PriceListDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<PriceList, PriceListDto>(await _priceListRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.PriceLists.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _priceListRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.PriceLists.Create)]
        public virtual async Task<PriceListDto> CreateAsync(PriceListCreateDto input)
        {
            bool isBase = (await _priceListRepository.CountAsync()) == 0;
            var basePriceListId = isBase ? null : input.BasePriceListId;

            if (input.IsDefault)
            {
                await SetAllPriceListDefaultToFalse();
            }

            var priceList = await _priceListManager.CreateAsync(
                basePriceListId, input.Code, input.Name, input.Active, isBase, input.IsDefault, 
                input.ArithmeticOperation, input.ArithmeticFactor, input.ArithmeticFactorType);

            await HandlePriceListDetail(priceList, isBase, basePriceListId);


            return ObjectMapper.Map<PriceList, PriceListDto>(priceList);
        }

        [Authorize(MdmServicePermissions.PriceLists.Edit)]
        public virtual async Task<PriceListDto> UpdateAsync(Guid id, PriceListUpdateDto input)
        {
            if (input.IsDefault)
            {
                await SetAllPriceListDefaultToFalse();
            }

            var priceList = await _priceListManager.UpdateAsync(
                id,
                input.BasePriceListId, input.Code, input.Name, input.Active, input.IsDefault, 
                input.ArithmeticOperation, input.ArithmeticFactor, input.ArithmeticFactorType, 
                input.ConcurrencyStamp);

            return ObjectMapper.Map<PriceList, PriceListDto>(priceList);
        }

        private async Task HandlePriceListDetail(PriceList priceList, bool isBase, Guid? basePriceListId)
        {
            List<PriceListDetail> priceListDetails = new();
            if (isBase) //Get all ItemMaster
            {
                var items = await _itemRepository.GetListAsync();
                foreach (var i in items)
                {
                    PriceListDetail priceListDetailObj = new()
                    {
                        Description = "",
                        PriceListId = priceList.Id,
                        ItemId = i.Id,
                        UOMId = i.InventoryUOMId,
                        BasedOnPrice = i.BasePrice,
                        Price = i.BasePrice
                    };

                    switch (priceList.ArithmeticOperation)
                    {
                        case ArithmeticOperator.ADD:
                            priceListDetailObj.Price = i.BasePrice + priceList.ArithmeticFactor.Value * (priceList.ArithmeticFactorType == ArithmeticFactorType.PERCENTAGE ? priceList.ArithmeticFactor.Value / 100 : 1);
                            break;
                        case ArithmeticOperator.SUBTRACT:
                            priceListDetailObj.Price = i.BasePrice - priceList.ArithmeticFactor.Value * (priceList.ArithmeticFactorType == ArithmeticFactorType.PERCENTAGE ? priceList.ArithmeticFactor.Value / 100 : 1);
                            break;
                        default:
                            break;
                    }

                    priceListDetails.Add(priceListDetailObj);

                    //priceListDetailObj.
                    //var priceListDetail = await _priceListDetailRepository.InsertAsync(priceListDetailObj);
                }
            }
            else
            { //Get detail from base price list
                priceListDetails = await _priceListDetailRepository.GetListAsync(filterText: $"PriceListId = {basePriceListId}");
                foreach (var item in priceListDetails)
                {
                    item.BasedOnPrice = item.Price;
                    switch (priceList.ArithmeticOperation)
                    {
                        case ArithmeticOperator.ADD:
                            item.Price = item.BasedOnPrice.Value + priceList.ArithmeticFactor.Value * (priceList.ArithmeticFactorType == ArithmeticFactorType.PERCENTAGE ? priceList.ArithmeticFactor.Value / 100 : 1);
                            break;
                        case ArithmeticOperator.SUBTRACT:
                            item.Price = item.BasedOnPrice.Value - priceList.ArithmeticFactor.Value * (priceList.ArithmeticFactorType == ArithmeticFactorType.PERCENTAGE ? priceList.ArithmeticFactor.Value / 100 : 1);
                            break;
                        default:
                            break;
                    }
                }
            }

            await _priceListDetailRepository.InsertManyAsync(priceListDetails);
        }

        private async Task SetAllPriceListDefaultToFalse()
        {
            var priceLists = await _priceListRepository.GetListAsync();
            priceLists.ForEach(x => x.IsDefault = false);
            await _priceListRepository.UpdateManyAsync(priceLists);
        }
    }
}