using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{

    public partial class PricelistAssignmentsAppService
    {
        [Authorize(MdmServicePermissions.PriceListAssignments.Release)]
        public virtual async Task<DateTime> ReleaseAsync(Guid id)
        {
            DateTime now = DateTime.Now;
            var record = await _pricelistAssignmentRepository.GetAsync(x => x.Id == id && x.ReleaseDate == null); 
            record.ReleaseDate = now;
            await _pricelistAssignmentRepository.UpdateAsync(record);
            return now;
        }

    }
}