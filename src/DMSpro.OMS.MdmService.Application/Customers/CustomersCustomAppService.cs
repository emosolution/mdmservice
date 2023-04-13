using DMSpro.OMS.MdmService.CustomerAttachments;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.NumberingConfigDetails;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Customers
{
    [Authorize(MdmServicePermissions.Customers.Default)]
    public partial class CustomersAppService
    {
        public virtual async Task<CustomerDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Customer, CustomerDto>(await _customerRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.Customers.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _customerRepository.DeleteAsync(id);
        }

        [Authorize(MdmServicePermissions.Customers.Create)]
        public virtual async Task<CustomerDto> CreateAsync(CustomerCreateDto input)
        {
            CheckEffectiveDate(input.EffectiveDate, input.EndDate);
            var (dto, companyId) = await GetCodeFromNumberingConfig();
            (GeoMasterDto geo0, GeoMasterDto geo1, GeoMasterDto geo2,
                GeoMasterDto geo3, GeoMasterDto geo4) =
                    await _geoMastersInternalAppService.CheckAllGeoInputs(input.GeoMaster0Id,
                input.GeoMaster1Id, input.GeoMaster2Id, input.GeoMaster3Id, input.GeoMaster4Id);
            string fullAddress = CreateFullAddressFromGeo(input.Street, input.Address,
                geo0, geo1, geo2, geo3, geo4);
            var customer = await _customerManager.CreateAsync(
                input.PaymentTermId, input.LinkedCompanyId, input.PriceListId,
                input.GeoMaster0Id, input.GeoMaster1Id, input.GeoMaster2Id, input.GeoMaster3Id, input.GeoMaster4Id,
                fullAddress,
                input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id,
                input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id,
                input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id,
                input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id,
                input.PaymentId, dto.SuggestedCode, input.Name, input.Phone1, input.Phone2, input.erpCode,
                input.License, input.TaxCode, input.vatName, input.vatAddress, input.Active,
                input.EffectiveDate, input.IsCompany, input.WarehouseId, input.Street, input.Address,
                input.Latitude, input.Longitude, input.SFACustomerCode, input.LastOrderDate,
                input.EndDate, input.CreditLimit);
            await _numberingConfigDetailsInternalAppService.SaveNumberingConfigAsync(
                CustomerConsts.NumberingConfigObjectType, companyId, dto.CurrentNumber);
            return ObjectMapper.Map<Customer, CustomerDto>(customer);
        }

        [Authorize(MdmServicePermissions.Customers.Edit)]
        public virtual async Task<CustomerDto> UpdateAsync(Guid id, CustomerUpdateDto input)
        {
            CheckEffectiveDate(input.EffectiveDate, input.EndDate);
            (GeoMasterDto geo0, GeoMasterDto geo1, GeoMasterDto geo2,
                GeoMasterDto geo3, GeoMasterDto geo4) =
                    await _geoMastersInternalAppService.CheckAllGeoInputs(input.GeoMaster0Id,
               input.GeoMaster1Id, input.GeoMaster2Id, input.GeoMaster3Id, input.GeoMaster4Id);
            string fullAddress = CreateFullAddressFromGeo(input.Street, input.Address,
                geo0, geo1, geo2, geo3, geo4);
            var customer = await _customerManager.UpdateAsync(
                id,
                input.PaymentTermId, input.LinkedCompanyId, input.PriceListId,
                input.GeoMaster0Id, input.GeoMaster1Id, input.GeoMaster2Id, input.GeoMaster3Id, input.GeoMaster4Id,
                fullAddress,
                input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id,
                input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id,
                input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id,
                input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id,
                input.PaymentId, input.Name, input.Phone1, input.Phone2, input.erpCode,
                input.License, input.TaxCode, input.vatName, input.vatAddress, input.Active,
                input.EffectiveDate, input.IsCompany, input.WarehouseId, input.Street, input.Address,
                input.Latitude, input.Longitude, input.SFACustomerCode, input.LastOrderDate,
                input.EndDate, input.CreditLimit,
                input.ConcurrencyStamp);

            return ObjectMapper.Map<Customer, CustomerDto>(customer);
        }

        public async Task<CustomerProfileDto> GetCustomerProfileAsync(Guid id)
        {
            Customer customer = await _customerRepository.GetAsync(id);
            List<CustomerAttachment> attachments = (await _customerAttachmentRepository.GetQueryableAsync()).Where(x => x.CustomerId == id).ToList();
            var result = new CustomerProfileDto()
            {
                Customer = ObjectMapper.Map<Customer, CustomerDto>(customer),
                Attachments = ObjectMapper.Map<List<CustomerAttachment>, List<CustomerAttachmentDto>>(attachments),
            };
            return result;
        }

        private async Task<(NumberingConfigDetailDto, Guid)> GetCodeFromNumberingConfig()
        {
            var hoCompany = await _companyRepository.GetAsync(x => x.IsHO == true);
            var dto =
                await _numberingConfigDetailsInternalAppService.GetSuggestedNumberingConfigAsync(
                    CustomerConsts.NumberingConfigObjectType, hoCompany.Id);
            string code = dto.SuggestedCode;
            await CheckCodeUniqueness(code);
            return (dto, hoCompany.Id);
        }

        private string CreateFullAddressFromGeo(string address, string street,
            GeoMasterDto geo0, GeoMasterDto geo1, GeoMasterDto geo2,
            GeoMasterDto geo3, GeoMasterDto geo4)
        {
            string addressString = string.IsNullOrEmpty(address) ? $"({L["Data:CustomersAppService:Address"]})" : address;
            string streetString = string.IsNullOrEmpty(street) ? $" ({L["Data:CustomersAppService:Street"]})" : $" {street}";
            string geo4String = geo4 == null ? "" : $", {geo4?.Name}";
            string geo3String = geo3 == null ? "" : $", {geo3?.Name}";
            string geo2String = geo2 == null ? "" : $", {geo2?.Name}";
            string geo1String = geo1 == null ? "" : $", {geo1?.Name}";
            string geo0String = geo0 == null ? "" : $", {geo0?.Name}";
            return $"{addressString}{streetString}{geo4String}{geo3String}{geo2String}{geo1String}{geo0String}";
        }
    }
}
