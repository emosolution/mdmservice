using System;
using System.Threading.Tasks;
using Grpc.Core;
using DMSpro.OMS.Shared.Protos.MdmService.Customers;
using DMSpro.OMS.MdmService.Helpers;
using DMSpro.OMS.MdmService.CustomerAssignments;
using System.Collections.Generic;
using Volo.Abp.Domain.Repositories;
using System.Linq;
using Volo.Abp.MultiTenancy;

namespace DMSpro.OMS.MdmService.Customers;

public class CustomersGRPCAppService : CustomersProtoAppService.CustomersProtoAppServiceBase
{
    private readonly ICustomerAssignmentRepository _customerAssignmentRepository;
    private readonly ICustomerRepository _repository;
    private readonly ICurrentTenant _currentTenant;

    public CustomersGRPCAppService(ICustomerRepository repository,
        ICustomerAssignmentRepository customerAssignmentRepository,
        ICurrentTenant currentTenant)
    {
        _repository = repository;
        _customerAssignmentRepository = customerAssignmentRepository;
        _currentTenant = currentTenant;
    }

    public override async Task<CustomerResponse> GetCustomerWithCompany(GetCustomerWithCompanyRequest request, ServerCallContext context)
    {
        Guid id = new(request.CustomerId);
        Guid? tenantId = string.IsNullOrEmpty(request.TenantId) ? null : new(request.TenantId);
        using (_currentTenant.Change(tenantId))
        {
            IQueryable<Customer> queryable = await _repository.GetQueryableAsync();
            var query = from item in queryable
                        where item.Id == id && item.TenantId == tenantId
                        select item;
            Customer customer = query.FirstOrDefault();
            var response = new CustomerResponse();
            if (customer == null)
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
            bool customerActive = MDMHelpers.CheckActive(customer.Active, customer.EffectiveDate, customer.EndDate);
            response.Customer = new OMS.Shared.Protos.MdmService.Customers.Customer()
            {
                Id = customer.Id.ToString(),
                CompanyId = customerAssignment.CompanyId.ToString(),
                TenantId = request.TenantId,
                Code = customer.Code,
                Name = customer.Name,
                Active = customerActive && assignmentActive,
            };
            return response;
        }
    }
}