using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.SystemDatas;
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

namespace DMSpro.OMS.MdmService.Routes
{

    [Authorize(MdmServicePermissions.Routes.Default)]
    public partial class RoutesAppService : ApplicationService, IRoutesAppService
    {
        private readonly IDistributedCache<RouteExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IRouteRepository _routeRepository;
        private readonly RouteManager _routeManager;
        private readonly IRepository<SystemData, Guid> _systemDataRepository;
        private readonly IRepository<ItemGroup, Guid> _itemGroupRepository;
        private readonly IRepository<SalesOrgHierarchy, Guid> _salesOrgHierarchyRepository;

        public RoutesAppService(IRouteRepository routeRepository, RouteManager routeManager, IDistributedCache<RouteExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<SystemData, Guid> systemDataRepository, IRepository<ItemGroup, Guid> itemGroupRepository, IRepository<SalesOrgHierarchy, Guid> salesOrgHierarchyRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _routeRepository = routeRepository;
            _routeManager = routeManager; _systemDataRepository = systemDataRepository;
            _itemGroupRepository = itemGroupRepository;
            _salesOrgHierarchyRepository = salesOrgHierarchyRepository;
        }

        public virtual async Task<PagedResultDto<RouteWithNavigationPropertiesDto>> GetListAsync(GetRoutesInput input)
        {
            var totalCount = await _routeRepository.GetCountAsync(input.FilterText, input.CheckIn, input.CheckOut, input.GPSLock, input.OutRoute, input.RouteTypeId, input.ItemGroupId, input.SalesOrgHierarchyId);
            var items = await _routeRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.CheckIn, input.CheckOut, input.GPSLock, input.OutRoute, input.RouteTypeId, input.ItemGroupId, input.SalesOrgHierarchyId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<RouteWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<RouteWithNavigationProperties>, List<RouteWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<RouteWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<RouteWithNavigationProperties, RouteWithNavigationPropertiesDto>
                (await _routeRepository.GetWithNavigationPropertiesAsync(id));
        }


        public virtual async Task<RouteDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Route, RouteDto>(await _routeRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetSystemDataLookupAsync(LookupRequestDto input)
        {
            var query = (await _systemDataRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.ValueCode != null &&
                         x.ValueCode.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<SystemData>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<SystemData>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetItemGroupLookupAsync(LookupRequestDto input)
        {
            var query = (await _itemGroupRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<ItemGroup>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ItemGroup>, List<LookupDto<Guid>>>(lookupData)
            };
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

        [Authorize(MdmServicePermissions.Routes.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _routeRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.Routes.Create)]
        public virtual async Task<RouteDto> CreateAsync(RouteCreateDto input)
        {
            if (input.RouteTypeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SystemData"]]);
            }
            if (input.ItemGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["ItemGroup"]]);
            }
            if (input.SalesOrgHierarchyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }

            var route = await _routeManager.CreateAsync(
            input.RouteTypeId, input.ItemGroupId, input.SalesOrgHierarchyId, input.CheckIn, input.CheckOut, input.GPSLock, input.OutRoute
            );

            return ObjectMapper.Map<Route, RouteDto>(route);
        }

        [Authorize(MdmServicePermissions.Routes.Edit)]
        public virtual async Task<RouteDto> UpdateAsync(Guid id, RouteUpdateDto input)
        {
            if (input.RouteTypeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SystemData"]]);
            }
            if (input.ItemGroupId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["ItemGroup"]]);
            }
            if (input.SalesOrgHierarchyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }

            var route = await _routeManager.UpdateAsync(
            id,
            input.RouteTypeId, input.ItemGroupId, input.SalesOrgHierarchyId, input.CheckIn, input.CheckOut, input.GPSLock, input.OutRoute, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Route, RouteDto>(route);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(RouteExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _routeRepository.GetListAsync(input.FilterText, input.CheckIn, input.CheckOut, input.GPSLock, input.OutRoute);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Route>, List<RouteExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Routes.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new RouteExcelDownloadTokenCacheItem { Token = token },
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