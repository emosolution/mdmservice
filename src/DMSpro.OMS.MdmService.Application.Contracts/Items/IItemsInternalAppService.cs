using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.Items
{
    public interface IItemsInternalAppService : IApplicationService
    {
        Task<bool> CheckCanBeUpdatedAsync(Guid id);
    }
}
