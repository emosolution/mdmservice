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
            await CheckCodeUniqueness(input.Code);

            bool isBase = (await _priceListRepository.CountAsync()) == 0;
            var basePriceListId = isBase ? null : input.BasePriceListId;

            await HandleDefault(input.IsDefaultForVendor, input.IsDefaultForVendor);

            var priceList = await _priceListManager.CreateAsync(
                basePriceListId, input.Code, input.Name, input.Active, isBase,
                input.IsDefaultForCustomer, input.IsDefaultForVendor,
                input.ArithmeticOperation, input.ArithmeticFactor, input.ArithmeticFactorType);

            await HandlePriceListDetail(priceList, isBase, basePriceListId);


            return ObjectMapper.Map<PriceList, PriceListDto>(priceList);
        }

        [Authorize(MdmServicePermissions.PriceLists.Edit)]
        public virtual async Task<PriceListDto> UpdateAsync(Guid id, PriceListUpdateDto input)
        {
            await CheckCodeUniqueness(input.Code, id);

            await HandleDefault(input.IsDefaultForVendor, input.IsDefaultForVendor);

            var priceList = await _priceListManager.UpdateAsync(
                id,
                input.BasePriceListId, input.Code, input.Name, input.Active,
                input.IsDefaultForCustomer, input.IsDefaultForVendor,
                input.ArithmeticOperation, input.ArithmeticFactor, input.ArithmeticFactorType,
                input.ConcurrencyStamp);

            return ObjectMapper.Map<PriceList, PriceListDto>(priceList);
        }

        private async Task HandleDefault(bool defaultForCustomer, bool defaultForVendor)
        {
            if (defaultForCustomer)
            {
                await SetAllPriceListDefaultForCustomerToFalse();
            }
            if (defaultForVendor)
            {
                await SetAllPriceListDefaultForVendorToFalse();
            }
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

        private async Task SetAllPriceListDefaultForCustomerToFalse()
        {
            var priceLists = await _priceListRepository.GetListAsync();
            priceLists.ForEach(x => x.IsDefaultForCustomer = false);
            await _priceListRepository.UpdateManyAsync(priceLists);
        }

        private async Task SetAllPriceListDefaultForVendorToFalse()
        {
            var priceLists = await _priceListRepository.GetListAsync();
            priceLists.ForEach(x => x.IsDefaultForVendor = false);
            await _priceListRepository.UpdateManyAsync(priceLists);
        }
    }
}