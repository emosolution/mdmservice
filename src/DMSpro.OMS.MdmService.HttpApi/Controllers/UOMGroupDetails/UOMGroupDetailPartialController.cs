using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.UOMGroupDetails
{
	public partial class UOMGroupDetailController
	{
		[HttpGet]
		[Route("GetListDevextremes")]
		public virtual Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
		{
			return _uOMGroupDetailsAppService.GetListDevextremesAsync(inputDev);
		}

		[HttpPost]
		[Route("update-from-excel")]
		public virtual Task<int> UpdateFromExcelAsync(IRemoteStreamContent file)
		{
			return _uOMGroupDetailsAppService.UpdateFromExcelAsync(file);
		}

		[HttpPost]
		[Route("insert-from-excel")]
		public virtual Task<int> InsertFromExcelAsync(IRemoteStreamContent file)
        {
            return _uOMGroupDetailsAppService.InsertFromExcelAsync(file);
        }
		
		[HttpGet]
        [Route("get-excel-template")]
        public virtual Task<IRemoteStreamContent> GenerateExcelTemplatesAsync()
        {
            return _uOMGroupDetailsAppService.GenerateExcelTemplatesAsync();
        }
	}
}