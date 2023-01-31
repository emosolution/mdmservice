using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using Volo.Abp.Threading;
using Volo.Abp.Uow;


namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public partial class SalesOrgHierarchyManager : DomainService
    {
        private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;

        public SalesOrgHierarchyManager(ISalesOrgHierarchyRepository salesOrgHierarchyRepository)
        {
            _salesOrgHierarchyRepository = salesOrgHierarchyRepository;
        }

        

        

    }
}