using DMSpro.OMS.MdmService.GeoMasters;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using DMSpro.OMS.MdmService.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Companies
{
    public partial class CompaniesInternalAppService : ApplicationService, ICompaniesInternalAppService //, ICompaniesGRPCService
    {
        //private readonly CompanyManager _companyManager;
        //private readonly IRepository<Company, Guid> _companyRepository;
        private readonly ICompanyCustomRepository _companyCustomRepository;

        public CompaniesInternalAppService(
            //CompanyManager companyManager,
            //IRepository<Company, Guid> companyRepository,
            ICompanyCustomRepository companyCustomRepository)
        {
            //_companyManager = companyManager;
            _companyCustomRepository = companyCustomRepository;
        }

        public async Task<CompanyDto> FindHOCompanyOfIdentityUser(Guid identityUserId, Guid tenantId)
        {
            try
            {
                Company companyHO = await _companyCustomRepository.FindHOCompanyOfIdentityUser(identityUserId, tenantId);
                return ObjectMapper.Map<Company, CompanyDto>(companyHO);
            }
            catch (BusinessException be)
            {
                return ObjectMapper.Map<Company, CompanyDto>(null);
            }
        }
    }
}