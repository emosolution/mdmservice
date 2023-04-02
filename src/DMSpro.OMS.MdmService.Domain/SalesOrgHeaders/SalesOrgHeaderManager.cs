using DMSpro.OMS.MdmService.SalesOrgHeaders;
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
        string code, string name, bool active, Status status)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), SalesOrgHeaderConsts.CodeMaxLength, SalesOrgHeaderConsts.CodeMinLength);
            Check.Length(name, nameof(name), SalesOrgHeaderConsts.NameMaxLength);
            Check.NotNull(status, nameof(status));

            var salesOrgHeader = new SalesOrgHeader(
             GuidGenerator.Create(),
             code, name, active, status
             );

            return await _salesOrgHeaderRepository.InsertAsync(salesOrgHeader);
        }

        public async Task<SalesOrgHeader> UpdateAsync(
            Guid id,
            string code, string name, bool active, Status status, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), SalesOrgHeaderConsts.CodeMaxLength, SalesOrgHeaderConsts.CodeMinLength);
            Check.Length(name, nameof(name), SalesOrgHeaderConsts.NameMaxLength);
            Check.NotNull(status, nameof(status));

            var salesOrgHeader = await _salesOrgHeaderRepository.GetAsync(id);

            salesOrgHeader.Code = code;
            salesOrgHeader.Name = name;
            salesOrgHeader.Active = active;
            salesOrgHeader.Status = status;

            salesOrgHeader.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _salesOrgHeaderRepository.UpdateAsync(salesOrgHeader);
        }

    }
}