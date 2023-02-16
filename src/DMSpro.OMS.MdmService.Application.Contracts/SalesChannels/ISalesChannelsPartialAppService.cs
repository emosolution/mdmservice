using System.Threading.Tasks;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.SalesChannels
{
	public partial interface ISalesChannelsAppService
	{
		Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

		Task<int> UpdateFromExcelAsync(IRemoteStreamContent file);

		Task<int> InsertFromExcelAsync(IRemoteStreamContent file);

		Task<IRemoteStreamContent> GenerateExcelTemplatesAsync();
	}
}