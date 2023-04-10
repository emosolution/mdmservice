namespace DMSpro.OMS.MdmService.CustomerAttributes
{
    public static class CustomerAttributeConsts
    {
        private const string DefaultSorting = "{0}AttrNo asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CustomerAttribute." : string.Empty);
        }

        public const int AttrNoMinLength = 0;
        public const int AttrNoMaxLength = 19;
        public const int AttrNameMinLength = 1;
        public const int AttrNameMaxLength = 100;
        public const int CodeMinLength = 1;
        public const int CodeMaxLength = 20;

        public const string DefaultAttributeNamePrefix = "Attribute ";
    }
}