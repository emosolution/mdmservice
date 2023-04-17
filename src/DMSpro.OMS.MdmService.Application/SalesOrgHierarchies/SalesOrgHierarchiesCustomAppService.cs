using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.SalesOrgHeaders;
using System.Linq;
using DMSpro.OMS.MdmService.NumberingConfigDetails;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Data;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{

    [Authorize(MdmServicePermissions.SalesOrgHierarchies.Default)]
    public partial class SalesOrgHierarchiesAppService
    {
        public virtual async Task<SalesOrgHierarchyDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<SalesOrgHierarchy, SalesOrgHierarchyDto>(await _salesOrgHierarchyRepository.GetAsync(id));
        }

        [Authorize(MdmServicePermissions.SalesOrgHierarchies.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            var node = await _salesOrgHierarchyRepository.GetAsync(id);
            await CheckHeader(node.SalesOrgHeaderId);

            if (node.ParentId != null)
            {
                var parent = await _salesOrgHierarchyRepository.GetAsync(node.ParentId.Value);
                parent.DirectChildren--;
                if (parent.DirectChildren <= 0 && parent.IsSellingZone)
                {
                    parent.IsSellingZone = false;
                }
                await _salesOrgHierarchyRepository.UpdateAsync(parent);
            }
            await _salesOrgHierarchiesInternalAppService.DeleteAsync(id);
            return;
        }

        [Authorize(MdmServicePermissions.SalesOrgHierarchies.Create)]
        public virtual async Task<SalesOrgHierarchyDto> CreateRootAsync(SalesOrgHierarchyCreateRootDto input)
        {
            if (input.SalesOrgHeaderId == default)
            {
                throw new UserFriendlyException(message: L["Error:SalesOrgHierarchiesAppService:550"], code: "0");
            }
            var header = await CheckHeader(input.SalesOrgHeaderId);
            var treeNodes = await _salesOrgHierarchyRepository.GetListAsync(x => x.SalesOrgHeaderId == header.Id);
            if (treeNodes.Any())
            {
                throw new UserFriendlyException(message: L["Error:SalesOrgHierarchiesAppService:553"], code: "0");
            }
            (var numberingConfig, var companyHOId) = await GetAndCheckSuggestedNumberingConfig();

            var salesOrgHierarchy = await _salesOrgHierarchiesInternalAppService.CreateAsync(
                salesOrgHeaderId: input.SalesOrgHeaderId,
                parentId: null,
                code: numberingConfig.SuggestedCode,
                name: input.Name,
                isRoute: false,
                isSellingZone: false);

            await SaveNumberingConfig(numberingConfig, companyHOId);
            return salesOrgHierarchy;
        }

        [Authorize(MdmServicePermissions.SalesOrgHierarchies.Create)]
        public virtual async Task<SalesOrgHierarchyDto> CreateSubAsync(SalesOrgHierarchyCreateSubDto input)
        {
            if (input.ParentId == default)
            {
                throw new UserFriendlyException(message: L["Error:SalesOrgHierarchiesAppService:551"], code: "0");
            }
            var parent = await _salesOrgHierarchyRepository.GetAsync((Guid)input.ParentId);

            if (await _salesOrgHierarchyRepository.AnyAsync(x => x.SalesOrgHeaderId == parent.SalesOrgHeaderId && x.Name == input.Name))
            {
                throw new UserFriendlyException(message: L["Error:SalesOrgHierarchiesAppService:NameUniqueness"]);
            }

            if (parent.IsRoute || parent.IsSellingZone)
            {
                throw new UserFriendlyException(message: L["Error:SalesOrgHierarchiesAppService:554"], code: "1");
            }

            var header = await CheckHeader(parent.SalesOrgHeaderId);
            (var numberingConfig, var companyHOId) = await GetAndCheckSuggestedNumberingConfig();

            var salesOrgHierarchy = await _salesOrgHierarchiesInternalAppService.CreateAsync(
                salesOrgHeaderId: header.Id,
                parentId: parent.Id,
                code: numberingConfig.SuggestedCode,
                name: input.Name,
                isRoute: false,
                isSellingZone: false);
            parent.DirectChildren++;
            await _salesOrgHierarchyRepository.UpdateAsync(parent);
            await SaveNumberingConfig(numberingConfig, companyHOId);
            return salesOrgHierarchy;
        }

        [Authorize(MdmServicePermissions.SalesOrgHierarchies.Create)]
        public virtual async Task<SalesOrgHierarchyDto> CreateRouteAsync(SalesOrgHierarchyCreateRouteDto input)
        {
            if (input.ParentId == default)
            {
                throw new UserFriendlyException(message: L["Error:SalesOrgHierarchiesAppService:551"], code: "0");
            }
            var parent = await _salesOrgHierarchyRepository.GetAsync((Guid)input.ParentId);

            if (await _salesOrgHierarchyRepository.AnyAsync(x => x.SalesOrgHeaderId == parent.SalesOrgHeaderId && x.Name == input.Name))
            {
                throw new UserFriendlyException(message: L["Error:SalesOrgHierarchiesAppService:NameUniqueness"]);
            }

            if (parent.IsRoute)
            {
                throw new UserFriendlyException(message: L["Error:SalesOrgHierarchiesAppService:555"], code: "1");
            }
            if (!parent.IsSellingZone)
            {
                if (await _salesOrgHierarchyRepository.AnyAsync(x => x.ParentId == input.ParentId))
                {
                    throw new UserFriendlyException(message: L["Error:SalesOrgHierarchiesAppService:556"], code: "1");
                }
            }
            var header = await CheckHeader(parent.SalesOrgHeaderId);
            (var numberingConfig, var companyHOId) = await GetAndCheckSuggestedNumberingConfig();

            var salesOrgHierarchy = await _salesOrgHierarchiesInternalAppService.CreateAsync(
                salesOrgHeaderId: header.Id,
                parentId: parent.Id,
                code: numberingConfig.SuggestedCode,
                name: input.Name,
                isRoute: true,
                isSellingZone: false);

            parent.DirectChildren++;
            parent.IsSellingZone = true;
            await _salesOrgHierarchyRepository.UpdateAsync(parent);

            await SaveNumberingConfig(numberingConfig, companyHOId);

            return salesOrgHierarchy;
        }

        [Authorize(MdmServicePermissions.SalesOrgHierarchies.Edit)]
        public virtual async Task<SalesOrgHierarchyDto> UpdateAsync(
            Guid id, SalesOrgHierarchyUpdateDto input)
        {
            Check.NotNullOrWhiteSpace(input.Name, nameof(input.Name));
            //Check.Length(input.Name, nameof(input.Name), SalesOrgHierarchyConsts.NameMaxLength, SalesOrgHierarchyConsts.NameMaxLength);

            var record = await _salesOrgHierarchyRepository.GetAsync(x => x.Id == id);

            if (await _salesOrgHierarchyRepository.AnyAsync(x => x.SalesOrgHeaderId == record.SalesOrgHeaderId
                && x.Id != id
                && x.Name == input.Name))
            {
                throw new UserFriendlyException(message: L["Error:SalesOrgHierarchiesAppService:NameUniqueness"]);
            }

            var header = await CheckHeader(record.SalesOrgHeaderId);
            //await _salesOrgHierarchiesInternalAppService.ValidateOrganizationUnitAsync(
            //    id, input.Name, record.ParentId, record.SalesOrgHeaderId);
            //record.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
            await _salesOrgHierarchyRepository.UpdateAsync(record);
            return ObjectMapper.Map<SalesOrgHierarchy, SalesOrgHierarchyDto>(record);
        }

        private async Task<SalesOrgHeader> CheckHeader(Guid headerId)
        {
            var header = await _salesOrgHeaderRepository.GetAsync(headerId);
            if (header.Status != Status.OPEN)
            {
                throw new UserFriendlyException(message: L["Error:SalesOrgHierarchiesAppService:552"], code: "0");
            }
            return header;
        }

        private async Task<(NumberingConfigDetailDto, Guid)> GetAndCheckSuggestedNumberingConfig()
        {
            var companyHO = await _companyRepository.GetAsync(x => x.IsHO == true);
            var companyHOId = companyHO.Id;
            var numberingConfig =
                await _numberingConfigDetailsInternalAppService.GetSuggestedNumberingConfigAsync(
                    SalesOrgHierarchyConsts.NumberingConfigObjectType, companyHOId);
            await CheckCodeUniqueness(numberingConfig.SuggestedCode);

            return (numberingConfig, companyHOId);
        }

        private async Task SaveNumberingConfig(NumberingConfigDetailDto numberingConfig, Guid companyHOId)
        {
            await _numberingConfigDetailsInternalAppService.SaveNumberingConfigAsync(
                SalesOrgHierarchyConsts.NumberingConfigObjectType, companyHOId,
                numberingConfig.CurrentNumber);
        }
    }
}