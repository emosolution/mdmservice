using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Controllers.VATs
{
	public partial class VATController
	{

		[HttpGet]
		[Route("GetListDevextremes")]
		public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
		{
			return _vATsAppService.GetListDevextremesAsync(inputDev);
		}
	}
}