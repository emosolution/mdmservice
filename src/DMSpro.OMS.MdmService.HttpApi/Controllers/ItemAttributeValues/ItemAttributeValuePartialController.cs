using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Controllers.ItemAttributeValues
{
	public partial class ItemAttributeValueController
	{

		[HttpGet]
		[Route("GetListDevextremes")]
		public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
		{
			return _itemAttributeValuesAppService.GetListDevextremesAsync(inputDev);
		}

		[HttpPost]
		[Route("update-from-excel")]
		public Task<int> UpdateFromExcelAsync(IFormFile file)
		{
			return _itemAttributeValuesAppService.UpdateFromExcelAsync(file);
		}

		[HttpPost]
		[Route("insert-from-excel")]
		public Task<int> InsertFromExcelAsync(IFormFile file)
        {
            return _itemAttributeValuesAppService.InsertFromExcelAsync(file);
        }
	}
}