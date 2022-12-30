using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{
    public class SalesOrgHeaderManager : DomainService
    {
        private readonly ISalesOrgHeaderRepository _salesOrgHeaderRepository;

        public SalesOrgHeaderManager(ISalesOrgHeaderRepository salesOrgHeaderRepository)
        {
            _salesOrgHeaderRepository = salesOrgHeaderRepository;
        }

        public async Task<SalesOrgHeader> CreateAsync(
        string code, string name, bool active)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), SalesOrgHeaderConsts.CodeMaxLength, SalesOrgHeaderConsts.CodeMinLength);

            var salesOrgHeader = new SalesOrgHeader(
             GuidGenerator.Create(),
             code, name, active
             );

            return await _salesOrgHeaderRepository.InsertAsync(salesOrgHeader);
        }

        public async Task<SalesOrgHeader> UpdateAsync(
            Guid id,
            string code, string name, bool active, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), SalesOrgHeaderConsts.CodeMaxLength, SalesOrgHeaderConsts.CodeMinLength);

            var queryable = await _salesOrgHeaderRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var salesOrgHeader = await AsyncExecuter.FirstOrDefaultAsync(query);

            salesOrgHeader.Code = code;
            salesOrgHeader.Name = name;
            salesOrgHeader.Active = active;

            salesOrgHeader.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _salesOrgHeaderRepository.UpdateAsync(salesOrgHeader);
        }

    }
}