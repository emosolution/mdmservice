namespace DMSpro.OMS.MdmService.EmployeeInZones
{
    public static class EmployeeInZoneConsts
    {
        private const string DefaultSorting = "{0}EffectiveDate asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "EmployeeInZone." : string.Empty);
        }

    }
}