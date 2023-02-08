using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Controllers.CompanyIdentityUserAssignments
{
	public partial class CompanyIdentityUserAssignmentController
	{
		[HttpGet]
		[Route("GetListCompanyByCurrentUser")]
		public Task<LoadResult> GetListCompanyByCurrentUserAsync(DataLoadOptionDevextreme inputDev)
		{
			return _companyIdentityUserAssignmentsAppService.GetListCompanyByCurrentUserAsync(inputDev);
		}
	}
}