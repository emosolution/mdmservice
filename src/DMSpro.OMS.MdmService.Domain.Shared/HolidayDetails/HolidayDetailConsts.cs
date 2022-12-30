namespace DMSpro.OMS.MdmService.HolidayDetails
{
    public static class HolidayDetailConsts
    {
        private const string DefaultSorting = "{0}StartDate asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "HolidayDetail." : string.Empty);
        }

    }
}