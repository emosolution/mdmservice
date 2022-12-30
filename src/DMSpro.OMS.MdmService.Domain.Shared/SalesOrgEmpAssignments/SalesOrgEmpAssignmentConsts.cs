namespace DMSpro.OMS.MdmService.SalesOrgEmpAssignments
{
    public static class SalesOrgEmpAssignmentConsts
    {
        private const string DefaultSorting = "{0}IsBase asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "SalesOrgEmpAssignment." : string.Empty);
        }

    }
}