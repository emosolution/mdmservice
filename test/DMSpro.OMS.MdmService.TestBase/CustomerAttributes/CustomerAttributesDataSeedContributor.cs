using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.CustomerAttributes;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{
    public class CustomerAttributesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICustomerAttributeRepository _customerAttributeRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public CustomerAttributesDataSeedContributor(ICustomerAttributeRepository customerAttributeRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _customerAttributeRepository = customerAttributeRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _customerAttributeRepository.InsertAsync(new CustomerAttribute
            (
                id: Guid.Parse("fe36733c-32c1-4fb0-a391-3764f01cde30"),
                attrNo: 0,
                attrName: "0fa86c88b9f34f8babb0c81937b6509b52bf0d66a089462db98e8f3557ae7b2ab3554ec628754fc0894c2fa0f7cdd1cb80f4",
                active: true
            ));

            await _customerAttributeRepository.InsertAsync(new CustomerAttribute
            (
                id: Guid.Parse("435fc449-90cb-4685-802b-c139695f2402"),
                attrNo: 17,
                attrName: "ac6d926cd9bd481da3132db2a6f313ec94d7ffd1e67a4a9bb29f5d711dac70021952e25de5fd41ed8fd08da23f6a88edc8b6",
                active: true
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}