﻿using System;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.SystemDatas;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
	public class NumberingConfigWithDetailsDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int PaddingZeroNumber { get; set; }
        public string Description { get; set; }
        public Guid? SystemDataId { get; set; }

        public string ConcurrencyStamp { get; set; }

        public SystemDataDto SystemData { get; set; }
        public NumberingConfigWithDetailsDto()
		{
		}
	}
}

