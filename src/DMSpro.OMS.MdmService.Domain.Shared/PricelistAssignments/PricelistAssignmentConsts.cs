namespace DMSpro.OMS.MdmService.PricelistAssignments
{
    public static class PricelistAssignmentConsts
    {
        private const string DefaultSorting = "{0}Description asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "PricelistAssignment." : string.Empty);
        }

    }
}