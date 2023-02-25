using System;

namespace DMSpro.OMS.MdmService.Helpers
{
    public partial class MDMHelpers
    {
        public static bool CheckActive(bool active, DateTime effectiveDate, DateTime? endDate)
        {
            if (effectiveDate > DateTime.Now.Date)
            {
                return false;
            }
            if (endDate.HasValue && endDate >= DateTime.Now.Date)
            {
                return true;
            }
            if (!endDate.HasValue && active)
            {
                return true;
            }
            return false;
        }
    }
}
