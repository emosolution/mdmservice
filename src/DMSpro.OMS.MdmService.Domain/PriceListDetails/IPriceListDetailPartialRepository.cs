using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.PriceListDetails
{
	public partial interface IPriceListDetailRepository
	{
		Task<List<PriceListDetail>> GetByIdAsync(List<Guid> ids);
    }
}