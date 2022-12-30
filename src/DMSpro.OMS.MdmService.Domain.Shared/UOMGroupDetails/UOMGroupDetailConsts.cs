namespace DMSpro.OMS.MdmService.UOMGroupDetails
{
    public static class UOMGroupDetailConsts
    {
        private const string DefaultSorting = "{0}AltQty asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "UOMGroupDetail." : string.Empty);
        }

    }
}