using System.Threading.Tasks;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.MdmService.Companies;
using System;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public partial interface ICompanyIdentityUserAssignmentsAppService
    {
        Task<LoadResult> GetListCompanyByCurrentUserAsync(DataLoadOptionDevextreme inputDev);

        Task<CompanyDto> SetCurrentlySelectedCompanyAsync(Guid companyId);

        Task<CompanyDto> GetCurrentlySelectedCompanyAsync(Guid? identityUserId = null, DateTime? checkTime = null);
    }
}