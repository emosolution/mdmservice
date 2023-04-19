using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.WorkingPositions
{
    public class WorkingPositionUpdateDto : IHasConcurrencyStamp
    {
        [StringLength(WorkingPositionConsts.NameMaxLength)]
        public string Name { get; set; }
        [StringLength(WorkingPositionConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public bool Active { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}