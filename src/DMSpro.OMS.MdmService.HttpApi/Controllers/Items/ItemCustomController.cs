﻿using DMSpro.OMS.MdmService.Items;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;

namespace DMSpro.OMS.MdmService.Controllers.Items
{
    public partial class ItemController
    {
        [HttpGet]
        [Route("item-profile/{id}")]
        public async Task<ItemProfileDto> GetItemProfileAsync(Guid id)
        {
            try
            {
                return await _itemsAppService.GetItemProfileAsync(id);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message, code: "1");
            }
        }
    }
}
