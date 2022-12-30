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
using DMSpro.OMS.MdmService.RouteAssignments;
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
namespace DMSpro.OMS.MdmService.RouteAssignments
{

    [Authorize(MdmServicePermissions.RouteAssignments.Default)]
    public class RouteAssignmentsAppService : ApplicationService, IRouteAssignmentsAppService
    {
        private readonly IDistributedCache<RouteAssignmentExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IRouteAssignmentRepository _routeAssignmentRepository;
        private readonly RouteAssignmentManager _routeAssignmentManager;
        private readonly IRepository<SalesOrgHierarchy, Guid> _salesOrgHierarchyRepository;
        private readonly IRepository<EmployeeProfile, Guid> _employeeProfileRepository;

        public RouteAssignmentsAppService(IRouteAssignmentRepository routeAssignmentRepository, RouteAssignmentManager routeAssignmentManager, IDistributedCache<RouteAssignmentExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<SalesOrgHierarchy, Guid> salesOrgHierarchyRepository, IRepository<EmployeeProfile, Guid> employeeProfileRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _routeAssignmentRepository = routeAssignmentRepository;
            _routeAssignmentManager = routeAssignmentManager; _salesOrgHierarchyRepository = salesOrgHierarchyRepository;
            _employeeProfileRepository = employeeProfileRepository;
        }

        public virtual async Task<PagedResultDto<RouteAssignmentWithNavigationPropertiesDto>> GetListAsync(GetRouteAssignmentsInput input)
        {
            var totalCount = await _routeAssignmentRepository.GetCountAsync(input.FilterText, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.RouteId, input.EmployeeId);
            var items = await _routeAssignmentRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.RouteId, input.EmployeeId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<RouteAssignmentWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<RouteAssignmentWithNavigationProperties>, List<RouteAssignmentWithNavigationPropertiesDto>>(items)
            };
        }


        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _routeAssignmentRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<RouteAssignment>, IEnumerable<RouteAssignmentDto>>(results.data.Cast<RouteAssignment>());
            
            return results;
                
        }

        public virtual async Task<RouteAssignmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<RouteAssignmentWithNavigationProperties, RouteAssignmentWithNavigationPropertiesDto>
                (await _routeAssignmentRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<RouteAssignmentDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<RouteAssignment, RouteAssignmentDto>(await _routeAssignmentRepository.GetAsync(id));
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

        [Authorize(MdmServicePermissions.RouteAssignments.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _routeAssignmentRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.RouteAssignments.Create)]
        public virtual async Task<RouteAssignmentDto> CreateAsync(RouteAssignmentCreateDto input)
        {
            if (input.RouteId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }
            if (input.EmployeeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["EmployeeProfile"]]);
            }

            var routeAssignment = await _routeAssignmentManager.CreateAsync(
            input.RouteId, input.EmployeeId, input.EffectiveDate, input.EndDate
            );

            return ObjectMapper.Map<RouteAssignment, RouteAssignmentDto>(routeAssignment);
        }

        [Authorize(MdmServicePermissions.RouteAssignments.Edit)]
        public virtual async Task<RouteAssignmentDto> UpdateAsync(Guid id, RouteAssignmentUpdateDto input)
        {
            if (input.RouteId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }
            if (input.EmployeeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["EmployeeProfile"]]);
            }

            var routeAssignment = await _routeAssignmentManager.UpdateAsync(
            id,
            input.RouteId, input.EmployeeId, input.EffectiveDate, input.EndDate, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<RouteAssignment, RouteAssignmentDto>(routeAssignment);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(RouteAssignmentExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _routeAssignmentRepository.GetListAsync(input.FilterText, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<RouteAssignment>, List<RouteAssignmentExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "RouteAssignments.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new RouteAssignmentExcelDownloadTokenCacheItem { Token = token },
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