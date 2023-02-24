using DMSpro.OMS.MdmService.EmployeeAttachments;
using DMSpro.OMS.MdmService.EmployeeImages;
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
    }
}
