namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public static class CompanyIdentityUserAssignmentConsts
    {
        private const string DefaultSorting = "{0}IdentityUserId asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CompanyIdentityUserAssignment." : string.Empty);
        }

    }
}