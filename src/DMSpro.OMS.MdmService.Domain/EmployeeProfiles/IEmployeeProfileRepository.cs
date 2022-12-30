using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    public interface IEmployeeProfileRepository : IRepository<EmployeeProfile, Guid>
    {
        Task<EmployeeProfileWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<EmployeeProfileWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string code = null,
            string erpCode = null,
            string firstName = null,
            string lastName = null,
            DateTime? dateOfBirthMin = null,
            DateTime? dateOfBirthMax = null,
            string idCardNumber = null,
            string email = null,
            string phone = null,
            string address = null,
            bool? active = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            Guid? identityUserId = null,
            Guid? workingPositionId = null,
            Guid? employeeTypeId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<EmployeeProfile>> GetListAsync(
                    string filterText = null,
                    string code = null,
                    string erpCode = null,
                    string firstName = null,
                    string lastName = null,
                    DateTime? dateOfBirthMin = null,
                    DateTime? dateOfBirthMax = null,
                    string idCardNumber = null,
                    string email = null,
                    string phone = null,
                    string address = null,
                    bool? active = null,
                    DateTime? effectiveDateMin = null,
                    DateTime? effectiveDateMax = null,
                    DateTime? endDateMin = null,
                    DateTime? endDateMax = null,
                    Guid? identityUserId = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string code = null,
            string erpCode = null,
            string firstName = null,
            string lastName = null,
            DateTime? dateOfBirthMin = null,
            DateTime? dateOfBirthMax = null,
            string idCardNumber = null,
            string email = null,
            string phone = null,
            string address = null,
            bool? active = null,
            DateTime? effectiveDateMin = null,
            DateTime? effectiveDateMax = null,
            DateTime? endDateMin = null,
            DateTime? endDateMax = null,
            Guid? identityUserId = null,
            Guid? workingPositionId = null,
            Guid? employeeTypeId = null,
            CancellationToken cancellationToken = default);
    }
}