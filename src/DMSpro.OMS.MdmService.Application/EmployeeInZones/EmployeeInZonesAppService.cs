using DMSpro.OMS.MdmService.EmployeeProfiles;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using DMSpro.OMS.MdmService.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using DMSpro.OMS.MdmService.Shared;

using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Lib.Parser;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.EmployeeInZones
{

    [Authorize(MdmServicePermissions.EmployeeInZones.Default)]
    public class EmployeeInZonesAppService : ApplicationService, IEmployeeInZonesAppService
    {
        private readonly IDistributedCache<EmployeeInZoneExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IEmployeeInZoneRepository _employeeInZoneRepository;
        private readonly EmployeeInZoneManager _employeeInZoneManager;
        private readonly IRepository<SalesOrgHierarchy, Guid> _salesOrgHierarchyRepository;
        private readonly IRepository<EmployeeProfile, Guid> _employeeProfileRepository;

        public EmployeeInZonesAppService(IEmployeeInZoneRepository employeeInZoneRepository, EmployeeInZoneManager employeeInZoneManager, IDistributedCache<EmployeeInZoneExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<SalesOrgHierarchy, Guid> salesOrgHierarchyRepository, IRepository<EmployeeProfile, Guid> employeeProfileRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _employeeInZoneRepository = employeeInZoneRepository;
            _employeeInZoneManager = employeeInZoneManager; _salesOrgHierarchyRepository = salesOrgHierarchyRepository;
            _employeeProfileRepository = employeeProfileRepository;
        }

        public virtual async Task<PagedResultDto<EmployeeInZoneWithNavigationPropertiesDto>> GetListAsync(GetEmployeeInZonesInput input)
        {
            var totalCount = await _employeeInZoneRepository.GetCountAsync(input.FilterText, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDate, input.SalesOrgHierarchyId, input.EmployeeId);
            var items = await _employeeInZoneRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDate, input.SalesOrgHierarchyId, input.EmployeeId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<EmployeeInZoneWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<EmployeeInZoneWithNavigationProperties>, List<EmployeeInZoneWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<EmployeeInZoneWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<EmployeeInZoneWithNavigationProperties, EmployeeInZoneWithNavigationPropertiesDto>
                (await _employeeInZoneRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _employeeInZoneRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<EmployeeInZone>, IEnumerable<EmployeeInZoneDto>>(results.data.Cast<EmployeeInZone>());
            
            return results;
                
        }

        public virtual async Task<EmployeeInZoneDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<EmployeeInZone, EmployeeInZoneDto>(await _employeeInZoneRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input)
        {
            var query = (await _salesOrgHierarchyRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<SalesOrgHierarchy>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<SalesOrgHierarchy>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetEmployeeProfileLookupAsync(LookupRequestDto input)
        {
            var query = (await _employeeProfileRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<EmployeeProfile>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<EmployeeProfile>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.EmployeeInZones.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _employeeInZoneRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.EmployeeInZones.Create)]
        public virtual async Task<EmployeeInZoneDto> CreateAsync(EmployeeInZoneCreateDto input)
        {
            if (input.SalesOrgHierarchyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }
            if (input.EmployeeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["EmployeeProfile"]]);
            }

            var employeeInZone = await _employeeInZoneManager.CreateAsync(
            input.SalesOrgHierarchyId, input.EmployeeId, input.EffectiveDate, input.EndDate
            );

            return ObjectMapper.Map<EmployeeInZone, EmployeeInZoneDto>(employeeInZone);
        }

        [Authorize(MdmServicePermissions.EmployeeInZones.Edit)]
        public virtual async Task<EmployeeInZoneDto> UpdateAsync(Guid id, EmployeeInZoneUpdateDto input)
        {
            if (input.SalesOrgHierarchyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }
            if (input.EmployeeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["EmployeeProfile"]]);
            }

            var employeeInZone = await _employeeInZoneManager.UpdateAsync(
            id,
            input.SalesOrgHierarchyId, input.EmployeeId, input.EffectiveDate, input.EndDate, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<EmployeeInZone, EmployeeInZoneDto>(employeeInZone);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(EmployeeInZoneExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _employeeInZoneRepository.GetListAsync(input.FilterText, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDate);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<EmployeeInZone>, List<EmployeeInZoneExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "EmployeeInZones.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new EmployeeInZoneExcelDownloadTokenCacheItem { Token = token },
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