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

namespace DMSpro.OMS.MdmService.EmployeeImages
{

    [Authorize(MdmServicePermissions.EmployeeProfiles.Default)]
    public partial class EmployeeImagesAppService
    {
        public virtual async Task<PagedResultDto<EmployeeImageWithNavigationPropertiesDto>> GetListAsync(GetEmployeeImagesInput input)
        {
            var totalCount = await _employeeImageRepository.GetCountAsync(input.FilterText, input.Description, input.url, input.Active, input.IsAvatar, input.EmployeeProfileId);
            var items = await _employeeImageRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Description, input.url, input.Active, input.IsAvatar, input.EmployeeProfileId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<EmployeeImageWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<EmployeeImageWithNavigationProperties>, List<EmployeeImageWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<EmployeeImageWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<EmployeeImageWithNavigationProperties, EmployeeImageWithNavigationPropertiesDto>
                (await _employeeImageRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<EmployeeImageDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<EmployeeImage, EmployeeImageDto>(await _employeeImageRepository.GetAsync(id));
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
            await _employeeImageRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.EmployeeProfiles.Create)]
        public virtual async Task<EmployeeImageDto> CreateAsync(EmployeeImageCreateDto input)
        {
            if (input.EmployeeProfileId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["EmployeeProfile"]]);
            }

            var employeeImage = await _employeeImageManager.CreateAsync(
            input.EmployeeProfileId, input.Description, input.url, input.Active, input.IsAvatar
            );

            return ObjectMapper.Map<EmployeeImage, EmployeeImageDto>(employeeImage);
        }

        [Authorize(MdmServicePermissions.EmployeeProfiles.Edit)]
        public virtual async Task<EmployeeImageDto> UpdateAsync(Guid id, EmployeeImageUpdateDto input)
        {
            if (input.EmployeeProfileId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["EmployeeProfile"]]);
            }

            var employeeImage = await _employeeImageManager.UpdateAsync(
            id,
            input.EmployeeProfileId, input.Description, input.url, input.Active, input.IsAvatar, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<EmployeeImage, EmployeeImageDto>(employeeImage);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(EmployeeImageExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _employeeImageRepository.GetListAsync(input.FilterText, input.Description, input.url, input.Active, input.IsAvatar);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<EmployeeImage>, List<EmployeeImageExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "EmployeeImages.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new EmployeeImageExcelDownloadTokenCacheItem { Token = token },
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