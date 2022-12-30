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
                id: Guid.Parse("14d12cb7-01d4-4f12-ace1-bce02dcebeae"),
                attrNo: 8,
                attrName: "d75a8396156444f7a4f8fef68925c67a9bc1cafd5a644b549e088f6b2f9064f921eba12b503149358e7fbcd9ded79c61c291",
                hierarchyLevel: 17,
                active: true
            ));

            await _customerAttributeRepository.InsertAsync(new CustomerAttribute
            (
                id: Guid.Parse("d7a80fca-0873-49ee-860d-38bbc820d59e"),
                attrNo: 11,
                attrName: "2f7842cae56248f798b63e33dfb6986c2a1d649fb83d4b32b2d97ee7fd7a27a4b38e51023db14a4591d02e49fd832e770675",
                hierarchyLevel: 17,
                active: true
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}