using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CusAttributeValues
{
	public partial interface ICusAttributeValuePartialRepository
	{
		Task<List<CusAttributeValue>> GetByIdAsync(List<Guid> ids);
    }
}