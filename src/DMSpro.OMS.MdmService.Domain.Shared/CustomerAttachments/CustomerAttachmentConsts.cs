namespace DMSpro.OMS.MdmService.CustomerAttachments
{
    public static class CustomerAttachmentConsts
    {
        private const string DefaultSorting = "{0}Description asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CustomerAttachment." : string.Empty);
        }

    }
}