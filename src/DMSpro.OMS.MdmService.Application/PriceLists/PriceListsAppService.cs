using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Domain.Repositories;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.PriceListDetails;
using Volo.Abp;
using System.Linq;

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
            var priceList = await _priceListRepository.FirstAsync(x => x.Id == id);
            if (priceList.IsBase || priceList.IsDefaultForCustomer || priceList.IsDefaultForVendor)
            {
                throw new UserFriendlyException(L["Error:General:DeleteContraint:550"]);
            }
            if (priceList.IsReleased)
            {
                throw new UserFriendlyException(L["Error:PriceListsAppService:550"], code: "1");
            }
            await _priceListRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.PriceLists.Create)]
        public virtual async Task<PriceListDto> CreateAsync(PriceListCreateDto input)
        {
            await CheckCodeUniqueness(input.Code);

            bool isBase = (await _priceListRepository.CountAsync()) == 0;
            var basePriceListId = isBase ? null : input.BasePriceListId;

            await HandleDefault(input.IsDefaultForCustomer, input.IsDefaultForVendor);
            var priceList = await _priceListManager.CreateAsync(
                basePriceListId, input.Code, input.Name, input.Active, isBase,
                input.IsDefaultForCustomer, input.IsDefaultForVendor,
                input.ArithmeticOperation, input.ArithmeticFactor, input.ArithmeticFactorType);
            await HandlePriceListDetail(priceList);

            return ObjectMapper.Map<PriceList, PriceListDto>(priceList);
        }

        [Authorize(MdmServicePermissions.PriceLists.Edit)]
        public virtual async Task<PriceListDto> UpdateAsync(Guid id, PriceListUpdateDto input)
        {
            await CheckCodeUniqueness(input.Code, id);
            var priceList = await _priceListRepository.GetAsync(id);
            if (priceList.IsReleased)
            {
                throw new UserFriendlyException(L["Error:PriceListsAppService:550"], code: "1");
            }

            if (priceList.IsBase)
            { 
                input.Active = true;
                input.ArithmeticFactor = null;
                input.ArithmeticFactorType = null;
                input.ArithmeticOperation = null;
            }

            await HandleDefault(input.IsDefaultForCustomer, input.IsDefaultForVendor);

            var record = await _priceListManager.UpdateAsync(
                id,
                input.BasePriceListId, input.Code, input.Name, input.Active,
                input.IsDefaultForCustomer, input.IsDefaultForVendor,
                input.ArithmeticOperation, input.ArithmeticFactor, input.ArithmeticFactorType,
                input.ConcurrencyStamp);

            await HandleUpdatePriceListDetail(record);

            return ObjectMapper.Map<PriceList, PriceListDto>(record);
        }

        private async Task HandleUpdatePriceListDetail(PriceList input)
        {
            var listPriceDetailUpdate = new List<PriceListDetail>();
            foreach (var item in await _priceListDetailRepository.GetListAsync(x => x.PriceListId == input.Id))
            {
                decimal addValue = 0;
                if (input.ArithmeticFactorType == ArithmeticFactorType.PERCENTAGE)
                {
                    addValue = item.BasedOnPrice.Value * (input.ArithmeticFactor ?? 0) / 100;
                }
                else addValue = input.ArithmeticFactor ?? 0;

                switch (input.ArithmeticOperation)
                {
                    case ArithmeticOperator.ADD:
                        item.Price = item.BasedOnPrice.Value + addValue;
                        break;
                    case ArithmeticOperator.SUBTRACT:
                        item.Price = item.BasedOnPrice.Value - addValue;
                        break;
                    default:
                        break;
                }

                listPriceDetailUpdate.Add(item);
            }
            await _priceListDetailRepository.UpdateManyAsync(listPriceDetailUpdate);
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

        private async Task HandlePriceListDetail(PriceList priceList)
        {
            List<PriceListDetail> priceListDetails = new();
            if (priceList.IsBase) //Get all ItemMaster
            {
                var items = (await _itemRepository.WithDetailsAsync()).ToList();
                List<Guid> group  = items.Select(x => x.UomGroupId).ToList();
                var groupDetails = await _uOMGroupDetailRepository.GetListAsync(x => group.Contains(x.UOMGroupId));

                foreach (var i in items)
                {
                    foreach (var uom in groupDetails.Where(x => x.UOMGroupId == i.UomGroupId))
                    {
                        PriceListDetail priceListDetailObj = new()
                        {
                            Description = "",
                            PriceListId = priceList.Id,
                            ItemId = i.Id,
                            UOMId = uom.AltUOMId,
                            BasedOnPrice = i.BasePrice * uom.BaseQty,
                            Price = i.BasePrice * uom.BaseQty
                        };

                        priceListDetails.Add(priceListDetailObj);
                    }
                }
            }
            else
            { //Get detail from base price list
                var basePrice = await _priceListRepository.FirstAsync(x => x.IsBase);

                var priceListDetailsBase = await _priceListDetailRepository.GetListAsync(x => x.PriceListId == basePrice.Id);
                
                foreach (var item in priceListDetailsBase)
                {
                    var newItem = item.ShallowCopy();
                    newItem.PriceList = null;
                    newItem.PriceListId = priceList.Id;
                    newItem.BasedOnPrice = item.Price;

                    decimal addValue = 0;
                    if (priceList.ArithmeticFactorType == ArithmeticFactorType.PERCENTAGE)
                    {
                        addValue = newItem.BasedOnPrice.Value * (priceList.ArithmeticFactor ?? 0) / 100;
                    }
                    else addValue = priceList.ArithmeticFactor ?? 0;

                    switch (priceList.ArithmeticOperation)
                    {
                        case ArithmeticOperator.ADD:
                            newItem.Price = newItem.BasedOnPrice.Value + addValue;
                            break;
                        case ArithmeticOperator.SUBTRACT:
                            newItem.Price = newItem.BasedOnPrice.Value - addValue;
                            break;
                        default:
                            break;
                    }
                    //Console.WriteLine(newItem);
                    priceListDetails.Add(newItem);
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