using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.Controllers.ItemAttachments
{
    public partial class ItemAttachmentController
    {
        [HttpDelete]
        [Route("delete-many")]
        public virtual Task DeleteManyAsync(List<Guid> ids)
        {
            return _itemAttachmentsAppService.DeleteManyAsync(ids);
        }
    }
}
