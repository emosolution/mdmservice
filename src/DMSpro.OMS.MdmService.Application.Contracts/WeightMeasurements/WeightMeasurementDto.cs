using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.WeightMeasurements
{
    public class WeightMeasurementDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public uint Value { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}