using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using System.Threading.Tasks;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.ItemGroupInZones
{
    public partial interface IItemGroupInZonesAppService
    {
        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<LoadResult> GetListDevextremesWithNavigationAsync(DataLoadOptionDevextreme inputDev);

        Task<int> UpdateFromExcelAsync(IRemoteStreamContent file);

        Task<int> InsertFromExcelAsync(IRemoteStreamContent file);

        Task<IRemoteStreamContent> GenerateExcelTemplatesAsync();
    }
}