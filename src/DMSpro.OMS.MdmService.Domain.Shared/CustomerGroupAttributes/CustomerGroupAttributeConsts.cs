namespace DMSpro.OMS.MdmService.CustomerGroupAttributes
{
    public static class CustomerGroupAttributeConsts
    {
        private const string DefaultSorting = "{0}Description asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CustomerGroupAttribute." : string.Empty);
        }

        public const int DescriptionMaxLength = 255;
    }
}