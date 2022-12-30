namespace DMSpro.OMS.MdmService.CustomerGroupByGeos
{
    public static class CustomerGroupByGeoConsts
    {
        private const string DefaultSorting = "{0}Active asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CustomerGroupByGeo." : string.Empty);
        }

    }
}