using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;

namespace DMSpro.OMS.MdmService.Controllers.CompanyIdentityUserAssignments
{
    public partial class CompanyIdentityUserAssignmentController
    {
        [HttpGet]
        [Route("GetListCompanyByCurrentUser")]
        public Task<LoadResult> GetListCompanyByCurrentUserAsync(DataLoadOptionDevextreme inputDev)
        {
            try
            {
                return _companyIdentityUserAssignmentsAppService.GetListCompanyByCurrentUserAsync(inputDev);
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
        [Route("set-selected-company")]
        public virtual async Task<CompanyDto> SetCurrentlySelectedCompanyAsync(Guid companyId)
        {
            try
            {
                return await 
                    _companyIdentityUserAssignmentsAppService.SetCurrentlySelectedCompanyAsync(companyId);
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
        [Route("get-selected-company")]
        public virtual async Task<CompanyDto> GetCurrentlySelectedCompanyAsync()
        {
            try
            {
                return await
                    _companyIdentityUserAssignmentsAppService.GetCurrentlySelectedCompanyAsync();
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