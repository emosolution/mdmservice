namespace DMSpro.OMS.MdmService.Holidays
{
    public static class HolidayConsts
    {
        private const string DefaultSorting = "{0}Year asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Holiday." : string.Empty);
        }

        public const int YearMinLength = 2023;
        public const int YearMaxLength = 2099;
        public const int DescriptionMaxLength = 500;
        public const int CodeMaxLength = 4;
    }
}