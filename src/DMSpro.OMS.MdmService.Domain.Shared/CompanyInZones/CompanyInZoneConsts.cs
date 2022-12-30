namespace DMSpro.OMS.MdmService.CompanyInZones
{
    public static class CompanyInZoneConsts
    {
        private const string DefaultSorting = "{0}EffectiveDate asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CompanyInZone." : string.Empty);
        }

    }
}