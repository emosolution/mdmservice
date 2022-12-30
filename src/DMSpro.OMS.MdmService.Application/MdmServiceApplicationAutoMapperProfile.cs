using DMSpro.OMS.MdmService.Customers;
using DMSpro.OMS.MdmService.SystemConfigs;
using DMSpro.OMS.MdmService.Vendors;
using DMSpro.OMS.MdmService.CustomerAttachments;
using DMSpro.OMS.MdmService.CustomerContacts;
using DMSpro.OMS.MdmService.CusAttributeValues;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using DMSpro.OMS.MdmService.SalesOrgHeaders;
using DMSpro.OMS.MdmService.EmployeeAttachments;
using DMSpro.OMS.MdmService.EmployeeImages;
using DMSpro.OMS.MdmService.PriceUpdateDetails;
using DMSpro.OMS.MdmService.PriceLists;
using DMSpro.OMS.MdmService.EmployeeProfiles;
using DMSpro.OMS.MdmService.SalesChannels;
using DMSpro.OMS.MdmService.RouteAssignments;
using DMSpro.OMS.MdmService.VisitPlans;
using DMSpro.OMS.MdmService.MCPDetails;
using DMSpro.OMS.MdmService.MCPHeaders;
using DMSpro.OMS.MdmService.Routes;
using DMSpro.OMS.MdmService.HolidayDetails;
using DMSpro.OMS.MdmService.Holidays;
using DMSpro.OMS.MdmService.CustomerAssignments;
using DMSpro.OMS.MdmService.CustomerGroupByGeos;
using DMSpro.OMS.MdmService.CustomerGroupByLists;
using DMSpro.OMS.MdmService.CustomerGroupByAtts;
using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.CustomerAttributes;
using DMSpro.OMS.MdmService.EmployeeInZones;
using DMSpro.OMS.MdmService.CustomerInZones;
using DMSpro.OMS.MdmService.SSHistoryInZones;
using DMSpro.OMS.MdmService.CompanyInZones;
using DMSpro.OMS.MdmService.SalesOrgEmpAssignments;
using DMSpro.OMS.MdmService.WorkingPositions;
using DMSpro.OMS.MdmService.NumberingConfigs;
using DMSpro.OMS.MdmService.SystemDatas;
using DMSpro.OMS.MdmService.PricelistAssignments;
using DMSpro.OMS.MdmService.PriceUpdates;
using DMSpro.OMS.MdmService.PriceListDetails;
using DMSpro.OMS.MdmService.ItemGroupLists;
using DMSpro.OMS.MdmService.ItemGroupAttrs;
using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.ItemAttachments;
using DMSpro.OMS.MdmService.ItemImages;
using DMSpro.OMS.MdmService.ItemMasters;
using DMSpro.OMS.MdmService.ProdAttributeValues;
using DMSpro.OMS.MdmService.ProductAttributes;
using DMSpro.OMS.MdmService.UOMGroupDetails;
using DMSpro.OMS.MdmService.UOMGroups;
using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.VATs;
using DMSpro.OMS.MdmService.WeightMeasurements;
using DMSpro.OMS.MdmService.DimensionMeasurements;
using DMSpro.OMS.MdmService.Currencies;
using DMSpro.OMS.MdmService.Streets;
using DMSpro.OMS.MdmService.GeoMasters;
using System;
using DMSpro.OMS.MdmService.Shared;
using Volo.Abp.AutoMapper;
using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.Shared.Grpc;
using AutoMapper;

namespace DMSpro.OMS.MdmService;

public class MdmServiceApplicationAutoMapperProfile : Profile
{
    public MdmServiceApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
        * Alternatively, you can split your mapping configurations
        * into multiple profile classes for a better organization. */

        CreateMap<Company, CompanyDto>();
        CreateMap<Company, CompanyGrpcDto>();
        CreateMap<Company, CompanyExcelDto>();

        CreateMap<GeoMaster, GeoMasterDto>();
        CreateMap<GeoMaster, GeoMasterExcelDto>();

        CreateMap<Street, StreetDto>();
        CreateMap<Street, StreetExcelDto>();

        CreateMap<Currency, CurrencyDto>();
        CreateMap<Currency, CurrencyExcelDto>();

        CreateMap<DimensionMeasurement, DimensionMeasurementDto>();
        CreateMap<DimensionMeasurement, DimensionMeasurementExcelDto>();

        CreateMap<WeightMeasurement, WeightMeasurementDto>();
        CreateMap<WeightMeasurement, WeightMeasurementExcelDto>();

        CreateMap<VAT, VATDto>();
        CreateMap<VAT, VATExcelDto>();

        CreateMap<UOM, UOMDto>();
        CreateMap<UOM, UOMExcelDto>();

        CreateMap<UOMGroup, UOMGroupDto>();
        CreateMap<UOMGroup, UOMGroupExcelDto>();

        CreateMap<UOMGroupDetail, UOMGroupDetailDto>();
        CreateMap<UOMGroupDetail, UOMGroupDetailExcelDto>();

        CreateMap<ProductAttribute, ProductAttributeDto>();
        CreateMap<ProductAttribute, ProductAttributeExcelDto>();

        CreateMap<ProdAttributeValue, ProdAttributeValueDto>();
        CreateMap<ProdAttributeValue, ProdAttributeValueExcelDto>();

        CreateMap<ItemMaster, ItemMasterDto>();
        CreateMap<ItemMaster, ItemMasterExcelDto>();

        CreateMap<ItemImage, ItemImageDto>();
        CreateMap<ItemImage, ItemImageExcelDto>();

        CreateMap<ItemAttachment, ItemAttachmentDto>();
        CreateMap<ItemAttachment, ItemAttachmentExcelDto>();

        CreateMap<ItemGroup, ItemGroupDto>();
        CreateMap<ItemGroup, ItemGroupExcelDto>();

        CreateMap<ItemGroupAttr, ItemGroupAttrDto>();
        CreateMap<ItemGroupAttr, ItemGroupAttrExcelDto>();

        CreateMap<ItemGroupList, ItemGroupListDto>();
        CreateMap<ItemGroupList, ItemGroupListExcelDto>();

        CreateMap<PriceListDetail, PriceListDetailDto>();
        CreateMap<PriceListDetail, PriceListDetailExcelDto>();

        CreateMap<PriceUpdate, PriceUpdateDto>();
        CreateMap<PriceUpdate, PriceUpdateExcelDto>();

        CreateMap<PricelistAssignment, PricelistAssignmentDto>();
        CreateMap<PricelistAssignment, PricelistAssignmentExcelDto>();

        CreateMap<SystemData, SystemDataDto>();
        CreateMap<SystemData, SystemDataExcelDto>();

        CreateMap<NumberingConfig, NumberingConfigDto>();
        CreateMap<NumberingConfig, NumberingConfigExcelDto>();

        CreateMap<WorkingPosition, WorkingPositionDto>();
        CreateMap<WorkingPosition, WorkingPositionExcelDto>();

        CreateMap<SalesOrgEmpAssignment, SalesOrgEmpAssignmentDto>();
        CreateMap<SalesOrgEmpAssignment, SalesOrgEmpAssignmentExcelDto>();

        CreateMap<CompanyInZone, CompanyInZoneDto>();
        CreateMap<CompanyInZone, CompanyInZoneExcelDto>();

        CreateMap<SSHistoryInZone, SSHistoryInZoneDto>();
        CreateMap<SSHistoryInZone, SSHistoryInZoneExcelDto>();

        CreateMap<CustomerInZone, CustomerInZoneDto>();
        CreateMap<CustomerInZone, CustomerInZoneExcelDto>();

        CreateMap<EmployeeInZone, EmployeeInZoneDto>();
        CreateMap<EmployeeInZone, EmployeeInZoneExcelDto>();

        CreateMap<CustomerAttribute, CustomerAttributeDto>();
        CreateMap<CustomerAttribute, CustomerAttributeExcelDto>();

        CreateMap<CustomerGroup, CustomerGroupDto>();
        CreateMap<CustomerGroup, CustomerGroupExcelDto>();

        CreateMap<CustomerGroupByAtt, CustomerGroupByAttDto>();
        CreateMap<CustomerGroupByAtt, CustomerGroupByAttExcelDto>();

        CreateMap<CustomerGroupByList, CustomerGroupByListDto>();
        CreateMap<CustomerGroupByList, CustomerGroupByListExcelDto>();

        CreateMap<CustomerGroupByGeo, CustomerGroupByGeoDto>();
        CreateMap<CustomerGroupByGeo, CustomerGroupByGeoExcelDto>();

        CreateMap<CustomerAssignment, CustomerAssignmentDto>();
        CreateMap<CustomerAssignment, CustomerAssignmentExcelDto>();

        CreateMap<Holiday, HolidayDto>();
        CreateMap<Holiday, HolidayExcelDto>();

        CreateMap<HolidayDetail, HolidayDetailDto>();
        CreateMap<HolidayDetail, HolidayDetailExcelDto>();

        CreateMap<Route, RouteDto>();
        CreateMap<Route, RouteExcelDto>();

        CreateMap<MCPHeader, MCPHeaderDto>();
        CreateMap<MCPHeader, MCPHeaderExcelDto>();

        CreateMap<MCPDetail, MCPDetailDto>();
        CreateMap<MCPDetail, MCPDetailExcelDto>();

        CreateMap<VisitPlan, VisitPlanDto>();
        CreateMap<VisitPlan, VisitPlanExcelDto>();

        CreateMap<RouteAssignment, RouteAssignmentDto>();
        CreateMap<RouteAssignment, RouteAssignmentExcelDto>();

        CreateMap<GeoMasterWithNavigationProperties, GeoMasterWithNavigationPropertiesDto>();
        CreateMap<GeoMaster, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<CompanyWithNavigationProperties, CompanyWithNavigationPropertiesDto>();
        CreateMap<Company, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<GeoMaster, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<SalesChannel, SalesChannelDto>();
        CreateMap<SalesChannel, SalesChannelExcelDto>();

        CreateMap<UOMGroupDetailWithNavigationProperties, UOMGroupDetailWithNavigationPropertiesDto>();
        CreateMap<UOMGroup, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));
        CreateMap<UOM, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));
        CreateMap<UOM, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<ProdAttributeValueWithNavigationProperties, ProdAttributeValueWithNavigationPropertiesDto>();
        CreateMap<ProductAttribute, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AttrName));
        CreateMap<ProdAttributeValue, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AttrValName));

        CreateMap<ItemMasterWithNavigationProperties, ItemMasterWithNavigationPropertiesDto>();
        CreateMap<SystemData, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.ValueCode));
        CreateMap<VAT, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<ItemImageWithNavigationProperties, ItemImageWithNavigationPropertiesDto>();
        CreateMap<ItemMaster, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<ItemAttachmentWithNavigationProperties, ItemAttachmentWithNavigationPropertiesDto>();
        CreateMap<ItemMaster, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<ItemGroupListWithNavigationProperties, ItemGroupListWithNavigationPropertiesDto>();
        CreateMap<ItemGroup, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<ItemGroupAttrWithNavigationProperties, ItemGroupAttrWithNavigationPropertiesDto>();

        CreateMap<EmployeeProfile, EmployeeProfileDto>();
        CreateMap<EmployeeProfile, EmployeeProfileExcelDto>();
        CreateMap<EmployeeProfileWithNavigationProperties, EmployeeProfileWithNavigationPropertiesDto>();
        CreateMap<WorkingPosition, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<SystemData, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.ValueCode));

        CreateMap<PriceList, PriceListDto>();
        CreateMap<PriceList, PriceListExcelDto>();

        CreateMap<PriceListWithNavigationProperties, PriceListWithNavigationPropertiesDto>();
        CreateMap<PriceList, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<PriceListDetailWithNavigationProperties, PriceListDetailWithNavigationPropertiesDto>();
        CreateMap<PriceList, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<PriceUpdateWithNavigationProperties, PriceUpdateWithNavigationPropertiesDto>();

        CreateMap<PriceUpdateDetail, PriceUpdateDetailDto>();
        CreateMap<PriceUpdateDetail, PriceUpdateDetailExcelDto>();
        CreateMap<PriceUpdateDetailWithNavigationProperties, PriceUpdateDetailWithNavigationPropertiesDto>();
        CreateMap<PriceUpdate, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));
        CreateMap<PriceListDetail, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Description));

        CreateMap<PricelistAssignmentWithNavigationProperties, PricelistAssignmentWithNavigationPropertiesDto>();
        CreateMap<CustomerGroup, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<EmployeeImage, EmployeeImageDto>();
        CreateMap<EmployeeImage, EmployeeImageExcelDto>();
        CreateMap<EmployeeImageWithNavigationProperties, EmployeeImageWithNavigationPropertiesDto>();
        CreateMap<EmployeeProfile, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<EmployeeAttachment, EmployeeAttachmentDto>();
        CreateMap<EmployeeAttachment, EmployeeAttachmentExcelDto>();
        CreateMap<EmployeeAttachmentWithNavigationProperties, EmployeeAttachmentWithNavigationPropertiesDto>();

        CreateMap<SalesOrgHeader, SalesOrgHeaderDto>();
        CreateMap<SalesOrgHeader, SalesOrgHeaderExcelDto>();

        CreateMap<SalesOrgHierarchy, SalesOrgHierarchyDto>();
        CreateMap<SalesOrgHierarchy, SalesOrgHierarchyExcelDto>();
        CreateMap<SalesOrgHierarchyWithNavigationProperties, SalesOrgHierarchyWithNavigationPropertiesDto>();
        CreateMap<SalesOrgHeader, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<SalesOrgHierarchy, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<SalesOrgEmpAssignmentWithNavigationProperties, SalesOrgEmpAssignmentWithNavigationPropertiesDto>();
        CreateMap<SalesOrgHierarchy, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<SSHistoryInZoneWithNavigationProperties, SSHistoryInZoneWithNavigationPropertiesDto>();

        CreateMap<CompanyInZoneWithNavigationProperties, CompanyInZoneWithNavigationPropertiesDto>();
        CreateMap<Company, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<EmployeeInZoneWithNavigationProperties, EmployeeInZoneWithNavigationPropertiesDto>();

        CreateMap<CusAttributeValue, CusAttributeValueDto>();
        CreateMap<CusAttributeValue, CusAttributeValueExcelDto>();
        CreateMap<CusAttributeValueWithNavigationProperties, CusAttributeValueWithNavigationPropertiesDto>();
        CreateMap<CustomerAttribute, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AttrName));

        CreateMap<CusAttributeValue, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AttrValName));

        CreateMap<Customer, CustomerDto>();
        CreateMap<Customer, CustomerExcelDto>();
        CreateMap<CustomerWithNavigationProperties, CustomerWithNavigationPropertiesDto>();
        CreateMap<Customer, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<CustomerInZoneWithNavigationProperties, CustomerInZoneWithNavigationPropertiesDto>();
        CreateMap<Customer, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<CustomerContact, CustomerContactDto>();
        CreateMap<CustomerContact, CustomerContactExcelDto>();
        CreateMap<CustomerContactWithNavigationProperties, CustomerContactWithNavigationPropertiesDto>();

        CreateMap<CustomerAttachment, CustomerAttachmentDto>();
        CreateMap<CustomerAttachment, CustomerAttachmentExcelDto>();
        CreateMap<CustomerAttachmentWithNavigationProperties, CustomerAttachmentWithNavigationPropertiesDto>();

        CreateMap<Vendor, VendorDto>();
        CreateMap<Vendor, VendorExcelDto>();
        CreateMap<VendorWithNavigationProperties, VendorWithNavigationPropertiesDto>();

        CreateMap<CustomerAssignmentWithNavigationProperties, CustomerAssignmentWithNavigationPropertiesDto>();

        CreateMap<CustomerGroupByAttWithNavigationProperties, CustomerGroupByAttWithNavigationPropertiesDto>();

        CreateMap<CustomerGroupByGeoWithNavigationProperties, CustomerGroupByGeoWithNavigationPropertiesDto>();

        CreateMap<CustomerGroupByListWithNavigationProperties, CustomerGroupByListWithNavigationPropertiesDto>();

        CreateMap<CusAttributeValue, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AttrValName));

        CreateMap<HolidayDetailWithNavigationProperties, HolidayDetailWithNavigationPropertiesDto>();
        CreateMap<Holiday, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Description));

        CreateMap<RouteWithNavigationProperties, RouteWithNavigationPropertiesDto>();

        CreateMap<MCPHeaderWithNavigationProperties, MCPHeaderWithNavigationPropertiesDto>();

        CreateMap<MCPDetailWithNavigationProperties, MCPDetailWithNavigationPropertiesDto>();

        CreateMap<MCPHeader, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<VisitPlanWithNavigationProperties, VisitPlanWithNavigationPropertiesDto>();
        CreateMap<MCPDetail, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<RouteAssignmentWithNavigationProperties, RouteAssignmentWithNavigationPropertiesDto>();

        CreateMap<NumberingConfigWithNavigationProperties, NumberingConfigWithNavigationPropertiesDto>();
        CreateMap<SystemData, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<SystemConfig, SystemConfigDto>();
        CreateMap<SystemConfig, SystemConfigExcelDto>();
    }
}