using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService
{
    public interface IPartialAppservice
    {
        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);
        Task<int> InsertFromExcelAsync(IFormFile file);
        Task<int> UpdateFromExcelAsync(IFormFile file);
    }
}
