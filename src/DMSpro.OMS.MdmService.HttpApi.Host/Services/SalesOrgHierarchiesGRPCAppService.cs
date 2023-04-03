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
    private readonly IEmployeeProfileRepository _employeeProfileRepository;
    private readonly ICompanyInZoneRepository _companyInZoneRepository;
    private readonly ICustomerInZoneRepository _customerInZoneRepository;
    private readonly ISalesOrgEmpAssignmentRepository _salesOrgEmpAssignmentRepository;
    private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;
    private readonly ICurrentTenant _currentTenant;

    public SalesOrgHierarchiesGRPCAppService(
        ISalesOrgHierarchyRepository repository,
        IEmployeeProfileRepository employeeProfileRepository,
        ICompanyInZoneRepository companyInZoneRepository,
        ICustomerInZoneRepository customerInZoneRepository,
        ISalesOrgEmpAssignmentRepository salesOrgEmpAssignmentRepository,
        ISalesOrgHierarchyRepository salesOrgHierarchyRepository,
        ICurrentTenant currentTenant)
    {
        _repository = repository;
        _employeeProfileRepository = employeeProfileRepository;
        _companyInZoneRepository = companyInZoneRepository;
        _customerInZoneRepository = customerInZoneRepository;
        _salesOrgEmpAssignmentRepository = salesOrgEmpAssignmentRepository;
        _salesOrgHierarchyRepository  = salesOrgHierarchyRepository;
        _currentTenant = currentTenant;
    }


    public override async Task<GetSOPORouteResponse> GetSOPORoute(GetSOPORouteRequest request, ServerCallContext context)
    {
        Guid id = new(request.RouteId);
        Guid? tenantId = string.IsNullOrEmpty(request.TenantId) ? null : new(request.TenantId);
        using (_currentTenant.Change(tenantId))
        {
            SalesOrgHierarchy route = await _repository.GetAsync(x => x.Id == id);
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
            SalesOrgHierarchy sellingZone = await _repository.GetAsync(x => x.Id == route.ParentId);
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
        DateTime now = DateTime.Now;
        using (_currentTenant.Change(tenantId))
        {
            var result = (await _companyInZoneRepository.GetListAsync(
                x => x.EffectiveDate < now &&
                (x.EndDate == null || x.EndDate >= now) &&
                x.CompanyId == companyId, includeDetails:true))
                .Select(x => x.SalesOrgHierarchy).Distinct().ToList();
            return result;
        }
    }

    private async Task<List<SalesOrgHierarchy>> GetSellingZonesFromCustomer(Guid customerId, Guid? tenantId)
    {
        using (_currentTenant.Change(tenantId))
        {
            var customersInZone =
                await _customerInZoneRepository.GetListAsync(x => x.CustomerId == customerId);
            List<SalesOrgHierarchy> zonesWithCustomer = new();
            foreach (var customerInZone in customersInZone)
            {
                bool active = MDMHelpers.CheckActive(true, customerInZone.EffectiveDate, customerInZone.EndDate);
                if (!active)
                {
                    continue;
                }
                var zone = await _salesOrgHierarchyRepository.GetAsync(customerInZone.SalesOrgHierarchyId);
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
            DateTime now = DateTime.Now;
            var assingments = await _salesOrgEmpAssignmentRepository.GetListAsync(
                x => x.SalesOrgHierarchyId == sellingZoneId &&
                x.EmployeeProfileId.ToString() == employeeId &&
                x.EffectiveDate < now &&
                (x.EndDate == null || x.EndDate > now));
            if (assingments.Count != 1)
            {
                return null;
            }
            var assignment = assingments.First();
            EmployeeProfile employee = await _employeeProfileRepository.GetAsync(
                x => x.Id == assignment.EmployeeProfileId);
            Employee result = new()
            {
                Id = employeeId,
                TenantId = employee.TenantId == null ? "" : employee.TenantId.ToString(),
                Code = employee.Code,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                ErpCode = employee.ERPCode,
                Active = employee.Active,
            };
            return result;
        }
    }

    private async Task<bool> CheckCompany(string companyId, Guid sellingZoneId, Guid? tenantId)
    {
        DateTime now = DateTime.Now;
        using (_currentTenant.Change(tenantId))
        {
            var assingments = await _companyInZoneRepository.GetListAsync(x => x.SalesOrgHierarchyId == sellingZoneId &&
                x.CompanyId.ToString() == companyId && x.EffectiveDate < now &&
                (x.EndDate == null && x.EndDate >= now));
            if (assingments.Count != 1)
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
            DateTime now = DateTime.Now;
            var customerInZone = await _customerInZoneRepository.GetListAsync(
                x => x.CustomerId.ToString() == customerId &&
                x.SalesOrgHierarchyId == sellingZoneId && 
                x.EffectiveDate < now &&
                (x.EndDate == null || x.EndDate > now));
            if (customerInZone.Count != 1)
            {
                return false;
            }
            return true;
        }
    }
}