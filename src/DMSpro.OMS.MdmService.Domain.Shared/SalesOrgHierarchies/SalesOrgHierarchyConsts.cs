namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public static class SalesOrgHierarchyConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "SalesOrgHierarchy." : string.Empty);
        }

        public const int CodeMinLength = 1;
        public const int CodeMaxLength = 20;
        public const int LevelMinLength = 0;
        public const int LevelMaxLength = 9;

        /// <summary>
        /// Maximum length of the DisplayName property.
        /// </summary>
        public static int MaxDisplayNameLength { get; set; } = 128;

        /// <summary>
        /// Maximum depth of an OU hierarchy.
        /// </summary>
        public const int MaxDepth = 16;

        /// <summary>
        /// Length of a code unit between dots.
        /// </summary>
        public const int CodeUnitLength = 5;

        /// <summary>
        /// Maximum length of the Code property.
        /// </summary>
        public const int MaxCodeLength = MaxDepth * (CodeUnitLength + 1) - 1;
    }
}