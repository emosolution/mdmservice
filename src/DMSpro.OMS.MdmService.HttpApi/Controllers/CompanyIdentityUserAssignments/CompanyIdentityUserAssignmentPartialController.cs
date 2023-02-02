using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Controllers.CompanyIdentityUserAssignments
{
	public partial class CompanyIdentityUserAssignmentController
	{

		[HttpGet]
		[Route("GetListDevextremes")]
		public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
		{
			return _companyIdentityUserAssignmentsAppService.GetListDevextremesAsync(inputDev);
		}

		[HttpGet]
		[Route("GetListCompanyByCurrentUser")]
		public Task<LoadResult> GetListCompanyByCurrentUserAsync(DataLoadOptionDevextreme inputDev)
		{
			return _companyIdentityUserAssignmentsAppService.GetListCompanyByCurrentUserAsync(inputDev);
		}

		[HttpPost]
		[Route("update-from-excel")]
		public Task<int> UpdateFromExcelAsync(IFormFile file)
		{
			return _companyIdentityUserAssignmentsAppService.UpdateFromExcelAsync(file);
		}

		[HttpPost]
		[Route("insert-from-excel")]
		public Task<int> InsertFromExcelAsync(IFormFile file)
        {
            return _companyIdentityUserAssignmentsAppService.InsertFromExcelAsync(file);
        }
	}
}