using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.UOMGroupDetails
{
    public class UOMGroupDetailExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public uint? AltQtyMin { get; set; }
        public uint? AltQtyMax { get; set; }
        public uint? BaseQtyMin { get; set; }
        public uint? BaseQtyMax { get; set; }
        public bool? Active { get; set; }
        public Guid? UOMGroupId { get; set; }
        public Guid? AltUOMId { get; set; }
        public Guid? BaseUOMId { get; set; }

        public UOMGroupDetailExcelDownloadDto()
        {

        }
    }
}