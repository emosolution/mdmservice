namespace DMSpro.OMS.MdmService.SSHistoryInZones
{
    public static class SSHistoryInZoneConsts
    {
        private const string DefaultSorting = "{0}EffectiveDate asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "SSHistoryInZone." : string.Empty);
        }

    }
}