using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.EmployeeAttachments
{
    public partial interface IEmployeeAttachmentsAppService
    {
        Task DeleteManyAsync(List<Guid> id);

        Task<IRemoteStreamContent> GetFileAsync(Guid id);

        Task<EmployeeAttachmentDto> CreateAsync(Guid employeeId,
            string description, bool active, IRemoteStreamContent inputFile);

        Task<EmployeeAttachmentDto> UpdateAsync(Guid id, Guid employeeId,
            string description, bool active, IRemoteStreamContent inputFile);
    }
}
