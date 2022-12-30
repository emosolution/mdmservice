namespace DMSpro.OMS.MdmService.CustomerInZones
{
    public static class CustomerInZoneConsts
    {
        private const string DefaultSorting = "{0}EffectiveDate asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CustomerInZone." : string.Empty);
        }

    }
}