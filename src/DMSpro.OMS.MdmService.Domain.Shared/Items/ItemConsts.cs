namespace DMSpro.OMS.MdmService.Items
{
    public static class ItemConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Item." : string.Empty);
        }

        public const int CodeMinLength = 1;
        public const int CodeMaxLength = 20;
        public const int NameMinLength = 1;
        public const int NameMaxLength = 255;
        public const int ShortNameMaxLength = 255;
        public const int erpCodeMaxLength = 20;
        public const int BarcodeMaxLength = 50;
    }
}