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
using DMSpro.OMS.MdmService.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.RouteAssignments
{

    [Authorize(MdmServicePermissions.RouteAssignments.Default)]
    public partial class RouteAssignmentsAppService 
    {
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