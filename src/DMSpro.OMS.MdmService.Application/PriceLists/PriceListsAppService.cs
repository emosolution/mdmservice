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
                throw new UserFriendlyException(L[""]);
            }
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

            await HandlePriceListDetail(priceList);

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
                    }
                }
            }
            else
            { //Get detail from base price list
                priceListDetails = await _priceListDetailRepository.GetListAsync(x => x.PriceListId == priceList.BasePriceListId);
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