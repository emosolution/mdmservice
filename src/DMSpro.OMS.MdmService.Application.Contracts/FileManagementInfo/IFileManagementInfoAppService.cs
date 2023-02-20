using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.FileManagementInfo
{
    public interface IFileManagementInfoAppService
    {
        Guid ItemAttachmentDirectoryId { get; }
        Guid ItemImageDirectoryId { get; }
        List<string> AcceptedAttachmentContentTypes { get; }
        List<string> AcceptedImageContentTypes { get; }
    }
}
