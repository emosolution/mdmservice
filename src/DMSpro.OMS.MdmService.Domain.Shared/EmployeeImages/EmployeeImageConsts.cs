namespace DMSpro.OMS.MdmService.EmployeeImages
{
    public static class EmployeeImageConsts
    {
        private const string DefaultSorting = "{0}Description asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "EmployeeImage." : string.Empty);
        }

    }
}