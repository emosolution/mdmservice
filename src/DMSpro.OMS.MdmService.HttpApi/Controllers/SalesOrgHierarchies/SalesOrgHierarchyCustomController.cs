using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;

namespace DMSpro.OMS.MdmService.Controllers.SalesOrgHierarchies
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("SalesOrgHierarchy")]
    [Route("api/mdm-service/sales-org-hierarchies")]
    public partial class SalesOrgHierarchyController : AbpController, ISalesOrgHierarchiesAppService
    {
        private readonly ISalesOrgHierarchiesAppService _salesOrgHierarchiesAppService;

        public SalesOrgHierarchyController(ISalesOrgHierarchiesAppService salesOrgHierarchiesAppService)
        {
            _salesOrgHierarchiesAppService = salesOrgHierarchiesAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<SalesOrgHierarchyDto> GetAsync(Guid id)
        {
            return _salesOrgHierarchiesAppService.GetAsync(id);
        }

        [HttpPost]
        [Route("root")]
        public virtual Task<SalesOrgHierarchyDto> CreateRootAsync(SalesOrgHierarchyCreateRootDto input)
        {
            return _salesOrgHierarchiesAppService.CreateRootAsync(input);
        }

        [HttpPost]
        [Route("sub")]
        public virtual Task<SalesOrgHierarchyDto> CreateSubAsync(SalesOrgHierarchyCreateSubDto input)
        {
            return _salesOrgHierarchiesAppService.CreateSubAsync(input);
        }

        [HttpPost]
        [Route("route")]
        public virtual Task<SalesOrgHierarchyDto> CreateRouteAsync(SalesOrgHierarchyCreateRouteDto input)
        {
            return _salesOrgHierarchiesAppService.CreateRouteAsync(input);
        }

        [HttpPut]
        public virtual Task<SalesOrgHierarchyDto> UpdateAsync(Guid id, SalesOrgHierarchyUpdateDto input)
        {
            return _salesOrgHierarchiesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        public virtual Task DeleteAsync(Guid id)
        {
            return _salesOrgHierarchiesAppService.DeleteAsync(id);
        }
    }
}