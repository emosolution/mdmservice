using System.Threading.Tasks;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
using Volo.Abp.Content;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
	public partial interface IItemAttributesAppService : IApplicationService
    {
		Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

		Task<int> UpdateFromExcelAsync(IRemoteStreamContent file);

		Task<int> InsertFromExcelAsync(IRemoteStreamContent file);

		Task<IRemoteStreamContent> GenerateExcelTemplatesAsync();
	}
}