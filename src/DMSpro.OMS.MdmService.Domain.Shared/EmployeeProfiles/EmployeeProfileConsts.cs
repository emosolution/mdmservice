namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    public static class EmployeeProfileConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "EmployeeProfile." : string.Empty);
        }

        public const int CodeMinLength = 1;
        public const int CodeMaxLength = 20;
        public const int ERPCodeMaxLength = 20;
        public const int FirstNameMinLength = 1;
        public const int FirstNameMaxLength = 255;
        public const int LastNameMaxLength = 255;
        public const int IdCardNumberMaxLength = 255;
        public const int EmailMaxLength = 255;
        public const int PhoneMaxLength = 255;
        public const int AddressMaxLength = 500;
    }
}