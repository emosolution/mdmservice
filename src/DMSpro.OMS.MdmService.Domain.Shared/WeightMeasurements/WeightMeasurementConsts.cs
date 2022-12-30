namespace DMSpro.OMS.MdmService.WeightMeasurements
{
    public static class WeightMeasurementConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "WeightMeasurement." : string.Empty);
        }

        public const int NameMinLength = 1;
        public const int NameMaxLength = 50;
    }
}