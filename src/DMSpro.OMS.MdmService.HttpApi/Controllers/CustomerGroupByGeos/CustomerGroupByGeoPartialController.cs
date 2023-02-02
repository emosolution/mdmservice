using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Controllers.CustomerGroupByGeos
{
	public partial class CustomerGroupByGeoController
	{

		[HttpGet]
		[Route("GetListDevextremes")]
		public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
		{
			return _customerGroupByGeosAppService.GetListDevextremesAsync(inputDev);
		}

		[HttpPost]
		[Route("update-from-excel")]
		public Task<int> UpdateFromExcelAsync(IFormFile file)
		{
			return _customerGroupByGeosAppService.UpdateFromExcelAsync(file);
		}

		[HttpPost]
		[Route("insert-from-excel")]
		public Task<int> InsertFromExcelAsync(IFormFile file)
        {
            return _customerGroupByGeosAppService.InsertFromExcelAsync(file);
        }
	}
}