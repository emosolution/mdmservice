using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DMSpro.OMS.MdmService.GeoMasters
{
    public partial interface IGeoMasterRepository
    {
        Task<Guid?> GetIdByCodeAsync(string code);
    }
}