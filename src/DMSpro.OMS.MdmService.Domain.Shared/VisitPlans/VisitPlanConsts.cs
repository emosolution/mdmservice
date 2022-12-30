namespace DMSpro.OMS.MdmService.VisitPlans
{
    public static class VisitPlanConsts
    {
        private const string DefaultSorting = "{0}DateVisit asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "VisitPlan." : string.Empty);
        }

    }
}