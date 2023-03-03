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
        Guid routeId, Guid companyId, Guid? itemGroupId, string code, string name, DateTime effectiveDate, DateTime? endDate = null)
        {
            Check.NotNull(routeId, nameof(routeId));
            Check.NotNull(companyId, nameof(companyId));
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), MCPHeaderConsts.CodeMaxLength, MCPHeaderConsts.CodeMinLength);
            Check.Length(name, nameof(name), MCPHeaderConsts.NameMaxLength);
            Check.NotNull(effectiveDate, nameof(effectiveDate));

            var mCPHeader = new MCPHeader(
             GuidGenerator.Create(),
             routeId, companyId, itemGroupId, code, name, effectiveDate, endDate
             );

            return await _mCPHeaderRepository.InsertAsync(mCPHeader);
        }

        public async Task<MCPHeader> UpdateAsync(
            Guid id,
            Guid routeId, Guid companyId, Guid? itemGroupId, string code, string name, DateTime effectiveDate, DateTime? endDate = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(routeId, nameof(routeId));
            Check.NotNull(companyId, nameof(companyId));
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), MCPHeaderConsts.CodeMaxLength, MCPHeaderConsts.CodeMinLength);
            Check.Length(name, nameof(name), MCPHeaderConsts.NameMaxLength);
            Check.NotNull(effectiveDate, nameof(effectiveDate));

            var mCPHeader = await _mCPHeaderRepository.GetAsync(id);

            mCPHeader.RouteId = routeId;
            mCPHeader.CompanyId = companyId;
            mCPHeader.ItemGroupId = itemGroupId;
            mCPHeader.Code = code;
            mCPHeader.Name = name;
            mCPHeader.EffectiveDate = effectiveDate;
            mCPHeader.EndDate = endDate;

            mCPHeader.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _mCPHeaderRepository.UpdateAsync(mCPHeader);
        }

    }
}