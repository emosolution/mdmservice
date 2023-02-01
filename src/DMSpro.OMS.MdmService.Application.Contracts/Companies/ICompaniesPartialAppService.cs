using System.Threading.Tasks;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace DMSpro.OMS.MdmService.Companies
{
	public partial interface ICompaniesAppService
	{
		Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);
        Task<int> InsertFromExcelAsync(IFormFile file);
        Task<int> UpdateFromExcelAsync(IFormFile file);
    }
}