namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
    public static class ItemAttributeValueConsts
    {
        private const string DefaultSorting = "{0}AttrValName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ItemAttributeValue." : string.Empty);
        }

        public const int CodeMinLength = 1;
        public const int CodeMaxLength = 1;
        public const int AttrValNameMinLength = 1;
        public const int AttrValNameMaxLength = 255;
    }
}