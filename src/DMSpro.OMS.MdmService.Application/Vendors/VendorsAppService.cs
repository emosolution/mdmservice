using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.PriceLists;
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

namespace DMSpro.OMS.MdmService.Vendors
{

    [Authorize(MdmServicePermissions.Vendors.Default)]
    public partial class VendorsAppService
    {
        public virtual async Task<PagedResultDto<VendorWithNavigationPropertiesDto>> GetListAsync(GetVendorsInput input)
        {
            var totalCount = await _vendorRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.ShortName, input.Phone1, input.Phone2, input.ERPCode, input.Active, input.EndDateMin, input.EndDateMax, input.Street, input.Address, input.Latitude, input.Longitude, input.PriceListId, input.GeoMaster0Id, input.GeoMaster1Id, input.GeoMaster2Id, input.GeoMaster3Id, input.GeoMaster4Id, input.CompanyId, input.LinkedCompanyId);
            var items = await _vendorRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Name, input.ShortName, input.Phone1, input.Phone2, input.ERPCode, input.Active, input.EndDateMin, input.EndDateMax, input.Street, input.Address, input.Latitude, input.Longitude, input.PriceListId, input.GeoMaster0Id, input.GeoMaster1Id, input.GeoMaster2Id, input.GeoMaster3Id, input.GeoMaster4Id, input.CompanyId, input.LinkedCompanyId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<VendorWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<VendorWithNavigationProperties>, List<VendorWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<VendorWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<VendorWithNavigationProperties, VendorWithNavigationPropertiesDto>
                (await _vendorRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<VendorDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Vendor, VendorDto>(await _vendorRepository.GetAsync(id));
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

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetGeoMasterLookupAsync(LookupRequestDto input)
        {
            var query = (await _geoMasterRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<GeoMaster>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<GeoMaster>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input)
        {
            var query = (await _companyRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Company>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Company>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.Vendors.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _vendorRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.Vendors.Create)]
        public virtual async Task<VendorDto> CreateAsync(VendorCreateDto input)
        {
            if (input.PriceListId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["PriceList"]]);
            }
            if (input.CompanyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Company"]]);
            }

            var vendor = await _vendorManager.CreateAsync(
            input.PriceListId, input.GeoMaster0Id, input.GeoMaster1Id, input.GeoMaster2Id, input.GeoMaster3Id, input.GeoMaster4Id, input.CompanyId, input.LinkedCompanyId, input.Code, input.Name, input.ShortName, input.Phone1, input.Phone2, input.ERPCode, input.Active, input.Street, input.Address, input.Latitude, input.Longitude, input.EndDate
            );

            return ObjectMapper.Map<Vendor, VendorDto>(vendor);
        }

        [Authorize(MdmServicePermissions.Vendors.Edit)]
        public virtual async Task<VendorDto> UpdateAsync(Guid id, VendorUpdateDto input)
        {
            if (input.PriceListId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["PriceList"]]);
            }
            if (input.CompanyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Company"]]);
            }

            var vendor = await _vendorManager.UpdateAsync(
            id,
            input.PriceListId, input.GeoMaster0Id, input.GeoMaster1Id, input.GeoMaster2Id, input.GeoMaster3Id, input.GeoMaster4Id, input.CompanyId, input.LinkedCompanyId, input.Code, input.Name, input.ShortName, input.Phone1, input.Phone2, input.ERPCode, input.Active, input.Street, input.Address, input.Latitude, input.Longitude, input.EndDate, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Vendor, VendorDto>(vendor);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(VendorExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var vendors = await _vendorRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Name, input.ShortName, input.Phone1, input.Phone2, input.ERPCode, input.Active, input.EndDateMin, input.EndDateMax, input.Street, input.Address, input.Latitude, input.Longitude);
            var items = vendors.Select(item => new
            {
                item.Vendor.Code,
                item.Vendor.Name,
                item.Vendor.ShortName,
                item.Vendor.Phone1,
                item.Vendor.Phone2,
                item.Vendor.ERPCode,
                item.Vendor.Active,
                item.Vendor.EndDate,
                item.Vendor.Street,
                item.Vendor.Address,
                item.Vendor.Latitude,
                item.Vendor.Longitude,

                PriceListCode = item.PriceList?.Code,
                GeoMasterCode = item.GeoMaster?.Code,
                GeoMasterCode1 = item.GeoMaster1?.Code,
                GeoMasterCode2 = item.GeoMaster2?.Code,
                GeoMasterCode3 = item.GeoMaster3?.Code,
                GeoMasterCode4 = item.GeoMaster4?.Code,
                CompanyCode = item.Company?.Code,
                Company1Code = item.Company1?.Code,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Vendors.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new VendorExcelDownloadTokenCacheItem { Token = token },
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