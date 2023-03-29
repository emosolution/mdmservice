using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{

    public partial class PricelistAssignmentsAppService
    {
        [Authorize(MdmServicePermissions.PriceListAssignments.Release)]
        public virtual async Task<PricelistAssignmentDto> ReleaseAsync(Guid id)
        {
            DateTime now = DateTime.Now;
            var record = await _pricelistAssignmentRepository.GetAsync(x => x.Id == id && 
                x.IsReleased == false && x.ReleasedDate == null); 
            record.ReleasedDate = now;
            record.IsReleased = true;
            await _pricelistAssignmentRepository.UpdateAsync(record);
            return ObjectMapper.Map<PricelistAssignment, PricelistAssignmentDto>(record);
        }
    }
}