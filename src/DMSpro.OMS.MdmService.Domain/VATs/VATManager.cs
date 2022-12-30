using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.VATs
{
    public class VATManager : DomainService
    {
        private readonly IVATRepository _vATRepository;

        public VATManager(IVATRepository vATRepository)
        {
            _vATRepository = vATRepository;
        }

        public async Task<VAT> CreateAsync(
        string code, string name, uint rate)
        {
            var vAT = new VAT(
             GuidGenerator.Create(),
             code, name, rate
             );

            return await _vATRepository.InsertAsync(vAT);
        }

        public async Task<VAT> UpdateAsync(
            Guid id,
            string code, string name, uint rate, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _vATRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var vAT = await AsyncExecuter.FirstOrDefaultAsync(query);

            vAT.Code = code;
            vAT.Name = name;
            vAT.Rate = rate;

            vAT.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _vATRepository.UpdateAsync(vAT);
        }

    }
}