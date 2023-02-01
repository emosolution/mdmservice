using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Controllers.EmployeeProfiles
{
	public partial class EmployeeProfileController
	{

		[HttpGet]
		[Route("GetListDevextremes")]
		public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
		{
			return _employeeProfilesAppService.GetListDevextremesAsync(inputDev);
		}

		[HttpPost]
		[Route("update-from-excel")]
		public Task<int> UpdateFromExcelAsync(IFormFile file)
		{
			return _employeeProfilesAppService.UpdateFromExcelAsync(file);
		}

		[HttpPost]
		[Route("insert-from-excel")]
		public Task<int> InsertFromExcelAsync(IFormFile file)
        {
            return _employeeProfilesAppService.InsertFromExcelAsync(file);
        }
	}
}