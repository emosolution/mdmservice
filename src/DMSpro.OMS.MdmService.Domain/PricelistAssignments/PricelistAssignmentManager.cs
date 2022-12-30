using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
    public class PricelistAssignmentManager : DomainService
    {
        private readonly IPricelistAssignmentRepository _pricelistAssignmentRepository;

        public PricelistAssignmentManager(IPricelistAssignmentRepository pricelistAssignmentRepository)
        {
            _pricelistAssignmentRepository = pricelistAssignmentRepository;
        }

        public async Task<PricelistAssignment> CreateAsync(
        Guid priceListId, Guid customerGroupId, string description)
        {
            Check.NotNull(priceListId, nameof(priceListId));
            Check.NotNull(customerGroupId, nameof(customerGroupId));

            var pricelistAssignment = new PricelistAssignment(
             GuidGenerator.Create(),
             priceListId, customerGroupId, description
             );

            return await _pricelistAssignmentRepository.InsertAsync(pricelistAssignment);
        }

        public async Task<PricelistAssignment> UpdateAsync(
            Guid id,
            Guid priceListId, Guid customerGroupId, string description, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(priceListId, nameof(priceListId));
            Check.NotNull(customerGroupId, nameof(customerGroupId));

            var queryable = await _pricelistAssignmentRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var pricelistAssignment = await AsyncExecuter.FirstOrDefaultAsync(query);

            pricelistAssignment.PriceListId = priceListId;
            pricelistAssignment.CustomerGroupId = customerGroupId;
            pricelistAssignment.Description = description;

            pricelistAssignment.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _pricelistAssignmentRepository.UpdateAsync(pricelistAssignment);
        }

    }
}