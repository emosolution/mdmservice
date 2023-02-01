using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.Companies
{
    public partial interface ICompanyRepository : IRepository<Company, Guid>
    {
        Task<CompanyWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<CompanyWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string street = null,
            string address = null,
            string phone = null,
            string license = null,
            string taxCode = null,
            string vatName = null,
            string vatAddress = null,
            string erpCode = null,
            bool? active = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            bool? isHO = null,
            string latitude = null,
            string longitude = null,
            string contactName = null,
            string contactPhone = null,
            Guid? parentId = null,
            Guid? geoLevel0Id = null,
            Guid? geoLevel1Id = null,
            Guid? geoLevel2Id = null,
            Guid? geoLevel3Id = null,
            Guid? geoLevel4Id = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<Company>> GetListAsync(
                    string filterText = null,
                    string code = null,
                    string name = null,
                    string street = null,
                    string address = null,
                    string phone = null,
                    string license = null,
                    string taxCode = null,
                    string vatName = null,
                    string vatAddress = null,
                    string erpCode = null,
                    bool? active = null,
                    DateTime? effectiveDateMin = null,
                    DateTime? effectiveDateMax = null,
                    DateTime? endDateMin = null,
                    DateTime? endDateMax = null,
                    bool? isHO = null,
                    string latitude = null,
                    string longitude = null,
                    string contactName = null,
                    string contactPhone = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string name = null,
            string street = null,
            string address = null,
            string phone = null,
            string license = null,
            string taxCode = null,
            string vatName = null,
            string vatAddress = null,
            string erpCode = null,
            bool? active = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            bool? isHO = null,
            string latitude = null,
            string longitude = null,
            string contactName = null,
            string contactPhone = null,
            Guid? parentId = null,
            Guid? geoLevel0Id = null,
            Guid? geoLevel1Id = null,
            Guid? geoLevel2Id = null,
            Guid? geoLevel3Id = null,
            Guid? geoLevel4Id = null,
            CancellationToken cancellationToken = default);
    }
}