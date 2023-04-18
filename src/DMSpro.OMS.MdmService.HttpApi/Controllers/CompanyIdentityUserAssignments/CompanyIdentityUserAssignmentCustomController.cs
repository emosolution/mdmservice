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
        public virtual Task<LoadResult> GetListCompanyByCurrentUserAsync(DataLoadOptionDevextreme inputDev)
        {
            return _companyIdentityUserAssignmentsAppService.GetListCompanyByCurrentUserAsync(inputDev);
        }

        [HttpPost]
        [Route("set-selected-company")]
        public virtual Task<CompanyDto> SetCurrentlySelectedCompanyAsync(Guid companyId)
        {
            return _companyIdentityUserAssignmentsAppService.SetCurrentlySelectedCompanyAsync(companyId);
            
        }

        [HttpPost]
        [Route("get-selected-company")]
        public virtual Task<CompanyDto> GetCurrentlySelectedCompanyAsync(
            Guid? identityUserId = null, DateTime? checkTime = null)
        {
            return _companyIdentityUserAssignmentsAppService.GetCurrentlySelectedCompanyAsync(
                        identityUserId, checkTime);
        }
    }
}