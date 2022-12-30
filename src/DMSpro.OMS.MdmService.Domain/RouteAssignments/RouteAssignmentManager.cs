using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.RouteAssignments
{
    public class RouteAssignmentManager : DomainService
    {
        private readonly IRouteAssignmentRepository _routeAssignmentRepository;

        public RouteAssignmentManager(IRouteAssignmentRepository routeAssignmentRepository)
        {
            _routeAssignmentRepository = routeAssignmentRepository;
        }

        public async Task<RouteAssignment> CreateAsync(
        Guid routeId, Guid employeeId, DateTime effectiveDate, DateTime? endDate = null)
        {
            Check.NotNull(routeId, nameof(routeId));
            Check.NotNull(employeeId, nameof(employeeId));
            Check.NotNull(effectiveDate, nameof(effectiveDate));

            var routeAssignment = new RouteAssignment(
             GuidGenerator.Create(),
             routeId, employeeId, effectiveDate, endDate
             );

            return await _routeAssignmentRepository.InsertAsync(routeAssignment);
        }

        public async Task<RouteAssignment> UpdateAsync(
            Guid id,
            Guid routeId, Guid employeeId, DateTime effectiveDate, DateTime? endDate = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(routeId, nameof(routeId));
            Check.NotNull(employeeId, nameof(employeeId));
            Check.NotNull(effectiveDate, nameof(effectiveDate));

            var queryable = await _routeAssignmentRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var routeAssignment = await AsyncExecuter.FirstOrDefaultAsync(query);

            routeAssignment.RouteId = routeId;
            routeAssignment.EmployeeId = employeeId;
            routeAssignment.EffectiveDate = effectiveDate;
            routeAssignment.EndDate = endDate;

            routeAssignment.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _routeAssignmentRepository.UpdateAsync(routeAssignment);
        }

    }
}