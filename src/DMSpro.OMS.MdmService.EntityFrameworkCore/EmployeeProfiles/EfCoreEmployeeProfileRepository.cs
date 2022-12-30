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

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    public class EfCoreEmployeeProfileRepository : EfCoreRepository<MdmServiceDbContext, EmployeeProfile, Guid>, IEmployeeProfileRepository
    {
        public EfCoreEmployeeProfileRepository(IDbContextProvider<MdmServiceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<EmployeeProfileWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(employeeProfile => new EmployeeProfileWithNavigationProperties
                {
                    EmployeeProfile = employeeProfile,
                    WorkingPosition = dbContext.WorkingPositions.FirstOrDefault(c => c.Id == employeeProfile.WorkingPositionId),
                    SystemData = dbContext.SystemDatas.FirstOrDefault(c => c.Id == employeeProfile.EmployeeTypeId)
                }).FirstOrDefault();
        }

        public async Task<List<EmployeeProfileWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, erpCode, firstName, lastName, dateOfBirthMin, dateOfBirthMax, idCardNumber, email, phone, address, active, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax, identityUserId, workingPositionId, employeeTypeId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EmployeeProfileConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<EmployeeProfileWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from employeeProfile in (await GetDbSetAsync())
                   join workingPosition in (await GetDbContextAsync()).WorkingPositions on employeeProfile.WorkingPositionId equals workingPosition.Id into workingPositions
                   from workingPosition in workingPositions.DefaultIfEmpty()
                   join systemData in (await GetDbContextAsync()).SystemDatas on employeeProfile.EmployeeTypeId equals systemData.Id into systemDatas
                   from systemData in systemDatas.DefaultIfEmpty()

                   select new EmployeeProfileWithNavigationProperties
                   {
                       EmployeeProfile = employeeProfile,
                       WorkingPosition = workingPosition,
                       SystemData = systemData
                   };
        }

        protected virtual IQueryable<EmployeeProfileWithNavigationProperties> ApplyFilter(
            IQueryable<EmployeeProfileWithNavigationProperties> query,
            string filterText,
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
            Guid? employeeTypeId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.EmployeeProfile.Code.Contains(filterText) || e.EmployeeProfile.ERPCode.Contains(filterText) || e.EmployeeProfile.FirstName.Contains(filterText) || e.EmployeeProfile.LastName.Contains(filterText) || e.EmployeeProfile.IdCardNumber.Contains(filterText) || e.EmployeeProfile.Email.Contains(filterText) || e.EmployeeProfile.Phone.Contains(filterText) || e.EmployeeProfile.Address.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.EmployeeProfile.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(erpCode), e => e.EmployeeProfile.ERPCode.Contains(erpCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(firstName), e => e.EmployeeProfile.FirstName.Contains(firstName))
                    .WhereIf(!string.IsNullOrWhiteSpace(lastName), e => e.EmployeeProfile.LastName.Contains(lastName))
                    .WhereIf(dateOfBirthMin.HasValue, e => e.EmployeeProfile.DateOfBirth >= dateOfBirthMin.Value)
                    .WhereIf(dateOfBirthMax.HasValue, e => e.EmployeeProfile.DateOfBirth <= dateOfBirthMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(idCardNumber), e => e.EmployeeProfile.IdCardNumber.Contains(idCardNumber))
                    .WhereIf(!string.IsNullOrWhiteSpace(email), e => e.EmployeeProfile.Email.Contains(email))
                    .WhereIf(!string.IsNullOrWhiteSpace(phone), e => e.EmployeeProfile.Phone.Contains(phone))
                    .WhereIf(!string.IsNullOrWhiteSpace(address), e => e.EmployeeProfile.Address.Contains(address))
                    .WhereIf(active.HasValue, e => e.EmployeeProfile.Active == active)
                    .WhereIf(effectiveDateMin.HasValue, e => e.EmployeeProfile.EffectiveDate >= effectiveDateMin.Value)
                    .WhereIf(effectiveDateMax.HasValue, e => e.EmployeeProfile.EffectiveDate <= effectiveDateMax.Value)
                    .WhereIf(endDateMin.HasValue, e => e.EmployeeProfile.EndDate >= endDateMin.Value)
                    .WhereIf(endDateMax.HasValue, e => e.EmployeeProfile.EndDate <= endDateMax.Value)
                    .WhereIf(identityUserId.HasValue, e => e.EmployeeProfile.IdentityUserId == identityUserId)
                    .WhereIf(workingPositionId != null && workingPositionId != Guid.Empty, e => e.WorkingPosition != null && e.WorkingPosition.Id == workingPositionId)
                    .WhereIf(employeeTypeId != null && employeeTypeId != Guid.Empty, e => e.SystemData != null && e.SystemData.Id == employeeTypeId);
        }

        public async Task<List<EmployeeProfile>> GetListAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, code, erpCode, firstName, lastName, dateOfBirthMin, dateOfBirthMax, idCardNumber, email, phone, address, active, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax, identityUserId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EmployeeProfileConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, code, erpCode, firstName, lastName, dateOfBirthMin, dateOfBirthMax, idCardNumber, email, phone, address, active, effectiveDateMin, effectiveDateMax, endDateMin, endDateMax, identityUserId, workingPositionId, employeeTypeId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<EmployeeProfile> ApplyFilter(
            IQueryable<EmployeeProfile> query,
            string filterText,
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
            Guid? identityUserId = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code.Contains(filterText) || e.ERPCode.Contains(filterText) || e.FirstName.Contains(filterText) || e.LastName.Contains(filterText) || e.IdCardNumber.Contains(filterText) || e.Email.Contains(filterText) || e.Phone.Contains(filterText) || e.Address.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code))
                    .WhereIf(!string.IsNullOrWhiteSpace(erpCode), e => e.ERPCode.Contains(erpCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(firstName), e => e.FirstName.Contains(firstName))
                    .WhereIf(!string.IsNullOrWhiteSpace(lastName), e => e.LastName.Contains(lastName))
                    .WhereIf(dateOfBirthMin.HasValue, e => e.DateOfBirth >= dateOfBirthMin.Value)
                    .WhereIf(dateOfBirthMax.HasValue, e => e.DateOfBirth <= dateOfBirthMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(idCardNumber), e => e.IdCardNumber.Contains(idCardNumber))
                    .WhereIf(!string.IsNullOrWhiteSpace(email), e => e.Email.Contains(email))
                    .WhereIf(!string.IsNullOrWhiteSpace(phone), e => e.Phone.Contains(phone))
                    .WhereIf(!string.IsNullOrWhiteSpace(address), e => e.Address.Contains(address))
                    .WhereIf(active.HasValue, e => e.Active == active)
                    .WhereIf(effectiveDateMin.HasValue, e => e.EffectiveDate >= effectiveDateMin.Value)
                    .WhereIf(effectiveDateMax.HasValue, e => e.EffectiveDate <= effectiveDateMax.Value)
                    .WhereIf(endDateMin.HasValue, e => e.EndDate >= endDateMin.Value)
                    .WhereIf(endDateMax.HasValue, e => e.EndDate <= endDateMax.Value)
                    .WhereIf(identityUserId.HasValue, e => e.IdentityUserId == identityUserId);
        }
    }
}