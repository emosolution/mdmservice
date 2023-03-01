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
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Microsoft.Extensions.Caching.Distributed;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{

    [Authorize(MdmServicePermissions.EmployeeProfiles.Default)]
    public partial class EmployeeProfilesAppService 
    {
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

        public virtual async Task<EmployeeProfileDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<EmployeeProfile, EmployeeProfileDto>(await _employeeProfileRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetWorkingPositionLookupAsync(LookupRequestDto input)
        {
            var query = (await _workingPositionRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Code != null &&
                         x.Code.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<WorkingPosition>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<WorkingPosition>, List<LookupDto<Guid>>>(lookupData)
            };
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

            var employeeProfiles = await _employeeProfileRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.ERPCode, input.FirstName, input.LastName, input.DateOfBirthMin, input.DateOfBirthMax, input.IdCardNumber, input.Email, input.Phone, input.Address, input.Active, input.EffectiveDateMin, input.EffectiveDateMax, input.EndDateMin, input.EndDateMax, input.IdentityUserId);
            var items = employeeProfiles.Select(item => new
            {
                Code = item.EmployeeProfile.Code,
                ERPCode = item.EmployeeProfile.ERPCode,
                FirstName = item.EmployeeProfile.FirstName,
                LastName = item.EmployeeProfile.LastName,
                DateOfBirth = item.EmployeeProfile.DateOfBirth,
                IdCardNumber = item.EmployeeProfile.IdCardNumber,
                Email = item.EmployeeProfile.Email,
                Phone = item.EmployeeProfile.Phone,
                Address = item.EmployeeProfile.Address,
                Active = item.EmployeeProfile.Active,
                EffectiveDate = item.EmployeeProfile.EffectiveDate,
                EndDate = item.EmployeeProfile.EndDate,
                IdentityUserId = item.EmployeeProfile.IdentityUserId,

                WorkingPositionCode = item.WorkingPosition?.Code,
                SystemDataValueCode = item.SystemData?.ValueCode,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
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