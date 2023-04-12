namespace DMSpro.OMS.MdmService.ItemGroupAttributes
{
    public static class ItemGroupAttributeConsts
    {
        private const string DefaultSorting = "{0}Description asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ItemGroupAttribute." : string.Empty);
        }

        public const int DescriptionMaxLength = 255;
    }
}