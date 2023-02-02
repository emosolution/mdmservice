using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.Vendors
{
    public partial interface IVendorRepository : IRepository<Vendor, Guid>
    {
        Task<VendorWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<VendorWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string shortName = null,
            string phone1 = null,
            string phone2 = null,
            string erpCode = null,
            bool? active = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            string linkedCompany = null,
            Guid? warehouseId = null,
            string street = null,
            string address = null,
            string latitude = null,
            string longitude = null,
            Guid? priceListId = null,
            Guid? geoMaster0Id = null,
            Guid? geoMaster1Id = null,
            Guid? geoMaster2Id = null,
            Guid? geoMaster3Id = null,
            Guid? geoMaster4Id = null,
            Guid? companyId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<Vendor>> GetListAsync(
                    string filterText = null,
                    string code = null,
                    string name = null,
                    string shortName = null,
                    string phone1 = null,
                    string phone2 = null,
                    string erpCode = null,
                    bool? active = null,
                    DateTime? endDateMin = null,
                    DateTime? endDateMax = null,
                    string linkedCompany = null,
                    Guid? warehouseId = null,
                    string street = null,
                    string address = null,
                    string latitude = null,
                    string longitude = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string shortName = null,
            string phone1 = null,
            string phone2 = null,
            string erpCode = null,
            bool? active = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            string linkedCompany = null,
            Guid? warehouseId = null,
            string street = null,
            string address = null,
            string latitude = null,
            string longitude = null,
            Guid? priceListId = null,
            Guid? geoMaster0Id = null,
            Guid? geoMaster1Id = null,
            Guid? geoMaster2Id = null,
            Guid? geoMaster3Id = null,
            Guid? geoMaster4Id = null,
            Guid? companyId = null,
            CancellationToken cancellationToken = default);
    }
}