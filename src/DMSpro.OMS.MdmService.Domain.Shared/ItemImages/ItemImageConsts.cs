namespace DMSpro.OMS.MdmService.ItemImages
{
    public static class ItemImageConsts
    {
        private const string DefaultSorting = "{0}Description asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ItemImage." : string.Empty);
        }

        public const int DescriptionMaxLength = 500;
    }
}