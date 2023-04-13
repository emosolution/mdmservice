using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public class CompanyIdentityUserAssignmentManager : DomainService
    {
        private readonly ICompanyIdentityUserAssignmentRepository _companyIdentityUserAssignmentRepository;

        public CompanyIdentityUserAssignmentManager(ICompanyIdentityUserAssignmentRepository companyIdentityUserAssignmentRepository)
        {
            _companyIdentityUserAssignmentRepository = companyIdentityUserAssignmentRepository;
        }

        public async Task<CompanyIdentityUserAssignment> CreateAsync(
        Guid companyId, Guid identityUserId)
        {
            Check.NotNull(companyId, nameof(companyId));
            Check.NotNull(identityUserId, nameof(identityUserId));

            var companyIdentityUserAssignment = new CompanyIdentityUserAssignment(
             GuidGenerator.Create(),
             companyId, identityUserId
             );

            return await _companyIdentityUserAssignmentRepository.InsertAsync(companyIdentityUserAssignment);
        }

        public async Task<CompanyIdentityUserAssignment> UpdateAsync(
            Guid id,
            Guid identityUserId, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(identityUserId, nameof(identityUserId));

            var queryable = await _companyIdentityUserAssignmentRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var companyIdentityUserAssignment = await AsyncExecuter.FirstOrDefaultAsync(query);

            companyIdentityUserAssignment.IdentityUserId = identityUserId;

            companyIdentityUserAssignment.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _companyIdentityUserAssignmentRepository.UpdateAsync(companyIdentityUserAssignment);
        }

    }
}