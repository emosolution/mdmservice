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

    public override async Task<GetCustomerResponse> GetCustomerWithCompany(GetCustomerRequest request, ServerCallContext context)
    {
        Guid id = new(request.CustomerId);
        CustomerWithTenantDto dto = await _internalAppService.GetWithTenantIdAsynce(id);
        var response = new GetCustomerResponse();
        if (dto == null)
        {
            return response;
        }
        List<CustomerAssignmentWithNavigationProperties> assignments = 
            await _customerAssignmentRepository.GetListWithNavigationPropertiesAsync
            (
                customerId: id, companyId: Guid.Parse(request.CompanyId)
            );
        if (assignments.Count != 1)
        {
            return response;
        }

        CustomerAssignment assignment = assignments[0].CustomerAssignment;
        bool assignmentActive = MDMHelpers.CheckActive(true, assignment.EffectiveDate, assignment.EndDate);
        bool companyActive = MDMHelpers.CheckActive(dto.Active, dto.EffectiveDate, dto.EndDate);
        response.Customer = new OMS.Shared.Protos.MdmService.Customers.Customer()
        {
            Id = dto.Id.ToString(),
            CompanyId = assignment.CompanyId.ToString(),
            TenantId = dto.TenantId == null ? "" : dto.TenantId.ToString(),
            Code = dto.Code,
            Name = dto.Name,
            Active =  companyActive && assignmentActive,
        };
        return response;
    }
}