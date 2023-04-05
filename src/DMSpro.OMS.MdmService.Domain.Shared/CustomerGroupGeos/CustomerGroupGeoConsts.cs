namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{
    public static class CustomerGroupGeoConsts
    {
        private const string DefaultSorting = "{0}Description asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CustomerGroupGeo." : string.Empty);
        }

        public const int DescriptionMaxLength = 20;
    }
}