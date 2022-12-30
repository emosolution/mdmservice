namespace DMSpro.OMS.MdmService.CustomerContacts
{
    public static class CustomerContactConsts
    {
        private const string DefaultSorting = "{0}Title asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CustomerContact." : string.Empty);
        }

    }
}