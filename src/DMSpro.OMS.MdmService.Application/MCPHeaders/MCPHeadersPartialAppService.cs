using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Lib.Parser;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Microsoft.AspNetCore.Http;
using Volo.Abp;
using System.IO;
using System;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
	public partial class MCPHeadersAppService
	{
		public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
		{
			var items = await _mCPHeaderRepository.GetQueryableAsync();
			var base_dataloadoption = new DataSourceLoadOptionsBase();
			DataLoadParser.Parse(base_dataloadoption,inputDev);
			LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);
			results.data = ObjectMapper.Map<IEnumerable<MCPHeader>, IEnumerable<MCPHeaderDto>>(results.data.Cast<MCPHeader>());
			return results;
		}

		public virtual Task<int> UpdateFromExcelAsync(IFormFile file)
		{
			return null;
		}

		public virtual async Task<int> InsertFromExcelAsync(IFormFile file)
		{
			if (file == null || file.Length <= 0) 
			{
				throw new BusinessException(message: L["Error:EmptyFormFile"], code: "0");
			}
			if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
			{
				throw new BusinessException(message: L["Error:ImportFileNotSupported"], code: "0");
			}
			// DUMMY LINE OF CODE TO REMOVE ASYNC AWAIT WARNING
			await _mCPHeaderRepository.GetQueryableAsync(); // to be remove

			return 0;
		}
	}
}