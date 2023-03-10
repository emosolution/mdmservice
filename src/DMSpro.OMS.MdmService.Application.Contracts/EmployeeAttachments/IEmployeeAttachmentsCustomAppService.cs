using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.EmployeeAttachments
{
    public partial interface IEmployeeAttachmentsAppService
    {
        Task DeleteManyAsync(List<Guid> ids);

        Task<IRemoteStreamContent> GetFileAsync(Guid id);

        Task<EmployeeAttachmentDto> CreateAsync(Guid employeeId,
            IRemoteStreamContent inputFile, string description, bool active);

        Task<EmployeeAttachmentDto> UpdateAsync(Guid id, Guid employeeId,
            IRemoteStreamContent inputFile, string description, bool active);
    }
}
