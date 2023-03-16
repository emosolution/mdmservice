using DMSpro.OMS.MdmService.ItemGroups;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;

namespace DMSpro.OMS.MdmService.Items
{
    public partial class ItemsAppService
    {
        public async Task<string> GetSOInfoAsync(Guid companyId,
            DateTime postingDate, DateTime? lastUpdateDate)
        {
            await CheckCompany(companyId, postingDate);
            List<Guid> zoneIds = await GetAllSellingZoneIds(companyId, postingDate);
            DateTime now = DateTime.Now;
            string nowString = $"\"{now:yyyy/MM/dd HH:mm:ss}\"";

            string soInfo = await GetSOInfoStringAsync(zoneIds, postingDate, lastUpdateDate, nowString);
            return $"{{{soInfo}}}";
        }

        private async Task CheckCompany(Guid companyId, DateTime postingDate)
        {
            var companyDto =
                await _companyIdentityUserAssignmentsAppService.GetCurrentlySelectedCompanyAsync();
            if (companyDto.Id != companyId)
            {
                throw new BusinessException(message: L["Error:ItemsAppService:550"], code: "1");
            }
            await _companyRepository.CheckActiveWithDateAsync(companyId, postingDate);
        }

        private async Task<List<Guid>> GetAllSellingZoneIds(Guid companyId, DateTime postingDate)
        {
            var companiesInZone = await _companyInZoneRepository.GetListAsync(x => x.CompanyId == companyId &&
                // x.IsBase == true && 
                x.EffectiveDate < postingDate && (x.EndDate == null || x.EndDate > postingDate));
            var zoneIds = companiesInZone.Select(x => x.SalesOrgHierarchyId).Distinct().ToList();
            var zones = await _salesOrgHierarchyRepository.GetListAsync(x => zoneIds.Contains(x.Id) &&
                x.Active == true && x.IsSellingZone == true);
            if (zones.Count < 1)
            {
                throw new BusinessException(message: L["Error:ItemsAppService:551"], code: "1");
            }
            var result = zones.Select(x => x.Id).Distinct().ToList();
            return result;
        }

        private async Task<(
            Dictionary<string, RouteSOPODto>,
            List<Guid>,
            Dictionary<string, string>
            )> GetRouteFromZoneIds(List<Guid> zoneIds)
        {
            var routes = (await _salesOrgHierarchyRepository.GetListAsync(x => x.IsRoute == true &&
                x.ParentId != null && zoneIds.Contains((Guid)x.ParentId) &&
                x.Active == true)).Distinct();
            var routeIds = routes.Select(x => x.Id).ToList();
            if (routeIds.Count < 1)
            {
                var detailDict = new Dictionary<string, string> { ["objectType"] = "route" };
                string detailString = JsonSerializer.Serialize(detailDict).ToString();
                throw new BusinessException(message: L["Error:ItemsAppService:553"], code: "0",
                    details: detailString);
            }
            var routeDictionary = routes.ToDictionary(x => x.Id.ToString(), x => new RouteSOPODto()
            {
                id = x.Id.ToString(),
                code = x.Code,
                name = x.Name,
            });
            var routeInZoneDictionary = routes.ToDictionary(x => x.Id.ToString(),
                x => ((Guid)x.ParentId).ToString());
            return (routeDictionary, routeIds, routeInZoneDictionary);
        }

        private async Task<(
            Dictionary<string, Dictionary<string, List<string>>>,
            Dictionary<string, CustomerSOPODto>,
            Dictionary<string, List<string>>
            )> GetCustomerRouteItemGroupDictionary(List<Guid> routeIds,
                DateTime postingDate)
        {
            var mcpHeaders = (await _mcpHeaderRepository.GetListAsync(
                x => routeIds.Contains(x.RouteId) &&
                x.EffectiveDate < postingDate &&
                (x.EndDate == null || x.EndDate >= postingDate))).ToList();
            var mcpHeaderIds = mcpHeaders.Select(x => x.Id)
                .Distinct().ToList();
            var mcpDetails = (await _mcpDetailRepository.GetListAsync(x =>
                x.EffectiveDate < postingDate &&
                (x.EndDate == null || x.EndDate >= postingDate) &&
                mcpHeaderIds.Contains(x.MCPHeaderId))).Distinct().ToList();
            var customerIds = mcpDetails.Select(x => x.CustomerId).Distinct().ToList();
            var validCustomers = await _customerRepository.GetListAsync(
               x => customerIds.Contains(x.Id) &&
               // x.EffectiveDate < postingDate &&
               // (x.EndDate == null || x.EndDate >= postingDate) &&
               x.Active == true);
            if (validCustomers.Count < 1)
            {
                var detailDict = new Dictionary<string, string> { ["objectType"] = "customer" };
                string detailString = JsonSerializer.Serialize(detailDict).ToString();
                throw new BusinessException(message: L["Error:ItemsAppService:553"], code: "0",
                    details: detailString);
            }
            var validCustomerIdStrings = validCustomers.Select(
                x => x.Id.ToString()).Distinct().ToList();
            var customerDictionary = validCustomers.ToDictionary(x => x.Id.ToString(),
                x => new CustomerSOPODto()
                {
                    id = x.Id.ToString(),
                    code = x.Code,
                    name = x.Name,
                    priceListId = x.PriceListId.ToString(),

                });
            var customersRoutesItemGroupsData = from mcpHeader in mcpHeaders
                                                join mcpDetail in mcpDetails on mcpHeader.Id equals mcpDetail.MCPHeaderId
                                                select new CustomerRouteItemGroup()
                                                {
                                                    CustomerIdString = mcpDetail.CustomerId.ToString(),
                                                    RouteIdString = mcpHeader.RouteId.ToString(),
                                                    ItemGroupId = Guid.Empty,
                                                };
            var customersRoutesDictionary = customersRoutesItemGroupsData.GroupBy(x => x.CustomerIdString)
                .ToDictionary(x => x.Key, x => x.Select(x => x.RouteIdString).ToList());
            Dictionary<string, Dictionary<string, List<string>>> result = new();
            foreach (var data in customersRoutesItemGroupsData)
            {
                var customerIdString = data.CustomerIdString;
                if (!validCustomerIdStrings.Contains(customerIdString))
                {
                    continue;
                }
                var routeIdString = data.RouteIdString;
                var itemGroupId = data.ItemGroupId;
                Dictionary<string, List<string>> itemGroupInRoute;
                if (result.TryGetValue(customerIdString, out var dictionary))
                {
                    itemGroupInRoute = dictionary;
                    List<string> routeIdStrings;
                    if (itemGroupInRoute.TryGetValue(routeIdString, out var list))
                    {
                        routeIdStrings = list;
                        if (!routeIdStrings.Contains(routeIdString))
                        {
                            routeIdStrings.Add(routeIdString);
                        }
                    }
                    else
                    {
                        routeIdStrings = new() { routeIdString };
                    }
                    itemGroupInRoute[routeIdString] = routeIdStrings;
                }
                else
                {
                    itemGroupInRoute = new()
                    {
                        [routeIdString] = new List<string> { itemGroupId.ToString() }
                    };
                }
                result[customerIdString] = itemGroupInRoute;
            }
            return (result, customerDictionary, customersRoutesDictionary);
        }

        private async Task<(
            Dictionary<string, List<string>>,
            List<Guid>
            )> GetAllItemsInNullItemGroup(bool isForSO)
        {
            string nullItemGroupIdString = Guid.Empty.ToString();
            var allItems = (await _itemRepository.GetListAsync(
                x => x.Active)).Distinct().ToList();
            if (isForSO)
            {
                allItems = allItems.Where(x => x.IsSaleable == true).ToList();
            }
            else
            {
                allItems = allItems.Where(x => x.IsPurchasable == true).ToList();
            }
            if (allItems.Count < 1)
            {
                var detailDict = new Dictionary<string, string> { ["objectType"] = "item" };
                string detailString = JsonSerializer.Serialize(detailDict).ToString();
                throw new BusinessException(message: L["Error:ItemsAppService:553"], code: "0",
                    details: detailString);
            }
            var allItemIdStrings = allItems.Select(x => x.Id.ToString()).ToList();
            Dictionary<string, List<string>> itemsInItemGroupsDictionary = new()
            {
                [nullItemGroupIdString] = allItemIdStrings,
            };
            var itemIds = allItems.Select(x => x.Id).ToList();
            return (itemsInItemGroupsDictionary, itemIds);
        }

        private async Task<(
            Dictionary<string, ItemSOPODto>,
            Dictionary<string, List<string>>,
            List<string>,
            List<Guid>
            )> GetItemDetailsFromItemIds(List<Guid> itemIds, bool isForSO)
        {
            Dictionary<string, ItemSOPODto> itemDictionary = new();
            Dictionary<string, List<string>> uomGroupDictionary = new();
            List<string> allAltUomIds = new();
            List<Guid> vatIds = new();

            var items = (await _itemRepository.GetListAsync(x => x.Active == true &&
                itemIds.Contains(x.Id))).Distinct().ToList();
            if (isForSO)
            {
                items = items.Where(x => x.IsSaleable == true).ToList();
            }
            else
            {
                items = items.Where(x => x.IsPurchasable == true).ToList();
            }
            foreach (var item in items)
            {
                string itemId = item.Id.ToString();
                if (!vatIds.Contains(item.VatId))
                {
                    vatIds.Add(item.VatId);
                }
                string uomGroupId = item.UomGroupId.ToString();
                if (!uomGroupDictionary.ContainsKey(uomGroupId))
                {
                    var altUomIds = await GetAltUOMs(item.UomGroupId);
                    allAltUomIds.AddRange(altUomIds);
                    uomGroupDictionary.Add(uomGroupId, altUomIds);
                }
                if (itemDictionary.ContainsKey(itemId))
                {
                    continue;
                }
                ItemSOPODto dto = new()
                {
                    #region INPUT PARAMS
                    id = itemId,
                    code = item.Code,
                    name = item.Name,
                    basePrice = item.BasePrice,
                    vatId = item.VatId.ToString(),
                    uomGroupId = item.UomGroupId.ToString(),
                    invUomId = item.InventoryUOMId.ToString(),
                    purUomId = item.PurUOMId.ToString(),
                    purRate = item.PurUnitRate,
                    salesUomId = item.SalesUOMId.ToString(),
                    salesRate = item.SalesUnitRate,
                    isPurchasable = item.IsPurchasable,
                    isSalesable = item.IsSaleable,
                    #endregion
                };
                itemDictionary.Add(itemId, dto);
            }
            return (itemDictionary, uomGroupDictionary,
                allAltUomIds, vatIds);
        }

        private async Task<Dictionary<string, VATSOPODto>> GetVatDictionary(List<Guid> vatIds)
        {
            var vats = await _vATRepository.GetListAsync(x => vatIds.Contains(x.Id));
            Dictionary<string, VATSOPODto> result = new();
            foreach (var vat in vats)
            {
                string id = vat.Id.ToString();
                if (result.ContainsKey(id))
                {
                    continue;
                }
                VATSOPODto dto = new()
                {
                    id = id,
                    code = vat.Code,
                    name = vat.Name,
                    rate = vat.Rate,
                };
                result.Add(id, dto);
            }
            return result;
        }

        private async Task<List<string>> GetAltUOMs(Guid uomGroupId)
        {
            var uomGroupDetails = await _uOMGroupDetailRepository.GetListAsync(
                x => x.UOMGroupId == uomGroupId && x.Active == true);
            var result = uomGroupDetails.Select(x => x.AltUOMId.ToString()).Distinct().ToList();
            return result;
        }

        private async Task<Dictionary<string, UOMSOPODto>> GetUOMDictionary(List<string> allAltUomIds)
        {
            var uoms = await _uOMRepository.GetListAsync(x => allAltUomIds.Contains(x.Id.ToString()));
            Dictionary<string, UOMSOPODto> result = new();
            foreach (var uom in uoms)
            {
                string id = uom.Id.ToString();
                if (result.ContainsKey(id))
                {
                    continue;
                }
                UOMSOPODto dto = new()
                {
                    id = id,
                    code = uom.Code,
                    name = uom.Name,
                };
                result.Add(id, dto);
            }
            return result;
        }

        private async Task<Dictionary<string, decimal>>
            GetPriceDictionary(
                Dictionary<string, CustomerSOPODto> customerDictionary,
                Dictionary<string, ItemSOPODto> itemDictionary,
                Dictionary<string, UOMSOPODto> uomDictionary)
        {
            var itemIds = itemDictionary.Keys.ToList();
            var uomIds = uomDictionary.Keys.ToList();
            var customers = customerDictionary.Values.ToList();
            List<string> priceListIds =
                customers.Select(x => x.priceListId).Distinct().ToList();

            List<Guid> activePriceListIds = (await _priceListRepository.GetListAsync(
                x => priceListIds.Contains(x.Id.ToString()) &&
                x.Active == true)).Select(x => x.Id).Distinct().ToList();
            var priceListDetails = (await _priceListDetailRepository.GetListAsync(
                x => activePriceListIds.Contains(x.PriceListId) &&
                uomIds.Contains(x.UOMId.ToString()) &&
                itemIds.Contains(x.ItemId.ToString()))).Distinct().ToList();
            var result = priceListDetails.ToDictionary(
                x => $"{x.PriceListId}|{x.ItemId}|{x.UOMId}",
                x => x.Price);
            return result;
        }

        private async Task<(
            Dictionary<string, List<string>>,
            Dictionary<string, List<string>>,
            Dictionary<string, EmployeeSOPODto>
            )> GetEmployeesDictionaries(List<Guid> routeIds, DateTime postingDate)
        {
            var assignments = await _salesOrgEmpAssignmentRepository.GetListAsync(
                x => routeIds.Contains(x.SalesOrgHierarchyId) &&
                // x.IsBase == true && 
                x.EffectiveDate <= postingDate &&
                (x.EndDate == null || x.EndDate > postingDate));
            var employeeIds =
                assignments.Select(x => x.EmployeeProfileId).Distinct().ToList();
            var validEmployees = (await _employeeProfileRepository.GetListAsync(
                    x => employeeIds.Contains(x.Id) && x.Active == true
                    // && (x.EffectiveDate == null && x.EffectiveDate < postingDate)
                    // && (x.EndDate == null || x.EndDate > postingDate))
                    )).Distinct().ToList();
            var employeeDictionary = validEmployees.ToDictionary(
                x => x.Id.ToString(),
                x => new EmployeeSOPODto()
                {
                    id = x.Id.ToString(),
                    code = x.Code,
                    firstName = x.FirstName,
                    lastName = x.LastName,
                    email = x.Email,
                });
            var validEmployeeIds = validEmployees.Select(x => x.Id).ToList();
            if (validEmployeeIds.Count < 1)
            {
                var detailDict = new Dictionary<string, string> { ["objectType"] = "employee" };
                string detailString = JsonSerializer.Serialize(detailDict).ToString();
                throw new BusinessException(message: L["Error:ItemsAppService:553"], code: "0",
                    details: detailString);
            }
            var validAssignments =
                assignments.Where(x => validEmployeeIds.Contains(x.EmployeeProfileId));
            var employeesInRoutesDictionary = validAssignments.GroupBy(x => x.EmployeeProfileId.ToString())
                .ToDictionary(x => x.Key, x => x.Select(x => x.SalesOrgHierarchyId.ToString()).ToList());
            var routesWithEmployeesDictionary = validAssignments.GroupBy(x => x.SalesOrgHierarchyId.ToString())
                .ToDictionary(x => x.Key, x => x.Select(x => x.EmployeeProfileId.ToString()).ToList());
            return (employeesInRoutesDictionary, routesWithEmployeesDictionary,
                employeeDictionary);
        }

        private async Task<string> GetSOInfoStringAsync(List<Guid> zoneIds, DateTime postingDate,
               DateTime? lastUpdateDate, string nowString)
        {
            bool isForSO = true;
            (Dictionary<string, RouteSOPODto> routeDictionary,
            List<Guid> routeIds,
            Dictionary<string, string> routeInZoneDictionary) =
                await GetRouteFromZoneIds(zoneIds);

            (Dictionary<string, Dictionary<string, List<string>>> customersRoutesItemGroupsDictionary,
            Dictionary<string, CustomerSOPODto> customerDictionary,
            Dictionary<string, List<string>> customersRoutesDictionary) =
                await GetCustomerRouteItemGroupDictionary(routeIds, postingDate);

            (Dictionary<string, List<string>> itemsInItemGroupsDictionary,
            List<Guid> itemIds) = await GetAllItemsInNullItemGroup(isForSO);

            (Dictionary<string, ItemSOPODto> itemDictionary,
            Dictionary<string, List<string>> uomGroupDictionary,
            List<string> allAltUomIds,
            List<Guid> vatIds) =
                await GetItemDetailsFromItemIds(itemIds, isForSO);

            Dictionary<string, UOMSOPODto> uomDictionary =
                await GetUOMDictionary(allAltUomIds);

            Dictionary<string, VATSOPODto> vatDictionary =
                await GetVatDictionary(vatIds);

            Dictionary<string, decimal> priceDictionary =
                await GetPriceDictionary(customerDictionary,
                    itemDictionary, uomDictionary);

            (Dictionary<string, List<string>> employeesInRoutesDictionary,
            Dictionary<string, List<string>> routesWithEmployeesDictionary,
            Dictionary<string, EmployeeSOPODto> employeeDictionary) =
                await GetEmployeesDictionaries(routeIds, postingDate);

            List<string> resultParts = new()
            {
                $"\"customersRoutesDictionary\": {_jsonSerializer.Serialize(customersRoutesDictionary)}",
                $"\"routeDictionary\": {_jsonSerializer.Serialize(routeDictionary)}",
                $"\"customersRoutesItemGroupsDictionary\": {_jsonSerializer.Serialize(customersRoutesItemGroupsDictionary)}",
                $"\"customerDictionary\": {_jsonSerializer.Serialize(customerDictionary)}",
                $"\"itemsInItemGroupsDictionary\": {_jsonSerializer.Serialize(itemsInItemGroupsDictionary)}",
                $"\"item\": {_jsonSerializer.Serialize(itemDictionary)}",
                $"\"uomGroup\": {_jsonSerializer.Serialize(uomGroupDictionary)}",
                $"\"uom\": {_jsonSerializer.Serialize(uomDictionary)}",
                $"\"vat\": {_jsonSerializer.Serialize(vatDictionary)}",
                $"\"priceDictionary\": {_jsonSerializer.Serialize(priceDictionary)}",
                $"\"employeeDictionary\": {_jsonSerializer.Serialize(employeeDictionary)}",
                $"\"employeesInRoutesDictionary\": {_jsonSerializer.Serialize(employeesInRoutesDictionary)}",
                $"\"routesWithEmployeesDictionary\": {_jsonSerializer.Serialize(routesWithEmployeesDictionary)}",
                $"\"lastUpdated\": {nowString}",
                $"\"updateRequired\": true",
            };
            return ($"\"soInfo\":{{{resultParts.JoinAsString(",")}}}");
        }

        #region Commented code
        //private async Task<(
        //    Dictionary<string, List<string>>,
        //    List<Guid>
        //    )> GetItemsInZones(
        //        Dictionary<string, List<string>> itemGroupsInZones)
        //{
        //    bool allItems = false;
        //    List<Guid> itemIds = new();
        //    Dictionary<string, List<string>> itemsInZones = new();
        //    foreach (string zoneIdString in itemGroupsInZones.Keys)
        //    {
        //        List<string> itemGroups = itemGroupsInZones[zoneIdString];
        //        if (itemGroups == null)
        //        {
        //            itemsInZones.Add(zoneIdString, null);
        //            allItems = true;
        //            continue;
        //        }
        //        else if (itemGroups.Count < 1)
        //        {
        //            itemsInZones.Add(zoneIdString, new List<string>());
        //            continue;
        //        }
        //        var itemGroupList = (await _itemGroupRepository.GetListAsync(
        //            x => itemGroups.Contains(x.Id.ToString()))).Distinct().ToList();
        //        var itemInGroupIds = await GetAllItemIdsFromItemGroups(itemGroupList);
        //        var itemInGroupIdString =
        //            itemInGroupIds.Select(x => x.ToString()).Distinct().ToList();
        //        itemsInZones.Add(zoneIdString, itemInGroupIdString);
        //        if (!allItems)
        //        {
        //            var newItemIds = itemInGroupIds.Where(x => !itemIds.Contains(x));
        //            itemIds.AddRange(newItemIds);
        //        }
        //    }
        //    if (allItems)
        //    {
        //        itemIds = (await _itemRepository.GetListAsync(x => x.Active == true))
        //            .Select(x => x.Id).Distinct().ToList(); 
        //    }
        //    return (itemsInZones, itemIds);
        //}

        //private async Task<Dictionary<string, List<string>>>
        //    GetItemsInRoutes(
        //        Dictionary<string, List<string>> itemGroupsInRoutes,
        //        Dictionary<string, List<string>> itemsInZones,
        //        Dictionary<string, string> routeInZoneDictionary)
        //{
        //    Dictionary<string, List<string>> itemsInRoutes = new();
        //    foreach (string routeIdString in itemGroupsInRoutes.Keys)
        //    {
        //        List<string> itemGroupsInRoute = itemGroupsInRoutes[routeIdString];
        //        string zoneIdString = routeInZoneDictionary[routeIdString];
        //        List<string> itemsInZone = itemsInZones[zoneIdString];
        //        if (itemGroupsInRoute == null)
        //        {
        //            itemsInRoutes.Add(routeIdString, itemsInZone);
        //            continue;
        //        }
        //        else if (itemGroupsInRoute.Count < 1)
        //        {
        //            itemsInRoutes.Add(routeIdString, new List<string>());
        //            continue;
        //        }
        //        if (itemsInZone != null && itemsInZone.Count < 1)
        //        {
        //            itemsInRoutes.Add(routeIdString, new List<string>());
        //            continue;
        //        }
        //        var itemGroupList = (await _itemGroupRepository.GetListAsync(
        //            x => itemGroupsInRoute.Contains(x.Id.ToString()))).Distinct().ToList();
        //        var itemInGroupIds = await GetAllItemIdsFromItemGroups(itemGroupList);
        //        var itemInGroupIdString =
        //            itemInGroupIds.Select(x => x.ToString()).Distinct().ToList();
        //        if (itemsInZone == null)
        //        {
        //            itemsInRoutes.Add(routeIdString, itemInGroupIdString);
        //            continue;
        //        }
        //        var itemsInBothRouteAndZone = itemInGroupIdString.Where(
        //            x => itemsInZone.Contains(x)).ToList();
        //        itemsInRoutes.Add(routeIdString, itemsInBothRouteAndZone);
        //    }
        //    return itemsInRoutes;
        //}




        /*
        private static (Dictionary<string, Dictionary<string, List<string>>>,
            List<Guid>) ProcessCustomerRouteItemgroupData(
                IEnumerable<CustomerRouteItemGroup> customersRoutesItemGroupsData,
                Dictionary<string, List<string>> itemGroupInZones,
                Dictionary<string, List<string>> itemGroupInRoutes)
        {
            Dictionary<string, Dictionary<string, List<string>>> result = new();
            List<Guid> itemGroupIds = new();
            foreach (var data in customersRoutesItemGroupsData)
            {
                string customerIdString = data.CustomerIdString;
                string routeIdString = data.RouteIdString;
                List<string> itemGroupIdStrings = new();
                if (data.ItemGroupId != null)
                {
                    itemGroupIdStrings.Add(data.ItemGroupId.ToString());
                }
                else
                {
                    List<string> itemGroupIdsOfParent =
                        ProcessNullItemGroupsInMCPHeaders(routeIdString, itemGroupInZones,
                            itemGroupInRoutes, routesInZoneDictionary);
                    itemGroupIdStrings.AddRange(itemGroupIdsOfParent);
                }
                if (!itemGroupIds.Contains(itemGroupId))
                {
                    itemGroupIds.Add(itemGroupId);
                }
                Dictionary<string, List<string>> routeAndItemGroups;
                if (result.TryGetValue(customerIdString, out var dictionary))
                {
                    routeAndItemGroups = dictionary;
                }
                else
                {
                    routeAndItemGroups = new();
                }
                List<string> itemGroupIdList = new();
                if (routeAndItemGroups.TryGetValue(routeIdString, out var list))
                {
                    itemGroupIdList = list;
                }
                if (!itemGroupIdList.Contains(itemGroupIdString))
                {
                    itemGroupIdList.Add(itemGroupIdString);
                }
                routeAndItemGroups[routeIdString] = itemGroupIdList;
                result[customerIdString] = routeAndItemGroups;
            }
            return (result, itemGroupIds);
        }
        */

        //private static List<string> ProcessNullItemGroupsInMCPHeaders(
        //    string routeIdString,
        //    Dictionary<string, List<string>> itemGroupInZones,
        //    Dictionary<string, List<string>> itemGroupInRoutes,
        //    Dictionary<string, List<string>> routesInZoneDictionary)
        //{
        //    List<string> result = new();
        //    List<string> itemGroupInRoute = itemGroupInRoutes[routeIdString];

        //    return result;
        //}

        //private async Task<(string,
        //   Dictionary<string, ItemSOPODto>,
        //   Dictionary<string, UOMSOPODto>)>
        //   GetSOInfoStringAsync(List<Guid> zoneIds, DateTime postingDate,
        //       DateTime? lastUpdateDate, string nowString)
        //{


        //    ItemInfoGroup zoneItemInfoGroup =
        //        await GetItemInfoFromSalesOrgHierarchyIds(zoneIds, postingDate);
        //    (Dictionary<string, RouteSOPODto> routeDictionary,
        //        List<Guid> routeIds) =
        //      await GetRouteFromZoneIds(zoneIds);
        //    ItemInfoGroup routeItemInfoGroup =
        //        await GetItemInfoFromSalesOrgHierarchyIds(routeIds, postingDate);




        //    List<string> resultParts = new() {
        //            $"\"uom\": {_jsonSerializer.Serialize(uomDictionary)}",
        //            $"\"vat\": {_jsonSerializer.Serialize(vatDictionary)}",
        //            $"\"item\": {_jsonSerializer.Serialize(itemDictionary)}",
        //            $"\"uomGroup\": {_jsonSerializer.Serialize(uomGroupDictionary)}",
        //            $"\"lastUpdated\": {nowString}",
        //            $"\"updateRequired\": true",
        //        };
        //    return ($"\"itemInfo\":{{{resultParts.JoinAsString(",")}}}", itemDictionary, uomDictionary);
        //}

        /*
        private async Task<Dictionary<string, List<string>>> GetItemGroupInRouteFromMCPs(
            List<Guid> zoneIds, DateTime postingDate)
        {
         
         
            foreach (var mcpHeader in mcpHeaders)
            {
                var routeId = mcpHeader.RouteId;
                List<string> itemGroupList = new();
                if (result.TryGetValue(routeId.ToString(), out var values))
                {
                    itemGroupList.AddRange(values);
                }
                Guid? itemGroupId = mcpHeader.ItemGroupId;
                if (itemGroupId == null)
                {
                    if (itemGroupInRoute.AllItems == true)
                    {
                        if (itemGroupInZones.AllItems == true)
                        {

                        }
                    }
                    else
                    {

                    }
                }

            }
        }
        */


        //private async Dictionary<string, List<string>> GetItemInRouteDictionary(
        //    DateTime postingDate, List<MCPHeader> headerList, 
        //    ItemInfoGroup zoneItemInfoGroup, ItemInfoGroup routeItemInfoGroup)
        //{
        //    foreach (var header in headerList)
        //    {
        //        Guid? itemGroupId = header.ItemGroupId;
        //        if (itemGroupId != null)
        //        {

        //        }
        //    }
        //}

        //private async Task<ItemInfoGroup> GetItemInfoFromSalesOrgHierarchyIds(
        //    List<Guid> zosalesOrgHierarchyIdsneIds, DateTime postingDate)
        //{
        //    Dictionary<string, ItemSOPODto> itemDictionary;
        //    Dictionary<string, List<string>> uomGroupDictionary;

        //    List<Guid> itemIds = await GetItemIdsInSalesOrgHierarchies(
        //        zosalesOrgHierarchyIdsneIds, postingDate);

        //    (itemDictionary, uomGroupDictionary, List<string> allAltUomIds,
        //        List<Guid> vatIds) = await GetItemDetailsFromItemIds(itemIds, true);
        //    Dictionary<string, VATSOPODto> vatDictionary = await GetVatDetails(vatIds);
        //    Dictionary<string, UOMSOPODto> uomDictionary = await GetUOMDetails(allAltUomIds);

        //    return new ItemInfoGroup(itemDictionary, uomGroupDictionary,
        //        vatDictionary, uomDictionary);
        //}

        //private async Task<List<Guid>> GetAllItemIdsFromItemGroups(List<ItemGroup> itemGroups)
        //{
        //    List<Guid> result = new();
        //    foreach (var itemGroup in itemGroups)
        //    {
        //        var itemIds = await GetAllItemIdsFromItemGroup(itemGroup);
        //        var newItemIds = itemIds.Where(x => !result.Contains(x));
        //        result.AddRange(newItemIds);
        //    }
        //    return result;
        //}
        #endregion

        #region Get Item From Item Group Type
        private async Task<List<Guid>> GetAllItemIdsFromItemGroup(ItemGroup itemGroup)
        {
            if (itemGroup.Type == GroupType.LIST)
            {
                return await GetAllItemIdsFromItemGroupList(itemGroup.Id);
            }
            else if (itemGroup.Type == GroupType.ATTRIBUTE)
            {
                return await GetAllItemIdsFromItemGroupAttr(itemGroup.Id);
            }
            throw new BusinessException(message: L["Error:ItemsAppService:552"], code: "1");
        }

        private async Task<List<Guid>> GetAllItemIdsFromItemGroupList(Guid itemGroupId)
        {
            var itemGroupLists = (await _itemGroupListRepository.GetListAsync(
                x => x.ItemGroupId == itemGroupId));
            var ids = itemGroupLists.Select(x => x.ItemId).Distinct().ToList();
            return ids;
        }

        private async Task<List<Guid>> GetAllItemIdsFromItemGroupAttr(Guid itemGroupId)
        {
            var itemAttribues = (await _itemGroupAttributeRepository.GetListAsync(
                x => x.ItemGroupId == itemGroupId));
            var attr0Values = itemAttribues.Select(x => x.Attr0Id).Distinct().ToList();
            var attr1Values = itemAttribues.Select(x => x.Attr1Id).Distinct().ToList();
            var attr2Values = itemAttribues.Select(x => x.Attr2Id).Distinct().ToList();
            var attr3Values = itemAttribues.Select(x => x.Attr3Id).Distinct().ToList();
            var attr4Values = itemAttribues.Select(x => x.Attr4Id).Distinct().ToList();
            var attr5Values = itemAttribues.Select(x => x.Attr5Id).Distinct().ToList();
            var attr6Values = itemAttribues.Select(x => x.Attr6Id).Distinct().ToList();
            var attr7Values = itemAttribues.Select(x => x.Attr7Id).Distinct().ToList();
            var attr8Values = itemAttribues.Select(x => x.Attr8Id).Distinct().ToList();
            var attr9Values = itemAttribues.Select(x => x.Attr9Id).Distinct().ToList();
            var attr10Values = itemAttribues.Select(x => x.Attr10Id).Distinct().ToList();
            var attr11Values = itemAttribues.Select(x => x.Attr11Id).Distinct().ToList();
            var attr12Values = itemAttribues.Select(x => x.Attr12Id).Distinct().ToList();
            var attr13Values = itemAttribues.Select(x => x.Attr13Id).Distinct().ToList();
            var attr14Values = itemAttribues.Select(x => x.Attr14Id).Distinct().ToList();
            var attr15Values = itemAttribues.Select(x => x.Attr15Id).Distinct().ToList();
            var attr16Values = itemAttribues.Select(x => x.Attr16Id).Distinct().ToList();
            var attr17Values = itemAttribues.Select(x => x.Attr17Id).Distinct().ToList();
            var attr18Values = itemAttribues.Select(x => x.Attr18Id).Distinct().ToList();
            var attr19Values = itemAttribues.Select(x => x.Attr19Id).Distinct().ToList();
            var items = await _itemRepository.GetListAsync(x => attr0Values.Contains(x.Attr0Id) &&
                attr1Values.Contains(x.Attr1Id) && attr2Values.Contains(x.Attr2Id) &&
                attr3Values.Contains(x.Attr3Id) && attr4Values.Contains(x.Attr4Id) &&
                attr5Values.Contains(x.Attr5Id) && attr6Values.Contains(x.Attr6Id) &&
                attr7Values.Contains(x.Attr7Id) && attr8Values.Contains(x.Attr8Id) &&
                attr9Values.Contains(x.Attr9Id) && attr10Values.Contains(x.Attr10Id) &&
                attr11Values.Contains(x.Attr11Id) && attr12Values.Contains(x.Attr12Id) &&
                attr13Values.Contains(x.Attr13Id) && attr14Values.Contains(x.Attr14Id) &&
                attr15Values.Contains(x.Attr15Id) && attr16Values.Contains(x.Attr16Id) &&
                attr17Values.Contains(x.Attr17Id) && attr18Values.Contains(x.Attr18Id) &&
                attr19Values.Contains(x.Attr19Id));
            var ids = items.Select(x => x.Id).Distinct().ToList();
            return ids;
        }
        #endregion

        #region not sure can be used
        //private async Task<string> GetVendorInfo(bool getVendorInfo,
        //    Guid companyId, DateTime? lastApiDate,
        //    DateTime postingDate, string postingDateString,
        //    Dictionary<string, ItemSOPODto> itemDictionary,
        //    Dictionary<string, UOMSOPODto> uomDictionary)
        //{
        //    if (!getVendorInfo)
        //    {
        //        getVendorInfo = await CheckVendorInfoRequired(lastApiDate);
        //    }
        //    if (!getVendorInfo)
        //    {
        //        return $"\"vendorInfo\":{{\"updateRequired\": \"false\"}}";
        //    }
        //    Dictionary<string, VendorSOPODto> vendorDictionary;
        //    List<string> priceListIds;
        //    (vendorDictionary, priceListIds) =
        //        await GetVendorAndPriceList(companyId, postingDate);
        //    Dictionary<string, decimal> priceDictionary = await GetPriceDi(priceListIds,
        //      itemDictionary, uomDictionary);

        //    List<string> resultParts = new()
        //    {
        //        $"\"vendor\": {_jsonSerializer.Serialize(vendorDictionary)}",
        //        $"\"price\": {_jsonSerializer.Serialize(priceDictionary)}",
        //        $"\"lastUpdated\": {postingDateString}",
        //        $"\"updateRequired\": true",
        //    };
        //    return $"\"vendorInfo\":{{{resultParts.JoinAsString(",")}}}";

        //}

        private async Task<(Dictionary<string, VendorSOPODto>, List<string>)>
            GetVendorAndPriceList(Guid companyId, DateTime postingDate)
        {
            var vendors = (await _vendorRepository.GetListAsync(x => x.CompanyId == companyId &&
                x.Active == true && (x.EndDate == null || x.EndDate > postingDate)))
                .Distinct().Select(x => new VendorSOPODto()
                {
                    id = x.Id.ToString(),
                    code = x.Code,
                    name = x.Name,
                    priceListId = x.PriceListId.ToString(),
                });
            List<string> priceListId = new();
            Dictionary<string, VendorSOPODto> vendorDictionary = new();
            foreach (var vendor in vendors)
            {
                vendorDictionary.Add(vendor.id, vendor);
                priceListId.Add(vendor.priceListId);
            }
            return (vendorDictionary, priceListId);
        }

        private async Task<string> GetRouteInfo(bool getRouteInfo,
            List<Guid> zoneIds, DateTime? lastApiDate,
            DateTime postingDate, string postingDateString)
        {
            if (!getRouteInfo)
            {
                getRouteInfo = await CheckRouteInfoRequired(lastApiDate);
            }
            if (!getRouteInfo)
            {
                return $"\"routeInfo\":{{\"updateRequired\": \"false\"}}";
            }
            (Dictionary<string, RouteSOPODto> routeDictionary,
                List<Guid> routeIds,
                Dictionary<string, string> routeInZoneDictionary) =
                    await GetRouteFromZoneIds(zoneIds);
            //Dictionary<string, List<string>> itemGroupsInRoute =
            //    await GetItemGroupsInRoute(routeDictionary.Keys.ToList());
            //(Dictionary<string, List<string>> employeesInRoute,
            //    Dictionary<string, EmployeeSOPODto> employeeDictionary) =
            //    await GetEmployeesDictionaries(
            //        routeDictionary.Keys.ToList(), postingDate);
            List<string> resultParts = new()
            {
                $"\"route\": {_jsonSerializer.Serialize(routeDictionary)}",
                //$"\"itemGroupInRoute\": {_jsonSerializer.Serialize(itemGroupsInRoute)}",
                //$"\"employee\": {_jsonSerializer.Serialize(employeeDictionary)}",
                //$"\"employeeInRoute\": {_jsonSerializer.Serialize(employeesInRoute)}",
                $"\"lastUpdated\": {postingDateString}",
                $"\"updateRequired\": \"true\"",
            };
            return $"\"routeInfo\":{{{resultParts.JoinAsString(",")}}}";
        }

        //private async Task<Dictionary<string, List<string>>>
        //    GetItemGroupsInRoute(List<string> routeIds)
        //{
        //    var assignments = (await _itemGroupInZoneRepository.GetListAsync(
        //        x => routeIds.Contains(x.Id.ToString()))).Distinct();
        //    var itemGroupIds = assignments.Select(x => x.ItemGroupId).ToList();
        //    var validItemGroupIds = (await _itemGroupRepository.GetListAsync(
        //        x => itemGroupIds.Contains(x.Id) &&
        //        x.Status == GroupStatus.RELEASED)).Distinct().Select(x => x.Id);
        //    Dictionary<string, List<string>> result = new();
        //    foreach (var assignment in assignments)
        //    {
        //        if (!validItemGroupIds.Contains(assignment.ItemGroupId))
        //        {
        //            continue;
        //        }
        //        string routeIdString = assignment.Id.ToString();
        //        string itemGroupIdString = assignment.ItemGroupId.ToString();
        //        if (result.TryGetValue(routeIdString, out List<string> parts))
        //        {
        //            if (!parts.Contains(itemGroupIdString))
        //            {
        //                parts.Add(itemGroupIdString);
        //            }
        //        }
        //        else
        //        {
        //            List<string> itemGroups = new() { itemGroupIdString };
        //            result.Add(routeIdString, itemGroups);
        //        }
        //    }
        //    return result
        //}

        //private async Task<string> GetCustomerInfoForSOAsync(bool getCustomerInfo,
        //    List<Guid> zoneIds, DateTime? lastApiDate,
        //    DateTime postingDate, string postingDateString,
        //    Dictionary<string, ItemSOPODto> itemDictionary,
        //    Dictionary<string, UOMSOPODto> uomDictionary)
        //{
        //    if (!getCustomerInfo)
        //    {
        //        getCustomerInfo = await CheckCustomerInfoRequired(lastApiDate);
        //    }
        //    if (!getCustomerInfo)
        //    {
        //        return $"\"customerInfo\":{{updateRequired: false}}";
        //    }

        //    Dictionary<string, CustomerSOPODto> customerDictionary;
        //    List<string> priceListIds;
        //    (customerDictionary, priceListIds) = await GetCustomerAndPriceList(zoneIds, postingDate);
        //    Dictionary<string, decimal> priceDictionary = await GetPrice(priceListIds,
        //        itemDictionary, uomDictionary);

        //    List<string> resultParts = new()
        //    {
        //        $"\"customer\": {_jsonSerializer.Serialize(customerDictionary)}",
        //        $"\"price\": {_jsonSerializer.Serialize(priceDictionary)}",
        //        $"\"lastUpdated\": {postingDateString}",
        //        $"\"updateRequired\": true",
        //    };
        //    return $"\"customerInfo\":{{{resultParts.JoinAsString(",")}}}";
        //}

        //private async Task<(string, Dictionary<string, ItemSOPODto>, Dictionary<string, UOMSOPODto>)>
        //    GetItemInfoForSOAsync(List<Guid> zoneIds, DateTime? lastApiDate, string postingDateString)
        //{
        //    var itemInfoRequired = await CheckItemInfoRequired(lastApiDate);
        //    if (!itemInfoRequired)
        //    {
        //        return ($"\"itemInfo\":{{updateRequired: false}}", null, null);
        //    }

        //    Dictionary<string, List<string>> itemGroupDictionary;
        //    Dictionary<string, ItemSOPODto> itemDictionary;
        //    Dictionary<string, List<string>> uomGroupDictionary;
        //    List<string> allAltUomIds;
        //    List<Guid> vatIds;
        //    List<Guid> itemGroupIds = await GetAllItemGroupIds(zoneIds);
        //    if (itemGroupIds.Count < 1)
        //    {
        //        (itemDictionary,
        //          uomGroupDictionary, allAltUomIds, vatIds) = await GetAllItemDetails(new List<Guid>(), true);
        //    }
        //    else
        //    {
        //        var itemGroups = await GetAllItemGroups(itemGroupIds);
        //        (itemGroupDictionary, itemDictionary,
        //            uomGroupDictionary, allAltUomIds, vatIds) = await GetItemDetails(itemGroups);
        //    }
        //    Dictionary<string, VATSOPODto> vatDictionary = await GetVatDetails(vatIds);
        //    Dictionary<string, UOMSOPODto> uomDictionary = await GetUOMDetails(allAltUomIds);
        //    List<string> resultParts = new() {
        //        $"\"uom\": {_jsonSerializer.Serialize(uomDictionary)}",
        //        $"\"vat\": {_jsonSerializer.Serialize(vatDictionary)}",
        //        $"\"itemGroup\": {_jsonSerializer.Serialize(itemGroupDictionary)}",
        //        $"\"item\": {_jsonSerializer.Serialize(itemDictionary)}",
        //        $"\"uomGroup\": {_jsonSerializer.Serialize(uomGroupDictionary)}",
        //        $"\"lastUpdated\": {postingDateString}",
        //        $"\"updateRequired\": true",
        //    };
        //    return ($"\"itemInfo\":{{{resultParts.JoinAsString(",")}}}", itemDictionary, uomDictionary);
        //}



        //private async Task<(
        //    Dictionary<string, ItemSOPODto>,
        //    Dictionary<string, List<string>>,
        //    List<string>,
        //    List<Guid>)>
        //    GetItemDetails(List<ItemGroup> itemGroups)
        //{
        //    Dictionary<string, ItemSOPODto> itemDictionary = new();
        //    Dictionary<string, List<string>> uomGroupDictionary = new();
        //    List<string> allAltUomIds = new();
        //    List<Guid> vatIds = new();
        //    foreach (ItemGroup itemGroup in itemGroups)
        //    {
        //        List<Item> itemsInGroup = await GetAllItemIdsFromItemGroup(itemGroup);
        //        List<string> itemInGroupIds = new();
        //        foreach (Item item in itemsInGroup)
        //        {
        //            string itemId = item.Id.ToString();
        //            if (!itemInGroupIds.Contains(itemId))
        //            {
        //                itemInGroupIds.Add(itemId);
        //            }
        //            if (!vatIds.Contains(item.VatId))
        //            {
        //                vatIds.Add(item.VatId);
        //            }
        //            string uomGroupId = item.UomGroupId.ToString();
        //            if (!uomGroupDictionary.ContainsKey(uomGroupId))
        //            {
        //                var altUomIds = await GetAltUOMs(item.UomGroupId);
        //                allAltUomIds.AddRange(altUomIds);
        //                uomGroupDictionary.Add(uomGroupId, altUomIds);
        //            }
        //            if (itemDictionary.ContainsKey(itemId))
        //            {
        //                continue;
        //            }
        //            ItemSOPODto dto = new()
        //            {
        //                id = itemId,
        //                code = item.Code,
        //                name = item.Name,
        //                basePrice = item.BasePrice,
        //                vatId = item.VatId.ToString(),
        //                uomGroupId = item.UomGroupId.ToString(),
        //                invUomId = item.InventoryUOMId.ToString(),
        //                purUomId = item.PurUOMId.ToString(),
        //                purRate = item.PurUnitRate,
        //                salesUomId = item.SalesUOMId.ToString(),
        //                salesRate = item.SalesUnitRate,
        //                isPurchasable = item.IsPurchasable,
        //                isSalesable = item.IsSaleable,
        //            };
        //            itemDictionary.Add(itemId, dto);
        //        }
        //        string itemGroupId = itemGroup.Id.ToString();
        //        if (!itemGroupDictionary.ContainsKey(itemGroupId))
        //        {
        //            itemGroupDictionary.Add(itemGroupId, itemInGroupIds);
        //        }
        //    }
        //    return (itemGroupDictionary, itemDictionary,
        //      uomGroupDictionary, allAltUomIds, vatIds);
        //    return (itemDictionary,
        //        uomGroupDictionary, allAltUomIds, vatIds);
        //}
        #endregion

        #region Checking last update
        private async Task<bool> CheckItemInfoRequired(DateTime? lastApiDate)
        {
            if (!lastApiDate.HasValue)
            {
                return true;
            }
            if ((await _itemRepository.GetListAsync(x => x.LastModificationTime >= lastApiDate)).Take(1).Any())
            {
                return true;
            }
            if ((await _uOMRepository.GetListAsync(x => x.LastModificationTime >= lastApiDate)).Take(1).Any())
            {
                return true;
            }
            if ((await _uOMGroupDetailRepository.GetListAsync(x => x.LastModificationTime >= lastApiDate)).Take(1).Any())
            {
                return true;
            }
            if ((await _vATRepository.GetListAsync(x => x.LastModificationTime >= lastApiDate)).Take(1).Any())
            {
                return true;
            }
            if ((await _itemGroupRepository.GetListAsync(x => x.LastModificationTime >= lastApiDate)).Take(1).Any())
            {
                return true;
            }
            if ((await _itemGroupListRepository.GetListAsync(x => x.LastModificationTime >= lastApiDate)).Take(1).Any())
            {
                return true;
            }
            if ((await _itemGroupAttributeRepository.GetListAsync(x => x.LastModificationTime >= lastApiDate)).Take(1).Any())
            {
                return true;
            }
            return false;
        }

        private async Task<bool> CheckCustomerInfoRequired(DateTime? lastApiDate)
        {
            if (!lastApiDate.HasValue)
            {
                return true;
            }
            if ((await _customerInZoneRepository.GetListAsync(x => x.LastModificationTime >= lastApiDate)).Take(1).Any())
            {
                return true;
            }
            if ((await _customerRepository.GetListAsync(x => x.LastModificationTime >= lastApiDate)).Take(1).Any())
            {
                return true;
            }
            if ((await _priceListDetailRepository.GetListAsync(x => x.LastModificationTime >= lastApiDate)).Take(1).Any())
            {
                return true;
            }
            if ((await _priceListRepository.GetListAsync(x => x.LastModificationTime >= lastApiDate)).Take(1).Any())
            {
                return true;
            }
            return false;
        }

        private async Task<bool> CheckRouteInfoRequired(DateTime? lastApiDate)
        {
            if (!lastApiDate.HasValue)
            {
                return true;
            }
            if ((await _salesOrgHierarchyRepository.GetListAsync(x => x.LastModificationTime >= lastApiDate)).Take(1).Any())
            {
                return true;
            }
            if ((await _employeeProfileRepository.GetListAsync(x => x.LastModificationTime >= lastApiDate)).Take(1).Any())
            {
                return true;
            }
            if ((await _salesOrgEmpAssignmentRepository.GetListAsync(x => x.LastModificationTime >= lastApiDate)).Take(1).Any())
            {
                return true;
            }
            return false;
        }

        private async Task<bool> CheckVendorInfoRequired(DateTime? lastApiDate)
        {
            if (!lastApiDate.HasValue)
            {
                return true;
            }
            if ((await _vendorRepository.GetListAsync(x => x.LastModificationTime >= lastApiDate)).Take(1).Any())
            {
                return true;
            }
            if ((await _priceListDetailRepository.GetListAsync(x => x.LastModificationTime >= lastApiDate)).Take(1).Any())
            {
                return true;
            }
            if ((await _priceListRepository.GetListAsync(x => x.LastModificationTime >= lastApiDate)).Take(1).Any())
            {
                return true;
            }
            return false;
        }
        #endregion

        private class ItemSOPODto
        {
            public string id { get; set; }
            public string name { get; set; }
            public string code { get; set; }
            public decimal basePrice { get; set; }
            public string vatId { get; set; }
            public string uomGroupId { get; set; }
            public string invUomId { get; set; }
            public string purUomId { get; set; }
            public decimal purRate { get; set; }
            public string salesUomId { get; set; }
            public decimal salesRate { get; set; }
            public bool isPurchasable { get; set; }
            public bool isSalesable { get; set; }
            public ItemSOPODto() { }
        }

        private class VATSOPODto
        {
            public string id { get; set; }
            public string name { get; set; }
            public string code { get; set; }
            public uint rate { get; set; }
            public VATSOPODto() { }
        }

        private class UOMSOPODto
        {
            public string id { get; set; }
            public string name { get; set; }
            public string code { get; set; }
            public UOMSOPODto() { }
        }

        private class CustomerSOPODto
        {
            public string id { get; set; }
            public string code { get; set; }
            public string name { get; set; }
            public string priceListId { get; set; }

            public CustomerSOPODto() { }
        }

        private class RouteSOPODto
        {
            public string id { get; set; }
            public string code { get; set; }
            public string name { get; set; }

            public RouteSOPODto() { }
        }

        private class EmployeeSOPODto
        {
            public string id { get; set; }
            public string code { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string email { get; set; }

            public EmployeeSOPODto() { }
        }

        private class VendorSOPODto
        {
            public string id { get; set; }
            public string code { get; set; }
            public string name { get; set; }
            public string priceListId { get; set; }
            public VendorSOPODto() { }
        }

        private class CustomerRouteItemGroup
        {
            public string CustomerIdString { get; set; }
            public string RouteIdString { get; set; }
            public Guid? ItemGroupId { get; set; }
            public CustomerRouteItemGroup() { }
        }
    }
}
