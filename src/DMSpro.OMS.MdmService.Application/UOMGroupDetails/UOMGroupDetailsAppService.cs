using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.UOMGroups;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.Domain.Repositories;
using static DMSpro.OMS.MdmService.Permissions.MdmServicePermissions;
using DMSpro.OMS.MdmService.PriceListDetails;
using DMSpro.OMS.MdmService.PriceLists;

namespace DMSpro.OMS.MdmService.UOMGroupDetails
{

    [Authorize(MdmServicePermissions.UOMGroupDetails.Default)]
    public partial class UOMGroupDetailsAppService
    {
        public virtual async Task<PagedResultDto<UOMGroupDetailWithNavigationPropertiesDto>> GetListAsync(GetUOMGroupDetailsInput input)
        {
            var totalCount = await _uOMGroupDetailRepository.GetCountAsync(input.FilterText, input.AltQtyMin, input.AltQtyMax, input.BaseQtyMin, input.BaseQtyMax, input.Active, input.UOMGroupId, input.AltUOMId, input.BaseUOMId);
            var items = await _uOMGroupDetailRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.AltQtyMin, input.AltQtyMax, input.BaseQtyMin, input.BaseQtyMax, input.Active, input.UOMGroupId, input.AltUOMId, input.BaseUOMId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<UOMGroupDetailWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<UOMGroupDetailWithNavigationProperties>, List<UOMGroupDetailWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<UOMGroupDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<UOMGroupDetailWithNavigationProperties, UOMGroupDetailWithNavigationPropertiesDto>
                (await _uOMGroupDetailRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<UOMGroupDetailDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<UOMGroupDetail, UOMGroupDetailDto>(await _uOMGroupDetailRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetUOMGroupLookupAsync(LookupRequestDto input)
        {
            var query = (await _uOMGroupRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<UOMGroup>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<UOMGroup>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetUOMLookupAsync(LookupRequestDto input)
        {
            var query = (await _uOMRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<UOM>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<UOM>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.UOMGroupDetails.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            var uomGroupDetail = await _uOMGroupDetailRepository.GetAsync(id);
            //base uom cannot inactive
            if (uomGroupDetail.AltUOMId == uomGroupDetail.BaseUOMId && uomGroupDetail.AltQty == 1 && uomGroupDetail.BaseQty == 1)
            {
                throw new UserFriendlyException(L["Error:General:DeleteContraint:550"]);
            }
            if (await _itemRepository.AnyAsync(x => x.UomGroupId == uomGroupDetail.UOMGroupId))
            {
                throw new UserFriendlyException(L["Error:General:DeleteContraint:550"]);
            }
            await _uOMGroupDetailRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.UOMGroupDetails.Create)]
        public virtual async Task<UOMGroupDetailDto> CreateAsync(UOMGroupDetailCreateDto input)
        {
            if (input.UOMGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOMGroup"]]);
            }
            if (input.AltUOMId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }
            if (input.BaseUOMId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }

            //can not duplicate altUomId in group
            if (await _uOMGroupDetailRepository.AnyAsync(x => x.UOMGroupId == input.UOMGroupId && x.AltUOMId == input.AltUOMId && x.Active))
            {
                throw new UserFriendlyException(L["Error:General:InsertContraint:550"]);
            }

            var uOMGroupDetail = await _uOMGroupDetailManager.CreateAsync(
            input.UOMGroupId, input.AltUOMId, input.BaseUOMId, altQty: 1, input.BaseQty, active: true);

            await InsertToPriceList(input.UOMGroupId, input.AltUOMId, input.BaseQty);


            return ObjectMapper.Map<UOMGroupDetail, UOMGroupDetailDto>(uOMGroupDetail);
        }

        private async Task InsertToPriceList(Guid uOMGroupId, Guid altUOMId, uint baseQty)
        {
            var items = await _itemRepository.GetListAsync(x => x.UomGroupId == uOMGroupId);
            var priceLists = await _priceListRepository.GetListAsync();
            List<PriceListDetail> details = new();
            foreach (var priceList in priceLists)
            {
                foreach (var i in items)
                {
                    PriceListDetail detail = new()
                    {
                        Description = "",
                        PriceListId = priceList.Id,
                        ItemId = i.Id,
                        UOMId = altUOMId,
                        BasedOnPrice = i.BasePrice * baseQty,
                        Price = i.BasePrice * baseQty
                    };

                    switch (priceList.ArithmeticOperation)
                    {
                        case ArithmeticOperator.ADD:
                            detail.Price = i.BasePrice + priceList.ArithmeticFactor.Value * (priceList.ArithmeticFactorType == ArithmeticFactorType.PERCENTAGE ? priceList.ArithmeticFactor.Value / 100 : 1);
                            break;
                        case ArithmeticOperator.SUBTRACT:
                            detail.Price = i.BasePrice - priceList.ArithmeticFactor.Value * (priceList.ArithmeticFactorType == ArithmeticFactorType.PERCENTAGE ? priceList.ArithmeticFactor.Value / 100 : 1);
                            break;
                        default:
                            break;
                    }
                    details.Add(detail);
                }
            }
        }

        [Authorize(MdmServicePermissions.UOMGroupDetails.Edit)]
        public virtual async Task<UOMGroupDetailDto> UpdateAsync(Guid id, UOMGroupDetailUpdateDto input)
        {
            if (input.UOMGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOMGroup"]]);
            }
            if (input.AltUOMId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }
            if (input.BaseUOMId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["UOM"]]);
            }

            var uomGroupDetail = await _uOMGroupDetailRepository.GetAsync(x => x.Id == id);
            //base uom cannot inactive
            if (uomGroupDetail.AltUOMId == uomGroupDetail.BaseUOMId && uomGroupDetail.AltQty == 1 && uomGroupDetail.BaseQty == 1)
            {
                throw new UserFriendlyException(L["Error:General:UpdateContraint:550"]);
            }

            if (await _itemRepository.AnyAsync(x => x.UomGroupId == uomGroupDetail.UOMGroupId))
            {
                throw new UserFriendlyException(L["Error:General:UpdateContraint:550"]);
            }

            if (input.Active)
            {
                //can not duplicate altUomId in group
                if (await _uOMGroupDetailRepository.AnyAsync(x => x.UOMGroupId == input.UOMGroupId && x.AltUOMId == input.AltUOMId && x.Active && x.Id != id))
                {
                    throw new UserFriendlyException(L["Error:General:InsertContraint:550"]);
                }
            }

            var uOMGroupDetail = await _uOMGroupDetailManager.UpdateAsync(
            id,
            input.UOMGroupId, input.AltUOMId, input.BaseUOMId, input.AltQty, input.BaseQty, input.Active, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<UOMGroupDetail, UOMGroupDetailDto>(uOMGroupDetail);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(UOMGroupDetailExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _uOMGroupDetailRepository.GetListAsync(input.FilterText, input.AltQtyMin, input.AltQtyMax, input.BaseQtyMin, input.BaseQtyMax, input.Active);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<UOMGroupDetail>, List<UOMGroupDetailExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "UOMGroupDetails.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new UOMGroupDetailExcelDownloadTokenCacheItem { Token = token },
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