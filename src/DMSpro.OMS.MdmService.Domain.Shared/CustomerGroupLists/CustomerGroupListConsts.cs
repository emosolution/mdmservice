namespace DMSpro.OMS.MdmService.CustomerGroupLists
{
    public static class CustomerGroupListConsts
    {
        private const string DefaultSorting = "{0}Description asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CustomerGroupList." : string.Empty);
        }

        public const int DescriptionMaxLength = 20;
    }
}