using DMSpro.OMS.MdmService.EmployeeImages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.EmployeeImages
{
    public partial class EmployeeImageController
    {
        [HttpDelete]
        [Route("delete-many")]
        public virtual Task DeleteManyAsync(List<Guid> ids)
        {
            return _employeeImagesAppService.DeleteManyAsync(ids);
        }

        [HttpGet]
        [Route("get-file")]
        public virtual Task<IRemoteStreamContent> GetFileAsync(Guid id)
        {
            return _employeeImagesAppService.GetFileAsync(id);
        }

        [HttpPost]
        [Route("avatar")]
        public virtual async Task<EmployeeImageDto> CreateAvatarAsync(EmployeeImageCreateDto input)
        {
            try
            {
                return await _employeeImagesAppService.CreateAvatarAsync(input);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message);
            }
        }

        [HttpPut]
        [Route("avatar")]
        public virtual async Task<EmployeeImageDto> UpdateAvatarAsync(EmployeeImageUpdateDto input)
        {
            try
            {
                return await _employeeImagesAppService.UpdateAvatarAsync(input);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message);
            }
        }
    }
}
