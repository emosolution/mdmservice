using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.Currencies
{
	public partial class CurrencyController
	{
		[HttpGet]
		[Route("GetListDevextremes")]
		public virtual Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
		{
			return _currenciesAppService.GetListDevextremesAsync(inputDev);
		}

		[HttpPost]
		[Route("update-from-excel")]
		public virtual Task<int> UpdateFromExcelAsync(IRemoteStreamContent file)
		{
			return _currenciesAppService.UpdateFromExcelAsync(file);
		}

		[HttpPost]
		[Route("insert-from-excel")]
		public virtual Task<int> InsertFromExcelAsync(IRemoteStreamContent file)
        {
            return _currenciesAppService.InsertFromExcelAsync(file);
        }
		
		[HttpGet]
        [Route("get-excel-template")]
        public virtual Task<IRemoteStreamContent> GenerateExcelTemplatesAsync()
        {
            return _currenciesAppService.GenerateExcelTemplatesAsync();
        }
	}
}