namespace DMSpro.OMS.MdmService.ItemGroupAttrs
{
    public static class ItemGroupAttrConsts
    {
        private const string DefaultSorting = "{0}Dummy asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ItemGroupAttr." : string.Empty);
        }

    }
}