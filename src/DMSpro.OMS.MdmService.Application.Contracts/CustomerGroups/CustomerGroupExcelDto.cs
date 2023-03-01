using System;

namespace DMSpro.OMS.MdmService.CustomerGroups
{
    public class CustomerGroupExcelDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public Type GroupBy { get; set; }
        public Status Status { get; set; }
    }
}