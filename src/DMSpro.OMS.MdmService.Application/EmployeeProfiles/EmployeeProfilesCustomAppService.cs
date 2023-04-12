using DMSpro.OMS.MdmService.EmployeeAttachments;
using DMSpro.OMS.MdmService.EmployeeImages;
using DMSpro.OMS.MdmService.NumberingConfigDetails;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    [Authorize(MdmServicePermissions.EmployeeProfiles.Default)]
    public partial class EmployeeProfilesAppService
    {
        public virtual async Task<EmployeeProfileDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<EmployeeProfile, EmployeeProfileDto>(await _employeeProfileRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.EmployeeProfiles.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _employeeProfileRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.EmployeeProfiles.Create)]
        public virtual async Task<EmployeeProfileDto> CreateAsync(EmployeeProfileCreateDto input)
        {
            CheckEffectiveDate(input.EffectiveDate, input.EndDate);
            var (dto, companyId) = await GetCodeFromNumberingConfig();
            var employeeProfile = await _employeeProfileManager.CreateAsync(
                input.WorkingPositionId, dto.SuggestedCode, input.ERPCode,
                input.FirstName, input.LastName, input.IdCardNumber,
                input.Email, input.Phone, input.Address,
                input.Active, input.EffectiveDate, input.DateOfBirth, input.EndDate,
                input.IdentityUserId, input.EmployeeType);
            await _numberingConfigDetailsInternalAppService.SaveNumberingConfigAsync(
                EmployeeProfileConsts.NumberingConfigObjectType, companyId, dto.CurrentNumber);
            return ObjectMapper.Map<EmployeeProfile, EmployeeProfileDto>(employeeProfile);
        }

        [Authorize(MdmServicePermissions.EmployeeProfiles.Edit)]
        public virtual async Task<EmployeeProfileDto> UpdateAsync(Guid id, EmployeeProfileUpdateDto input)
        {
            CheckEffectiveDate(input.EffectiveDate, input.EndDate);
            var employeeProfile = await _employeeProfileManager.UpdateAsync(
                id,
                input.WorkingPositionId, input.ERPCode,
                input.FirstName, input.LastName, input.IdCardNumber,
                input.Email, input.Phone, input.Address, input.Active,
                input.EffectiveDate, input.DateOfBirth, input.EndDate,
                input.IdentityUserId, input.EmployeeType,
                input.ConcurrencyStamp);

            return ObjectMapper.Map<EmployeeProfile, EmployeeProfileDto>(employeeProfile);
        }

        public async Task<EmployeeProfileFullDto> GetEmployeeProfileAsync(Guid id)
        {
            EmployeeProfile employee = await _employeeProfileRepository.GetAsync(id);
            List<EmployeeAttachment> attachments = (await _employeeAttachmentRepository.GetQueryableAsync()).Where(x => x.EmployeeProfileId == id).ToList();
            List<EmployeeImage> images = (await _employeeImageRepository.GetQueryableAsync()).Where(x => x.EmployeeProfileId == id).ToList();
            var result = new EmployeeProfileFullDto()
            {
                Employee = ObjectMapper.Map<EmployeeProfile, EmployeeProfileDto>(employee),
                Attachments = ObjectMapper.Map<List<EmployeeAttachment>, List<EmployeeAttachmentDto>>(attachments),
                Images = ObjectMapper.Map<List<EmployeeImage>, List<EmployeeImageDto>>(images),
            };
            return result;
        }

        private async Task<(NumberingConfigDetailDto, Guid)> GetCodeFromNumberingConfig()
        {
            var hoCompany = await _companyRepository.GetAsync(x => x.IsHO == true);
            var dto =
                await _numberingConfigDetailsInternalAppService.GetSuggestedNumberingConfigAsync(
                    EmployeeProfileConsts.NumberingConfigObjectType, hoCompany.Id);
            string code = dto.SuggestedCode;
            await CheckCodeUniqueness(code);
            return (dto, hoCompany.Id);
        }

        public virtual async Task<EmployeeProfileWithAvatarDto> GetWithAvatarAsync(Guid id)
        {
            var employee = await _employeeProfileRepository.GetAsync(id);
            var dto = ObjectMapper.Map<EmployeeProfile, EmployeeProfileWithAvatarDto>(employee);
            var employeeAvatarImages = (await _employeeImageRepository.GetListAsync(
                x => x.EmployeeProfileId == id && x.IsAvatar == true))
                .OrderByDescending(x => x.CreationTime).ToList();
            if (employeeAvatarImages.Count < 1)
            {
                return dto;
            }
            var avatarImage = employeeAvatarImages.FirstOrDefault();
            dto.Avatar = await _employeeImagesAppService.GetFileLocalAsync(avatarImage.FileId);
            Console.WriteLine(dto.Avatar.GetStream());
            return dto;
        }
    }
}
