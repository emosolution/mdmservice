using DMSpro.OMS.MdmService.GeoMasters;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using DMSpro.OMS.MdmService.Shared;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DMSpro.OMS.Shared.Lib.Parser;
namespace DMSpro.OMS.MdmService.Companies
{

    [Authorize(MdmServicePermissions.CompanyMasters.Default)]
    public partial class CompaniesAppService
    { 
        public virtual async Task<LoadResult> GetListDevextremesWithDetailsAsync(DataLoadOptionDevextreme inputDev)
        {
            var items = await _companyRepository.GetQueryWithDetailsAsync();
            Console.WriteLine("==========");
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption, inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);
            Console.WriteLine("AsDASDASDASD");
            Console.WriteLine(results);
            // if (inputDev.Group == null)
            // {
            //     results.data = ObjectMapper.Map<IEnumerable<CompanyWithDetails>, IEnumerable<CompanyWithDetailsDto>>(results.data.Cast<CompanyWithDetails>());
            // }
            return results;
        }
    }
}