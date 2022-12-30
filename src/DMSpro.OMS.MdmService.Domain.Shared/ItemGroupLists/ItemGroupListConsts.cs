namespace DMSpro.OMS.MdmService.ItemGroupLists
{
    public static class ItemGroupListConsts
    {
        private const string DefaultSorting = "{0}Rate asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ItemGroupList." : string.Empty);
        }

    }
}