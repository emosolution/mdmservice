namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public static partial class SalesOrgHierarchyConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "SalesOrgHierarchy." : string.Empty);
        }

        public const int CodeMinLength = 1;
        public const int CodeMaxLength = 20;
        public const int NameMaxLength = 255;
        public const int LevelMinLength = 0;
        public const int LevelMaxLength = 9;
        public const int HierarchyCodeMinLength = 0;
        public const int HierarchyCodeMaxLength = 500;
    }
}