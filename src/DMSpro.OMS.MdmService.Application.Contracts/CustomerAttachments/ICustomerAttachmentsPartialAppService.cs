using System.Threading.Tasks;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Http;

namespace DMSpro.OMS.MdmService.CustomerAttachments
{
	public partial interface ICustomerAttachmentsAppService
	{
		Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

		Task<int>UpdateFromExcelAsync(IFormFile file);

		Task<int> InsertFromExcelAsync(IFormFile file);
	}
}