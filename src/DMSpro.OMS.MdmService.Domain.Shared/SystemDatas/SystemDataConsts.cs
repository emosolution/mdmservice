namespace DMSpro.OMS.MdmService.SystemDatas
{
    public static class SystemDataConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "SystemData." : string.Empty);
        }

        public const int CodeMinLength = 1;
        public const int CodeMaxLength = 10;
        public const int ValueCodeMinLength = 1;
        public const int ValueCodeMaxLength = 10;
        public const int ValueNameMinLength = 1;
        public const int ValueNameMaxLength = 100;
    }
}