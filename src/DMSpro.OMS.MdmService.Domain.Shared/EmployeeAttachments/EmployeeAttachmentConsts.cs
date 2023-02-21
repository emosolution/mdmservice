namespace DMSpro.OMS.MdmService.EmployeeAttachments
{
    public static class EmployeeAttachmentConsts
    {
        private const string DefaultSorting = "{0}Description asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "EmployeeAttachment." : string.Empty);
        }

    }
}