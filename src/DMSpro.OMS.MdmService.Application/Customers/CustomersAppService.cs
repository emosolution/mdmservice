using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;
using System.Runtime.InteropServices;

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
            await CheckCodeUniqueness(input.Code);
            CheckEffectiveDate(input.EffectiveDate, input.EndDate);
            var customer = await _customerManager.CreateAsync(
            input.PaymentTermId, input.LinkedCompanyId, input.PriceListId, input.GeoMaster0Id, input.GeoMaster1Id, input.GeoMaster2Id, input.GeoMaster3Id, input.GeoMaster4Id, input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id, input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id, input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id, input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id, input.PaymentId, input.Code, input.Name, input.Phone1, input.Phone2, input.erpCode, input.License, input.TaxCode, input.vatName, input.vatAddress, input.Active, input.EffectiveDate, input.IsCompany, input.WarehouseId, input.Street, input.Address, input.Latitude, input.Longitude, input.SFACustomerCode, input.LastOrderDate, input.EndDate, input.CreditLimit
            );

            return ObjectMapper.Map<Customer, CustomerDto>(customer);
        }

        [Authorize(MdmServicePermissions.Customers.Edit)]
        public virtual async Task<CustomerDto> UpdateAsync(Guid id, CustomerUpdateDto input)
        {
            await CheckCodeUniqueness(input.Code, id);
            CheckEffectiveDate(input.EffectiveDate, input.EndDate);
            var customer = await _customerManager.UpdateAsync(
            id,
            input.PaymentTermId, input.LinkedCompanyId, input.PriceListId, input.GeoMaster0Id, input.GeoMaster1Id, input.GeoMaster2Id, input.GeoMaster3Id, input.GeoMaster4Id, input.Attr0Id, input.Attr1Id, input.Attr2Id, input.Attr3Id, input.Attr4Id, input.Attr5Id, input.Attr6Id, input.Attr7Id, input.Attr8Id, input.Attr9Id, input.Attr10Id, input.Attr11Id, input.Attr12Id, input.Attr13Id, input.Attr14Id, input.Attr15Id, input.Attr16Id, input.Attr17Id, input.Attr18Id, input.Attr19Id, input.PaymentId, input.Code, input.Name, input.Phone1, input.Phone2, input.erpCode, input.License, input.TaxCode, input.vatName, input.vatAddress, input.Active, input.EffectiveDate, input.IsCompany, input.WarehouseId, input.Street, input.Address, input.Latitude, input.Longitude, input.SFACustomerCode, input.LastOrderDate, input.EndDate, input.CreditLimit, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Customer, CustomerDto>(customer);
        }
    }
}