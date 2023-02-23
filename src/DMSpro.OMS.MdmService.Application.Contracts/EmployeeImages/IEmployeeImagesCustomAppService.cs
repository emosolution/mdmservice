using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.EmployeeImages
{
    public partial interface IEmployeeImagesAppService
    {
        Task DeleteManyAsync(List<Guid> id);

        Task<IRemoteStreamContent> GetFile(Guid id);
    }
}
