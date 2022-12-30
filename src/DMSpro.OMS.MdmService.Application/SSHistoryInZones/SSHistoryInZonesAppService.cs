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
namespace DMSpro.OMS.MdmService.SSHistoryInZones
{

    [Authorize(MdmServicePermissions.SSHistoryInZones.Default)]
    public class SSHistoryInZonesAppService : ApplicationService, ISSHistoryInZonesAppService
    {
        private readonly IDistributedCache<SSHistoryInZoneExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ISSHistoryInZoneRepository _sSHistoryInZoneRepository;
        private readonly SSHistoryInZoneManager _sSHistoryInZoneManager;
        private readonly IRepository<SalesOrgHierarchy, Guid> _salesOrgHierarchyRepository;
        private readonly IRepository<EmployeeProfile, Guid> _employeeProfileRepository;

        public SSHistoryInZonesAppService(ISSHistoryInZoneRepository sSHistoryInZoneRepository, SSHistoryInZoneManager sSHistoryInZoneManager, IDistributedCache<SSHistoryInZoneExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<SalesOrgHierarchy, Guid> salesOrgHierarchyRepository, IRepository<EmployeeProfile, Guid> employeeProfileRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _sSHistoryInZoneRepository = sSHistoryInZoneRepository;
            _sSHistoryInZoneManager = sSHistoryInZoneManager; _salesOrgHierarchyRepository = salesOrgHierarchyRepository;
            _employeeProfileRepository = employeeProfileRepository;
        }

        public virtual async Task<PagedResultDto<SSHistoryInZoneWithNavigationPropertiesDto>> GetListAsync(GetSSHistoryInZonesInput input)
        {
            var totalCount = await _sSHistoryInZoneRepository.GetCountAsync(input.FilterText, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.SalesOrgHierarchyId, input.EmployeeId);
            var items = await _sSHistoryInZoneRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.SalesOrgHierarchyId, input.EmployeeId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<SSHistoryInZoneWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<SSHistoryInZoneWithNavigationProperties>, List<SSHistoryInZoneWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<SSHistoryInZoneWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<SSHistoryInZoneWithNavigationProperties, SSHistoryInZoneWithNavigationPropertiesDto>
                (await _sSHistoryInZoneRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _sSHistoryInZoneRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<SSHistoryInZone>, IEnumerable<SSHistoryInZoneDto>>(results.data.Cast<SSHistoryInZone>());
            
            return results;
                
        }

        public virtual async Task<SSHistoryInZoneDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<SSHistoryInZone, SSHistoryInZoneDto>(await _sSHistoryInZoneRepository.GetAsync(id));
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

        [Authorize(MdmServicePermissions.SSHistoryInZones.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _sSHistoryInZoneRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.SSHistoryInZones.Create)]
        public virtual async Task<SSHistoryInZoneDto> CreateAsync(SSHistoryInZoneCreateDto input)
        {
            if (input.SalesOrgHierarchyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }
            if (input.EmployeeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["EmployeeProfile"]]);
            }

            var ssHistoryInZone = await _sSHistoryInZoneManager.CreateAsync(
            input.SalesOrgHierarchyId, input.EmployeeId, input.EffectiveDate, input.EndDate
            );

            return ObjectMapper.Map<SSHistoryInZone, SSHistoryInZoneDto>(ssHistoryInZone);
        }

        [Authorize(MdmServicePermissions.SSHistoryInZones.Edit)]
        public virtual async Task<SSHistoryInZoneDto> UpdateAsync(Guid id, SSHistoryInZoneUpdateDto input)
        {
            if (input.SalesOrgHierarchyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }
            if (input.EmployeeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["EmployeeProfile"]]);
            }

            var ssHistoryInZone = await _sSHistoryInZoneManager.UpdateAsync(
            id,
            input.SalesOrgHierarchyId, input.EmployeeId, input.EffectiveDate, input.EndDate, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<SSHistoryInZone, SSHistoryInZoneDto>(ssHistoryInZone);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(SSHistoryInZoneExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _sSHistoryInZoneRepository.GetListAsync(input.FilterText, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<SSHistoryInZone>, List<SSHistoryInZoneExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "SSHistoryInZones.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new SSHistoryInZoneExcelDownloadTokenCacheItem { Token = token },
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