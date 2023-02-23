using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.EmployeeProfiles;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
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
            var totalCount = await _employeeImageRepository.GetCountAsync(input.FilterText, input.Description, input.Active, input.IsAvatar, input.FileId, input.EmployeeProfileId);
            var items = await _employeeImageRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Description, input.Active, input.IsAvatar, input.FileId, input.EmployeeProfileId, input.Sorting, input.MaxResultCount, input.SkipCount);

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

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(EmployeeImageExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var employeeImages = await _employeeImageRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Description, input.Active, input.IsAvatar, input.FileId);
            var items = employeeImages.Select(item => new
            {
                Description = item.EmployeeImage.Description,
                Active = item.EmployeeImage.Active,
                IsAvatar = item.EmployeeImage.IsAvatar,
                FileId = item.EmployeeImage.FileId,

                EmployeeProfileCode = item.EmployeeProfile?.Code,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
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