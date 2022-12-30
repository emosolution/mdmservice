namespace DMSpro.OMS.MdmService.CustomerAssignments
{
    public static class CustomerAssignmentConsts
    {
        private const string DefaultSorting = "{0}EffectiveDate asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CustomerAssignment." : string.Empty);
        }

    }
}