using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.MCPDetails
{
    public class MCPDetailManager : DomainService
    {
        private readonly IMCPDetailRepository _mCPDetailRepository;

        public MCPDetailManager(IMCPDetailRepository mCPDetailRepository)
        {
            _mCPDetailRepository = mCPDetailRepository;
        }

        public async Task<MCPDetail> CreateAsync(
        Guid customerId, Guid mCPHeaderId, string code, DateTime effectiveDate, int distance, int visitOrder, bool monday, bool tuesday, bool wednesday, bool thursday, bool friday, bool saturday, bool sunday, bool week1, bool week2, bool week3, bool week4, DateTime? endDate = null)
        {
            Check.NotNull(customerId, nameof(customerId));
            Check.NotNull(mCPHeaderId, nameof(mCPHeaderId));
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), MCPDetailConsts.CodeMaxLength, MCPDetailConsts.CodeMinLength);
            Check.NotNull(effectiveDate, nameof(effectiveDate));

            var mcpDetail = new MCPDetail(
             GuidGenerator.Create(),
             customerId, mCPHeaderId, code, effectiveDate, distance, visitOrder, monday, tuesday, wednesday, thursday, friday, saturday, sunday, week1, week2, week3, week4, endDate
             );

            return await _mCPDetailRepository.InsertAsync(mcpDetail);
        }

        public async Task<MCPDetail> UpdateAsync(
            Guid id,
            Guid customerId, Guid mCPHeaderId, string code, DateTime effectiveDate, int distance, int visitOrder, bool monday, bool tuesday, bool wednesday, bool thursday, bool friday, bool saturday, bool sunday, bool week1, bool week2, bool week3, bool week4, DateTime? endDate = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(customerId, nameof(customerId));
            Check.NotNull(mCPHeaderId, nameof(mCPHeaderId));
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.Length(code, nameof(code), MCPDetailConsts.CodeMaxLength, MCPDetailConsts.CodeMinLength);
            Check.NotNull(effectiveDate, nameof(effectiveDate));

            var queryable = await _mCPDetailRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var mcpDetail = await AsyncExecuter.FirstOrDefaultAsync(query);

            mcpDetail.CustomerId = customerId;
            mcpDetail.MCPHeaderId = mCPHeaderId;
            mcpDetail.Code = code;
            mcpDetail.EffectiveDate = effectiveDate;
            mcpDetail.Distance = distance;
            mcpDetail.VisitOrder = visitOrder;
            mcpDetail.Monday = monday;
            mcpDetail.Tuesday = tuesday;
            mcpDetail.Wednesday = wednesday;
            mcpDetail.Thursday = thursday;
            mcpDetail.Friday = friday;
            mcpDetail.Saturday = saturday;
            mcpDetail.Sunday = sunday;
            mcpDetail.Week1 = week1;
            mcpDetail.Week2 = week2;
            mcpDetail.Week3 = week3;
            mcpDetail.Week4 = week4;
            mcpDetail.EndDate = endDate;

            mcpDetail.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _mCPDetailRepository.UpdateAsync(mcpDetail);
        }

    }
}