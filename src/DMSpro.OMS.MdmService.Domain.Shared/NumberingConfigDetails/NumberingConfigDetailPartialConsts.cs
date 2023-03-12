using DMSpro.OMS.MdmService.NumberingConfigs;
using Volo.Abp;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public static partial class NumberingConfigDetailConsts
    {
        public const short CurrentNumberMinValue = 1;

        public static (string, int, string, int, bool) GetBaseDetailData(
            string inputPrefix, int? inputPaddingZeroNumber, 
            string inputSuffix, int inputCurrentNumber, bool? inputActive, 
            string objectType)
        {
            (string prefix, int paddingZeroNumber, string suffix) =
               NumberingConfigConsts.GetBaseData(inputPrefix,
               inputPaddingZeroNumber, inputSuffix, objectType);
            Check.Range((short)inputCurrentNumber, nameof(inputCurrentNumber),
               CurrentNumberMinValue);
            bool active = inputActive == null || (bool) inputActive;
            return (prefix, paddingZeroNumber, suffix, inputCurrentNumber,
                active);
        }
    }
}