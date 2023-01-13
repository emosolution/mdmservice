namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public static class NumberingConfigConsts
    {
        private const string DefaultSorting = "{0}StartNumber asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "NumberingConfig." : string.Empty);
        }

    }
}