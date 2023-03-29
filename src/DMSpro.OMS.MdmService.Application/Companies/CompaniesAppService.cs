using DMSpro.OMS.MdmService.GeoMasters;
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
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Companies
{

    [Authorize(MdmServicePermissions.CompanyMasters.Default)]
    public partial class CompaniesAppService
    { 
        public virtual async Task<PagedResultDto<CompanyWithNavigationPropertiesDto>> GetListAsync(GetCompaniesInput input)
        {
            var totalCount = await _companyRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.Street, input.Address, input.Phone, input.License, input.TaxCode, input.VATName, input.VATAddress, input.ERPCode, input.Active, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.IsHO, input.Latitude, input.Longitude, input.ContactName, input.ContactPhone, input.ParentId, input.GeoLevel0Id, input.GeoLevel1Id, input.GeoLevel2Id, input.GeoLevel3Id, input.GeoLevel4Id);
            var items = await _companyRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Name, input.Street, input.Address, input.Phone, input.License, input.TaxCode, input.VATName, input.VATAddress, input.ERPCode, input.Active, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.IsHO, input.Latitude, input.Longitude, input.ContactName, input.ContactPhone, input.ParentId, input.GeoLevel0Id, input.GeoLevel1Id, input.GeoLevel2Id, input.GeoLevel3Id, input.GeoLevel4Id, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CompanyWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CompanyWithNavigationProperties>, List<CompanyWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<CompanyWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<CompanyWithNavigationProperties, CompanyWithNavigationPropertiesDto>
                (await _companyRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<CompanyDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Company, CompanyDto>(await _companyRepository.GetAsync(id));
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

        [Authorize(MdmServicePermissions.CompanyMasters.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _companyRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.CompanyMasters.Create)]
        public virtual async Task<CompanyDto> CreateAsync(CompanyCreateDto input)
        {
            await CheckCodeUniqueness(input.Code);
            var company = await _companyManager.CreateAsync(
            input.ParentId, input.GeoLevel0Id, input.GeoLevel1Id, input.GeoLevel2Id, input.GeoLevel3Id, input.GeoLevel4Id, input.Code, input.Name, input.Street, input.Address, input.Phone, input.License, input.TaxCode, input.VATName, input.VATAddress, input.ERPCode, input.Active, input.EffectiveDate, input.IsHO, input.Latitude, input.Longitude, input.ContactName, input.ContactPhone, input.EndDate
            );

            return ObjectMapper.Map<Company, CompanyDto>(company);
        }

        [Authorize(MdmServicePermissions.CompanyMasters.Edit)]
        public virtual async Task<CompanyDto> UpdateAsync(Guid id, CompanyUpdateDto input)
        {
            await CheckCodeUniqueness(input.Code, id);
            var company = await _companyManager.UpdateAsync(
            id,
            input.ParentId, input.GeoLevel0Id, input.GeoLevel1Id, input.GeoLevel2Id, input.GeoLevel3Id, input.GeoLevel4Id, input.Code, input.Name, input.Street, input.Address, input.Phone, input.License, input.TaxCode, input.VATName, input.VATAddress, input.ERPCode, input.Active, input.EffectiveDate, input.IsHO, input.Latitude, input.Longitude, input.ContactName, input.ContactPhone, input.EndDate, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Company, CompanyDto>(company);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CompanyExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var companies = await _companyRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Name, input.Street, input.Address, input.Phone, input.License, input.TaxCode, input.VATName, input.VATAddress, input.ERPCode, input.Active, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.IsHO, input.Latitude, input.Longitude, input.ContactName, input.ContactPhone);
            var items = companies.Select(item => new
            {
                item.Company.Code,
                item.Company.Name,
                item.Company.Street,
                item.Company.Address,
                item.Company.Phone,
                item.Company.License,
                item.Company.TaxCode,
                item.Company.VATName,
                item.Company.VATAddress,
                item.Company.ERPCode,
                item.Company.Active,
                item.Company.EffectiveDate,
                item.Company.EndDate,
                item.Company.IsHO,
                item.Company.Latitude,
                item.Company.Longitude,
                item.Company.ContactName,
                item.Company.ContactPhone,
                GeoMasterCode = item.GeoMaster?.Code,
                GeoMasterCode1 = item.GeoMaster1?.Code,
                GeoMasterCode2 = item.GeoMaster2?.Code,
                GeoMasterCode3 = item.GeoMaster3?.Code,
                GeoMasterCode4 = item.GeoMaster4?.Code,
            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Companies.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CompanyExcelDownloadTokenCacheItem { Token = token },
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