using System.Threading.Tasks;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Http;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
	public partial interface ICompanyIdentityUserAssignmentsAppService
	{
		Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

		Task<LoadResult> GetListCompanyByCurrentUserAsync(DataLoadOptionDevextreme inputDev);
		Task<int>UpdateFromExcelAsync(IFormFile file);

		Task<int> InsertFromExcelAsync(IFormFile file);
	}
}