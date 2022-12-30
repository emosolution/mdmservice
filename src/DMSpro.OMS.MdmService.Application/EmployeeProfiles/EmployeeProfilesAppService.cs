using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.SystemDatas;
using DMSpro.OMS.MdmService.WorkingPositions;
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
using DMSpro.OMS.MdmService.EmployeeProfiles;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;

using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Lib.Parser;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.EmployeeProfiles
{

    [Authorize(MdmServicePermissions.EmployeeProfiles.Default)]
    public class EmployeeProfilesAppService : ApplicationService, IEmployeeProfilesAppService
    {
        private readonly IDistributedCache<EmployeeProfileExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IEmployeeProfileRepository _employeeProfileRepository;
        private readonly EmployeeProfileManager _employeeProfileManager;
        private readonly IRepository<WorkingPosition, Guid> _workingPositionRepository;
        private readonly IRepository<SystemData, Guid> _systemDataRepository;

        public EmployeeProfilesAppService(IEmployeeProfileRepository employeeProfileRepository, EmployeeProfileManager employeeProfileManager, IDistributedCache<EmployeeProfileExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<WorkingPosition, Guid> workingPositionRepository, IRepository<SystemData, Guid> systemDataRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _employeeProfileRepository = employeeProfileRepository;
            _employeeProfileManager = employeeProfileManager; _workingPositionRepository = workingPositionRepository;
            _systemDataRepository = systemDataRepository;
        }

        public virtual async Task<PagedResultDto<EmployeeProfileWithNavigationPropertiesDto>> GetListAsync(GetEmployeeProfilesInput input)
        {
            var totalCount = await _employeeProfileRepository.GetCountAsync(input.FilterText, input.Code, input.ERPCode, input.FirstName, input.LastName, input.DateOfBirthMin, input.DateOfBirthMax, input.IdCardNumber, input.Email, input.Phone, input.Address, input.Active, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.IdentityUserId, input.WorkingPositionId, input.EmployeeTypeId);
            var items = await _employeeProfileRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.ERPCode, input.FirstName, input.LastName, input.DateOfBirthMin, input.DateOfBirthMax, input.IdCardNumber, input.Email, input.Phone, input.Address, input.Active, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.IdentityUserId, input.WorkingPositionId, input.EmployeeTypeId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<EmployeeProfileWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<EmployeeProfileWithNavigationProperties>, List<EmployeeProfileWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<EmployeeProfileWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<EmployeeProfileWithNavigationProperties, EmployeeProfileWithNavigationPropertiesDto>
                (await _employeeProfileRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {   
            var items = await _employeeProfileRepository.GetQueryableAsync();    
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption,inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);    
            results.data = ObjectMapper.Map<IEnumerable<EmployeeProfile>, IEnumerable<EmployeeProfileDto>>(results.data.Cast<EmployeeProfile>());
            
            return results;
                
        }
        public virtual async Task<EmployeeProfileDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<EmployeeProfile, EmployeeProfileDto>(await _employeeProfileRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetWorkingPositionLookupAsync(LookupRequestDto input)
        {
            var query = (await _workingPositionRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<WorkingPosition>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<WorkingPosition>, List<LookupDto<Guid?>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetSystemDataLookupAsync(LookupRequestDto input)
        {
            var query = (await _systemDataRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.ValueCode != null &&
                         x.ValueCode.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<SystemData>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<SystemData>, List<LookupDto<Guid?>>>(lookupData)
            };
        }

        [Authorize(MdmServicePermissions.EmployeeProfiles.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _employeeProfileRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.EmployeeProfiles.Create)]
        public virtual async Task<EmployeeProfileDto> CreateAsync(EmployeeProfileCreateDto input)
        {

            var employeeProfile = await _employeeProfileManager.CreateAsync(
            input.WorkingPositionId, input.EmployeeTypeId, input.Code, input.ERPCode, input.FirstName, input.LastName, input.IdCardNumber, input.Email, input.Phone, input.Address, input.Active, input.DateOfBirth, input.EffectiveDate, input.EndDate, input.IdentityUserId
            );

            return ObjectMapper.Map<EmployeeProfile, EmployeeProfileDto>(employeeProfile);
        }

        [Authorize(MdmServicePermissions.EmployeeProfiles.Edit)]
        public virtual async Task<EmployeeProfileDto> UpdateAsync(Guid id, EmployeeProfileUpdateDto input)
        {

            var employeeProfile = await _employeeProfileManager.UpdateAsync(
            id,
            input.WorkingPositionId, input.EmployeeTypeId, input.Code, input.ERPCode, input.FirstName, input.LastName, input.IdCardNumber, input.Email, input.Phone, input.Address, input.Active, input.DateOfBirth, input.EffectiveDate, input.EndDate, input.IdentityUserId, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<EmployeeProfile, EmployeeProfileDto>(employeeProfile);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(EmployeeProfileExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _employeeProfileRepository.GetListAsync(input.FilterText, input.Code, input.ERPCode, input.FirstName, input.LastName, input.DateOfBirthMin, input.DateOfBirthMax, input.IdCardNumber, input.Email, input.Phone, input.Address, input.Active, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.IdentityUserId);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<EmployeeProfile>, List<EmployeeProfileExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "EmployeeProfiles.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new EmployeeProfileExcelDownloadTokenCacheItem { Token = token },
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