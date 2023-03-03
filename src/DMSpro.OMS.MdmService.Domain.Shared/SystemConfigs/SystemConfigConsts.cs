namespace DMSpro.OMS.MdmService.SystemConfigs
{
    public static class SystemConfigConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "SystemConfig." : string.Empty);
        }

        public const int CodeMinLength = 1;
        public const int CodeMaxLength = 20;
        public const int DescriptionMinLength = 1;
        public const int DescriptionMaxLength = 500;
        public const int ValueMinLength = 1;
        public const int ValueMaxLength = 255;
        public const int DefaultValueMinLength = 1;
        public const int DefaultValueMaxLength = 255;
        public const int DataSourceMaxLength = 1000;
    }
}