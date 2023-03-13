
using DMSpro.OMS.MdmService.ItemGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.Items
{
    public partial class ItemsAppService
    {
        public async Task<string> GetInfoForSOAsync(Guid companyId, 
            DateTime postingDate, 
            DateTime? lastItemInfoUpdate, DateTime? lastCustomerInfoUpdate,
            DateTime? lastRouteInfoUpdate, DateTime? lastVendorInfoUpdate,
            bool getCustomerInfo, bool getVendorInfo, bool getRouteInfo)
        {
            await CheckCompany(companyId, postingDate);
            List<Guid> zoneIds = await GetAllSellingZoneIds(companyId, postingDate);
            string postingDateString = $"\"{postingDate:yyyy/MM/dd HH:mm:ss}\"";
            (string itemInfo,
                Dictionary<string, ItemSOPODto> itemDictionary,
                Dictionary<string, UOMSOPODto> uomDictionary) =
                await GetItemInfoForSOAsync(zoneIds, lastItemInfoUpdate, postingDateString);
            string customerInfo = await GetCustomerInfoForSOAsync(getCustomerInfo, zoneIds,
                lastCustomerInfoUpdate, postingDate, postingDateString, itemDictionary, uomDictionary);
            string routeInfo = await GetRouteInfo(getRouteInfo, zoneIds, lastRouteInfoUpdate,
                postingDate, postingDateString);
            string vendorInfo = await GetVendorInfo(getVendorInfo, companyId,
                lastVendorInfoUpdate, postingDate, postingDateString, itemDictionary, uomDictionary);
            return $"{{{itemInfo}, {customerInfo}, {routeInfo}, {vendorInfo}}}";
        }

        private async Task<(
            Dictionary<string, RouteSOPODto>,
            Dictionary<string, List<string>>,
            Dictionary<string, ItemSOPODto>,
            Dictionary<string, List<string>>,
            Dictionary<string, EmployeeSOPODto>,
            Dictionary<string, List<string>>,
            Dictionary<string, CustomerSOPODto>,
            Dictionary<string, List<string>>,
            Dictionary<string, VATSOPODto>,
            Dictionary<string, UOMSOPODto>)>
            GetSOInfoAsync(List<Guid> zoneIds,
            DateTime? lastSOInfoUpdate, DateTime postingDate, string postingDateString)
        {

            Dictionary<string, RouteSOPODto> routeDictionary =
                await GetRouteFromZoneIds(zoneIds);
            Dictionary<string, List<string>> itemInRoute = new();
            Dictionary<string, ItemSOPODto> itemDictionary;
            Dictionary<string, List<string>> uomGroupDictionary;
            Dictionary<string, List<string>> employeeInRoute = new();
            Dictionary<string, EmployeeSOPODto> employeeDictionary = new();
            Dictionary<string, List<string>> customerInRoute = new();
            Dictionary<string, CustomerSOPODto> customerDictionary = new();
            
            List<Guid> itemIds = await GetItemIdsInZones(zoneIds, postingDate);

            (itemDictionary, uomGroupDictionary, List<string> allAltUomIds, 
                List<Guid> vatIds) = await GetAllItemDetails(itemIds, true);
            Dictionary<string, VATSOPODto> vatDictionary = await GetVatDetails(vatIds);
            Dictionary<string, UOMSOPODto> uomDictionary = await GetUOMDetails(allAltUomIds);


            return (routeDictionary,
                itemInRoute, itemDictionary,
                employeeInRoute, employeeDictionary,
                customerInRoute, customerDictionary,
                uomGroupDictionary, vatDictionary, uomDictionary);
        }

        private async Task<List<Guid>> GetItemIdsInZones(List<Guid> zoneIds, DateTime postingDate)
        {
            List<Guid> itemIds = new();
            var itemAssignments = (await _itemGroupInZoneRepository.GetListAsync(x =>
                zoneIds.Contains(x.SellingZoneId))).Distinct().ToList();
            if (itemAssignments.Count == 0)
            {
                itemIds = (await _itemRepository.GetListAsync()).Select(x => x.Id)
                    .Distinct().ToList();
                return itemIds;
            }
            var eligibleItemAssignments = itemAssignments.Where(x => x.Active == true &&
                x.EffectiveDate < postingDate &&
                (x.EndDate == null || x.EndDate >= postingDate)).ToList();
            if (eligibleItemAssignments.Count < 1)
            {
                return itemIds;
            }
            var itemGroupIds = eligibleItemAssignments.Select(x => x.ItemGroupId).Distinct();
            var itemGroups =
                (await _itemGroupRepository.GetListAsync(x => itemGroupIds.Contains(x.Id))).ToList();
            if (itemGroups.Count < 1)
            {
                throw new BusinessException(message: "Error:ItemsAppService:553", code: "1");
            }
            itemIds.AddRange(await GetAllItemIdsFromAllItemGroups(itemGroups));
            return itemIds;
        }

        private async Task<(
            Dictionary<string, ItemSOPODto>,
            Dictionary<string, List<string>>,
            List<string>,
            List<Guid>)>
            GetAllItemDetails(List<Guid> itemIds, bool forSO)
        {
            Dictionary<string, ItemSOPODto> itemDictionary = new();
            Dictionary<string, List<string>> uomGroupDictionary = new();
            List<string> allAltUomIds = new();
            List<Guid> vatIds = new();

            var items = (await _itemRepository.GetListAsync(x => x.Active == true &&
                itemIds.Contains(x.Id))).Distinct().ToList();
            if (forSO)
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

        private async Task<List<Guid>> GetAllItemIdsFromAllItemGroups(List<ItemGroup> itemGroups)
        {
            List<Guid> result = new();
            foreach (var itemGroup in itemGroups)
            {
                var itemIds = await GetAllItemIdsFromItemGroup(itemGroup);
                var newItemIds = itemIds.Where(x => !result.Contains(x));
                result.AddRange(newItemIds);
            }
            return result;
        }

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

        private async Task<Dictionary<string, RouteSOPODto>> GetRouteFromZoneIds(List<Guid> zoneIds)
        {
            var routes = await _salesOrgHierarchyRepository.GetListAsync(x => x.IsRoute == true &&
                x.ParentId != null && zoneIds.Contains((Guid)x.ParentId) &&
                x.Active == true);
            var result = routes.ToDictionary(x => x.Id.ToString(), x => new RouteSOPODto()
            {
                id = x.Id.ToString(),
                code = x.Code,
                name = x.Name,
            });
            return result;
        }

        private async Task CheckCompany(Guid companyId, DateTime postingDate)
        {
            try
            {
                await _companyRepository.GetAsync(x => x.Id == companyId &&
                    x.Active == true && x.EffectiveDate < postingDate && (x.EndDate == null || x.EndDate > postingDate));
            }
            catch (EntityNotFoundException)
            {
                throw new BusinessException(message: L["Error:ItemsAppService:550"], code: "1");
            }
        }

        private async Task<List<Guid>> GetAllSellingZoneIds(Guid companyId, DateTime postingDate)
        {
            var companiesInZone = await _companyInZoneRepository.GetListAsync(x => x.CompanyId == companyId &&
                x.IsBase == true && x.EffectiveDate < postingDate && (x.EndDate == null || x.EndDate > postingDate));
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


        #region not sure can be used
        private async Task<string> GetVendorInfo(bool getVendorInfo,
            Guid companyId, DateTime? lastApiDate,
            DateTime postingDate, string postingDateString,
            Dictionary<string, ItemSOPODto> itemDictionary,
            Dictionary<string, UOMSOPODto> uomDictionary)
        {
            if (!getVendorInfo)
            {
                getVendorInfo = await CheckVendorInfoRequired(lastApiDate);
            }
            if (!getVendorInfo)
            {
                return $"\"vendorInfo\":{{\"updateRequired\": \"false\"}}";
            }
            Dictionary<string, VendorSOPODto> vendorDictionary;
            List<string> priceListIds;
            (vendorDictionary, priceListIds) =
                await GetVendorAndPriceList(companyId, postingDate);
            Dictionary<string, decimal> priceDictionary = await GetPrice(priceListIds,
              itemDictionary, uomDictionary);

            List<string> resultParts = new()
            {
                $"\"vendor\": {_jsonSerializer.Serialize(vendorDictionary)}",
                $"\"price\": {_jsonSerializer.Serialize(priceDictionary)}",
                $"\"lastUpdated\": {postingDateString}",
                $"\"updateRequired\": true",
            };
            return $"\"vendorInfo\":{{{resultParts.JoinAsString(",")}}}";

        }

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
            Dictionary<string, RouteSOPODto> routeDictionary =
                await GetRouteFromZoneIds(zoneIds);
            Dictionary<string, List<string>> itemGroupsInRoute =
                await GetItemGroupsInRoute(routeDictionary.Keys.ToList());
            (Dictionary<string, List<string>> employeesInRoute,
                Dictionary<string, EmployeeSOPODto> employeeDictionary) =
                await GetEmployeesInRoute(
                    routeDictionary.Keys.ToList(), postingDate);
            List<string> resultParts = new()
            {
                $"\"route\": {_jsonSerializer.Serialize(routeDictionary)}",
                $"\"itemGroupInRoute\": {_jsonSerializer.Serialize(itemGroupsInRoute)}",
                $"\"employee\": {_jsonSerializer.Serialize(employeeDictionary)}",
                $"\"employeeInRoute\": {_jsonSerializer.Serialize(employeesInRoute)}",
                $"\"lastUpdated\": {postingDateString}",
                $"\"updateRequired\": \"true\"",
            };
            return $"\"routeInfo\":{{{resultParts.JoinAsString(",")}}}";
        }

        private async Task<Dictionary<string, List<string>>>
            GetItemGroupsInRoute(List<string> routeIds)
        {
            var assignments = (await _itemGroupInZoneRepository.GetListAsync(
                x => routeIds.Contains(x.Id.ToString()))).Distinct();
            var itemGroupIds = assignments.Select(x => x.ItemGroupId).ToList();
            var validItemGroupIds = (await _itemGroupRepository.GetListAsync(
                x => itemGroupIds.Contains(x.Id) &&
                x.Status == GroupStatus.RELEASED)).Distinct().Select(x => x.Id);
            Dictionary<string, List<string>> result = new();
            foreach (var assignment in assignments)
            {
                if (!validItemGroupIds.Contains(assignment.ItemGroupId))
                {
                    continue;
                }
                string routeIdString = assignment.Id.ToString();
                string itemGroupIdString = assignment.ItemGroupId.ToString();
                if (result.TryGetValue(routeIdString, out List<string> parts))
                {
                    if (!parts.Contains(itemGroupIdString))
                    {
                        parts.Add(itemGroupIdString);
                    }
                }
                else
                {
                    List<string> itemGroups = new() { itemGroupIdString };
                    result.Add(routeIdString, itemGroups);
                }
            }
            return result;
        }



        private async Task<(Dictionary<string, List<string>>,
            Dictionary<string, EmployeeSOPODto>)>
            GetEmployeesInRoute(List<string> routeIds, DateTime postingDate)
        {
            var assignments = await _salesOrgEmpAssignmentRepository.GetListAsync(
                x => routeIds.Contains(x.SalesOrgHierarchyId.ToString()) &&
                x.IsBase == true && x.EffectiveDate <= postingDate &&
                (x.EndDate == null || x.EndDate > postingDate));

            Dictionary<string, List<string>> employeesInRoute = new();
            Dictionary<string, EmployeeSOPODto> employeeDictionary = new();
            foreach (var assignment in assignments)
            {
                string routeIdString = assignment.SalesOrgHierarchyId.ToString();
                Guid employeeId = assignment.EmployeeProfileId;
                string employeeIdString = assignment.EmployeeProfileId.ToString();
                var employeeItems = await _employeeProfileRepository.GetListAsync(
                    x => x.Id == employeeId && x.Active == true &&
                    x.EffectiveDate <= postingDate && x.EndDate > postingDate);
                if (employeeItems.Count != 1)
                {
                    continue;
                }
                var employee = employeeItems.First();
                var employeeDto = new EmployeeSOPODto()
                {
                    id = employee.Id.ToString(),
                    code = employee.Code,
                    firstName = employee.FirstName,
                    lastName = employee.LastName,
                    email = employee.Email,
                };
                if (!employeeDictionary.ContainsKey(employeeIdString))
                {
                    employeeDictionary.Add(employeeIdString, employeeDto);
                }
                if (employeesInRoute.TryGetValue(routeIdString, out List<string> employees))
                {
                    if (!employees.Contains(employeeIdString))
                    {
                        employees.Add(employeeIdString);
                    }
                }
                else
                {
                    List<string> newEmployeeList = new()
                    {
                        employeeIdString
                    };
                    employeesInRoute.Add(routeIdString, newEmployeeList);
                }
            }
            return (employeesInRoute, employeeDictionary);
        }

        private async Task<string> GetCustomerInfoForSOAsync(bool getCustomerInfo,
            List<Guid> zoneIds, DateTime? lastApiDate,
            DateTime postingDate, string postingDateString,
            Dictionary<string, ItemSOPODto> itemDictionary,
            Dictionary<string, UOMSOPODto> uomDictionary)
        {
            if (!getCustomerInfo)
            {
                getCustomerInfo = await CheckCustomerInfoRequired(lastApiDate);
            }
            if (!getCustomerInfo)
            {
                return $"\"customerInfo\":{{updateRequired: false}}";
            }

            Dictionary<string, CustomerSOPODto> customerDictionary;
            List<string> priceListIds;
            (customerDictionary, priceListIds) = await GetCustomerAndPriceList(zoneIds, postingDate);
            Dictionary<string, decimal> priceDictionary = await GetPrice(priceListIds,
                itemDictionary, uomDictionary);

            List<string> resultParts = new()
            {
                $"\"customer\": {_jsonSerializer.Serialize(customerDictionary)}",
                $"\"price\": {_jsonSerializer.Serialize(priceDictionary)}",
                $"\"lastUpdated\": {postingDateString}",
                $"\"updateRequired\": true",
            };
            return $"\"customerInfo\":{{{resultParts.JoinAsString(",")}}}";
        }

        private async Task<Dictionary<string, decimal>> GetPrice(List<string> priceListIds,
            Dictionary<string, ItemSOPODto> itemDictionary,
            Dictionary<string, UOMSOPODto> uomDictionary)
        {
            List<string> itemIds = itemDictionary.Keys.ToList();
            List<string> uomIds = uomDictionary.Keys.ToList();
            List<Guid> activePriceListIds = (await _priceListRepository.GetListAsync(
                x => priceListIds.Contains(x.Id.ToString()) &&
                x.Active == true)).Select(x => x.Id).Distinct().ToList();
            var priceListDetails = await _priceListDetailRepository.GetListAsync(
                x => activePriceListIds.Contains(x.PriceListId) &&
                uomIds.Contains(x.UOMId.ToString()) &&
                itemIds.Contains(x.ItemId.ToString()));
            var parts = priceListDetails.Select(x => new
            {
                Key = $"{x.PriceListId}|{x.ItemId}|{x.UOMId}",
                x.Price,
            }).ToList();
            Dictionary<string, decimal> result = new();
            foreach (var part in parts)
            {
                result.Add(part.Key, part.Price);
            }
            return result;
        }

        private async Task<(string, Dictionary<string, ItemSOPODto>, Dictionary<string, UOMSOPODto>)>
            GetItemInfoForSOAsync(List<Guid> zoneIds, DateTime? lastApiDate, string postingDateString)
        {
            var itemInfoRequired = await CheckItemInfoRequired(lastApiDate);
            if (!itemInfoRequired)
            {
                return ($"\"itemInfo\":{{updateRequired: false}}", null, null);
            }

            Dictionary<string, List<string>> itemGroupDictionary;
            Dictionary<string, ItemSOPODto> itemDictionary;
            Dictionary<string, List<string>> uomGroupDictionary;
            List<string> allAltUomIds;
            List<Guid> vatIds;
            List<Guid> itemGroupIds = await GetAllItemGroupIds(zoneIds);
            if (itemGroupIds.Count < 1)
            {
                (itemDictionary,
                  uomGroupDictionary, allAltUomIds, vatIds) = await GetAllItemDetails(new List<Guid>(), true);
            }
            else
            {
                var itemGroups = await GetAllItemGroups(itemGroupIds);
                (itemGroupDictionary, itemDictionary,
                    uomGroupDictionary, allAltUomIds, vatIds) = await GetItemDetails(itemGroups);
            }
            Dictionary<string, VATSOPODto> vatDictionary = await GetVatDetails(vatIds);
            Dictionary<string, UOMSOPODto> uomDictionary = await GetUOMDetails(allAltUomIds);
            List<string> resultParts = new() {
                $"\"uom\": {_jsonSerializer.Serialize(uomDictionary)}",
                $"\"vat\": {_jsonSerializer.Serialize(vatDictionary)}",
                $"\"itemGroup\": {_jsonSerializer.Serialize(itemGroupDictionary)}",
                $"\"item\": {_jsonSerializer.Serialize(itemDictionary)}",
                $"\"uomGroup\": {_jsonSerializer.Serialize(uomGroupDictionary)}",
                $"\"lastUpdated\": {postingDateString}",
                $"\"updateRequired\": true",
            };
            return ($"\"itemInfo\":{{{resultParts.JoinAsString(",")}}}", itemDictionary, uomDictionary);
        }



        private async Task<(Dictionary<string, CustomerSOPODto>, List<string>)>
            GetCustomerAndPriceList(List<Guid> zoneIds, DateTime postingDate)
        {
            var customersInZone = await _customerInZoneRepository.GetListAsync(
                x => zoneIds.Contains(x.SalesOrgHierarchyId) &&
                x.EffectiveDate < postingDate && (x.EndDate == null || x.EndDate > postingDate));
            List<Guid> customerIds = customersInZone.Select(x => x.CustomerId).Distinct().ToList();
            var customers = await _customerRepository.GetListAsync(x => customerIds.Contains(x.Id) &&
                x.Active == true && x.EffectiveDate < postingDate && (x.EndDate == null || x.EndDate > postingDate));
            Dictionary<string, CustomerSOPODto> customerDictionary = new();
            List<string> priceListIds = new();
            foreach (var customer in customers)
            {
                string id = customer.Id.ToString();
                CustomerSOPODto dto = new()
                {
                    id = customer.Id.ToString(),
                    code = customer.Code,
                    name = customer.Name,
                    priceListId = customer.PriceListId.ToString(),
                };
                customerDictionary.Add(id, dto);
                priceListIds.Add(dto.priceListId);
            }
            return (customerDictionary, priceListIds);
        }

        private async Task<List<Guid>> GetAllItemGroupIds(List<Guid> zoneIds)
        {
            var itemGroupsInZone = await _itemGroupInZoneRepository.GetListAsync(x => zoneIds.Contains(x.SellingZoneId));
            var result = itemGroupsInZone.Select(x => x.ItemGroupId).Distinct().ToList();
            return result;
        }

        private async Task<List<ItemGroup>> GetAllItemGroups(List<Guid> itemGroupIds)
        {
            var itemGroups = await _itemGroupRepository.GetListAsync(x => itemGroupIds.Contains(x.Id) &&
                x.Status == GroupStatus.RELEASED);
            return itemGroups;
        }


        private async Task<(
            Dictionary<string, ItemSOPODto>,
            Dictionary<string, List<string>>,
            List<string>,
            List<Guid>)>
            GetItemDetails(List<ItemGroup> itemGroups)
        {
            Dictionary<string, ItemSOPODto> itemDictionary = new();
            Dictionary<string, List<string>> uomGroupDictionary = new();
            List<string> allAltUomIds = new();
            List<Guid> vatIds = new();
            foreach (ItemGroup itemGroup in itemGroups)
            {
                List<Item> itemsInGroup = await GetAllItemIdsFromItemGroup(itemGroup);
                List<string> itemInGroupIds = new();
                foreach (Item item in itemsInGroup)
                {
                    string itemId = item.Id.ToString();
                    if (!itemInGroupIds.Contains(itemId))
                    {
                        itemInGroupIds.Add(itemId);
                    }
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
                    };
                    itemDictionary.Add(itemId, dto);
                }
                string itemGroupId = itemGroup.Id.ToString();
                if (!itemGroupDictionary.ContainsKey(itemGroupId))
                {
                    itemGroupDictionary.Add(itemGroupId, itemInGroupIds);
                }
            }
            return (itemGroupDictionary, itemDictionary,
                uomGroupDictionary, allAltUomIds, vatIds);
        }

        private async Task<List<string>> GetAltUOMs(Guid uomGroupId)
        {
            var uomGroupDetails = await _uOMGroupDetailRepository.GetListAsync(
                x => x.UOMGroupId == uomGroupId && x.Active == true);
            var result = uomGroupDetails.Select(x => x.AltUOMId.ToString()).Distinct().ToList();
            return result;
        }

        private async Task<Dictionary<string, VATSOPODto>> GetVatDetails(List<Guid> vatIds)
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

        private async Task<Dictionary<string, UOMSOPODto>> GetUOMDetails(List<string> allAltUomIds)
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

        private async Task<bool> CheckItemInfoRequired(DateTime? lastApiDate)
        {
            if (!lastApiDate.HasValue)
            {
                return true;
            }
            if ((await _itemGroupInZoneRepository.GetListAsync(x => x.LastModificationTime >= lastApiDate)).Take(1).Any())
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
    }
}
