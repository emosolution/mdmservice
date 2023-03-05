namespace DMSpro.OMS.MdmService.ItemGroupInZones
{
    public static class ItemGroupInZoneConsts
    {
        private const string DefaultSorting = "{0}EffectiveDate asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ItemGroupInZone." : string.Empty);
        }

        public const int DescriptionMaxLength = 500;
    }
}