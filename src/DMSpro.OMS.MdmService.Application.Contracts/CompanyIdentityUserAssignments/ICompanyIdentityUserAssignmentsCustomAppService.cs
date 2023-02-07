using System.Threading.Tasks;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public partial interface ICompanyIdentityUserAssignmentsAppService
    {
        Task<LoadResult> GetListCompanyByCurrentUserAsync(DataLoadOptionDevextreme inputDev);
    }
}