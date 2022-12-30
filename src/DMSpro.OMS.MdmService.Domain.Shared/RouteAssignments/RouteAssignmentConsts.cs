namespace DMSpro.OMS.MdmService.RouteAssignments
{
    public static class RouteAssignmentConsts
    {
        private const string DefaultSorting = "{0}EffectiveDate asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "RouteAssignment." : string.Empty);
        }

    }
}