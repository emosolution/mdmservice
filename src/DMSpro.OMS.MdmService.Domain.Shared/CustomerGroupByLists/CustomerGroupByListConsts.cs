namespace DMSpro.OMS.MdmService.CustomerGroupByLists
{
    public static class CustomerGroupByListConsts
    {
        private const string DefaultSorting = "{0}Active asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CustomerGroupByList." : string.Empty);
        }

    }
}