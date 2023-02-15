using System.Threading.Tasks;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
	public partial interface IPriceUpdatesAppService
	{
		Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

		Task<int>UpdateFromExcelAsync(IRemoteStreamContent file);

		Task<int> InsertFromExcelAsync(IRemoteStreamContent file);
	}
}