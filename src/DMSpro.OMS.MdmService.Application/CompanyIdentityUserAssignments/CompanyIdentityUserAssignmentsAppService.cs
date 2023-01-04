using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.Companies;
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
using DMSpro.OMS.MdmService.CompanyIdentityUserAssignments;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{

    [Authorize(MdmServicePermissions.CompanyIdentityUserAssignments.Default)]
    public class CompanyIdentityUserAssignmentsAppService : ApplicationService, ICompanyIdentityUserAssignmentsAppService
    {
        private readonly IDistributedCache<CompanyIdentityUserAssignmentExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ICompanyIdentityUserAssignmentRepository _companyIdentityUserAssignmentRepository;
        private readonly CompanyIdentityUserAssignmentManager _companyIdentityUserAssignmentManager;
        private readonly IRepository<Company, Guid> _companyRepository;

        public CompanyIdentityUserAssignmentsAppService(ICompanyIdentityUserAssignmentRepository companyIdentityUserAssignmentRepository, CompanyIdentityUserAssignmentManager companyIdentityUserAssignmentManager, IDistributedCache<CompanyIdentityUserAssignmentExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Company, Guid> companyRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _companyIdentityUserAssignmentRepository = companyIdentityUserAssignmentRepository;
            _companyIdentityUserAssignmentManager = companyIdentityUserAssignmentManager; _companyRepository = companyRepository;
        }

        public virtual async Task<PagedResultDto<CompanyIdentityUserAssignmentWithNavigationPropertiesDto>> GetListAsync(GetCompanyIdentityUserAssignmentsInput input)
        {
            var totalCount = await _companyIdentityUserAssignmentRepository.GetCountAsync(input.FilterText, input.IdentityUserId, input.CompanyId);
            var items = await _companyIdentityUserAssignmentRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.IdentityUserId, input.CompanyId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CompanyIdentityUserAssignmentWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CompanyIdentityUserAssignmentWithNavigationProperties>, List<CompanyIdentityUserAssignmentWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<CompanyIdentityUserAssignmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<CompanyIdentityUserAssignmentWithNavigationProperties, CompanyIdentityUserAssignmentWithNavigationPropertiesDto>
                (await _companyIdentityUserAssignmentRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<CompanyIdentityUserAssignmentDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CompanyIdentityUserAssignment, CompanyIdentityUserAssignmentDto>(await _companyIdentityUserAssignmentRepository.GetAsync(id));
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

        [Authorize(MdmServicePermissions.CompanyIdentityUserAssignments.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _companyIdentityUserAssignmentRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.CompanyIdentityUserAssignments.Create)]
        public virtual async Task<CompanyIdentityUserAssignmentDto> CreateAsync(CompanyIdentityUserAssignmentCreateDto input)
        {
            if (input.CompanyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Company"]]);
            }

            var companyIdentityUserAssignment = await _companyIdentityUserAssignmentManager.CreateAsync(
            input.CompanyId, input.IdentityUserId
            );

            return ObjectMapper.Map<CompanyIdentityUserAssignment, CompanyIdentityUserAssignmentDto>(companyIdentityUserAssignment);
        }

        [Authorize(MdmServicePermissions.CompanyIdentityUserAssignments.Edit)]
        public virtual async Task<CompanyIdentityUserAssignmentDto> UpdateAsync(Guid id, CompanyIdentityUserAssignmentUpdateDto input)
        {
            if (input.CompanyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Company"]]);
            }

            var companyIdentityUserAssignment = await _companyIdentityUserAssignmentManager.UpdateAsync(
            id,
            input.CompanyId, input.IdentityUserId, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<CompanyIdentityUserAssignment, CompanyIdentityUserAssignmentDto>(companyIdentityUserAssignment);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CompanyIdentityUserAssignmentExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _companyIdentityUserAssignmentRepository.GetListAsync(input.FilterText, input.IdentityUserId);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<CompanyIdentityUserAssignment>, List<CompanyIdentityUserAssignmentExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "CompanyIdentityUserAssignments.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new CompanyIdentityUserAssignmentExcelDownloadTokenCacheItem { Token = token },
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