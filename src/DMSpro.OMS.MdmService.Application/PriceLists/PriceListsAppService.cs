using DMSpro.OMS.MdmService.Shared;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using DMSpro.OMS.MdmService.PriceListDetails;
using DMSpro.OMS.MdmService.Items;
using Volo.Abp.Guids;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.PriceLists
{

    [Authorize(MdmServicePermissions.PriceLists.Default)]
    public partial class PriceListsAppService 
    {
        public virtual async Task<PagedResultDto<PriceListWithNavigationPropertiesDto>> GetListAsync(GetPriceListsInput input)
        {
            var totalCount = await _priceListRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.Active, input.ArithmeticOperation, input.ArithmeticFactorMin, input.ArithmeticFactorMax, input.ArithmeticFactorType, input.IsFirstPriceList, input.BasePriceListId);
            var items = await _priceListRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Name, input.Active, input.ArithmeticOperation, input.ArithmeticFactorMin, input.ArithmeticFactorMax, input.ArithmeticFactorType, input.IsFirstPriceList, input.BasePriceListId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<PriceListWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<PriceListWithNavigationProperties>, List<PriceListWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<PriceListWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<PriceListWithNavigationProperties, PriceListWithNavigationPropertiesDto>
                (await _priceListRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<PriceListDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<PriceList, PriceListDto>(await _priceListRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetPriceListLookupAsync(LookupRequestDto input)
        {
            var query = (await _priceListRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<PriceList>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<PriceList>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.PriceLists.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _priceListRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.PriceLists.Create)]
        public virtual async Task<PriceListDto> CreateAsync(PriceListCreateDto input)
        {
            bool isFirstPriceList = _priceListRepository.CountAsync().Result == 0;
            var basePriceListId = isFirstPriceList ? null : input.BasePriceListId;

            var priceList = await _priceListManager.CreateAsync(
            basePriceListId, input.Code, input.Name, input.Active, isFirstPriceList, input.ArithmeticOperation, input.ArithmeticFactor, input.ArithmeticFactorType
            );

            List<PriceListDetail> priceListDetails = new();
            if (isFirstPriceList) //Get all ItemMaster
            {
                var items = await _itemRepository.GetListAsync();
                foreach (Item i in items)
                {
                    PriceListDetail priceListDetailObj = new PriceListDetail();
                    priceListDetailObj.Description = "";
                    priceListDetailObj.PriceListId = priceList.Id;
                    priceListDetailObj.ItemId = i.Id;
                    priceListDetailObj.UOMId = i.InventoryUOMId;
                    priceListDetailObj.BasedOnPrice = i.BasePrice;
                    priceListDetailObj.Price = i.BasePrice;

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
            else { //Get detail from base price list
                priceListDetails = _priceListDetailRepository.GetListAsync(filterText: $"PriceListId = {input.BasePriceListId}").Result;
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
            return ObjectMapper.Map<PriceList, PriceListDto>(priceList);
        }

        [Authorize(MdmServicePermissions.PriceLists.Edit)]
        public virtual async Task<PriceListDto> UpdateAsync(Guid id, PriceListUpdateDto input)
        {

            var priceList = await _priceListManager.UpdateAsync(
            id,
            input.BasePriceListId, input.Code, input.Name, input.Active, input.IsFirstPriceList, input.ArithmeticOperation, input.ArithmeticFactor, input.ArithmeticFactorType, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<PriceList, PriceListDto>(priceList);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(PriceListExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var priceLists = await _priceListRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Name, input.Active, input.ArithmeticOperation, input.ArithmeticFactorMin, input.ArithmeticFactorMax, input.ArithmeticFactorType, input.IsFirstPriceList);
            var items = priceLists.Select(item => new
            {
                Code = item.PriceList.Code,
                Name = item.PriceList.Name,
                Active = item.PriceList.Active,
                ArithmeticOperation = item.PriceList.ArithmeticOperation,
                ArithmeticFactor = item.PriceList.ArithmeticFactor,
                ArithmeticFactorType = item.PriceList.ArithmeticFactorType,
                IsFirstPriceList = item.PriceList.IsFirstPriceList,

                PriceListCode = item.PriceList?.Code,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "PriceLists.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new PriceListExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}