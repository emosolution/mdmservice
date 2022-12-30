using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.Streets
{
    public class StreetManager : DomainService
    {
        private readonly IStreetRepository _streetRepository;

        public StreetManager(IStreetRepository streetRepository)
        {
            _streetRepository = streetRepository;
        }

        public async Task<Street> CreateAsync(
        string name)
        {
            var street = new Street(
             GuidGenerator.Create(),
             name
             );

            return await _streetRepository.InsertAsync(street);
        }

        public async Task<Street> UpdateAsync(
            Guid id,
            string name, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _streetRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var street = await AsyncExecuter.FirstOrDefaultAsync(query);

            street.Name = name;

            street.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _streetRepository.UpdateAsync(street);
        }

    }
}