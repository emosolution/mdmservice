namespace DMSpro.OMS.MdmService.CustomerGroupByAtts
{
    public static class CustomerGroupByAttConsts
    {
        private const string DefaultSorting = "{0}ValueCode asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CustomerGroupByAtt." : string.Empty);
        }

    }
}