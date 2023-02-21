﻿using System;
using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.Items;
using DMSpro.OMS.MdmService.UOMs;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
namespace DMSpro.OMS.MdmService.ItemGroupLists
{
	public class ItemGroupListWithDetailsDto: FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
	{
        public int Rate { get; set; }
        public decimal Price { get; set; }
        public Guid ItemGroupId { get; set; }
        public Guid ItemId { get; set; }
        public Guid UomId { get; set; }

        public string ConcurrencyStamp { get; set; }

        public ItemGroupDto ItemGroup { get; set; }
        public ItemDto Item { get; set; }
        public UOMDto UOM { get; set; }
        public ItemGroupListWithDetailsDto()
		{
		}
	}
}
