using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.CompanyIdentityUserAssignments;
using DMSpro.OMS.MdmService.CompanyInZones;
using DMSpro.OMS.MdmService.CustomerInZones;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.EmployeeProfiles;
using DMSpro.OMS.MdmService.ItemGroupAttributes;
using DMSpro.OMS.MdmService.ItemGroupLists;
using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.Items;
using DMSpro.OMS.MdmService.MCPDetails;
using DMSpro.OMS.MdmService.MCPHeaders;
using DMSpro.OMS.MdmService.NumberingConfigDetails;
using DMSpro.OMS.MdmService.PriceListDetails;
using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.SalesOrgEmpAssignments;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.UOMGroupDetails;
using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.VATs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Json;

namespace DMSpro.OMS.MdmService.SalesOrders
{
    public class SalesOrdersAppService : ApplicationService, ISalesOrdersAppService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyInZoneRepository _companyInZoneRepository;
        private readonly IItemGroupRepository _itemGroupRepository;
        private readonly IItemGroupListRepository _itemGroupListRepository;
        private readonly IItemGroupAttributeRepository _itemGroupAttributeRepository;
        private readonly IUOMGroupDetailRepository _uOMGroupDetailRepository;
        private readonly ISalesOrgHierarchyRepository _salesOrgHierarchyRepository;
        private readonly ICustomerInZoneRepository _customerInZoneRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPriceListRepository _priceListRepository;
        private readonly IPriceListDetailRepository _priceListDetailRepository;
        private readonly ISalesOrgEmpAssignmentRepository _salesOrgEmpAssignmentRepository;
        private readonly IEmployeeProfileRepository _employeeProfileRepository;
        private readonly IMCPHeaderRepository _mcpHeaderRepository;
        private readonly IMCPDetailRepository _mcpDetailRepository;
        private readonly ICompanyIdentityUserAssignmentsAppService _companyIdentityUserAssignmentsAppService;
        private readonly IVATRepository _vATRepository;
        private readonly IUOMRepository _uOMRepository;

        private readonly INumberingConfigDetailsAppService _numberingConfigDetailsAppService;

        private readonly IJsonSerializer _jsonSerializer;

        public SalesOrdersAppService(
            IItemRepository itemRepository,
            ICompanyRepository companyRepository,
            ICompanyInZoneRepository companyInZoneRepository,
            IItemGroupRepository itemGroupRepository,
            IItemGroupListRepository itemGroupListRepository,
            IItemGroupAttributeRepository itemGroupAttributeRepository,
            IUOMGroupDetailRepository uOMGroupDetailRepository,
            ISalesOrgHierarchyRepository salesOrgHierarchyRepository,
            ICustomerInZoneRepository customerInZoneRepository,
            ICustomerRepository customerRepository,
            IPriceListRepository priceListRepository,
            IPriceListDetailRepository priceListDetailRepository,
            ISalesOrgEmpAssignmentRepository salesOrgEmpAssignmentRepository,
            IEmployeeProfileRepository employeeProfileRepository,
            IMCPHeaderRepository mcpHeaderRepository,
            IMCPDetailRepository mcpDetailRepository,
            ICompanyIdentityUserAssignmentsAppService companyIdentityUserAssignmentsAppService,
            IVATRepository vATRepository,
            IUOMRepository uOMRepository,

            INumberingConfigDetailsAppService numberingConfigDetailsAppService,

            IJsonSerializer jsonSerializer)
        {
            _itemRepository = itemRepository;
            _companyRepository = companyRepository;
            _companyInZoneRepository = companyInZoneRepository;
            _mcpHeaderRepository = mcpHeaderRepository;
            _mcpDetailRepository = mcpDetailRepository;
            _companyIdentityUserAssignmentsAppService = companyIdentityUserAssignmentsAppService;
            _itemGroupRepository = itemGroupRepository;
            _itemGroupListRepository = itemGroupListRepository;
            _itemGroupAttributeRepository = itemGroupAttributeRepository;
            _uOMGroupDetailRepository = uOMGroupDetailRepository;
            _salesOrgHierarchyRepository = salesOrgHierarchyRepository;
            _customerInZoneRepository = customerInZoneRepository;
            _customerRepository = customerRepository;
            _priceListDetailRepository = priceListDetailRepository;
            _priceListRepository = priceListRepository;
            _salesOrgEmpAssignmentRepository = salesOrgEmpAssignmentRepository;
            _employeeProfileRepository = employeeProfileRepository;
            _vATRepository = vATRepository;
            _uOMRepository = uOMRepository;

            _numberingConfigDetailsAppService = numberingConfigDetailsAppService;

            _jsonSerializer = jsonSerializer;
        }


        public async Task<string> GetInfoSOAsync(Guid companyId, DateTime postingDate,
            string objectType = "", DateTime? lastUpdateDate = null,
            Guid? identityUserId = null)
        {
            await CheckCompany(companyId, identityUserId, postingDate);
            List<Guid> zoneIds = await GetAllSellingZoneIds(companyId, postingDate);
            DateTime now = DateTime.Now;
            string nowString = $"\"{now:yyyy/MM/dd HH:mm:ss}\"";

            string soInfo = await GetSOInfoStringAsync(zoneIds, postingDate, nowString,
                companyId, objectType, lastUpdateDate);
            return $"{{{soInfo}}}";
        }

        private async Task<string> GetSOInfoStringAsync(List<Guid> zoneIds,
            DateTime postingDate, string nowString, Guid companyId,
            string objectType = "", DateTime? lastUpdateDate = null)
        {
            bool isForSO = true;
            (Dictionary<string, RouteDto> routeDictionary,
            List<Guid> routeIds,
            Dictionary<string, string> routeInZoneDictionary) =
                await GetRouteFromZoneIds(zoneIds);

            (Dictionary<string, Dictionary<string, List<string>>> customersRoutesItemGroupsDictionary,
            Dictionary<string, CustomerDto> customerDictionary,
            Dictionary<string, List<string>> customersRoutesDictionary) =
                await GetCustomerRouteItemGroupDictionary(routeIds, postingDate);

            (Dictionary<string, List<string>> itemsInItemGroupsDictionary,
            List<Guid> itemIds) = await GetAllItemsInNullItemGroup(isForSO);


            (Dictionary<string, ItemDto> itemDictionary,
            Dictionary<string, List<string>> uomGroupDictionary,
            Dictionary<string, UOMGroupDto> uomGroupWithDetailsDictionary,
            List<string> allAltUomIds,
            List<Guid> vatIds) =
                await GetItemDetailsFromItemIds(itemIds, isForSO);

            Dictionary<string, UOMDto> uomDictionary =
                await GetUOMDictionary(allAltUomIds);

            Dictionary<string, VATDto> vatDictionary =
                await GetVatDictionary(vatIds);

            (Dictionary<string, CustomerDto> allCustomersInZoneDictionary,
            List<string> customerIdsWithoutRoute) = await GetCustomersWithoutRoute(
                postingDate, zoneIds, customerDictionary);

            Dictionary<string, decimal> priceDictionary =
                await GetPriceDictionary(allCustomersInZoneDictionary,
                    itemDictionary, uomDictionary);

            (Dictionary<string, List<string>> employeesInRoutesDictionary,
            Dictionary<string, List<string>> routesWithEmployeesDictionary,
            Dictionary<string, EmployeeDto> employeeDictionary) =
                await GetEmployeesDictionaries(routeIds, postingDate);

            var customerEmployeesDictionary =
                GetCustomerEmployeesDictionary(customersRoutesDictionary,
                    routesWithEmployeesDictionary);

            List<string> resultParts = new()
            {
                $"\"customersRoutesDictionary\": {_jsonSerializer.Serialize(customersRoutesDictionary)}",
                $"\"customerEmployeesDictionary\": {_jsonSerializer.Serialize(customerEmployeesDictionary)}",
                $"\"routeDictionary\": {_jsonSerializer.Serialize(routeDictionary)}",
                $"\"customersRoutesItemGroupsDictionary\": {_jsonSerializer.Serialize(customersRoutesItemGroupsDictionary)}",
                $"\"customerDictionary\": {_jsonSerializer.Serialize(allCustomersInZoneDictionary)}",
                $"\"itemsInItemGroupsDictionary\": {_jsonSerializer.Serialize(itemsInItemGroupsDictionary)}",
                $"\"item\": {_jsonSerializer.Serialize(itemDictionary)}",
                $"\"uomGroup\": {_jsonSerializer.Serialize(uomGroupDictionary)}",
                $"\"uomGroupWithDetailsDictionary\": {_jsonSerializer.Serialize(uomGroupWithDetailsDictionary)}",
                $"\"uom\": {_jsonSerializer.Serialize(uomDictionary)}",
                $"\"vat\": {_jsonSerializer.Serialize(vatDictionary)}",
                $"\"priceDictionary\": {_jsonSerializer.Serialize(priceDictionary)}",
                $"\"employeeDictionary\": {_jsonSerializer.Serialize(employeeDictionary)}",
                $"\"employeesInRoutesDictionary\": {_jsonSerializer.Serialize(employeesInRoutesDictionary)}",
                $"\"routesWithEmployeesDictionary\": {_jsonSerializer.Serialize(routesWithEmployeesDictionary)}",
                $"\"customerIdsWithoutRoute\": {_jsonSerializer.Serialize(customerIdsWithoutRoute)}",
                $"\"lastUpdated\": {nowString}",
                $"\"updateRequired\": true",
            };

            if (!string.IsNullOrEmpty(objectType))
            {
                var config =
                    await _numberingConfigDetailsAppService.GetSuggestedNumberingConfigAsync(objectType, companyId);
                resultParts.Add($"\"numberingConfig\": {_jsonSerializer.Serialize(config)}");
            }

            return ($"\"soInfo\":{{{resultParts.JoinAsString(",")}}}");
        }

        private async Task<(
            Dictionary<string, CustomerDto>,
            List<string>
            )> GetCustomersWithoutRoute(
            DateTime postingDate,
            List<Guid> zoneIds,
            Dictionary<string, CustomerDto> customerDictionary)
        {
            var allCustomersInZones = (await _customerInZoneRepository.GetListAsync(
                x => zoneIds.Contains(x.SalesOrgHierarchyId) &&
                (x.EffectiveDate == null || x.EffectiveDate < postingDate) &&
                (x.EndDate == null || x.EndDate >= postingDate))).Distinct().ToList();
            var allCustomerIdsInZone = allCustomersInZones.Select(
                x => x.CustomerId.ToString()).Distinct().ToList();
            var customerIdsWithRoute =
                customerDictionary.Values.ToList().Select(x => x.id).Distinct().ToList();
            var customerIdsWithoutRoute =
                allCustomerIdsInZone.Where(x => !customerIdsWithRoute.Contains(x)).ToList();
            var customersWithoutRoute = (await _customerRepository.GetListAsync(
                x => customerIdsWithoutRoute.Contains(x.Id.ToString()) &&
                x.Active == true)).Distinct().ToList();
            var customersWithoutRouteDictionary =
                customersWithoutRoute.ToDictionary(x => x.Id.ToString(),
                x => new CustomerDto()
                {
                    id = x.Id.ToString(),
                    code = x.Code,
                    name = x.Name,
                    priceListId = x.PriceListId.ToString(),
                });
            Dictionary<string, CustomerDto> result = new();
            List<Dictionary<string, CustomerDto>> dictionaries = new()
            { customerDictionary, customersWithoutRouteDictionary };
            foreach (var dictionary in dictionaries)
            {
                foreach (var pair in dictionary)
                {
                    result[pair.Key] = pair.Value;
                }
            }
            return (result, customerIdsWithoutRoute);
        }

        private async Task CheckCompany(Guid companyId, Guid? identityUserId, DateTime postingDate)
        {
            var companyDto =
                await _companyIdentityUserAssignmentsAppService.GetCurrentlySelectedCompanyAsync(
                    identityUserId, postingDate);
            if (companyDto.Id != companyId)
            {
                throw new BusinessException(message: L["Error:ItemsAppService:550"], code: "1");
            }
        }

        private async Task<List<Guid>> GetAllSellingZoneIds(Guid companyId, DateTime postingDate)
        {
            var companiesInZone = await _companyInZoneRepository.GetListAsync(x => x.CompanyId == companyId &&
                // x.IsBase == true && 
                x.EffectiveDate < postingDate && (x.EndDate == null || x.EndDate > postingDate));
            var zoneIds = companiesInZone.Select(x => x.SalesOrgHierarchyId).Distinct().ToList();
            var zones = await _salesOrgHierarchyRepository.GetListAsync(x => zoneIds.Contains(x.Id) &&
                x.Active == true && x.IsSellingZone == true);
            // if (zones.Count < 1)
            // {
            //     throw new BusinessException(message: L["Error:ItemsAppService:551"], code: "1");
            // }
            var result = zones.Select(x => x.Id).Distinct().ToList();
            return result;
        }

        private async Task<(
            Dictionary<string, RouteDto>,
            List<Guid>,
            Dictionary<string, string>
            )> GetRouteFromZoneIds(List<Guid> zoneIds)
        {
            var routes = (await _salesOrgHierarchyRepository.GetListAsync(x => x.IsRoute == true &&
                x.ParentId != null && zoneIds.Contains((Guid)x.ParentId) &&
                x.Active == true)).Distinct();
            var routeIds = routes.Select(x => x.Id).ToList();
            // if (routeIds.Count < 1)
            // {
            //     var detailDict = new Dictionary<string, string> { ["objectType"] = "route" };
            //     string detailString = JsonSerializer.Serialize(detailDict).ToString();
            //     throw new BusinessException(message: L["Error:ItemsAppService:553"], code: "0",
            //         details: detailString);
            // }
            var routeDictionary = routes.ToDictionary(x => x.Id.ToString(), x => new RouteDto()
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
            Dictionary<string, CustomerDto>,
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
            //if (validCustomers.Count < 1)
            //{
            //    var detailDict = new Dictionary<string, string> { ["objectType"] = "customer" };
            //    string detailString = JsonSerializer.Serialize(detailDict).ToString();
            //    throw new BusinessException(message: L["Error:ItemsAppService:553"], code: "0",
            //        details: detailString);
            //}
            var validCustomerIdStrings = validCustomers.Select(
                x => x.Id.ToString()).Distinct().ToList();
            var customerDictionary = validCustomers.ToDictionary(x => x.Id.ToString(),
                x => new CustomerDto()
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
            // if (allItems.Count < 1)
            // {
            //     var detailDict = new Dictionary<string, string> { ["objectType"] = "item" };
            //     string detailString = JsonSerializer.Serialize(detailDict).ToString();
            //     throw new BusinessException(message: L["Error:ItemsAppService:553"], code: "0",
            //         details: detailString);
            // }
            var allItemIdStrings = allItems.Select(x => x.Id.ToString()).ToList();
            Dictionary<string, List<string>> itemsInItemGroupsDictionary = new()
            {
                [nullItemGroupIdString] = allItemIdStrings,
            };
            var itemIds = allItems.Select(x => x.Id).ToList();
            return (itemsInItemGroupsDictionary, itemIds);
        }

        private async Task<(
            Dictionary<string, ItemDto>,
            Dictionary<string, List<string>>,
            Dictionary<string, UOMGroupDto>,
            List<string>,
            List<Guid>
            )> GetItemDetailsFromItemIds(List<Guid> itemIds, bool isForSO)
        {
            Dictionary<string, ItemDto> itemDictionary = new();
            Dictionary<string, List<string>> uomGroupDictionary = new();
            List<string> allAltUomIds = new();
            List<Guid> vatIds = new();
            Dictionary<string, UOMGroupDto> uomGroupWithDetailsDictionary = new();

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
                    (var altUomIds, var uomBaseId, var detailsDictionary) = 
                        await GetUOMDetails(item.UomGroupId);
                    allAltUomIds.AddRange(altUomIds);
                    uomGroupDictionary.Add(uomGroupId, altUomIds);
                    UOMGroupDto uomGroupDto = new()
                    {
                        baseUOMId = uomBaseId,
                        detailsDictionary = detailsDictionary,
                    };
                    uomGroupWithDetailsDictionary.Add(uomGroupId, uomGroupDto);
                }
                if (itemDictionary.ContainsKey(itemId))
                {
                    continue;
                }
                ItemDto dto = new()
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
            return (itemDictionary, uomGroupDictionary, uomGroupWithDetailsDictionary,
                allAltUomIds, vatIds);
        }

        private async Task<Dictionary<string, VATDto>> GetVatDictionary(List<Guid> vatIds)
        {
            var vats = await _vATRepository.GetListAsync(x => vatIds.Contains(x.Id));
            Dictionary<string, VATDto> result = new();
            foreach (var vat in vats)
            {
                string id = vat.Id.ToString();
                if (result.ContainsKey(id))
                {
                    continue;
                }
                VATDto dto = new()
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

        private async Task<(List<string>,
            string, 
            Dictionary<string, UOMDetailDto>
            )> GetUOMDetails(Guid uomGroupId)
        {
            var uomGroupDetails = await _uOMGroupDetailRepository.GetListAsync(
                x => x.UOMGroupId == uomGroupId && x.Active == true);

            var altUOMIds = uomGroupDetails.Select(x => x.AltUOMId.ToString())
                .Distinct().ToList();
            var baseDetailIds = uomGroupDetails.Where(x => x.BaseQty == 1 && x.AltQty == 1 &&
                x.BaseUOMId == x.AltUOMId).Select(x => x.Id).Distinct().ToList();
            var baseUOMId = uomGroupDetails.Where(x => x.Id == baseDetailIds.First())
                .Select(x => x.BaseUOMId.ToString()).First();
            var details = uomGroupDetails.Where(x => !baseDetailIds.Contains(x.Id))
                .Distinct().ToList();
            var detailDtos = details.ToDictionary(x => x.AltUOMId.ToString(),
                x => new UOMDetailDto()
                    {
                        altUOMId = x.AltUOMId.ToString(),
                        altQty = (int)x.AltQty,
                        baseQty = (int)x.BaseQty,
                    });
            return (altUOMIds, baseUOMId, detailDtos);
        }

        private async Task<Dictionary<string, UOMDto>> GetUOMDictionary(List<string> allAltUomIds)
        {
            var uoms =
                (await _uOMRepository.GetListAsync(x => allAltUomIds.Contains(x.Id.ToString())))
                    .Distinct().ToList();
            var result = uoms.ToDictionary(x => x.Id.ToString(),
                x => new UOMDto()
                {
                    id = x.Id.ToString(),
                    code = x.Code,
                    name = x.Name,
                });
            return result;
        }

        private async Task<Dictionary<string, decimal>>
            GetPriceDictionary(
                Dictionary<string, CustomerDto> customerDictionary,
                Dictionary<string, ItemDto> itemDictionary,
                Dictionary<string, UOMDto> uomDictionary)
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
            Dictionary<string, EmployeeDto>
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
                x => new EmployeeDto()
                {
                    id = x.Id.ToString(),
                    code = x.Code,
                    firstName = x.FirstName,
                    lastName = x.LastName,
                    email = x.Email,
                });
            var validEmployeeIds = validEmployees.Select(x => x.Id).ToList();
            //if (validEmployeeIds.Count < 1)
            //{
            //    var detailDict = new Dictionary<string, string> { ["objectType"] = "employee" };
            //    string detailString = JsonSerializer.Serialize(detailDict).ToString();
            //    throw new BusinessException(message: L["Error:ItemsAppService:553"], code: "0",
            //        details: detailString);
            //}
            var validAssignments =
                assignments.Where(x => validEmployeeIds.Contains(x.EmployeeProfileId));
            var employeesInRoutesDictionary = validAssignments.GroupBy(x => x.EmployeeProfileId.ToString())
                .ToDictionary(x => x.Key, x => x.Select(x => x.SalesOrgHierarchyId.ToString()).ToList());
            var routesWithEmployeesDictionary = validAssignments.GroupBy(x => x.SalesOrgHierarchyId.ToString())
                .ToDictionary(x => x.Key, x => x.Select(x => x.EmployeeProfileId.ToString()).ToList());
            return (employeesInRoutesDictionary, routesWithEmployeesDictionary,
                employeeDictionary);
        }

        private Dictionary<string, List<string>>
            GetCustomerEmployeesDictionary(
                Dictionary<string, List<string>> customerRouteDictionary,
                Dictionary<string, List<string>> routeEmployeeDictionary)
        {
            Dictionary<string, List<string>> result = new();
            foreach (var customerId in customerRouteDictionary.Keys)
            {
                List<string> resultEmployees = new();
                result[customerId] = resultEmployees;
                var routeIds = customerRouteDictionary[customerId];
                foreach (var routeId in routeIds)
                {
                    if (routeEmployeeDictionary.TryGetValue(routeId, out var employeeList))
                    {
                        foreach (var employee in employeeList)
                        {
                            if (!resultEmployees.Contains(employee))
                            {
                                resultEmployees.Add(employee);
                            }
                        }
                    }
                }
                result[customerId] = resultEmployees;
            }
            return result;
        }

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

        private class ItemDto
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
            public ItemDto() { }
        }

        private class VATDto
        {
            public string id { get; set; }
            public string name { get; set; }
            public string code { get; set; }
            public uint rate { get; set; }
            public VATDto() { }
        }

        private class UOMDto
        {
            public string id { get; set; }
            public string name { get; set; }
            public string code { get; set; }
            public UOMDto() { }
        }

        private class UOMDetailDto
        {
            public string altUOMId { get; set; }
            public int altQty { get; set; }
            public int baseQty { get; set; }

            public UOMDetailDto() { }
        }

        private class UOMGroupDto
        {
            public string baseUOMId { get; set; }
            public Dictionary<string, UOMDetailDto> detailsDictionary { get; set; }

            public UOMGroupDto() { }
        }

        private class CustomerDto
        {
            public string id { get; set; }
            public string code { get; set; }
            public string name { get; set; }
            public string priceListId { get; set; }

            public CustomerDto() { }
        }

        private class RouteDto
        {
            public string id { get; set; }
            public string code { get; set; }
            public string name { get; set; }

            public RouteDto() { }
        }

        private class EmployeeDto
        {
            public string id { get; set; }
            public string code { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string email { get; set; }

            public EmployeeDto() { }
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
