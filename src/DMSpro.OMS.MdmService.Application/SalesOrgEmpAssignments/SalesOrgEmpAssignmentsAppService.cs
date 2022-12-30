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
using DMSpro.OMS.MdmService.SalesOrgEmpAssignments;
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
namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{

    [Authorize(MdmServicePermissions.SalesOrgEmpAssignments.Default)]
    public class SalesOrgEmpAssignmentsAppService : ApplicationService, ISalesOrgEmpAssignmentsAppService
    {
        private readonly IDistributedCache<SalesOrgEmpAssignmentExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ISalesOrgEmpAssignmentRepository _salesOrgEmpAssignmentRepository;
        private readonly SalesOrgEmpAssignmentManager _salesOrgEmpAssignmentManager;
        private readonly IRepository<SalesOrgHierarchy, Guid> _salesOrgHierarchyRepository;
        private readonly IRepository<EmployeeProfile, Guid> _employeeProfileRepository;

        public SalesOrgEmpAssignmentsAppService(ISalesOrgEmpAssignmentRepository salesOrgEmpAssignmentRepository, SalesOrgEmpAssignmentManager salesOrgEmpAssignmentManager, IDistributedCache<SalesOrgEmpAssignmentExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<SalesOrgHierarchy, Guid> salesOrgHierarchyRepository, IRepository<EmployeeProfile, Guid> employeeProfileRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _salesOrgEmpAssignmentRepository = salesOrgEmpAssignmentRepository;
            _salesOrgEmpAssignmentManager = salesOrgEmpAssignmentManager; _salesOrgHierarchyRepository = salesOrgHierarchyRepository;
            _employeeProfileRepository = employeeProfileRepository;
        }

        public virtual async Task<PagedResultDto<SalesOrgEmpAssignmentWithNavigationPropertiesDto>> GetListAsync(GetSalesOrgEmpAssignmentsInput input)
        {
            var totalCount = await _salesOrgEmpAssignmentRepository.GetCountAsync(input.FilterText, input.IsBase, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.SalesOrgHierarchyId, input.EmployeeProfileId);
            var items = await _salesOrgEmpAssignmentRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.IsBase, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.SalesOrgHierarchyId, input.EmployeeProfileId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<SalesOrgEmpAssignmentWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<SalesOrgEmpAssignmentWithNavigationProperties>, List<SalesOrgEmpAssignmentWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<SalesOrgEmpAssignmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<SalesOrgEmpAssignmentWithNavigationProperties, SalesOrgEmpAssignmentWithNavigationPropertiesDto>
                (await _salesOrgEmpAssignmentRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _salesOrgEmpAssignmentRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<SalesOrgEmpAssignment>, IEnumerable<SalesOrgEmpAssignmentDto>>(results.data.Cast<SalesOrgEmpAssignment>());
            
            return results;
                
        }

        public virtual async Task<SalesOrgEmpAssignmentDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<SalesOrgEmpAssignment, SalesOrgEmpAssignmentDto>(await _salesOrgEmpAssignmentRepository.GetAsync(id));
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

        [Authorize(MdmServicePermissions.SalesOrgEmpAssignments.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _salesOrgEmpAssignmentRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.SalesOrgEmpAssignments.Create)]
        public virtual async Task<SalesOrgEmpAssignmentDto> CreateAsync(SalesOrgEmpAssignmentCreateDto input)
        {
            if (input.SalesOrgHierarchyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }
            if (input.EmployeeProfileId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["EmployeeProfile"]]);
            }

            var salesOrgEmpAssignment = await _salesOrgEmpAssignmentManager.CreateAsync(
            input.SalesOrgHierarchyId, input.EmployeeProfileId, input.IsBase, input.EffectiveDate, input.EndDate
            );

            return ObjectMapper.Map<SalesOrgEmpAssignment, SalesOrgEmpAssignmentDto>(salesOrgEmpAssignment);
        }

        [Authorize(MdmServicePermissions.SalesOrgEmpAssignments.Edit)]
        public virtual async Task<SalesOrgEmpAssignmentDto> UpdateAsync(Guid id, SalesOrgEmpAssignmentUpdateDto input)
        {
            if (input.SalesOrgHierarchyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["SalesOrgHierarchy"]]);
            }
            if (input.EmployeeProfileId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["EmployeeProfile"]]);
            }

            var salesOrgEmpAssignment = await _salesOrgEmpAssignmentManager.UpdateAsync(
            id,
            input.SalesOrgHierarchyId, input.EmployeeProfileId, input.IsBase, input.EffectiveDate, input.EndDate, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<SalesOrgEmpAssignment, SalesOrgEmpAssignmentDto>(salesOrgEmpAssignment);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(SalesOrgEmpAssignmentExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _salesOrgEmpAssignmentRepository.GetListAsync(input.FilterText, input.IsBase, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<SalesOrgEmpAssignment>, List<SalesOrgEmpAssignmentExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "SalesOrgEmpAssignments.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new SalesOrgEmpAssignmentExcelDownloadTokenCacheItem { Token = token },
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