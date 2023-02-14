using System;
using System.Threading.Tasks;
using Grpc.Core;
using DMSpro.OMS.Shared.Protos.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.Helpers;
using DMSpro.OMS.MdmService.SalesOrgEmpAssignments;
using DMSpro.OMS.MdmService.CompanyInZones;
using DMSpro.OMS.MdmService.CustomerInZones;
using DMSpro.OMS.MdmService.EmployeeProfiles;
using DMSpro.OMS.Shared.Protos.MdmService.EmployeeProfiles;
using System.Linq;
using Volo.Abp.MultiTenancy;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies;

public class SalesOrgHierarchiesGRPCAppService : SalesOrgHierarchiesProtoAppService.SalesOrgHierarchiesProtoAppServiceBase
{
    private readonly ISalesOrgHierarchyRepository _repository;
    private readonly ISalesOrgEmpAssignmentsAppService _employeeAssignmentsAppService;
    private readonly ICompanyInZonesAppService _companyInZonesAppService;
    private readonly ICustomerInZonesAppService _customerInZonesAppService;
    private readonly IEmployeeProfileRepository _employeeProfileRepository;
    private readonly ICurrentTenant _currentTenant;

    public SalesOrgHierarchiesGRPCAppService(
        ISalesOrgHierarchyRepository repository,
        ISalesOrgEmpAssignmentsAppService employeeAssignmentsAppService,
        ICompanyInZonesAppService companyInZonesAppService,
        ICustomerInZonesAppService customerInZonesAppService,
        IEmployeeProfileRepository employeeProfileRepository,
        ICurrentTenant currentTenant)
    {
        _repository = repository;
        _employeeAssignmentsAppService = employeeAssignmentsAppService;
        _companyInZonesAppService = companyInZonesAppService;
        _customerInZonesAppService = customerInZonesAppService;
        _employeeProfileRepository = employeeProfileRepository;
        _currentTenant = currentTenant;
    }


    public override async Task<GetSOPORouteResponse> GetSOPORoute(GetSOPORouteRequest request, ServerCallContext context)
    {
        Guid id = new(request.RouteId);
        Guid? tenantId = string.IsNullOrEmpty(request.TenantId) ? null : new(request.TenantId);
        using (_currentTenant.Change(tenantId))
        {
            IQueryable<SalesOrgHierarchy> queryable = await _repository.GetQueryableAsync();
            var query = from item in queryable
                        where item.Id == id && item.TenantId == tenantId
                        select item;
            SalesOrgHierarchy route = query.FirstOrDefault();
            var response = new GetSOPORouteResponse();
            if (route == null)
            {
                return response;
            }
            if (!route.Active)
            {
                return response;
            }
            if (route.ParentId == null)
            {
                return response;
            }
            var sellingZonequery = from item in queryable
                                   where item.Id == route.ParentId && item.TenantId == tenantId
                                   select item;
            SalesOrgHierarchy sellingZone = query.FirstOrDefault();
            if (sellingZone == null)
            {
                return response;
            }

            if (!CheckSellingZone(sellingZone, tenantId))
            {
                return response;
            }
            Employee employee = null;
            if (!string.IsNullOrEmpty(request.EmployeeId))
            {
                employee = await CheckEmployee(request.EmployeeId, sellingZone.Id, tenantId);
                if (employee == null)
                {
                    return response;
                }
            }
            if (!(await CheckCompany(request.CompanyId, sellingZone.Id, tenantId)))
            {
                return response;
            }
            if (!string.IsNullOrEmpty(request.CustomerId))
            {
                if (!(await CheckCustomer(request.CustomerId, sellingZone.Id, tenantId)))
                {
                    return response;
                }
            }

            response.Route = new OMS.Shared.Protos.MdmService.SalesOrgHierarchies.Route()
            {
                TenantId = route.TenantId == null ? "" : route.TenantId.ToString(),
                Code = route.Code,
                Name = route.Name,
            };
            response.Employee = employee;
            return response;
        }
    }

    private static bool CheckSellingZone(SalesOrgHierarchy sellingZone, Guid? tenantId)
    {
        if (sellingZone.TenantId != tenantId)
        {
            return false;
        }
        if (!sellingZone.IsSellingZone)
        {
            return false;
        }
        if (!sellingZone.Active)
        {
            return false;
        }
        return true;
    }

    private async Task<Employee> CheckEmployee(string employeeId, Guid sellingZoneId, Guid? tenantId)
    {
        using (_currentTenant.Change(tenantId))
        {
            SalesOrgEmpAssignmentWithNavigationPropertiesDto dto =
            await _employeeAssignmentsAppService.GetWithNavigationPropertiesAsync(sellingZoneId);
            if (dto.EmployeeProfile.Id.ToString().CompareTo(employeeId) != 0)
            {
                return null;
            }
            SalesOrgEmpAssignmentDto assingment = dto.SalesOrgEmpAssignment;
            if (!MDMHelpers.CheckActive(true, assingment.EffectiveDate, assingment.EndDate))
            {
                return null;
            }
            IQueryable<EmployeeProfile> queryable = await _employeeProfileRepository.GetQueryableAsync();
            var query = from item in queryable
                        where item.Id == Guid.Parse(employeeId)
                        select item;
            EmployeeProfile employee = query.FirstOrDefault();
            DateTime effectiveDate = DateTime.Now;
            if (employee.EffectiveDate != null)
            {
                effectiveDate = (DateTime)employee.EffectiveDate;
            }
            // TODO Do we need to check employee type?
            Employee result = new()
            {
                Id = employeeId,
                TenantId = employee.TenantId == null ? "" : employee.TenantId.ToString(),
                Code = employee.Code,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                ErpCode = employee.ERPCode,
                Active = MDMHelpers.CheckActive(employee.Active, effectiveDate, employee.EndDate),
            };
            return result;
        }
    }

    private async Task<bool> CheckCompany(string companyId, Guid sellingZoneId, Guid? tenantId)
    {
        using (_currentTenant.Change(tenantId))
        {
            CompanyInZoneWithNavigationPropertiesDto dto =
            await _companyInZonesAppService.GetWithNavigationPropertiesAsync(sellingZoneId);
            if (dto.Company.Id.ToString().CompareTo(companyId) != 0)
            {
                return false;
            }
            CompanyInZoneDto assingment = dto.CompanyInZone;
            if (!MDMHelpers.CheckActive(true, assingment.EffectiveDate, assingment.EndDate))
            {
                return false;
            }
            return true;
        }
    }

    private async Task<bool> CheckCustomer(string customerId, Guid sellingZoneId, Guid? tenantId)
    {
        using (_currentTenant.Change(tenantId))
        {
            CustomerInZoneWithNavigationPropertiesDto dto =
            await _customerInZonesAppService.GetWithNavigationPropertiesAsync(sellingZoneId);
            if (dto.Customer.Id.ToString().CompareTo(customerId) != 0)
            {
                return false;
            }
            CustomerInZoneDto assingment = dto.CustomerInZone;
            DateTime assignmentEffectiveDate = DateTime.Now;
            if (assingment.EffectiveDate != null)
            {
                assignmentEffectiveDate = (DateTime)assingment.EffectiveDate;
            }
            if (!MDMHelpers.CheckActive(true, assignmentEffectiveDate, assingment.EndDate))
            {
                return false;
            }
            return true;
        }
    }
}