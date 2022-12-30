namespace DMSpro.OMS.MdmService.ItemMasters
{
    public static class ItemMasterConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ItemMaster." : string.Empty);
        }

        public const int CodeMinLength = 1;
        public const int CodeMaxLength = 20;
        public const int NameMinLength = 1;
        public const int NameMaxLength = 255;
        public const int ShortNameMinLength = 0;
        public const int ShortNameMaxLength = 255;
        public const int ERPCodeMinLength = 0;
        public const int ERPCodeMaxLength = 255;
        public const int BarcodeMinLength = 0;
        public const int BarcodeMaxLength = 255;
    }
}