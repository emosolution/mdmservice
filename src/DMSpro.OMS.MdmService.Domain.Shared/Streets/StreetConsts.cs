namespace DMSpro.OMS.MdmService.Streets
{
    public static class StreetConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Street." : string.Empty);
        }

        public const int NameMinLength = 1;
        public const int NameMaxLength = 500;
    }
}