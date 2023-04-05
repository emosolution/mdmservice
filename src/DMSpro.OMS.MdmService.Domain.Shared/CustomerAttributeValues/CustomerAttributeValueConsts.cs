namespace DMSpro.OMS.MdmService.CustomerAttributeValues
{
    public static class CustomerAttributeValueConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CustomerAttributeValue." : string.Empty);
        }

        public const int CodeMinLength = 1;
        public const int CodeMaxLength = 20;
        public const int AttrValNameMinLength = 1;
        public const int AttrValNameMaxLength = 255;
    }
}