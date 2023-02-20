using System;

namespace DMSpro.OMS.MdmService.FileManagementInfo
{
    public interface IFileManagementInfoAppService
    {
        Guid ItemAttachmentDirectoryId { get; }
        Guid ItemImageDirectoryId { get; }
    }
}
