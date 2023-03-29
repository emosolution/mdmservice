using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using System;
using DMSpro.OMS.MdmService.PricelistAssignments;

namespace DMSpro.OMS.MdmService.Controllers.PricelistAssignments
{
	public partial class PricelistAssignmentController
	{
		[HttpPut]
		[Route("release")]
		public virtual async Task<PricelistAssignmentDto> ReleaseAsync(Guid id)
		{
            try
            {
                return await _pricelistAssignmentsAppService.ReleaseAsync(id);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message, code: "1");
            }
        }
	}
}