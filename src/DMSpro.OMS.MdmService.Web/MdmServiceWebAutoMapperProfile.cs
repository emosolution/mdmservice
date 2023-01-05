/*
using DMSpro.OMS.MdmService.Web.Pages.ItemAttributes;
using DMSpro.OMS.MdmService.ItemAttributes;
using DMSpro.OMS.MdmService.Web.Pages.CompanyIdentityUserAssignments;
using DMSpro.OMS.MdmService.CompanyIdentityUserAssignments;
using DMSpro.OMS.MdmService.Web.Pages.Vendors;
using DMSpro.OMS.MdmService.Vendors;
using DMSpro.OMS.MdmService.Web.Pages.CustomerAttachments;
using DMSpro.OMS.MdmService.CustomerAttachments;
using DMSpro.OMS.MdmService.Web.Pages.CustomerContacts;
using DMSpro.OMS.MdmService.CustomerContacts;
using DMSpro.OMS.MdmService.Web.Pages.CusAttributeValues;
using DMSpro.OMS.MdmService.CusAttributeValues;
using DMSpro.OMS.MdmService.Web.Pages.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.Web.Pages.SalesOrgHeaders;
using DMSpro.OMS.MdmService.SalesOrgHeaders;
using DMSpro.OMS.MdmService.Web.Pages.EmployeeAttachments;
using DMSpro.OMS.MdmService.EmployeeAttachments;
using DMSpro.OMS.MdmService.Web.Pages.EmployeeImages;
using DMSpro.OMS.MdmService.EmployeeImages;
using DMSpro.OMS.MdmService.Web.Pages.PriceUpdateDetails;
using DMSpro.OMS.MdmService.PriceUpdateDetails;
using DMSpro.OMS.MdmService.Web.Pages.PriceLists;
using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.Web.Pages.EmployeeProfiles;
using DMSpro.OMS.MdmService.EmployeeProfiles;
using DMSpro.OMS.MdmService.Web.Pages.SalesChannels;
using DMSpro.OMS.MdmService.SalesChannels;
using DMSpro.OMS.MdmService.Web.Pages.RouteAssignments;
using DMSpro.OMS.MdmService.RouteAssignments;
using DMSpro.OMS.MdmService.Web.Pages.VisitPlans;
using DMSpro.OMS.MdmService.VisitPlans;
using DMSpro.OMS.MdmService.Web.Pages.MCPDetails;
using DMSpro.OMS.MdmService.MCPDetails;
using DMSpro.OMS.MdmService.Web.Pages.MCPHeaders;
using DMSpro.OMS.MdmService.MCPHeaders;
using DMSpro.OMS.MdmService.Web.Pages.Routes;
using DMSpro.OMS.MdmService.Routes;
using DMSpro.OMS.MdmService.Web.Pages.HolidayDetails;
using DMSpro.OMS.MdmService.HolidayDetails;
using DMSpro.OMS.MdmService.Web.Pages.Holidays;
using DMSpro.OMS.MdmService.Holidays;
using DMSpro.OMS.MdmService.Web.Pages.CustomerAssignments;
using DMSpro.OMS.MdmService.CustomerAssignments;
using DMSpro.OMS.MdmService.Web.Pages.CustomerGroupByGeos;
using DMSpro.OMS.MdmService.CustomerGroupByGeos;
using DMSpro.OMS.MdmService.Web.Pages.CustomerGroupByLists;
using DMSpro.OMS.MdmService.CustomerGroupByLists;
using DMSpro.OMS.MdmService.Web.Pages.CustomerGroupByAtts;
using DMSpro.OMS.MdmService.CustomerGroupByAtts;
using DMSpro.OMS.MdmService.Web.Pages.CustomerGroups;
using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.Web.Pages.CustomerAttributes;
using DMSpro.OMS.MdmService.CustomerAttributes;
using DMSpro.OMS.MdmService.Web.Pages.EmployeeInZones;
using DMSpro.OMS.MdmService.EmployeeInZones;
using DMSpro.OMS.MdmService.Web.Pages.CustomerInZones;
using DMSpro.OMS.MdmService.CustomerInZones;
using DMSpro.OMS.MdmService.Web.Pages.CompanyInZones;
using DMSpro.OMS.MdmService.CompanyInZones;
using DMSpro.OMS.MdmService.Web.Pages.SalesOrgEmpAssignments;
using DMSpro.OMS.MdmService.SalesOrgEmpAssignments;
using DMSpro.OMS.MdmService.Web.Pages.WorkingPositions;
using DMSpro.OMS.MdmService.WorkingPositions;
using DMSpro.OMS.MdmService.Web.Pages.NumberingConfigs;
using DMSpro.OMS.MdmService.NumberingConfigs;
using DMSpro.OMS.MdmService.Web.Pages.SystemDatas;
using DMSpro.OMS.MdmService.SystemDatas;
using DMSpro.OMS.MdmService.Web.Pages.PricelistAssignments;
using DMSpro.OMS.MdmService.PricelistAssignments;
using DMSpro.OMS.MdmService.Web.Pages.PriceUpdates;
using DMSpro.OMS.MdmService.PriceUpdates;
using DMSpro.OMS.MdmService.Web.Pages.PriceListDetails;
using DMSpro.OMS.MdmService.PriceListDetails;
using DMSpro.OMS.MdmService.Web.Pages.ItemGroupLists;
using DMSpro.OMS.MdmService.ItemGroupLists;
using DMSpro.OMS.MdmService.Web.Pages.ItemGroupAttrs;
using DMSpro.OMS.MdmService.ItemGroupAttrs;
using DMSpro.OMS.MdmService.Web.Pages.ItemGroups;
using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.Web.Pages.ItemAttachments;
using DMSpro.OMS.MdmService.ItemAttachments;
using DMSpro.OMS.MdmService.Web.Pages.ItemImages;
using DMSpro.OMS.MdmService.ItemImages;
using DMSpro.OMS.MdmService.Web.Pages.ProdAttributeValues;
using DMSpro.OMS.MdmService.ProdAttributeValues;
using DMSpro.OMS.MdmService.Web.Pages.ProductAttributes;
using DMSpro.OMS.MdmService.ProductAttributes;
using DMSpro.OMS.MdmService.Web.Pages.UOMGroupDetails;
using DMSpro.OMS.MdmService.UOMGroupDetails;
using DMSpro.OMS.MdmService.Web.Pages.UOMGroups;
using DMSpro.OMS.MdmService.UOMGroups;
using DMSpro.OMS.MdmService.Web.Pages.UOMs;
using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.Web.Pages.VATs;
using DMSpro.OMS.MdmService.VATs;
using DMSpro.OMS.MdmService.Web.Pages.WeightMeasurements;
using DMSpro.OMS.MdmService.WeightMeasurements;
using DMSpro.OMS.MdmService.Web.Pages.DimensionMeasurements;
using DMSpro.OMS.MdmService.DimensionMeasurements;
using DMSpro.OMS.MdmService.Web.Pages.Currencies;
using DMSpro.OMS.MdmService.Currencies;
using DMSpro.OMS.MdmService.Web.Pages.Streets;
using DMSpro.OMS.MdmService.Streets;
using DMSpro.OMS.MdmService.Web.Pages.GeoMasters;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.Web.Pages.Companies;
using Volo.Abp.AutoMapper;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.Web.Pages.Customers;
using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.Web.Pages.SystemConfigs;
using DMSpro.OMS.MdmService.SystemConfigs;
using DMSpro.OMS.MdmService.Web.Pages.Vendors;
using DMSpro.OMS.MdmService.Web.Pages.CustomerAttachments;
using DMSpro.OMS.MdmService.Web.Pages.CustomerContacts;
using DMSpro.OMS.MdmService.Web.Pages.CusAttributeValues;
using DMSpro.OMS.MdmService.Web.Pages.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.Web.Pages.SalesOrgHeaders;
using DMSpro.OMS.MdmService.Web.Pages.EmployeeAttachments;
using DMSpro.OMS.MdmService.Web.Pages.EmployeeImages;
using DMSpro.OMS.MdmService.Web.Pages.PriceUpdateDetails;
using DMSpro.OMS.MdmService.Web.Pages.PriceLists;
using DMSpro.OMS.MdmService.Web.Pages.EmployeeProfiles;
using DMSpro.OMS.MdmService.Web.Pages.SalesChannels;
using DMSpro.OMS.MdmService.Web.Pages.RouteAssignments;
using DMSpro.OMS.MdmService.Web.Pages.VisitPlans;
using DMSpro.OMS.MdmService.Web.Pages.MCPDetails;
using DMSpro.OMS.MdmService.Web.Pages.MCPHeaders;
using DMSpro.OMS.MdmService.Web.Pages.Routes;
using DMSpro.OMS.MdmService.Web.Pages.HolidayDetails;
using DMSpro.OMS.MdmService.Web.Pages.Holidays;
using DMSpro.OMS.MdmService.Web.Pages.CustomerAssignments;
using DMSpro.OMS.MdmService.Web.Pages.CustomerGroupByGeos;
using DMSpro.OMS.MdmService.Web.Pages.CustomerGroupByLists;
using DMSpro.OMS.MdmService.Web.Pages.CustomerGroupByAtts;
using DMSpro.OMS.MdmService.Web.Pages.CustomerGroups;
using DMSpro.OMS.MdmService.Web.Pages.CustomerAttributes;
using DMSpro.OMS.MdmService.Web.Pages.EmployeeInZones;
using DMSpro.OMS.MdmService.Web.Pages.CustomerInZones;
using DMSpro.OMS.MdmService.Web.Pages.CompanyInZones;
using DMSpro.OMS.MdmService.Web.Pages.SalesOrgEmpAssignments;
using DMSpro.OMS.MdmService.Web.Pages.WorkingPositions;
using DMSpro.OMS.MdmService.Web.Pages.NumberingConfigs;
using DMSpro.OMS.MdmService.Web.Pages.SystemDatas;
using DMSpro.OMS.MdmService.Web.Pages.PricelistAssignments;
using DMSpro.OMS.MdmService.Web.Pages.PriceUpdates;
using DMSpro.OMS.MdmService.Web.Pages.PriceListDetails;
using DMSpro.OMS.MdmService.Web.Pages.ItemGroupLists;
using DMSpro.OMS.MdmService.Web.Pages.ItemGroupAttrs;
using DMSpro.OMS.MdmService.Web.Pages.ItemGroups;
using DMSpro.OMS.MdmService.Web.Pages.ItemAttachments;
using DMSpro.OMS.MdmService.Web.Pages.ItemImages;
using DMSpro.OMS.MdmService.Web.Pages.ProdAttributeValues;
using DMSpro.OMS.MdmService.Web.Pages.ProductAttributes;
using DMSpro.OMS.MdmService.Web.Pages.UOMGroupDetails;
using DMSpro.OMS.MdmService.Web.Pages.UOMGroups;
using DMSpro.OMS.MdmService.Web.Pages.UOMs;
using DMSpro.OMS.MdmService.Web.Pages.VATs;
using DMSpro.OMS.MdmService.Web.Pages.WeightMeasurements;
using DMSpro.OMS.MdmService.Web.Pages.DimensionMeasurements;
using DMSpro.OMS.MdmService.Web.Pages.Currencies;
using DMSpro.OMS.MdmService.Web.Pages.Streets;
using DMSpro.OMS.MdmService.Web.Pages.GeoMasters;
using DMSpro.OMS.MdmService.Web.Pages.Companies;
*/
using AutoMapper;

namespace DMSpro.OMS.MdmService.Web;

public class MdmServiceWebAutoMapperProfile : Profile
{
    public MdmServiceWebAutoMapperProfile()
    {
        /*
        CreateMap<CompanyDto, CompanyUpdateViewModel>();
        CreateMap<CompanyUpdateViewModel, CompanyUpdateDto>();
        CreateMap<CompanyCreateViewModel, CompanyCreateDto>();

        CreateMap<GeoMasterDto, GeoMasterUpdateViewModel>();
        CreateMap<GeoMasterUpdateViewModel, GeoMasterUpdateDto>();
        CreateMap<GeoMasterCreateViewModel, GeoMasterCreateDto>();

        CreateMap<StreetDto, StreetUpdateViewModel>();
        CreateMap<StreetUpdateViewModel, StreetUpdateDto>();
        CreateMap<StreetCreateViewModel, StreetCreateDto>();

        CreateMap<CurrencyDto, CurrencyUpdateViewModel>();
        CreateMap<CurrencyUpdateViewModel, CurrencyUpdateDto>();
        CreateMap<CurrencyCreateViewModel, CurrencyCreateDto>();

        CreateMap<DimensionMeasurementDto, DimensionMeasurementUpdateViewModel>();
        CreateMap<DimensionMeasurementUpdateViewModel, DimensionMeasurementUpdateDto>();
        CreateMap<DimensionMeasurementCreateViewModel, DimensionMeasurementCreateDto>();

        CreateMap<WeightMeasurementDto, WeightMeasurementUpdateViewModel>();
        CreateMap<WeightMeasurementUpdateViewModel, WeightMeasurementUpdateDto>();
        CreateMap<WeightMeasurementCreateViewModel, WeightMeasurementCreateDto>();

        CreateMap<VATDto, VATUpdateViewModel>();
        CreateMap<VATUpdateViewModel, VATUpdateDto>();
        CreateMap<VATCreateViewModel, VATCreateDto>();

        CreateMap<UOMDto, UOMUpdateViewModel>();
        CreateMap<UOMUpdateViewModel, UOMUpdateDto>();
        CreateMap<UOMCreateViewModel, UOMCreateDto>();

        CreateMap<UOMGroupDto, UOMGroupUpdateViewModel>();
        CreateMap<UOMGroupUpdateViewModel, UOMGroupUpdateDto>();
        CreateMap<UOMGroupCreateViewModel, UOMGroupCreateDto>();

        CreateMap<UOMGroupDetailDto, UOMGroupDetailUpdateViewModel>();
        CreateMap<UOMGroupDetailUpdateViewModel, UOMGroupDetailUpdateDto>();
        CreateMap<UOMGroupDetailCreateViewModel, UOMGroupDetailCreateDto>();

        CreateMap<ProductAttributeDto, ProductAttributeUpdateViewModel>();
        CreateMap<ProductAttributeUpdateViewModel, ProductAttributeUpdateDto>();
        CreateMap<ProductAttributeCreateViewModel, ProductAttributeCreateDto>();

        CreateMap<ProdAttributeValueDto, ProdAttributeValueUpdateViewModel>();
        CreateMap<ProdAttributeValueUpdateViewModel, ProdAttributeValueUpdateDto>();
        CreateMap<ProdAttributeValueCreateViewModel, ProdAttributeValueCreateDto>();

        CreateMap<ItemImageDto, ItemImageUpdateViewModel>();
        CreateMap<ItemImageUpdateViewModel, ItemImageUpdateDto>();
        CreateMap<ItemImageCreateViewModel, ItemImageCreateDto>();

        CreateMap<ItemAttachmentDto, ItemAttachmentUpdateViewModel>();
        CreateMap<ItemAttachmentUpdateViewModel, ItemAttachmentUpdateDto>();
        CreateMap<ItemAttachmentCreateViewModel, ItemAttachmentCreateDto>();

        CreateMap<ItemGroupDto, ItemGroupUpdateViewModel>();
        CreateMap<ItemGroupUpdateViewModel, ItemGroupUpdateDto>();
        CreateMap<ItemGroupCreateViewModel, ItemGroupCreateDto>();

        CreateMap<ItemGroupAttrDto, ItemGroupAttrUpdateViewModel>();
        CreateMap<ItemGroupAttrUpdateViewModel, ItemGroupAttrUpdateDto>();
        CreateMap<ItemGroupAttrCreateViewModel, ItemGroupAttrCreateDto>();

        CreateMap<ItemGroupListDto, ItemGroupListUpdateViewModel>();
        CreateMap<ItemGroupListUpdateViewModel, ItemGroupListUpdateDto>();
        CreateMap<ItemGroupListCreateViewModel, ItemGroupListCreateDto>();

        CreateMap<PriceListDetailDto, PriceListDetailUpdateViewModel>();
        CreateMap<PriceListDetailUpdateViewModel, PriceListDetailUpdateDto>();
        CreateMap<PriceListDetailCreateViewModel, PriceListDetailCreateDto>();

        CreateMap<PriceUpdateDto, PriceUpdateUpdateViewModel>();
        CreateMap<PriceUpdateUpdateViewModel, PriceUpdateUpdateDto>();
        CreateMap<PriceUpdateCreateViewModel, PriceUpdateCreateDto>();

        CreateMap<PricelistAssignmentDto, PricelistAssignmentUpdateViewModel>();
        CreateMap<PricelistAssignmentUpdateViewModel, PricelistAssignmentUpdateDto>();
        CreateMap<PricelistAssignmentCreateViewModel, PricelistAssignmentCreateDto>();

        CreateMap<SystemDataDto, SystemDataUpdateViewModel>();
        CreateMap<SystemDataUpdateViewModel, SystemDataUpdateDto>();
        CreateMap<SystemDataCreateViewModel, SystemDataCreateDto>();

        CreateMap<NumberingConfigDto, NumberingConfigUpdateViewModel>();
        CreateMap<NumberingConfigUpdateViewModel, NumberingConfigUpdateDto>();
        CreateMap<NumberingConfigCreateViewModel, NumberingConfigCreateDto>();

        CreateMap<WorkingPositionDto, WorkingPositionUpdateViewModel>();
        CreateMap<WorkingPositionUpdateViewModel, WorkingPositionUpdateDto>();
        CreateMap<WorkingPositionCreateViewModel, WorkingPositionCreateDto>();

        CreateMap<SalesOrgEmpAssignmentDto, SalesOrgEmpAssignmentUpdateViewModel>();
        CreateMap<SalesOrgEmpAssignmentUpdateViewModel, SalesOrgEmpAssignmentUpdateDto>();
        CreateMap<SalesOrgEmpAssignmentCreateViewModel, SalesOrgEmpAssignmentCreateDto>();

        CreateMap<CompanyInZoneDto, CompanyInZoneUpdateViewModel>();
        CreateMap<CompanyInZoneUpdateViewModel, CompanyInZoneUpdateDto>();
        CreateMap<CompanyInZoneCreateViewModel, CompanyInZoneCreateDto>();

        CreateMap<CustomerInZoneDto, CustomerInZoneUpdateViewModel>();
        CreateMap<CustomerInZoneUpdateViewModel, CustomerInZoneUpdateDto>();
        CreateMap<CustomerInZoneCreateViewModel, CustomerInZoneCreateDto>();

        CreateMap<EmployeeInZoneDto, EmployeeInZoneUpdateViewModel>();
        CreateMap<EmployeeInZoneUpdateViewModel, EmployeeInZoneUpdateDto>();
        CreateMap<EmployeeInZoneCreateViewModel, EmployeeInZoneCreateDto>();

        CreateMap<CustomerAttributeDto, CustomerAttributeUpdateViewModel>();
        CreateMap<CustomerAttributeUpdateViewModel, CustomerAttributeUpdateDto>();
        CreateMap<CustomerAttributeCreateViewModel, CustomerAttributeCreateDto>();

        CreateMap<CustomerGroupDto, CustomerGroupUpdateViewModel>();
        CreateMap<CustomerGroupUpdateViewModel, CustomerGroupUpdateDto>();
        CreateMap<CustomerGroupCreateViewModel, CustomerGroupCreateDto>();

        CreateMap<CustomerGroupByAttDto, CustomerGroupByAttUpdateViewModel>();
        CreateMap<CustomerGroupByAttUpdateViewModel, CustomerGroupByAttUpdateDto>();
        CreateMap<CustomerGroupByAttCreateViewModel, CustomerGroupByAttCreateDto>();

        CreateMap<CustomerGroupByListDto, CustomerGroupByListUpdateViewModel>();
        CreateMap<CustomerGroupByListUpdateViewModel, CustomerGroupByListUpdateDto>();
        CreateMap<CustomerGroupByListCreateViewModel, CustomerGroupByListCreateDto>();

        CreateMap<CustomerGroupByGeoDto, CustomerGroupByGeoUpdateViewModel>();
        CreateMap<CustomerGroupByGeoUpdateViewModel, CustomerGroupByGeoUpdateDto>();
        CreateMap<CustomerGroupByGeoCreateViewModel, CustomerGroupByGeoCreateDto>();

        CreateMap<CustomerAssignmentDto, CustomerAssignmentUpdateViewModel>();
        CreateMap<CustomerAssignmentUpdateViewModel, CustomerAssignmentUpdateDto>();
        CreateMap<CustomerAssignmentCreateViewModel, CustomerAssignmentCreateDto>();

        CreateMap<HolidayDto, HolidayUpdateViewModel>();
        CreateMap<HolidayUpdateViewModel, HolidayUpdateDto>();
        CreateMap<HolidayCreateViewModel, HolidayCreateDto>();

        CreateMap<HolidayDetailDto, HolidayDetailUpdateViewModel>();
        CreateMap<HolidayDetailUpdateViewModel, HolidayDetailUpdateDto>();
        CreateMap<HolidayDetailCreateViewModel, HolidayDetailCreateDto>();

        CreateMap<RouteDto, RouteUpdateViewModel>();
        CreateMap<RouteUpdateViewModel, RouteUpdateDto>();
        CreateMap<RouteCreateViewModel, RouteCreateDto>();

        CreateMap<MCPHeaderDto, MCPHeaderUpdateViewModel>();
        CreateMap<MCPHeaderUpdateViewModel, MCPHeaderUpdateDto>();
        CreateMap<MCPHeaderCreateViewModel, MCPHeaderCreateDto>();

        CreateMap<MCPDetailDto, MCPDetailUpdateViewModel>();
        CreateMap<MCPDetailUpdateViewModel, MCPDetailUpdateDto>();
        CreateMap<MCPDetailCreateViewModel, MCPDetailCreateDto>();

        CreateMap<VisitPlanDto, VisitPlanUpdateViewModel>();
        CreateMap<VisitPlanUpdateViewModel, VisitPlanUpdateDto>();
        CreateMap<VisitPlanCreateViewModel, VisitPlanCreateDto>();

        CreateMap<RouteAssignmentDto, RouteAssignmentUpdateViewModel>();
        CreateMap<RouteAssignmentUpdateViewModel, RouteAssignmentUpdateDto>();
        CreateMap<RouteAssignmentCreateViewModel, RouteAssignmentCreateDto>();

        CreateMap<SalesChannelDto, SalesChannelUpdateViewModel>();
        CreateMap<SalesChannelUpdateViewModel, SalesChannelUpdateDto>();
        CreateMap<SalesChannelCreateViewModel, SalesChannelCreateDto>();

        CreateMap<EmployeeProfileDto, EmployeeProfileUpdateViewModel>();
        CreateMap<EmployeeProfileUpdateViewModel, EmployeeProfileUpdateDto>();
        CreateMap<EmployeeProfileCreateViewModel, EmployeeProfileCreateDto>();

        CreateMap<PriceListDto, PriceListUpdateViewModel>();
        CreateMap<PriceListUpdateViewModel, PriceListUpdateDto>();
        CreateMap<PriceListCreateViewModel, PriceListCreateDto>();

        CreateMap<PriceUpdateDetailDto, PriceUpdateDetailUpdateViewModel>();
        CreateMap<PriceUpdateDetailUpdateViewModel, PriceUpdateDetailUpdateDto>();
        CreateMap<PriceUpdateDetailCreateViewModel, PriceUpdateDetailCreateDto>();

        CreateMap<EmployeeImageDto, EmployeeImageUpdateViewModel>();
        CreateMap<EmployeeImageUpdateViewModel, EmployeeImageUpdateDto>();
        CreateMap<EmployeeImageCreateViewModel, EmployeeImageCreateDto>();

        CreateMap<EmployeeAttachmentDto, EmployeeAttachmentUpdateViewModel>();
        CreateMap<EmployeeAttachmentUpdateViewModel, EmployeeAttachmentUpdateDto>();
        CreateMap<EmployeeAttachmentCreateViewModel, EmployeeAttachmentCreateDto>();

        CreateMap<SalesOrgHeaderDto, SalesOrgHeaderUpdateViewModel>();
        CreateMap<SalesOrgHeaderUpdateViewModel, SalesOrgHeaderUpdateDto>();
        CreateMap<SalesOrgHeaderCreateViewModel, SalesOrgHeaderCreateDto>();

        CreateMap<SalesOrgHierarchyDto, SalesOrgHierarchyUpdateViewModel>();
        CreateMap<SalesOrgHierarchyUpdateViewModel, SalesOrgHierarchyUpdateDto>();
        CreateMap<SalesOrgHierarchyCreateViewModel, SalesOrgHierarchyCreateDto>();

        CreateMap<CusAttributeValueDto, CusAttributeValueUpdateViewModel>();
        CreateMap<CusAttributeValueUpdateViewModel, CusAttributeValueUpdateDto>();
        CreateMap<CusAttributeValueCreateViewModel, CusAttributeValueCreateDto>();

        CreateMap<CustomerProfileDto, CustomerProfileUpdateViewModel>();
        CreateMap<CustomerProfileUpdateViewModel, CustomerProfileUpdateDto>();
        CreateMap<CustomerProfileCreateViewModel, CustomerProfileCreateDto>();

        CreateMap<CustomerContactDto, CustomerContactUpdateViewModel>();
        CreateMap<CustomerContactUpdateViewModel, CustomerContactUpdateDto>();
        CreateMap<CustomerContactCreateViewModel, CustomerContactCreateDto>();

        CreateMap<CustomerAttachmentDto, CustomerAttachmentUpdateViewModel>();
        CreateMap<CustomerAttachmentUpdateViewModel, CustomerAttachmentUpdateDto>();
        CreateMap<CustomerAttachmentCreateViewModel, CustomerAttachmentCreateDto>();

        CreateMap<VendorDto, VendorUpdateViewModel>();
        CreateMap<VendorUpdateViewModel, VendorUpdateDto>();
        CreateMap<VendorCreateViewModel, VendorCreateDto>();
        CreateMap<CustomerContactDto, CustomerContactUpdateViewModel>();
        CreateMap<CustomerContactUpdateViewModel, CustomerContactUpdateDto>();
        CreateMap<CustomerContactCreateViewModel, CustomerContactCreateDto>();

        CreateMap<CustomerAttachmentDto, CustomerAttachmentUpdateViewModel>();
        CreateMap<CustomerAttachmentUpdateViewModel, CustomerAttachmentUpdateDto>();
        CreateMap<CustomerAttachmentCreateViewModel, CustomerAttachmentCreateDto>();

        CreateMap<VendorDto, VendorUpdateViewModel>();
        CreateMap<VendorUpdateViewModel, VendorUpdateDto>();
        CreateMap<VendorCreateViewModel, VendorCreateDto>();

        CreateMap<SystemConfigDto, SystemConfigUpdateViewModel>();
        CreateMap<SystemConfigUpdateViewModel, SystemConfigUpdateDto>();
        CreateMap<SystemConfigCreateViewModel, SystemConfigCreateDto>();

        CreateMap<CustomerDto, CustomerUpdateViewModel>();
        CreateMap<CustomerUpdateViewModel, CustomerUpdateDto>();
        CreateMap<CustomerCreateViewModel, CustomerCreateDto>();

        CreateMap<CompanyIdentityUserAssignmentDto, CompanyIdentityUserAssignmentUpdateViewModel>();
        CreateMap<CompanyIdentityUserAssignmentUpdateViewModel, CompanyIdentityUserAssignmentUpdateDto>();
        CreateMap<CompanyIdentityUserAssignmentCreateViewModel, CompanyIdentityUserAssignmentCreateDto>();

        CreateMap<ItemAttributeDto, ItemAttributeUpdateViewModel>();
        CreateMap<ItemAttributeUpdateViewModel, ItemAttributeUpdateDto>();
        CreateMap<ItemAttributeCreateViewModel, ItemAttributeCreateDto>();
        */
    }
}