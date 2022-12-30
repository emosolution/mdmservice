using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.ProdAttributeValues
{
    public class ProdAttributeValueExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string AttrValName { get; set; }
        public Guid? ProdAttributeId { get; set; }
        public Guid? ParentProdAttributeValueId { get; set; }

        public ProdAttributeValueExcelDownloadDto()
        {

        }
    }
}