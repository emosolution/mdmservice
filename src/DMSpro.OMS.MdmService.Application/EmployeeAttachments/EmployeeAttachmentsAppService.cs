using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.EmployeeProfiles;
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

namespace DMSpro.OMS.MdmService.EmployeeAttachments
{

    [Authorize(MdmServicePermissions.EmployeeProfiles.Default)]
    public partial class EmployeeAttachmentsAppService
    {
        public virtual async Task<PagedResultDto<EmployeeAttachmentWithNavigationPropertiesDto>> GetListAsync(GetEmployeeAttachmentsInput input)
        {
            var totalCount = await _employeeAttachmentRepository.GetCountAsync(input.FilterText, input.Description, input.Active, input.FileId, input.EmployeeProfileId);
            var items = await _employeeAttachmentRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Description, input.Active, input.FileId, input.EmployeeProfileId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<EmployeeAttachmentWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<EmployeeAttachmentWithNavigationProperties>, List<EmployeeAttachmentWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<EmployeeAttachmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<EmployeeAttachmentWithNavigationProperties, EmployeeAttachmentWithNavigationPropertiesDto>
                (await _employeeAttachmentRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<EmployeeAttachmentDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<EmployeeAttachment, EmployeeAttachmentDto>(await _employeeAttachmentRepository.GetAsync(id));
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

        [Authorize(MdmServicePermissions.EmployeeProfiles.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _employeeAttachmentRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.EmployeeProfiles.Create)]
        public virtual async Task<EmployeeAttachmentDto> CreateAsync(EmployeeAttachmentCreateDto input)
        {
            if (input.EmployeeProfileId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["EmployeeProfile"]]);
            }

            var employeeAttachment = await _employeeAttachmentManager.CreateAsync(
            input.EmployeeProfileId, input.Description, input.Active, input.FileId
            );

            return ObjectMapper.Map<EmployeeAttachment, EmployeeAttachmentDto>(employeeAttachment);
        }

        [Authorize(MdmServicePermissions.EmployeeProfiles.Edit)]
        public virtual async Task<EmployeeAttachmentDto> UpdateAsync(Guid id, EmployeeAttachmentUpdateDto input)
        {
            if (input.EmployeeProfileId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["EmployeeProfile"]]);
            }

            var employeeAttachment = await _employeeAttachmentManager.UpdateAsync(
            id,
            input.EmployeeProfileId, input.Description, input.Active, input.FileId, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<EmployeeAttachment, EmployeeAttachmentDto>(employeeAttachment);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(EmployeeAttachmentExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var employeeAttachments = await _employeeAttachmentRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Description, input.Active, input.FileId);
            var items = employeeAttachments.Select(item => new
            {
                Description = item.EmployeeAttachment.Description,
                Active = item.EmployeeAttachment.Active,
                FileId = item.EmployeeAttachment.FileId,

                EmployeeProfileCode = item.EmployeeProfile?.Code,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "EmployeeAttachments.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new EmployeeAttachmentExcelDownloadTokenCacheItem { Token = token },
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