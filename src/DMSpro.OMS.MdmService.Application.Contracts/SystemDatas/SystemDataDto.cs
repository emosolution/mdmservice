using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.SystemDatas
{
    public class SystemDataDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Code { get; set; }
        public string ValueCode { get; set; }
        public string ValueName { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}