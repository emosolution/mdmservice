namespace DMSpro.OMS.MdmService.ProdAttributeValues
{
    public static class ProdAttributeValueConsts
    {
        private const string DefaultSorting = "{0}AttrValName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ProdAttributeValue." : string.Empty);
        }

        public const int AttrValNameMinLength = 1;
        public const int AttrValNameMaxLength = 100;
    }
}