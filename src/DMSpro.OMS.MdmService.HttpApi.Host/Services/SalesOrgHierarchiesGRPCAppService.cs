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
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies;

public class SalesOrgHierarchiesGRPCAppService : SalesOrgHierarchiesProtoAppService.SalesOrgHierarchiesProtoAppServiceBase
{
    private readonly ISalesOrgHierarchyRepository _repository;
    private readonly ISalesOrgEmpAssignmentsAppService _employeeAssignmentsAppService;
    private readonly ICompanyInZonesAppService _companyInZonesAppService;
    private readonly ICustomerInZonesAppService _customerInZonesAppService;
    private readonly IEmployeeProfileRepository _employeeProfileRepository;
    private readonly ICompanyInZoneRepository _companyInZoneRepository;
    private readonly ICustomerInZoneRepository _customerInZoneRepository;
    private readonly ICurrentTenant _currentTenant;

    public SalesOrgHierarchiesGRPCAppService(
        ISalesOrgHierarchyRepository repository,
        ISalesOrgEmpAssignmentsAppService employeeAssignmentsAppService,
        ICompanyInZonesAppService companyInZonesAppService,
        ICustomerInZonesAppService customerInZonesAppService,
        IEmployeeProfileRepository employeeProfileRepository,
        ICompanyInZoneRepository companyInZoneRepository,
        ICustomerInZoneRepository customerInZoneRepository,
        ICurrentTenant currentTenant)
    {
        _repository = repository;
        _employeeAssignmentsAppService = employeeAssignmentsAppService;
        _companyInZonesAppService = companyInZonesAppService;
        _customerInZonesAppService = customerInZonesAppService;
        _employeeProfileRepository = employeeProfileRepository;
        _companyInZoneRepository = companyInZoneRepository;
        _customerInZoneRepository = customerInZoneRepository;
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
                response.Employee = employee;
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
            return response;
        }
    }

    public override async Task<GetSellingZoneFromCompanyAndCustomerResponse>
        GetSellingZoneFromCompanyAndCustomer(GetSellingZoneFromCompanyAndCustomerRequest request, 
            ServerCallContext context)
    {
        Guid? tenantId = string.IsNullOrEmpty(request.TenantId) ? null : new(request.TenantId);
        Guid companyId = Guid.Parse(request.CompanyId);
        Guid? customerId = string.IsNullOrEmpty(request.CustomerId) ? null : new(request.CustomerId);
        var response = new GetSellingZoneFromCompanyAndCustomerResponse();
        List<SalesOrgHierarchy> companyZones = await GetSellingZonesFromCompany(companyId, tenantId);
        List<SalesOrgHierarchy> commonZones = new(companyZones);
        if (customerId != null)
        {
            List<SalesOrgHierarchy> customerZones = await GetSellingZonesFromCustomer((Guid) customerId, tenantId);
            commonZones = customerZones.Intersect(companyZones).ToList();
        }
        if (commonZones.Count() < 1)
        {
            return response;
        }
        foreach (var zone in commonZones)
        {
            response.SellingZoneIds.Add(zone.Id.ToString());
        }
        return response;
    }

    private async Task<List<SalesOrgHierarchy>> GetSellingZonesFromCompany(Guid companyId, Guid? tenantId)
    {
        using (_currentTenant.Change(tenantId))
        {
            var companiesInZone =
                await _companyInZoneRepository.GetListWithNavigationPropertiesAsync(isBase: true, companyId: companyId);
            List<SalesOrgHierarchy> zonesWithCompany = new();
            foreach (var companyInZone in companiesInZone)
            {
                var assignment = companyInZone.CompanyInZone;
                bool active = MDMHelpers.CheckActive(true, assignment.EffectiveDate, assignment.EndDate);
                if (!active)
                {
                    continue;
                }
                var zone = companyInZone.SalesOrgHierarchy;
                if (!zone.Active || !zone.IsSellingZone)
                {
                    continue;
                }
                zonesWithCompany.Add(zone);
            }
            return zonesWithCompany;
        }
    }

    private async Task<List<SalesOrgHierarchy>> GetSellingZonesFromCustomer(Guid customerId, Guid? tenantId)
    {
        using (_currentTenant.Change(tenantId))
        {
            var customersInZone =
                await _customerInZoneRepository.GetListWithNavigationPropertiesAsync(customerId: customerId);
            List<SalesOrgHierarchy> zonesWithCustomer = new();
            foreach (var customerInZone in customersInZone)
            {
                var assignment = customerInZone.CustomerInZone;
                DateTime assignmentStartDate = assignment.EffectiveDate == null ? new DateTime(1900, 1, 1) : 
                    (DateTime) assignment.EffectiveDate;
                bool active = MDMHelpers.CheckActive(true, assignmentStartDate, assignment.EndDate);
                if (!active)
                {
                    continue;
                }
                var zone = customerInZone.SalesOrgHierarchy;
                if (!zone.Active || !zone.IsSellingZone)
                {
                    continue;
                }
                zonesWithCustomer.Add(zone);
            }
            return zonesWithCustomer;
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
            Employee result = new Employee();
            if (employee is not null)
            {
                if (employee.EffectiveDate != null)
                {
                    effectiveDate = (DateTime)employee.EffectiveDate;
                }
                // TODO Do we need to check employee type?
                result = new Employee()
                {
                    Id = employeeId,
                    TenantId = employee.TenantId == null ? "" : employee.TenantId.ToString(),
                    Code = employee.Code,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    ErpCode = employee.ERPCode,
                    Active = MDMHelpers.CheckActive(employee.Active, effectiveDate, employee.EndDate),
                };
            }
            
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