using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using DMSpro.OMS.MdmService.EntityFrameworkCore;

namespace DMSpro.OMS.MdmService.Customers
{
    public class EfCoreCustomerRepository : EfCoreRepository<MdmServiceDbContext, Customer, Guid>, ICustomerRepository
    {
        public EfCoreCustomerRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<CustomerWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(customer => new CustomerWithNavigationProperties
                {
                    Customer = customer,
                    SystemData = dbContext.SystemDatas.FirstOrDefault(c => c.Id == customer.PaymentTermId),
                    Company = dbContext.Companies.FirstOrDefault(c => c.Id == customer.LinkedCompanyId),
                    PriceList = dbContext.PriceLists.FirstOrDefault(c => c.Id == customer.PriceListId),
                    GeoMaster = dbContext.GeoMasters.FirstOrDefault(c => c.Id == customer.GeoMaster0Id),
                    GeoMaster1 = dbContext.GeoMasters.FirstOrDefault(c => c.Id == customer.GeoMaster1Id),
                    GeoMaster2 = dbContext.GeoMasters.FirstOrDefault(c => c.Id == customer.GeoMaster2Id),
                    GeoMaster3 = dbContext.GeoMasters.FirstOrDefault(c => c.Id == customer.GeoMaster3Id),
                    GeoMaster4 = dbContext.GeoMasters.FirstOrDefault(c => c.Id == customer.GeoMaster4Id),
                    CusAttributeValue = dbContext.CusAttributeValues.FirstOrDefault(c => c.Id == customer.Attribute0Id),
                    CusAttributeValue1 = dbContext.CusAttributeValues.FirstOrDefault(c => c.Id == customer.Attribute1Id),
                    CusAttributeValue2 = dbContext.CusAttributeValues.FirstOrDefault(c => c.Id == customer.Attribute2Id),
                    CusAttributeValue3 = dbContext.CusAttributeValues.FirstOrDefault(c => c.Id == customer.Attribute3Id),
                    CusAttributeValue4 = dbContext.CusAttributeValues.FirstOrDefault(c => c.Id == customer.Attribute4Id),
                    CusAttributeValue5 = dbContext.CusAttributeValues.FirstOrDefault(c => c.Id == customer.Attribute5Id),
                    CusAttributeValue6 = dbContext.CusAttributeValues.FirstOrDefault(c => c.Id == customer.Attribute6Id),
                    CusAttributeValue7 = dbContext.CusAttributeValues.FirstOrDefault(c => c.Id == customer.Attribute7Id),
                    CusAttributeValue8 = dbContext.CusAttributeValues.FirstOrDefault(c => c.Id == customer.Attribute8Id),
                    CusAttributeValue9 = dbContext.CusAttributeValues.FirstOrDefault(c => c.Id == customer.Attribute9Id),
                    CusAttributeValue10 = dbContext.CusAttributeValues.FirstOrDefault(c => c.Id == customer.Attribute10Id),
                    CusAttributeValue11 = dbContext.CusAttributeValues.FirstOrDefault(c => c.Id == customer.Attribute11Id),
                    CusAttributeValue12 = dbContext.CusAttributeValues.FirstOrDefault(c => c.Id == customer.Attribute12Id),
                    CusAttributeValue13 = dbContext.CusAttributeValues.FirstOrDefault(c => c.Id == customer.Attribute13Id),
                    CusAttributeValue14 = dbContext.CusAttributeValues.FirstOrDefault(c => c.Id == customer.Attribute14Id),
                    CusAttributeValue15 = dbContext.CusAttributeValues.FirstOrDefault(c => c.Id == customer.Attribute15Id),
                    CusAttributeValue16 = dbContext.CusAttributeValues.FirstOrDefault(c => c.Id == customer.Attribute16Id),
                    CusAttributeValue17 = dbContext.CusAttributeValues.FirstOrDefault(c => c.Id == customer.Attribute1I7d),
                    CusAttributeValue18 = dbContext.CusAttributeValues.FirstOrDefault(c => c.Id == customer.Attribute18Id),
                    CusAttributeValue19 = dbContext.CusAttributeValues.FirstOrDefault(c => c.Id == customer.Attribute19Id),
                    Customer1 = dbContext.Customers.FirstOrDefault(c => c.Id == customer.PaymentId)
                }).FirstOrDefault();
        }

        public async Task<List<CustomerWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string phone1 = null,
            string phone2 = null,
            string erpCode = null,
            string license = null,
            string taxCode = null,
            string vatName = null,
            string vatAddress = null,
            bool? active = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            int? creditLimitMin = null,
            int? creditLimitMax = null,
            bool? isCompany = null,
            Guid? warehouseId = null,
            string street = null,
            string address = null,
            string latitude = null,
            string longitude = null,
            string sfaCustomerCode = null,
            DateTime? lastOrderDateMin = null,
            DateTime? lastOrderDateMax = null,
            Guid? paymentTermId = null,
            Guid? linkedCompanyId = null,
            Guid? priceListId = null,
            Guid? geoMaster0Id = null,
            Guid? geoMaster1Id = null,
            Guid? geoMaster2Id = null,
            Guid? geoMaster3Id = null,
            Guid? geoMaster4Id = null,
            Guid? attribute0Id = null,
            Guid? attribute1Id = null,
            Guid? attribute2Id = null,
            Guid? attribute3Id = null,
            Guid? attribute4Id = null,
            Guid? attribute5Id = null,
            Guid? attribute6Id = null,
            Guid? attribute7Id = null,
            Guid? attribute8Id = null,
            Guid? attribute9Id = null,
            Guid? attribute10Id = null,
            Guid? attribute11Id = null,
            Guid? attribute12Id = null,
            Guid? attribute13Id = null,
            Guid? attribute14Id = null,
            Guid? attribute15Id = null,
            Guid? attribute16Id = null,
            Guid? attribute1I7d = null,
            Guid? attribute18Id = null,
            Guid? attribute19Id = null,
            Guid? paymentId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, name, phone1, phone2, erpCode, license, taxCode, vatName, vatAddress, active, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax, creditLimitMin, creditLimitMax, isCompany, warehouseId, street, address, latitude, longitude, sfaCustomerCode, lastOrderDateMin, lastOrderDateMax, paymentTermId, linkedCompanyId, priceListId, geoMaster0Id, geoMaster1Id, geoMaster2Id, geoMaster3Id, geoMaster4Id, attribute0Id, attribute1Id, attribute2Id, attribute3Id, attribute4Id, attribute5Id, attribute6Id, attribute7Id, attribute8Id, attribute9Id, attribute10Id, attribute11Id, attribute12Id, attribute13Id, attribute14Id, attribute15Id, attribute16Id, attribute1I7d, attribute18Id, attribute19Id, paymentId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<CustomerWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from customer in (await GetDbSetAsync())
                   join systemData in (await GetDbContextAsync()).SystemDatas on customer.PaymentTermId equals systemData.Id into systemDatas
                   from systemData in systemDatas.DefaultIfEmpty()
                   join company in (await GetDbContextAsync()).Companies on customer.LinkedCompanyId equals company.Id into companies
                   from company in companies.DefaultIfEmpty()
                   join priceList in (await GetDbContextAsync()).PriceLists on customer.PriceListId equals priceList.Id into priceLists
                   from priceList in priceLists.DefaultIfEmpty()
                   join geoMaster in (await GetDbContextAsync()).GeoMasters on customer.GeoMaster0Id equals geoMaster.Id into geoMasters
                   from geoMaster in geoMasters.DefaultIfEmpty()
                   join geoMaster1 in (await GetDbContextAsync()).GeoMasters on customer.GeoMaster1Id equals geoMaster1.Id into geoMasters1
                   from geoMaster1 in geoMasters1.DefaultIfEmpty()
                   join geoMaster2 in (await GetDbContextAsync()).GeoMasters on customer.GeoMaster2Id equals geoMaster2.Id into geoMasters2
                   from geoMaster2 in geoMasters2.DefaultIfEmpty()
                   join geoMaster3 in (await GetDbContextAsync()).GeoMasters on customer.GeoMaster3Id equals geoMaster3.Id into geoMasters3
                   from geoMaster3 in geoMasters3.DefaultIfEmpty()
                   join geoMaster4 in (await GetDbContextAsync()).GeoMasters on customer.GeoMaster4Id equals geoMaster4.Id into geoMasters4
                   from geoMaster4 in geoMasters4.DefaultIfEmpty()
                   join cusAttributeValue in (await GetDbContextAsync()).CusAttributeValues on customer.Attribute0Id equals cusAttributeValue.Id into cusAttributeValues
                   from cusAttributeValue in cusAttributeValues.DefaultIfEmpty()
                   join cusAttributeValue1 in (await GetDbContextAsync()).CusAttributeValues on customer.Attribute1Id equals cusAttributeValue1.Id into cusAttributeValues1
                   from cusAttributeValue1 in cusAttributeValues1.DefaultIfEmpty()
                   join cusAttributeValue2 in (await GetDbContextAsync()).CusAttributeValues on customer.Attribute2Id equals cusAttributeValue2.Id into cusAttributeValues2
                   from cusAttributeValue2 in cusAttributeValues2.DefaultIfEmpty()
                   join cusAttributeValue3 in (await GetDbContextAsync()).CusAttributeValues on customer.Attribute3Id equals cusAttributeValue3.Id into cusAttributeValues3
                   from cusAttributeValue3 in cusAttributeValues3.DefaultIfEmpty()
                   join cusAttributeValue4 in (await GetDbContextAsync()).CusAttributeValues on customer.Attribute4Id equals cusAttributeValue4.Id into cusAttributeValues4
                   from cusAttributeValue4 in cusAttributeValues4.DefaultIfEmpty()
                   join cusAttributeValue5 in (await GetDbContextAsync()).CusAttributeValues on customer.Attribute5Id equals cusAttributeValue5.Id into cusAttributeValues5
                   from cusAttributeValue5 in cusAttributeValues5.DefaultIfEmpty()
                   join cusAttributeValue6 in (await GetDbContextAsync()).CusAttributeValues on customer.Attribute6Id equals cusAttributeValue6.Id into cusAttributeValues6
                   from cusAttributeValue6 in cusAttributeValues6.DefaultIfEmpty()
                   join cusAttributeValue7 in (await GetDbContextAsync()).CusAttributeValues on customer.Attribute7Id equals cusAttributeValue7.Id into cusAttributeValues7
                   from cusAttributeValue7 in cusAttributeValues7.DefaultIfEmpty()
                   join cusAttributeValue8 in (await GetDbContextAsync()).CusAttributeValues on customer.Attribute8Id equals cusAttributeValue8.Id into cusAttributeValues8
                   from cusAttributeValue8 in cusAttributeValues8.DefaultIfEmpty()
                   join cusAttributeValue9 in (await GetDbContextAsync()).CusAttributeValues on customer.Attribute9Id equals cusAttributeValue9.Id into cusAttributeValues9
                   from cusAttributeValue9 in cusAttributeValues9.DefaultIfEmpty()
                   join cusAttributeValue10 in (await GetDbContextAsync()).CusAttributeValues on customer.Attribute10Id equals cusAttributeValue10.Id into cusAttributeValues10
                   from cusAttributeValue10 in cusAttributeValues10.DefaultIfEmpty()
                   join cusAttributeValue11 in (await GetDbContextAsync()).CusAttributeValues on customer.Attribute11Id equals cusAttributeValue11.Id into cusAttributeValues11
                   from cusAttributeValue11 in cusAttributeValues11.DefaultIfEmpty()
                   join cusAttributeValue12 in (await GetDbContextAsync()).CusAttributeValues on customer.Attribute12Id equals cusAttributeValue12.Id into cusAttributeValues12
                   from cusAttributeValue12 in cusAttributeValues12.DefaultIfEmpty()
                   join cusAttributeValue13 in (await GetDbContextAsync()).CusAttributeValues on customer.Attribute13Id equals cusAttributeValue13.Id into cusAttributeValues13
                   from cusAttributeValue13 in cusAttributeValues13.DefaultIfEmpty()
                   join cusAttributeValue14 in (await GetDbContextAsync()).CusAttributeValues on customer.Attribute14Id equals cusAttributeValue14.Id into cusAttributeValues14
                   from cusAttributeValue14 in cusAttributeValues14.DefaultIfEmpty()
                   join cusAttributeValue15 in (await GetDbContextAsync()).CusAttributeValues on customer.Attribute15Id equals cusAttributeValue15.Id into cusAttributeValues15
                   from cusAttributeValue15 in cusAttributeValues15.DefaultIfEmpty()
                   join cusAttributeValue16 in (await GetDbContextAsync()).CusAttributeValues on customer.Attribute16Id equals cusAttributeValue16.Id into cusAttributeValues16
                   from cusAttributeValue16 in cusAttributeValues16.DefaultIfEmpty()
                   join cusAttributeValue17 in (await GetDbContextAsync()).CusAttributeValues on customer.Attribute1I7d equals cusAttributeValue17.Id into cusAttributeValues17
                   from cusAttributeValue17 in cusAttributeValues17.DefaultIfEmpty()
                   join cusAttributeValue18 in (await GetDbContextAsync()).CusAttributeValues on customer.Attribute18Id equals cusAttributeValue18.Id into cusAttributeValues18
                   from cusAttributeValue18 in cusAttributeValues18.DefaultIfEmpty()
                   join cusAttributeValue19 in (await GetDbContextAsync()).CusAttributeValues on customer.Attribute19Id equals cusAttributeValue19.Id into cusAttributeValues19
                   from cusAttributeValue19 in cusAttributeValues19.DefaultIfEmpty()
                   join customer1 in (await GetDbContextAsync()).Customers on customer.PaymentId equals customer1.Id into customers1
                   from customer1 in customers1.DefaultIfEmpty()

                   select new CustomerWithNavigationProperties
                   {
                       Customer = customer,
                       SystemData = systemData,
                       Company = company,
                       PriceList = priceList,
                       GeoMaster = geoMaster,
                       GeoMaster1 = geoMaster1,
                       GeoMaster2 = geoMaster2,
                       GeoMaster3 = geoMaster3,
                       GeoMaster4 = geoMaster4,
                       CusAttributeValue = cusAttributeValue,
                       CusAttributeValue1 = cusAttributeValue1,
                       CusAttributeValue2 = cusAttributeValue2,
                       CusAttributeValue3 = cusAttributeValue3,
                       CusAttributeValue4 = cusAttributeValue4,
                       CusAttributeValue5 = cusAttributeValue5,
                       CusAttributeValue6 = cusAttributeValue6,
                       CusAttributeValue7 = cusAttributeValue7,
                       CusAttributeValue8 = cusAttributeValue8,
                       CusAttributeValue9 = cusAttributeValue9,
                       CusAttributeValue10 = cusAttributeValue10,
                       CusAttributeValue11 = cusAttributeValue11,
                       CusAttributeValue12 = cusAttributeValue12,
                       CusAttributeValue13 = cusAttributeValue13,
                       CusAttributeValue14 = cusAttributeValue14,
                       CusAttributeValue15 = cusAttributeValue15,
                       CusAttributeValue16 = cusAttributeValue16,
                       CusAttributeValue17 = cusAttributeValue17,
                       CusAttributeValue18 = cusAttributeValue18,
                       CusAttributeValue19 = cusAttributeValue19,
                       Customer1 = customer1
                   };
        }

        protected virtual IQueryable<CustomerWithNavigationProperties> ApplyFilter(
            IQueryable<CustomerWithNavigationProperties> query,
            string filterText,
            string code = null,
            string name = null,
            string phone1 = null,
            string phone2 = null,
            string erpCode = null,
            string license = null,
            string taxCode = null,
            string vatName = null,
            string vatAddress = null,
            bool? active = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            int? creditLimitMin = null,
            int? creditLimitMax = null,
            bool? isCompany = null,
            Guid? warehouseId = null,
            string street = null,
            string address = null,
            string latitude = null,
            string longitude = null,
            string sfaCustomerCode = null,
            DateTime? lastOrderDateMin = null,
            DateTime? lastOrderDateMax = null,
            Guid? paymentTermId = null,
            Guid? linkedCompanyId = null,
            Guid? priceListId = null,
            Guid? geoMaster0Id = null,
            Guid? geoMaster1Id = null,
            Guid? geoMaster2Id = null,
            Guid? geoMaster3Id = null,
            Guid? geoMaster4Id = null,
            Guid? attribute0Id = null,
            Guid? attribute1Id = null,
            Guid? attribute2Id = null,
            Guid? attribute3Id = null,
            Guid? attribute4Id = null,
            Guid? attribute5Id = null,
            Guid? attribute6Id = null,
            Guid? attribute7Id = null,
            Guid? attribute8Id = null,
            Guid? attribute9Id = null,
            Guid? attribute10Id = null,
            Guid? attribute11Id = null,
            Guid? attribute12Id = null,
            Guid? attribute13Id = null,
            Guid? attribute14Id = null,
            Guid? attribute15Id = null,
            Guid? attribute16Id = null,
            Guid? attribute1I7d = null,
            Guid? attribute18Id = null,
            Guid? attribute19Id = null,
            Guid? paymentId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Customer.Code.Contains(filterText) || e.Customer.Name.Contains(filterText) || e.Customer.Phone1.Contains(filterText) || e.Customer.Phone2.Contains(filterText) || e.Customer.erpCode.Contains(filterText) || e.Customer.License.Contains(filterText) || e.Customer.TaxCode.Contains(filterText) || e.Customer.vatName.Contains(filterText) || e.Customer.vatAddress.Contains(filterText) || e.Customer.Street.Contains(filterText) || e.Customer.Address.Contains(filterText) || e.Customer.Latitude.Contains(filterText) || e.Customer.Longitude.Contains(filterText) || e.Customer.SFACustomerCode.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Customer.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Customer.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(phone1), e => e.Customer.Phone1.Contains(phone1))
                    .WhereIf(!string.IsNullOrWhiteSpace(phone2), e => e.Customer.Phone2.Contains(phone2))
                    .WhereIf(!string.IsNullOrWhiteSpace(erpCode), e => e.Customer.erpCode.Contains(erpCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(license), e => e.Customer.License.Contains(license))
                    .WhereIf(!string.IsNullOrWhiteSpace(taxCode), e => e.Customer.TaxCode.Contains(taxCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(vatName), e => e.Customer.vatName.Contains(vatName))
                    .WhereIf(!string.IsNullOrWhiteSpace(vatAddress), e => e.Customer.vatAddress.Contains(vatAddress))
                    .WhereIf(active.HasValue, e => e.Customer.Active == active)
                    .WhereIf(effectiveDateMin.HasValue, e => e.Customer.EffectiveDate >= effectiveDateMin.Value)
                    .WhereIf(effectiveDateMax.HasValue, e => e.Customer.EffectiveDate <= effectiveDateMax.Value)
                    .WhereIf(endDateMin.HasValue, e => e.Customer.EndDate >= endDateMin.Value)
                    .WhereIf(endDateMax.HasValue, e => e.Customer.EndDate <= endDateMax.Value)
                    .WhereIf(creditLimitMin.HasValue, e => e.Customer.CreditLimit >= creditLimitMin.Value)
                    .WhereIf(creditLimitMax.HasValue, e => e.Customer.CreditLimit <= creditLimitMax.Value)
                    .WhereIf(isCompany.HasValue, e => e.Customer.IsCompany == isCompany)
                    .WhereIf(warehouseId.HasValue, e => e.Customer.WarehouseId == warehouseId)
                    .WhereIf(!string.IsNullOrWhiteSpace(street), e => e.Customer.Street.Contains(street))
                    .WhereIf(!string.IsNullOrWhiteSpace(address), e => e.Customer.Address.Contains(address))
                    .WhereIf(!string.IsNullOrWhiteSpace(latitude), e => e.Customer.Latitude.Contains(latitude))
                    .WhereIf(!string.IsNullOrWhiteSpace(longitude), e => e.Customer.Longitude.Contains(longitude))
                    .WhereIf(!string.IsNullOrWhiteSpace(sfaCustomerCode), e => e.Customer.SFACustomerCode.Contains(sfaCustomerCode))
                    .WhereIf(lastOrderDateMin.HasValue, e => e.Customer.LastOrderDate >= lastOrderDateMin.Value)
                    .WhereIf(lastOrderDateMax.HasValue, e => e.Customer.LastOrderDate <= lastOrderDateMax.Value)
                    .WhereIf(paymentTermId != null && paymentTermId != Guid.Empty, e => e.SystemData != null && e.SystemData.Id == paymentTermId)
                    .WhereIf(linkedCompanyId != null && linkedCompanyId != Guid.Empty, e => e.Company != null && e.Company.Id == linkedCompanyId)
                    .WhereIf(priceListId != null && priceListId != Guid.Empty, e => e.PriceList != null && e.PriceList.Id == priceListId)
                    .WhereIf(geoMaster0Id != null && geoMaster0Id != Guid.Empty, e => e.GeoMaster != null && e.GeoMaster.Id == geoMaster0Id)
                    .WhereIf(geoMaster1Id != null && geoMaster1Id != Guid.Empty, e => e.GeoMaster1 != null && e.GeoMaster1.Id == geoMaster1Id)
                    .WhereIf(geoMaster2Id != null && geoMaster2Id != Guid.Empty, e => e.GeoMaster2 != null && e.GeoMaster2.Id == geoMaster2Id)
                    .WhereIf(geoMaster3Id != null && geoMaster3Id != Guid.Empty, e => e.GeoMaster3 != null && e.GeoMaster3.Id == geoMaster3Id)
                    .WhereIf(geoMaster4Id != null && geoMaster4Id != Guid.Empty, e => e.GeoMaster4 != null && e.GeoMaster4.Id == geoMaster4Id)
                    .WhereIf(attribute0Id != null && attribute0Id != Guid.Empty, e => e.CusAttributeValue != null && e.CusAttributeValue.Id == attribute0Id)
                    .WhereIf(attribute1Id != null && attribute1Id != Guid.Empty, e => e.CusAttributeValue1 != null && e.CusAttributeValue1.Id == attribute1Id)
                    .WhereIf(attribute2Id != null && attribute2Id != Guid.Empty, e => e.CusAttributeValue2 != null && e.CusAttributeValue2.Id == attribute2Id)
                    .WhereIf(attribute3Id != null && attribute3Id != Guid.Empty, e => e.CusAttributeValue3 != null && e.CusAttributeValue3.Id == attribute3Id)
                    .WhereIf(attribute4Id != null && attribute4Id != Guid.Empty, e => e.CusAttributeValue4 != null && e.CusAttributeValue4.Id == attribute4Id)
                    .WhereIf(attribute5Id != null && attribute5Id != Guid.Empty, e => e.CusAttributeValue5 != null && e.CusAttributeValue5.Id == attribute5Id)
                    .WhereIf(attribute6Id != null && attribute6Id != Guid.Empty, e => e.CusAttributeValue6 != null && e.CusAttributeValue6.Id == attribute6Id)
                    .WhereIf(attribute7Id != null && attribute7Id != Guid.Empty, e => e.CusAttributeValue7 != null && e.CusAttributeValue7.Id == attribute7Id)
                    .WhereIf(attribute8Id != null && attribute8Id != Guid.Empty, e => e.CusAttributeValue8 != null && e.CusAttributeValue8.Id == attribute8Id)
                    .WhereIf(attribute9Id != null && attribute9Id != Guid.Empty, e => e.CusAttributeValue9 != null && e.CusAttributeValue9.Id == attribute9Id)
                    .WhereIf(attribute10Id != null && attribute10Id != Guid.Empty, e => e.CusAttributeValue10 != null && e.CusAttributeValue10.Id == attribute10Id)
                    .WhereIf(attribute11Id != null && attribute11Id != Guid.Empty, e => e.CusAttributeValue11 != null && e.CusAttributeValue11.Id == attribute11Id)
                    .WhereIf(attribute12Id != null && attribute12Id != Guid.Empty, e => e.CusAttributeValue12 != null && e.CusAttributeValue12.Id == attribute12Id)
                    .WhereIf(attribute13Id != null && attribute13Id != Guid.Empty, e => e.CusAttributeValue13 != null && e.CusAttributeValue13.Id == attribute13Id)
                    .WhereIf(attribute14Id != null && attribute14Id != Guid.Empty, e => e.CusAttributeValue14 != null && e.CusAttributeValue14.Id == attribute14Id)
                    .WhereIf(attribute15Id != null && attribute15Id != Guid.Empty, e => e.CusAttributeValue15 != null && e.CusAttributeValue15.Id == attribute15Id)
                    .WhereIf(attribute16Id != null && attribute16Id != Guid.Empty, e => e.CusAttributeValue16 != null && e.CusAttributeValue16.Id == attribute16Id)
                    .WhereIf(attribute1I7d != null && attribute1I7d != Guid.Empty, e => e.CusAttributeValue17 != null && e.CusAttributeValue17.Id == attribute1I7d)
                    .WhereIf(attribute18Id != null && attribute18Id != Guid.Empty, e => e.CusAttributeValue18 != null && e.CusAttributeValue18.Id == attribute18Id)
                    .WhereIf(attribute19Id != null && attribute19Id != Guid.Empty, e => e.CusAttributeValue19 != null && e.CusAttributeValue19.Id == attribute19Id)
                    .WhereIf(paymentId != null && paymentId != Guid.Empty, e => e.Customer1 != null && e.Customer1.Id == paymentId);
        }

        public async Task<List<Customer>> GetListAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string phone1 = null,
            string phone2 = null,
            string erpCode = null,
            string license = null,
            string taxCode = null,
            string vatName = null,
            string vatAddress = null,
            bool? active = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            int? creditLimitMin = null,
            int? creditLimitMax = null,
            bool? isCompany = null,
            Guid? warehouseId = null,
            string street = null,
            string address = null,
            string latitude = null,
            string longitude = null,
            string sfaCustomerCode = null,
            DateTime? lastOrderDateMin = null,
            DateTime? lastOrderDateMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, name, phone1, phone2, erpCode, license, taxCode, vatName, vatAddress, active, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax, creditLimitMin, creditLimitMax, isCompany, warehouseId, street, address, latitude, longitude, sfaCustomerCode, lastOrderDateMin, lastOrderDateMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string phone1 = null,
            string phone2 = null,
            string erpCode = null,
            string license = null,
            string taxCode = null,
            string vatName = null,
            string vatAddress = null,
            bool? active = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            int? creditLimitMin = null,
            int? creditLimitMax = null,
            bool? isCompany = null,
            Guid? warehouseId = null,
            string street = null,
            string address = null,
            string latitude = null,
            string longitude = null,
            string sfaCustomerCode = null,
            DateTime? lastOrderDateMin = null,
            DateTime? lastOrderDateMax = null,
            Guid? paymentTermId = null,
            Guid? linkedCompanyId = null,
            Guid? priceListId = null,
            Guid? geoMaster0Id = null,
            Guid? geoMaster1Id = null,
            Guid? geoMaster2Id = null,
            Guid? geoMaster3Id = null,
            Guid? geoMaster4Id = null,
            Guid? attribute0Id = null,
            Guid? attribute1Id = null,
            Guid? attribute2Id = null,
            Guid? attribute3Id = null,
            Guid? attribute4Id = null,
            Guid? attribute5Id = null,
            Guid? attribute6Id = null,
            Guid? attribute7Id = null,
            Guid? attribute8Id = null,
            Guid? attribute9Id = null,
            Guid? attribute10Id = null,
            Guid? attribute11Id = null,
            Guid? attribute12Id = null,
            Guid? attribute13Id = null,
            Guid? attribute14Id = null,
            Guid? attribute15Id = null,
            Guid? attribute16Id = null,
            Guid? attribute1I7d = null,
            Guid? attribute18Id = null,
            Guid? attribute19Id = null,
            Guid? paymentId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, name, phone1, phone2, erpCode, license, taxCode, vatName, vatAddress, active, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax, creditLimitMin, creditLimitMax, isCompany, warehouseId, street, address, latitude, longitude, sfaCustomerCode, lastOrderDateMin, lastOrderDateMax, paymentTermId, linkedCompanyId, priceListId, geoMaster0Id, geoMaster1Id, geoMaster2Id, geoMaster3Id, geoMaster4Id, attribute0Id, attribute1Id, attribute2Id, attribute3Id, attribute4Id, attribute5Id, attribute6Id, attribute7Id, attribute8Id, attribute9Id, attribute10Id, attribute11Id, attribute12Id, attribute13Id, attribute14Id, attribute15Id, attribute16Id, attribute1I7d, attribute18Id, attribute19Id, paymentId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Customer> ApplyFilter(
            IQueryable<Customer> query,
            string filterText,
            string code = null,
            string name = null,
            string phone1 = null,
            string phone2 = null,
            string erpCode = null,
            string license = null,
            string taxCode = null,
            string vatName = null,
            string vatAddress = null,
            bool? active = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            int? creditLimitMin = null,
            int? creditLimitMax = null,
            bool? isCompany = null,
            Guid? warehouseId = null,
            string street = null,
            string address = null,
            string latitude = null,
            string longitude = null,
            string sfaCustomerCode = null,
            DateTime? lastOrderDateMin = null,
            DateTime? lastOrderDateMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code.Contains(filterText) || e.Name.Contains(filterText) || e.Phone1.Contains(filterText) || e.Phone2.Contains(filterText) || e.erpCode.Contains(filterText) || e.License.Contains(filterText) || e.TaxCode.Contains(filterText) || e.vatName.Contains(filterText) || e.vatAddress.Contains(filterText) || e.Street.Contains(filterText) || e.Address.Contains(filterText) || e.Latitude.Contains(filterText) || e.Longitude.Contains(filterText) || e.SFACustomerCode.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(phone1), e => e.Phone1.Contains(phone1))
                    .WhereIf(!string.IsNullOrWhiteSpace(phone2), e => e.Phone2.Contains(phone2))
                    .WhereIf(!string.IsNullOrWhiteSpace(erpCode), e => e.erpCode.Contains(erpCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(license), e => e.License.Contains(license))
                    .WhereIf(!string.IsNullOrWhiteSpace(taxCode), e => e.TaxCode.Contains(taxCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(vatName), e => e.vatName.Contains(vatName))
                    .WhereIf(!string.IsNullOrWhiteSpace(vatAddress), e => e.vatAddress.Contains(vatAddress))
                    .WhereIf(active.HasValue, e => e.Active == active)
                    .WhereIf(effectiveDateMin.HasValue, e => e.EffectiveDate >= effectiveDateMin.Value)
                    .WhereIf(effectiveDateMax.HasValue, e => e.EffectiveDate <= effectiveDateMax.Value)
                    .WhereIf(endDateMin.HasValue, e => e.EndDate >= endDateMin.Value)
                    .WhereIf(endDateMax.HasValue, e => e.EndDate <= endDateMax.Value)
                    .WhereIf(creditLimitMin.HasValue, e => e.CreditLimit >= creditLimitMin.Value)
                    .WhereIf(creditLimitMax.HasValue, e => e.CreditLimit <= creditLimitMax.Value)
                    .WhereIf(isCompany.HasValue, e => e.IsCompany == isCompany)
                    .WhereIf(warehouseId.HasValue, e => e.WarehouseId == warehouseId)
                    .WhereIf(!string.IsNullOrWhiteSpace(street), e => e.Street.Contains(street))
                    .WhereIf(!string.IsNullOrWhiteSpace(address), e => e.Address.Contains(address))
                    .WhereIf(!string.IsNullOrWhiteSpace(latitude), e => e.Latitude.Contains(latitude))
                    .WhereIf(!string.IsNullOrWhiteSpace(longitude), e => e.Longitude.Contains(longitude))
                    .WhereIf(!string.IsNullOrWhiteSpace(sfaCustomerCode), e => e.SFACustomerCode.Contains(sfaCustomerCode))
                    .WhereIf(lastOrderDateMin.HasValue, e => e.LastOrderDate >= lastOrderDateMin.Value)
                    .WhereIf(lastOrderDateMax.HasValue, e => e.LastOrderDate <= lastOrderDateMax.Value);
        }
    }
}