using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using DMSpro.OMS.MdmService.PricelistAssignments;

namespace DMSpro.OMS.MdmService.Controllers.PricelistAssignments
{
	public partial class PricelistAssignmentController
	{
		[HttpPut]
		[Route("release")]
		public virtual Task<PricelistAssignmentDto> ReleaseAsync(Guid id)
		{
            return _pricelistAssignmentsAppService.ReleaseAsync(id);
        }
	}
}