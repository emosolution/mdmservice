using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.NumberingConfigDetails;

namespace DMSpro.OMS.MdmService.Vendors
{

    [Authorize(MdmServicePermissions.Vendors.Default)]
    public partial class VendorsAppService
    {
        public virtual async Task<VendorDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Vendor, VendorDto>(await _vendorRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.Vendors.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _vendorRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.Vendors.Create)]
        public virtual async Task<VendorDto> CreateAsync(VendorCreateDto input)
        {
            if (input.PriceListId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["PriceList"]]);
            }
            if (input.CompanyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Company"]]);
            }
            var (dto, companyId) = await GetCodeFromNumberingConfig();
            var vendor = await _vendorManager.CreateAsync(
                input.PriceListId, 
                input.GeoMaster0Id, input.GeoMaster1Id, input.GeoMaster2Id, input.GeoMaster3Id, input.GeoMaster4Id, 
                input.CompanyId, input.LinkedCompanyId, dto.SuggestedCode, input.Name, input.ShortName, 
                input.Phone1, input.Phone2, input.ERPCode, input.Active, input.Street, 
                input.Address, input.Latitude, input.Longitude, input.EndDate);
            await _numberingConfigDetailsInternalAppService.SaveNumberingConfigAsync(
                VendorConsts.NumberingConfigObjectType, companyId, dto.CurrentNumber);
            return ObjectMapper.Map<Vendor, VendorDto>(vendor);
        }

        [Authorize(MdmServicePermissions.Vendors.Edit)]
        public virtual async Task<VendorDto> UpdateAsync(Guid id, VendorUpdateDto input)
        {
            if (input.PriceListId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["PriceList"]]);
            }
            if (input.CompanyId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Company"]]);
            }

            var vendor = await _vendorManager.UpdateAsync(
                id,
                input.PriceListId, input.GeoMaster0Id, 
                input.GeoMaster1Id, input.GeoMaster2Id, input.GeoMaster3Id, input.GeoMaster4Id, 
                input.CompanyId, input.LinkedCompanyId, input.Name, input.ShortName, input.Phone1, input.Phone2, 
                input.ERPCode, input.Active, input.Street, input.Address, input.Latitude, input.Longitude, 
                input.EndDate, input.ConcurrencyStamp);

            return ObjectMapper.Map<Vendor, VendorDto>(vendor);
        }

        private async Task<(NumberingConfigDetailDto, Guid)> GetCodeFromNumberingConfig()
        {
            var hoCompany = await _companyRepository.GetAsync(x => x.IsHO == true);
            var dto =
                await _numberingConfigDetailsInternalAppService.GetSuggestedNumberingConfigAsync(
                    VendorConsts.NumberingConfigObjectType, hoCompany.Id);
            string code = dto.SuggestedCode;
            await CheckCodeUniqueness(code);
            return (dto, hoCompany.Id);
        }
    }
}