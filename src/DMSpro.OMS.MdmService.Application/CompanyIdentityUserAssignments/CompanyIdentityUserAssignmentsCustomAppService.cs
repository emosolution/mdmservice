using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DMSpro.OMS.Shared.Lib.Parser;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMSpro.OMS.MdmService.Companies;
using System;
using Volo.Abp;
using Grpc.Net.Client;
using DMSpro.OMS.Shared.Protos.IdentityService.IdentityUsers;
using Microsoft.AspNetCore.Authorization;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public partial class CompanyIdentityUserAssignmentsAppService
    {
        [AllowAnonymous]
        public virtual async Task<LoadResult> GetListCompanyByCurrentUserAsync(DataLoadOptionDevextreme inputDev)
        {
            if (!_currentUser.IsAuthenticated)
            {
                return new LoadResult();
            }
            var items = await _companyIdentityUserAssignmentRepository.GetQueryAbleForNavigationPropertiesAsync(CurrentUser.Id.Value);
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption, inputDev);
            LoadResult results = DataSourceLoader.Load(items, base_dataloadoption);
            results.data = ObjectMapper.Map<IEnumerable<CompanyIdentityUserAssignmentWithNavigationProperties>,
                IEnumerable<CompanyIdentityUserAssignmentWithNavigationPropertiesDto>>(
                results.data.Cast<CompanyIdentityUserAssignmentWithNavigationProperties>());
            return results;
        }

        public override async Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            await CheckPermission();
            // var items = await _companyIdentityUserAssignmentRepository.GetQueryAbleForNavigationPropertiesAsync(null);
            var assignmentWithNavigationProperties =
                await _companyIdentityUserAssignmentRepository.GetListWithNavigationPropertiesAsync();
            var assignments =
                assignmentWithNavigationProperties.Select(x => x.CompanyIdentityUserAssignment).ToList();
            var identityUserIds =
                assignments.Select(x => x.IdentityUserId.ToString()).Distinct().ToList();
            List<IdentityUser> identityUsers = await GetListIdentityUsers(identityUserIds);
            List<CompanyIdentityUserAssignmentDevExtremeDto> dtos =
                CreateCompanyIdentityUserAssignmentDevExtremeDtos(
                    assignmentWithNavigationProperties, identityUsers);
            var base_dataloadoption = new DataSourceLoadOptionsBase();
            DataLoadParser.Parse(base_dataloadoption, inputDev);
            LoadResult results = DataSourceLoader.Load(dtos, base_dataloadoption);
            if (inputDev.Group == null)
            {
                results.data = dtos;
            }
            return results;
        }

        [AllowAnonymous]
        public virtual async Task<CompanyDto> SetCurrentlySelectedCompanyAsync(Guid companyId)
        {
            if (!_currentUser.IsAuthenticated)
            {
                return null;
            }
            var selectedCompany = await _companiesInternalAppService.CheckActiveAsync(companyId, null, true);
            var assignments = await _companyIdentityUserAssignmentRepository.GetListAsync(x =>
                x.IdentityUserId == _currentUser.Id);
            var companies = assignments.Distinct().Select(x => x.CompanyId).ToList();
            if (!companies.Contains(companyId))
            {
                throw new BusinessException(message: L["Error:CompanyIdentityUserAssignment:550"], code: "1");
            }
            foreach (var assignment in assignments)
            {
                assignment.CurrentlySelected = false;
                if (assignment.CompanyId == companyId)
                {
                    assignment.CurrentlySelected = true;
                }
            }
            await _companyIdentityUserAssignmentRepository.UpdateManyAsync(assignments);
            return selectedCompany;
        }

        [AllowAnonymous]
        public virtual async Task<CompanyDto> GetCurrentlySelectedCompanyAsync(
            Guid? inputIdentityUserId = null, DateTime? checkTime = null)
        {
            if (!_currentUser.IsAuthenticated)
            {
                return null;
            }
            return
                await _companyIdentityUserAssignmentsInternalAppService
                    .GetCurrentlySelectedCompanyAsync(inputIdentityUserId, checkTime);
        }

        private async Task<List<IdentityUser>>
            GetListIdentityUsers(List<string> identityUserIds)
        {
            using (GrpcChannel channel =
                GrpcChannel.ForAddress(_settingProvider["GrpcRemotes:IdentiyServiceUrl"]))
            {
                var client =
                    new IdentityUsersProtoAppService.IdentityUsersProtoAppServiceClient(channel);
                GetListIdentityUsersRequest request = new()
                {
                    TenantId = _currentTenant.Id.ToString(),
                };
                request.IdentityUserIds.Add(identityUserIds);
                var response = await client.GetListIdentityUsersAsync(request);
                if ((response.IdentityUsers == null && identityUserIds.Count != 0) ||
                    response.IdentityUsers.Count != identityUserIds.Count)
                {
                    throw new Exception(L["Error:CompanyIdentityUserAssignment:552"]);
                }
                List<IdentityUser> identityUsers = response.IdentityUsers.ToList();
                return identityUsers;
            }
        }

        private List<CompanyIdentityUserAssignmentDevExtremeDto>
            CreateCompanyIdentityUserAssignmentDevExtremeDtos(
            List<CompanyIdentityUserAssignmentWithNavigationProperties>
                assignmentWithNavigationProperties,
            List<IdentityUser> identityUsers)
        {
            var assignmentsWithIdentity = from x in assignmentWithNavigationProperties
                                          join y in identityUsers
                                          on x.CompanyIdentityUserAssignment.IdentityUserId.ToString() equals y.Id
                                          select new
                                          {
                                              x.CompanyIdentityUserAssignment,
                                              x.Company,
                                              y
                                          };
            List<CompanyIdentityUserAssignmentDevExtremeDto> dtos = new();
            foreach (var assignmentWithIdentity in assignmentsWithIdentity)
            {
                CompanyIdentityUserAssignmentDevExtremeDto dto = new()
                {
                    CompanyIdentityUserAssignment =
                        ObjectMapper.Map<CompanyIdentityUserAssignment, CompanyIdentityUserAssignmentDto>
                        (assignmentWithIdentity.CompanyIdentityUserAssignment),
                    Company = ObjectMapper.Map<Company, CompanyDto>
                        (assignmentWithIdentity.Company),
                    IdentityUser = new()
                    {
                        Id = Guid.Parse(assignmentWithIdentity.y.Id),
                        TenantId = _currentTenant.Id,
                        UserName = assignmentWithIdentity.y.UserName,
                        Name = assignmentWithIdentity.y.Name == "" ? null : assignmentWithIdentity.y.Name,
                        Email = assignmentWithIdentity.y.Email,
                        EmailConfirmed = assignmentWithIdentity.y.EmailConfirmed,
                        PhoneNumber = assignmentWithIdentity.y.PhoneNumber == "" ? null : assignmentWithIdentity.y.PhoneNumber,
                        PhoneNumberConfirmed = assignmentWithIdentity.y.PhoneNumberConfirmed,
                        IsActive = assignmentWithIdentity.y.IsActive,
                    },
                };
                dtos.Add(dto);
            }
            if (dtos.Count != assignmentWithNavigationProperties.Count)
            {
                throw new UserFriendlyException(L["Error:CompanyIdentityUserAssignment:552"]);
            }
            return dtos;
        }
    }
}
