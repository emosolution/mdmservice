using System.Threading.Tasks;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Http;

namespace DMSpro.OMS.MdmService.UOMGroupDetails
{
    public partial interface IUOMGroupDetailsAppService
    {
        Task<LoadResult> GetListDevextremeswithNavigationAsync(DataLoadOptionDevextreme inputDev);
    }
}