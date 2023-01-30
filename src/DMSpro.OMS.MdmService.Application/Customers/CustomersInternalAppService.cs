using AutoMapper.Internal.Mappers;
using DMSpro.OMS.MdmService.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.ObjectMapping;

namespace DMSpro.OMS.MdmService.Customers
{
    internal class CustomersInternalAppService : ApplicationService, ICustomersInternalAppService
    {
        private readonly ICustomerRepository _repository;

        public CustomersInternalAppService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public virtual async Task<CustomerWithTenantDto> GetWithTenantIdAsynce(Guid id)
        {
            try
            {
                Customer customer = await _repository.GetAsync(id);
                return ObjectMapper.Map<Customer, CustomerWithTenantDto>(customer);
            }
            catch (EntityNotFoundException)
            {
                return null;
            }
        }
    }
}