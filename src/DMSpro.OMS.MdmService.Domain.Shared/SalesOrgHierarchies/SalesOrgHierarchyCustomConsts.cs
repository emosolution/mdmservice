namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public static partial class SalesOrgHierarchyConsts
    {
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

        public const string NumberingConfigObjectType = "SalesOrgHierarchies";
    }
}