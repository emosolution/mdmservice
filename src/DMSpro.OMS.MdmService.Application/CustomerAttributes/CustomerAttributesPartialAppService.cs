using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Lib.Parser;
using DMSpro.OMS.Shared.Domain.Devextreme;
using Volo.Abp;
using System.IO;
using System;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{
	public partial class CustomerAttributesAppService
	{
		public virtual async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
		{
			var items = await _customerAttributeRepository.GetQueryableAsync();
			var base_dataloadoption = new DataSourceLoadOptionsBase();
			DataLoadParser.Parse(base_dataloadoption,inputDev);
			LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);
			results.data = ObjectMapper.Map<IEnumerable<CustomerAttribute>, IEnumerable<CustomerAttributeDto>>(results.data.Cast<CustomerAttribute>());
			return results;
		}

		public virtual Task<int> UpdateFromExcelAsync(IRemoteStreamContent file)
		{
			return null;
		}

		public virtual async Task<int> InsertFromExcelAsync(IRemoteStreamContent file)
		{
			if (file == null || file.ContentLength <= 0) 
			{
				throw new BusinessException(message: L["Error:EmptyFormFile"], code: "0");
			}
			if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
			{
				throw new BusinessException(message: L["Error:ImportFileNotSupported"], code: "0");
			}
			// DUMMY LINE OF CODE TO REMOVE ASYNC AWAIT WARNING
			await _customerAttributeRepository.GetQueryableAsync(); // to be remove

			return 0;
		}
	}
}