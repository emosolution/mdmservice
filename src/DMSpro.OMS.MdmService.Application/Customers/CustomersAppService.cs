using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DMSpro.OMS.MdmService.Permissions;

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

            var customer = await _customerManager.CreateAsync(
            input.PaymentTermId, input.LinkedCompanyId, input.PriceListId, input.GeoMaster0Id, input.GeoMaster1Id, input.GeoMaster2Id, input.GeoMaster3Id, input.GeoMaster4Id, input.Attribute0Id, input.Attribute1Id, input.Attribute2Id, input.Attribute3Id, input.Attribute4Id, input.Attribute5Id, input.Attribute6Id, input.Attribute7Id, input.Attribute8Id, input.Attribute9Id, input.Attribute10Id, input.Attribute11Id, input.Attribute12Id, input.Attribute13Id, input.Attribute14Id, input.Attribute15Id, input.Attribute16Id, input.Attribute17Id, input.Attribute18Id, input.Attribute19Id, input.PaymentId, input.Code, input.Name, input.Phone1, input.Phone2, input.erpCode, input.License, input.TaxCode, input.vatName, input.vatAddress, input.Active, input.EffectiveDate, input.IsCompany, input.WarehouseId, input.Street, input.Address, input.Latitude, input.Longitude, input.SFACustomerCode, input.LastOrderDate, input.EndDate, input.CreditLimit
            );

            return ObjectMapper.Map<Customer, CustomerDto>(customer);
        }

        [Authorize(MdmServicePermissions.Customers.Edit)]
        public virtual async Task<CustomerDto> UpdateAsync(Guid id, CustomerUpdateDto input)
        {

            var customer = await _customerManager.UpdateAsync(
            id,
            input.PaymentTermId, input.LinkedCompanyId, input.PriceListId, input.GeoMaster0Id, input.GeoMaster1Id, input.GeoMaster2Id, input.GeoMaster3Id, input.GeoMaster4Id, input.Attribute0Id, input.Attribute1Id, input.Attribute2Id, input.Attribute3Id, input.Attribute4Id, input.Attribute5Id, input.Attribute6Id, input.Attribute7Id, input.Attribute8Id, input.Attribute9Id, input.Attribute10Id, input.Attribute11Id, input.Attribute12Id, input.Attribute13Id, input.Attribute14Id, input.Attribute15Id, input.Attribute16Id, input.Attribute17Id, input.Attribute18Id, input.Attribute19Id, input.PaymentId, input.Code, input.Name, input.Phone1, input.Phone2, input.erpCode, input.License, input.TaxCode, input.vatName, input.vatAddress, input.Active, input.EffectiveDate, input.IsCompany, input.WarehouseId, input.Street, input.Address, input.Latitude, input.Longitude, input.SFACustomerCode, input.LastOrderDate, input.EndDate, input.CreditLimit, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Customer, CustomerDto>(customer);
        }
    }
}