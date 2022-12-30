using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
    public class MCPHeaderManager : DomainService
    {
        private readonly IMCPHeaderRepository _mCPHeaderRepository;

        public MCPHeaderManager(IMCPHeaderRepository mCPHeaderRepository)
        {
            _mCPHeaderRepository = mCPHeaderRepository;
        }

        public async Task<MCPHeader> CreateAsync(
        Guid routeId, Guid companyId, string code, string name, DateTime effectiveDate, DateTime? endDate = null)
        {
            Check.NotNull(routeId, nameof(routeId));
            Check.NotNull(companyId, nameof(companyId));
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), MCPHeaderConsts.CodeMaxLength, MCPHeaderConsts.CodeMinLength);
            Check.NotNull(effectiveDate, nameof(effectiveDate));

            var mcpHeader = new MCPHeader(
             GuidGenerator.Create(),
             routeId, companyId, code, name, effectiveDate, endDate
             );

            return await _mCPHeaderRepository.InsertAsync(mcpHeader);
        }

        public async Task<MCPHeader> UpdateAsync(
            Guid id,
            Guid routeId, Guid companyId, string code, string name, DateTime effectiveDate, DateTime? endDate = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(routeId, nameof(routeId));
            Check.NotNull(companyId, nameof(companyId));
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), MCPHeaderConsts.CodeMaxLength, MCPHeaderConsts.CodeMinLength);
            Check.NotNull(effectiveDate, nameof(effectiveDate));

            var queryable = await _mCPHeaderRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var mcpHeader = await AsyncExecuter.FirstOrDefaultAsync(query);

            mcpHeader.RouteId = routeId;
            mcpHeader.CompanyId = companyId;
            mcpHeader.Code = code;
            mcpHeader.Name = name;
            mcpHeader.EffectiveDate = effectiveDate;
            mcpHeader.EndDate = endDate;

            mcpHeader.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _mCPHeaderRepository.UpdateAsync(mcpHeader);
        }

    }
}