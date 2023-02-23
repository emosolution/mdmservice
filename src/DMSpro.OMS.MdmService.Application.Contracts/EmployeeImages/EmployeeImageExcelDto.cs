using System;

namespace DMSpro.OMS.MdmService.EmployeeImages
{
    public class EmployeeImageExcelDto
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool IsAvatar { get; set; }
        public Guid FileId { get; set; }
    }
}