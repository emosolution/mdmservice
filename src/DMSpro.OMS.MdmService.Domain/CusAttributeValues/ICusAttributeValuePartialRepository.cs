using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CusAttributeValues
{
	public partial interface ICusAttributeValueRepository
	{
		Task<List<CusAttributeValue>> GetByIdAsync(List<Guid> ids);
    }
}