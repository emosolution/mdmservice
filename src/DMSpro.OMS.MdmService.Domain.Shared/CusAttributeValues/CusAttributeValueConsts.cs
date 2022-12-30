namespace DMSpro.OMS.MdmService.CusAttributeValues
{
    public static class CusAttributeValueConsts
    {
        private const string DefaultSorting = "{0}AttrValName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CusAttributeValue." : string.Empty);
        }

        public const int AttrValNameMinLength = 1;
        public const int AttrValNameMaxLength = 100;
    }
}