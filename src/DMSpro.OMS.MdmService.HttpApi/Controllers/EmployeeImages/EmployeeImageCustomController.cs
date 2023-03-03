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
        public virtual async Task<EmployeeImageDto> CreateAvatarAsync(EmployeeImageCreateDto input, IRemoteStreamContent file)
        {
            try
            {
                return await _employeeImagesAppService.CreateAvatarAsync(input, file);
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
        public virtual async Task<EmployeeImageDto> UpdateAvatarAsync(EmployeeImageUpdateDto input, IRemoteStreamContent file)
        {
            try
            {
                return await _employeeImagesAppService.UpdateAvatarAsync(input, file);
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

        [HttpPost]
        [Route("test/avatar")]
        public virtual async Task<EmployeeImageDto> TestCreateAvatarAsync(Guid id, IRemoteStreamContent file)
        {
            try
            {
                return await _employeeImagesAppService.TestCreateAvatarAsync(id, file);
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

        [HttpPost]
        [Route("test/avatarfileonly")]
        public virtual async Task<EmployeeImageDto> TestCreateAvatarOnlyFileAsync(IRemoteStreamContent file)
        {
            try
            {
                return await _employeeImagesAppService.TestCreateAvatarOnlyFileAsync(file);
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

        [HttpPost]
        [Route("test/avatarextensible")]
        public virtual async Task<EmployeeImageDto> TestCreateAvatarExtensibleAsync(AvartarCreateDto input)
        {
            try
            {
                return await _employeeImagesAppService.TestCreateAvatarExtensibleAsync(input);
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
