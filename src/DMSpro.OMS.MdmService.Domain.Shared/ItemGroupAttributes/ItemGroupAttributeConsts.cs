namespace DMSpro.OMS.MdmService.ItemGroupAttributes
{
    public static class ItemGroupAttributeConsts
    {
        private const string DefaultSorting = "{0}dummy asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ItemGroupAttribute." : string.Empty);
        }

        public const int dummyMinLength = 1;
        public const int dummyMaxLength = 20;
    }
}