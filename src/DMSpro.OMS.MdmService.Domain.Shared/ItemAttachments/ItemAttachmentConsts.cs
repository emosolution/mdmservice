namespace DMSpro.OMS.MdmService.ItemAttachments
{
    public static class ItemAttachmentConsts
    {
        private const string DefaultSorting = "{0}Description asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ItemAttachment." : string.Empty);
        }

    }
}