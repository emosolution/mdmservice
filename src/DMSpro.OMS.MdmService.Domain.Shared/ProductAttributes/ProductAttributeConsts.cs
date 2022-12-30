namespace DMSpro.OMS.MdmService.ProductAttributes
{
    public static class ProductAttributeConsts
    {
        private const string DefaultSorting = "{0}AttrNo asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ProductAttribute." : string.Empty);
        }

        public const int AttrNoMinLength = 0;
        public const int AttrNoMaxLength = 19;
        public const int AttrNameMinLength = 1;
        public const int AttrNameMaxLength = 100;
        public const int HierarchyLevelMinLength = 0;
        public const int HierarchyLevelMaxLength = 19;
    }
}