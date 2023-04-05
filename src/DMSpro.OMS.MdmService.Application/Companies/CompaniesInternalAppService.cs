using DMSpro.OMS.MdmService.CompanyIdentityUserAssignments;
using DMSpro.OMS.MdmService.Localization;
using DMSpro.OMS.Shared.Protos.IdentityService.IdentityUsers;
using DMSpro.OMS.Shared.Protos.Shared.Import;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace DMSpro.OMS.MdmService.Companies
{
    public class CompaniesInternalAppService : ApplicationService, ICompaniesInternalAppService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyIdentityUserAssignmentRepository _companyIdentityUserAssignmentRepository;
        private readonly ICurrentTenant _currentTenant;
        private readonly IConfiguration _settingProvider;

        public CompaniesInternalAppService(
            ICompanyRepository companyRepository,
            ICompanyIdentityUserAssignmentRepository companyIdentityUserAssignmentRepository,
            ICurrentTenant currentTenant,
            IConfiguration settingProvider)
        {
            _companyRepository = companyRepository;
            _companyIdentityUserAssignmentRepository = companyIdentityUserAssignmentRepository;
            _currentTenant = currentTenant;
            _settingProvider = settingProvider;

            LocalizationResource = typeof(MdmServiceResource);
        }

        public async Task<CompanyWithTenantDto> GetHOCompanyFromIdentityUserAsync(Guid identityUserId, Guid? tenantId)
        {
            try
            {
                Company companyHO = await _companyRepository.GetHOCompanyFromIdentityUserAsync(identityUserId, tenantId);
                return ObjectMapper.Map<Company, CompanyWithTenantDto>(companyHO);
            }
            catch (EntityNotFoundException)
            {
                return null;
            }

        }

        public async Task<CompanyWithTenantDto> CheckCompanyBelongToIdentityUserAsync(Guid companyId,
            Guid identityUserId, Guid? tenantId)
        {
            List<CompanyIdentityUserAssignmentWithNavigationProperties> assignments =
                await _companyIdentityUserAssignmentRepository.GetListWithNavigationPropertiesAsync(
                    identityUserId: identityUserId, companyId: companyId);
            if (assignments.Count != 1)
            {
                return null;
            }
            Company company = assignments[0].Company;
            if (company.TenantId != tenantId)
            {
                return null;
            }
            return ObjectMapper.Map<Company, CompanyWithTenantDto>(company);
        }

        public async Task<CompanyDto> CheckActiveAsync(Guid id, DateTime? checkingDate,
            bool throwErrorOnInactive = false)
        {
            checkingDate = checkingDate == null ? DateTime.Now : (DateTime)checkingDate;
            try
            {
                var record = await _companyRepository.GetAsync(
                    x => x.Id == id && x.Active == true &&
                    x.EffectiveDate < checkingDate &&
                    (x.EndDate == null || x.EndDate >= checkingDate));
                return ObjectMapper.Map<Company, CompanyDto>(record);
            }
            catch (EntityNotFoundException)
            {
                if (throwErrorOnInactive)
                {
                    throw new BusinessException(message: L["Error:CompaniesAppService:550"], code: "1");
                }
                return null;
            }
        }

        public virtual async Task<CompanyIdentityUserAssignmentDto> SeedHOCompanyAndAssignAdminToHO(Guid tenantId)
        {
            using (_currentTenant.Change(tenantId))
            {
                Guid hoCompanyID = await SeedHOCompany(tenantId);
                using (GrpcChannel channel =
                    GrpcChannel.ForAddress(_settingProvider["GrpcRemotes:IdentiyServiceUrl"]))
                {
                    var client =
                        new IdentityUsersProtoAppService.IdentityUsersProtoAppServiceClient(channel);
                    ListCodeAndIdRequest request = new()
                    {
                        TenantId = _currentTenant.Id.ToString(),
                    };
                    request.Codes.Add("admin");
                    var response = await client.GetCodeAndIdWithCodeAsync(request);
                    if (response.CodeAndIds == null || response.CodeAndIds.Count != 1)
                    {
                        throw new Exception(L["Error:CompanyIdentityUserAssignment:552"]);
                    }
                    CodeAndId codeAndId = response.CodeAndIds[0];
                    Guid adminId = Guid.Parse(codeAndId.Id);
                    CompanyIdentityUserAssignment assignment = new(
                        GuidGenerator.Create(), hoCompanyID, adminId);
                    await _companyIdentityUserAssignmentRepository.InsertAsync(assignment);
                    return ObjectMapper.Map<CompanyIdentityUserAssignment, CompanyIdentityUserAssignmentDto>(assignment);
                }
            }
        }

        private async Task<Guid> SeedHOCompany(Guid tenantId)
        {
            Company hoCompany = new(
            #region INPUT PARAMS
                id: GuidGenerator.Create(),
                parentId: null,
                geoLevel0Id: null,
                geoLevel1Id: null,
                geoLevel2Id: null,
                geoLevel3Id: null,
                geoLevel4Id: null,
                code: "HOCompany",
                name: "HO Company",
                street: null,
                address: null,
                phone: null,
                license: null,
                taxCode: null,
                vatName: null,
                vatAddress: null,
                erpCode: null,
                active: true,
                effectiveDate: DateTime.Now,
                isHO: true,
                latitude: null,
                longitude: null,
                contactName: null,
                contactPhone: null,
                endDate: null
            #endregion
            );
            await _companyRepository.InsertAsync(hoCompany);
            return hoCompany.Id;
        }
    }
}