namespace DMSpro.OMS.MdmService.Routes
{
    public static class RouteConsts
    {
        private const string DefaultSorting = "{0}CheckIn asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Route." : string.Empty);
        }

    }
}