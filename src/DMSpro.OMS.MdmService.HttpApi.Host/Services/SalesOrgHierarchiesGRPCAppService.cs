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

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies;

public class SalesOrgHierarchiesGRPCAppService : SalesOrgHierarchiesProtoAppService.SalesOrgHierarchiesProtoAppServiceBase
{
    private readonly ISalesOrgHierarchiesInternalAppService _internalAppService;
    private readonly ISalesOrgEmpAssignmentsAppService _employeeAssignmentsAppService;
    private readonly ICompanyInZonesAppService _companyInZonesAppService;
    private readonly ICustomerInZonesAppService _customerInZonesAppService;
    private readonly IEmployeeProfilesInternalAppService _employeeProfilesInternallAppService;

    public SalesOrgHierarchiesGRPCAppService(
        ISalesOrgHierarchiesInternalAppService internalAppService,
        ISalesOrgEmpAssignmentsAppService employeeAssignmentsAppService,
        ICompanyInZonesAppService companyInZonesAppService,
        ICustomerInZonesAppService customerInZonesAppService,
        IEmployeeProfilesInternalAppService employeeProfilesInternallAppService
    )
    {
        _internalAppService = internalAppService;
        _employeeAssignmentsAppService = employeeAssignmentsAppService;
        _companyInZonesAppService = companyInZonesAppService;
        _customerInZonesAppService = customerInZonesAppService;
        _employeeProfilesInternallAppService = employeeProfilesInternallAppService;
    }


    public override async Task<GetSOPORouteResponse> GetSOPORoute(GetSOPORouteRequest request, ServerCallContext context)
    {
        Guid id = new(request.RouteId);
        SalesOrgHierarchyWithTenantDto dto = await _internalAppService.GetWithTenantIdAsynce(id);
        var response = new GetSOPORouteResponse();

        if (dto == null)
        {
            return response;
        }

        Guid? tenantId = string.IsNullOrEmpty(request.TenantId) ? null : Guid.Parse(request.TenantId);
        if (dto.TenantId != tenantId)
        {
            return response;
        }

        if (!dto.Active)
        {
            return response;
        }
        if (dto.ParentId == null)
        {
            return response;
        }
        SalesOrgHierarchyWithTenantDto sellingZoneDto = await _internalAppService.GetWithTenantIdAsynce((Guid)dto.ParentId);
        if (sellingZoneDto == null)
        {
            return response;
        }

        if (!CheckSellingZone(sellingZoneDto, tenantId))
        {
            return response;
        }
        Employee employee = null;
        if (!string.IsNullOrEmpty(request.EmployeeId))
        {
            employee = await CheckEmployee(request.EmployeeId, sellingZoneDto.Id);
            if (employee == null)
            {
                return response;
            }
        }
        if (!(await CheckCompany(request.CompanyId, sellingZoneDto.Id)))
        {
            return response;
        }
        if (!string.IsNullOrEmpty(request.CustomerId))
        {
            if (!(await CheckCustomer(request.CustomerId, sellingZoneDto.Id)))
            {
                return response;
            }
        }

        response.Route = new OMS.Shared.Protos.MdmService.SalesOrgHierarchies.Route()
        {
            TenantId = dto.TenantId == null ? "" : dto.TenantId.ToString(),
            Code = dto.Code,
            Name = dto.Name,
        };
        response.Employee = employee;
        return response;
    }

    private bool CheckSellingZone(SalesOrgHierarchyWithTenantDto sellingZone, Guid? tenantId)
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

    private async Task<Employee> CheckEmployee(string employeeId, Guid sellingZoneId)
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
        EmployeeProfileWithTenantDto employee =
            await _employeeProfilesInternallAppService.GetWithTenantIdAsynce(Guid.Parse(employeeId));
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

    private async Task<bool> CheckCompany(string companyId, Guid sellingZoneId)
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

    private async Task<bool> CheckCustomer(string customerId, Guid sellingZoneId)
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