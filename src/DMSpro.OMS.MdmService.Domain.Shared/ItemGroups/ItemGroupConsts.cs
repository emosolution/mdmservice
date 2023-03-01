namespace DMSpro.OMS.MdmService.ItemGroups
{
    public static class ItemGroupConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ItemGroup." : string.Empty);
        }

        public const int CodeMinLength = 1;
        public const int CodeMaxLength = 50;
        public const int NameMaxLength = 255;
        public const int DescriptionMaxLength = 500;
    }
}