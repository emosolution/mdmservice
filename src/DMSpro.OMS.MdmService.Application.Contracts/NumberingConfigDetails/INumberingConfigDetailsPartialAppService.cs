using System.Threading.Tasks;
using Volo.Abp.Content;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public partial interface INumberingConfigDetailsAppService
    {
        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<int> UpdateFromExcelAsync(IRemoteStreamContent file);

        Task<int> InsertFromExcelAsync(IRemoteStreamContent file);

        Task<IRemoteStreamContent> GenerateExcelTemplatesAsync();
    }
}