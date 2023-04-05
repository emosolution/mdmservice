using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{
    public partial interface ICustomerGroupGeosAppService : IApplicationService
    {
        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<int> UpdateFromExcelAsync(IRemoteStreamContent file);

        Task<int> InsertFromExcelAsync(IRemoteStreamContent file);

        Task<IRemoteStreamContent> GenerateExcelTemplatesAsync();
    }
}