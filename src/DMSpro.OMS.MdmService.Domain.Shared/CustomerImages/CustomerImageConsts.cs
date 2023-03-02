namespace DMSpro.OMS.MdmService.CustomerImages
{
    public static class CustomerImageConsts
    {
        private const string DefaultSorting = "{0}Description asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CustomerImage." : string.Empty);
        }

        public const int DescriptionMaxLength = 500;
    }
}