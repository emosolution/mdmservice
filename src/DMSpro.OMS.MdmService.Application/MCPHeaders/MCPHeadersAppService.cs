using DMSpro.OMS.MdmService.Companies;
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
using DMSpro.OMS.MdmService.MCPHeaders;
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
namespace DMSpro.OMS.MdmService.MCPHeaders
{

    [Authorize(MdmServicePermissions.MCPHeaders.Default)]
    public class MCPHeadersAppService : ApplicationService, IMCPHeadersAppService
    {
        private readonly IDistributedCache<MCPHeaderExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IMCPHeaderRepository _mCPHeaderRepository;
        private readonly MCPHeaderManager _mCPHeaderManager;
        private readonly IRepository<SalesOrgHierarchy, Guid> _salesOrgHierarchyRepository;
        private readonly IRepository<Company, Guid> _companyRepository;

        public MCPHeadersAppService(IMCPHeaderRepository mCPHeaderRepository, MCPHeaderManager mCPHeaderManager, IDistributedCache<MCPHeaderExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<SalesOrgHierarchy, Guid> salesOrgHierarchyRepository, IRepository<Company, Guid> companyRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _mCPHeaderRepository = mCPHeaderRepository;
            _mCPHeaderManager = mCPHeaderManager; _salesOrgHierarchyRepository = salesOrgHierarchyRepository;
            _companyRepository = companyRepository;
        }

        public virtual async Task<PagedResultDto<MCPHeaderWithNavigationPropertiesDto>> GetListAsync(GetMCPHeadersInput input)
        {
            var totalCount = await _mCPHeaderRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.RouteId, input.CompanyId);
            var items = await _mCPHeaderRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Name, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.RouteId, input.CompanyId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<MCPHeaderWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<MCPHeaderWithNavigationProperties>, List<MCPHeaderWithNavigationPropertiesDto>>(items)
            };
        }


        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _mCPHeaderRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<MCPHeader>, IEnumerable<MCPHeaderDto>>(results.data.Cast<MCPHeader>());
            
            return results;
                
        }

        public virtual async Task<MCPHeaderWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<MCPHeaderWithNavigationProperties, MCPHeaderWithNavigationPropertiesDto>
                (await _mCPHeaderRepository.GetWithNavigationPropertiesAsync(id));
        }


        public virtual async Task<MCPHeaderDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<MCPHeader, MCPHeaderDto>(await _mCPHeaderRepository.GetAsync(id));
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

        [Authorize(MdmServicePermissions.MCPHeaders.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _mCPHeaderRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.MCPHeaders.Create)]
        public virtual async Task<MCPHeaderDto> CreateAsync(MCPHeaderCreateDto input)
        {
            if (input.RouteId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }
            if (input.CompanyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Company"]]);
            }

            var mcpHeader = await _mCPHeaderManager.CreateAsync(
            input.RouteId, input.CompanyId, input.Code, input.Name, input.EffectiveDate, input.EndDate
            );

            return ObjectMapper.Map<MCPHeader, MCPHeaderDto>(mcpHeader);
        }

        [Authorize(MdmServicePermissions.MCPHeaders.Edit)]
        public virtual async Task<MCPHeaderDto> UpdateAsync(Guid id, MCPHeaderUpdateDto input)
        {
            if (input.RouteId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }
            if (input.CompanyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Company"]]);
            }

            var mcpHeader = await _mCPHeaderManager.UpdateAsync(
            id,
            input.RouteId, input.CompanyId, input.Code, input.Name, input.EffectiveDate, input.EndDate, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<MCPHeader, MCPHeaderDto>(mcpHeader);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(MCPHeaderExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _mCPHeaderRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<MCPHeader>, List<MCPHeaderExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "MCPHeaders.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new MCPHeaderExcelDownloadTokenCacheItem { Token = token },
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