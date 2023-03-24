using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.NumberingConfigs;
using DMSpro.OMS.MdmService.SystemDatas;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public class NumberingConfigDetailManager : DomainService
    {
        private readonly INumberingConfigRepository _numberingConfigRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ISystemDataRepository _systemDataRepository;
        private readonly INumberingConfigDetailRepository _numberingConfigDetailRepository;

        public NumberingConfigDetailManager(
            INumberingConfigRepository numberingConfigRepository,
            ICompanyRepository companyRepository,
            ISystemDataRepository systemDataRepository,
            INumberingConfigDetailRepository numberingConfigDetailRepository
            )
        {
            _numberingConfigRepository = numberingConfigRepository;
            _companyRepository = companyRepository;
            _systemDataRepository = systemDataRepository;
            _numberingConfigDetailRepository = numberingConfigDetailRepository;
        }

        public virtual async Task<NumberingConfigDetail> CreateAsync(string inputPrefix,
            int? inputPaddingZeroNumber, string inputSuffix, bool? inputActive, 
            int inputCurrentNumber, Guid numberingConfigId, Guid companyId)
        {
            var header = await _numberingConfigRepository.GetAsync(numberingConfigId);
            var company = await _companyRepository.GetAsync(companyId);
            var systemDatas = await _systemDataRepository.GetNumberingConfigsSystemDataAsync();
            var systemData = systemDatas.Where(x => x.Id == header.SystemDataId).First();
            (string prefix, int paddingZeroNumber, string suffix, int currentNumer, bool active) =
                NumberingConfigDetailConsts.GetBaseDetailData(inputPrefix,
                inputPaddingZeroNumber, inputSuffix, inputCurrentNumber, inputActive, systemData.ValueName);
            string description = $"Numbering config detail for compamy {company.Code}, type {systemData.ValueName}";

            var numberingConfigDetail = new NumberingConfigDetail(
                GuidGenerator.Create(), numberingConfigId, companyId,
                    description, prefix, paddingZeroNumber, suffix, active,
                    currentNumer);

            var detail = await _numberingConfigDetailRepository.InsertAsync(numberingConfigDetail);
            return detail;
        }
    }
}
