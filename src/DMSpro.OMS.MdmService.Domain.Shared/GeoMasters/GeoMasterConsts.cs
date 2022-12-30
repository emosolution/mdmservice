namespace DMSpro.OMS.MdmService.GeoMasters
{
    public static class GeoMasterConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "GeoMaster." : string.Empty);
        }

        public const int NameMinLength = 1;
        public const int NameMaxLength = 100;
    }
}