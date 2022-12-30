using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.UOMs
{
    public class UOMManager : DomainService
    {
        private readonly IUOMRepository _uOMRepository;

        public UOMManager(IUOMRepository uOMRepository)
        {
            _uOMRepository = uOMRepository;
        }

        public async Task<UOM> CreateAsync(
        string code, string name)
        {
            var uOM = new UOM(
             GuidGenerator.Create(),
             code, name
             );

            return await _uOMRepository.InsertAsync(uOM);
        }

        public async Task<UOM> UpdateAsync(
            Guid id,
            string code, string name, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _uOMRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var uOM = await AsyncExecuter.FirstOrDefaultAsync(query);

            uOM.Code = code;
            uOM.Name = name;

            uOM.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _uOMRepository.UpdateAsync(uOM);
        }

    }
}