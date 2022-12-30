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
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.EmployeeImages;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;

using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Lib.Parser;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.EmployeeImages
{

    [Authorize(MdmServicePermissions.EmployeeProfiles.Default)]
    public class EmployeeImagesAppService : ApplicationService, IEmployeeImagesAppService
    {
        private readonly IDistributedCache<EmployeeImageExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IEmployeeImageRepository _employeeImageRepository;
        private readonly EmployeeImageManager _employeeImageManager;
        private readonly IRepository<EmployeeProfile, Guid> _employeeProfileRepository;

        public EmployeeImagesAppService(IEmployeeImageRepository employeeImageRepository, EmployeeImageManager employeeImageManager, IDistributedCache<EmployeeImageExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<EmployeeProfile, Guid> employeeProfileRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _employeeImageRepository = employeeImageRepository;
            _employeeImageManager = employeeImageManager; _employeeProfileRepository = employeeProfileRepository;
        }

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

        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _employeeImageRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<EmployeeImage>, IEnumerable<EmployeeImageDto>>(results.data.Cast<EmployeeImage>());
            
            return results;
                
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