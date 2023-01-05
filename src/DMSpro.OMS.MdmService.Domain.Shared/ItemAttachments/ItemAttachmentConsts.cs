namespace DMSpro.OMS.MdmService.ItemAttachments
{
    public static class ItemAttachmentConsts
    {
        private const string DefaultSorting = "{0}Description asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ItemAttachment." : string.Empty);
        }

        public const int DescriptionMaxLength = 500;
        public const int UrlMinLength = 1;
        public const int UrlMaxLength = 500;
    }
}