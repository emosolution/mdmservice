namespace DMSpro.OMS.MdmService.MCPHeaders
{
    public static class MCPHeaderConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "MCPHeader." : string.Empty);
        }

        public const int CodeMinLength = 1;
        public const int CodeMaxLength = 20;
        public const int NameMaxLength = 255;
    }
}