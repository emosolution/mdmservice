using System;
using System.Threading.Tasks;
using Grpc.Core;
using DMSpro.OMS.Shared.Protos.MdmService.Customers;
using DMSpro.OMS.MdmService.Helpers;
using DMSpro.OMS.MdmService.CustomerAssignments;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.Customers;

public class CustomersGRPCAppService : CustomersProtoAppService.CustomersProtoAppServiceBase
{
    private readonly ICustomersInternalAppService _internalAppService;
    private readonly ICustomerAssignmentRepository _customerAssignmentRepository;


    public CustomersGRPCAppService(ICustomersInternalAppService internalAppService,
        ICustomerAssignmentRepository customerAssignmentRepository)
    {
        _internalAppService = internalAppService;
        _customerAssignmentRepository = customerAssignmentRepository;
    }

    public override async Task<CustomerResponse> GetCustomerWithCompany(GetCustomerWithCompanyRequest request, ServerCallContext context)
    {
        Guid id = new(request.CustomerId);
        CustomerWithTenantDto dto = await _internalAppService.GetWithTenantIdAsynce(id);
        var response = new CustomerResponse();
        if (dto == null)
        {
            return response;
        }

        Guid? tenantId = string.IsNullOrEmpty(request.TenantId) ? null : Guid.Parse(request.TenantId);
        if (dto.TenantId != tenantId)
        {
            return response;
        }
        List<CustomerAssignmentWithNavigationProperties> assignments =
            await _customerAssignmentRepository.GetListWithNavigationPropertiesAsync(
                customerId: id, companyId: Guid.Parse(request.CompanyId));
        CustomerAssignment customerAssignment = null;
        bool assignmentActive = false;
        if (assignments.Count == 1)
        {
            customerAssignment = assignments[0].CustomerAssignment;
            assignmentActive = MDMHelpers.CheckActive(true, customerAssignment.EffectiveDate, customerAssignment.EndDate);
        }
        else
        {
            int activeCount = 0;
            foreach (CustomerAssignmentWithNavigationProperties item in assignments)
            {
                CustomerAssignment assignment = item.CustomerAssignment;
                bool active = MDMHelpers.CheckActive(true, assignment.EffectiveDate, assignment.EndDate);
                if (active)
                {
                    customerAssignment = assignment;
                    assignmentActive = true;
                    activeCount++;
                }
            }
            if (activeCount != 1)
            {
                return response;
            }
        }
        if (customerAssignment.TenantId != tenantId)
        {
            return response;
        }
        bool customerActive = MDMHelpers.CheckActive(dto.Active, dto.EffectiveDate, dto.EndDate);
        response.Customer = new OMS.Shared.Protos.MdmService.Customers.Customer()
        {
            Id = dto.Id.ToString(),
            CompanyId = customerAssignment.CompanyId.ToString(),
            TenantId = request.TenantId,
            Code = dto.Code,
            Name = dto.Name,
            Active = customerActive && assignmentActive,
        };
        return response;
    }
}