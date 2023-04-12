using DMSpro.OMS.MdmService.CustomerGroupAttributes;
using DMSpro.OMS.MdmService.CustomerAttributeValues;
using DMSpro.OMS.MdmService.CustomerGroupGeos;
using DMSpro.OMS.MdmService.CustomerGroupLists;
using DMSpro.OMS.MdmService.NumberingConfigDetails;
using DMSpro.OMS.MdmService.CustomerImages;
using Volo.Abp.AutoMapper;
using System;
using AutoMapper;
using DMSpro.OMS.MdmService.ItemGroupLists;
using DMSpro.OMS.MdmService.ItemAttachments;
using DMSpro.OMS.MdmService.ItemImages;
using DMSpro.OMS.MdmService.Items;
using DMSpro.OMS.MdmService.ItemGroupAttributes;
using DMSpro.OMS.MdmService.ItemAttributeValues;
using DMSpro.OMS.MdmService.ItemAttributes;
using DMSpro.OMS.MdmService.CompanyIdentityUserAssignments;
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
using DMSpro.OMS.MdmService.VisitPlans;
using DMSpro.OMS.MdmService.MCPDetails;
using DMSpro.OMS.MdmService.MCPHeaders;
using DMSpro.OMS.MdmService.HolidayDetails;
using DMSpro.OMS.MdmService.Holidays;
using DMSpro.OMS.MdmService.CustomerAssignments;
using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.CustomerAttributes;
using DMSpro.OMS.MdmService.CustomerInZones;
using DMSpro.OMS.MdmService.CompanyInZones;
using DMSpro.OMS.MdmService.SalesOrgEmpAssignments;
using DMSpro.OMS.MdmService.WorkingPositions;
using DMSpro.OMS.MdmService.NumberingConfigs;
using DMSpro.OMS.MdmService.SystemDatas;
using DMSpro.OMS.MdmService.PricelistAssignments;
using DMSpro.OMS.MdmService.PriceUpdates;
using DMSpro.OMS.MdmService.PriceListDetails;
using DMSpro.OMS.MdmService.ItemGroups;
using DMSpro.OMS.MdmService.UOMGroupDetails;
using DMSpro.OMS.MdmService.UOMGroups;
using DMSpro.OMS.MdmService.UOMs;
using DMSpro.OMS.MdmService.VATs;
using DMSpro.OMS.MdmService.WeightMeasurements;
using DMSpro.OMS.MdmService.DimensionMeasurements;
using DMSpro.OMS.MdmService.Currencies;
using DMSpro.OMS.MdmService.Streets;
using DMSpro.OMS.MdmService.GeoMasters;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.Companies;

namespace DMSpro.OMS.MdmService;

public class MdmServiceApplicationAutoMapperProfile : Profile
{
    public MdmServiceApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
        * Alternatively, you can split your mapping configurations
        * into multiple profile classes for a better organization. */

        CreateMap<Company, CompanyDto>();
        CreateMap<Company, CompanyExcelDto>();
        CreateMap<Company, CompanyWithTenantDto>();
        CreateMap<Company, CompanyWithDetailsDto>();

        CreateMap<GeoMaster, GeoMasterDto>();
        CreateMap<GeoMaster, GeoMasterExcelDto>();
        CreateMap<GeoMaster, GeoMasterWithDetailsDto>();

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
        CreateMap<UOMGroup, UOMGroupWithDetailsDto>();
        CreateMap<UOMGroupDetailWithNavigationProperties, UOMGroupDetailWithNavigationPropertiesDto>();
        CreateMap<UOMGroup, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));
        CreateMap<UOM, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));
        CreateMap<UOM, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<UOMGroupDetail, UOMGroupDetailDto>();
        CreateMap<UOMGroupDetail, UOMGroupDetailWithDetailsDto>();
        CreateMap<UOMGroupDetail, UOMGroupDetailExcelDto>();

        CreateMap<ItemGroup, ItemGroupDto>();

        CreateMap<PriceListDetail, PriceListDetailDto>();
        CreateMap<PriceListDetail, PriceListDetailWithDetailsDto>();
        CreateMap<PriceListDetail, PriceListDetailExcelDto>();

        CreateMap<PriceUpdate, PriceUpdateDto>();
        CreateMap<PriceUpdate, PriceUpdateWithDetailsDto>();
        CreateMap<PriceUpdate, PriceUpdateExcelDto>();

        CreateMap<PricelistAssignment, PricelistAssignmentDto>();
        CreateMap<PricelistAssignment, PricelistAssignmentWithDetailsDto>();

        CreateMap<SystemData, SystemDataDto>();
        CreateMap<SystemData, SystemDataExcelDto>();

        CreateMap<NumberingConfig, NumberingConfigDto>();
        CreateMap<NumberingConfig, NumberingConfigWithDetailsDto>();

        CreateMap<WorkingPosition, WorkingPositionDto>();
        CreateMap<WorkingPosition, WorkingPositionExcelDto>();

        CreateMap<SalesOrgEmpAssignment, SalesOrgEmpAssignmentDto>();
        CreateMap<SalesOrgEmpAssignment, SalesOrgEmpAssignmentWithDetailsDto>();
        CreateMap<SalesOrgEmpAssignment, SalesOrgEmpAssignmentExcelDto>();

        CreateMap<CompanyInZone, CompanyInZoneDto>();
        CreateMap<CompanyInZone, CompanyInZoneWithDetailsDto>();

        CreateMap<CustomerInZone, CustomerInZoneDto>();
        CreateMap<CustomerInZone, CustomerInZoneWithDetailsDto>();

        CreateMap<CustomerAttribute, CustomerAttributeDto>();

        CreateMap<CustomerGroup, CustomerGroupDto>();

        CreateMap<CustomerAssignment, CustomerAssignmentDto>();
        CreateMap<CustomerAssignment, CustomerAssignmentWithDetailsDto>();
        CreateMap<CustomerAssignment, CustomerAssignmentExcelDto>();

        CreateMap<Holiday, HolidayDto>();
        CreateMap<Holiday, HolidayExcelDto>();

        CreateMap<HolidayDetail, HolidayDetailDto>();
        CreateMap<HolidayDetail, HolidayDetailWithDetailsDto>();
        CreateMap<HolidayDetail, HolidayDetailExcelDto>();

        CreateMap<MCPHeader, MCPHeaderDto>();
        CreateMap<MCPHeader, MCPHeaderExcelDto>();
        CreateMap<MCPHeader, MCPHeaderWithDetailsDto>();

        CreateMap<MCPDetail, MCPDetailDto>();
        CreateMap<MCPDetail, MCPDetailWithDetailsDto>();
        CreateMap<MCPDetail, MCPDetailExcelDto>();

        CreateMap<VisitPlan, VisitPlanDto>();
        CreateMap<VisitPlan, VisitPlanWithDetailsDto>();

        CreateMap<GeoMasterWithNavigationProperties, GeoMasterWithNavigationPropertiesDto>();
        CreateMap<GeoMaster, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<CompanyWithNavigationProperties, CompanyWithNavigationPropertiesDto>();
        CreateMap<Company, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<GeoMaster, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<SystemData, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.ValueCode));
        CreateMap<VAT, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<ItemGroup, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<EmployeeProfile, EmployeeProfileDto>();
        CreateMap<EmployeeProfile, EmployeeProfileWithDetailsDto>();
        CreateMap<EmployeeProfile, EmployeeProfileWithAvatarDto>().Ignore(x => x.Avatar);
        CreateMap<WorkingPosition, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<SystemData, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.ValueCode));

        CreateMap<PriceList, PriceListDto>();
        CreateMap<PriceList, PriceListWithDetailsDto>();

        CreateMap<PriceList, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<PriceListDetailWithNavigationProperties, PriceListDetailWithNavigationPropertiesDto>();
        CreateMap<PriceList, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<PriceUpdateWithNavigationProperties, PriceUpdateWithNavigationPropertiesDto>();

        CreateMap<PriceUpdateDetail, PriceUpdateDetailDto>();
        CreateMap<PriceUpdateDetail, PriceUpdateDetailWithDetailsDto>();
        CreateMap<PriceUpdateDetail, PriceUpdateDetailExcelDto>();
        CreateMap<PriceUpdateDetailWithNavigationProperties, PriceUpdateDetailWithNavigationPropertiesDto>();
        CreateMap<PriceUpdate, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));
        CreateMap<PriceListDetail, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Description));

        CreateMap<CustomerGroup, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<EmployeeImage, EmployeeImageDto>();
        CreateMap<EmployeeImage, EmployeeImageWithDetailsDto>();
        CreateMap<EmployeeImage, EmployeeImageExcelDto>();
        CreateMap<EmployeeImageWithNavigationProperties, EmployeeImageWithNavigationPropertiesDto>();
        CreateMap<EmployeeProfile, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<EmployeeAttachment, EmployeeAttachmentDto>();
        CreateMap<EmployeeAttachment, EmployeeAttachmentWithDetailsDto>();
        CreateMap<EmployeeAttachment, EmployeeAttachmentExcelDto>();
        CreateMap<EmployeeAttachmentWithNavigationProperties, EmployeeAttachmentWithNavigationPropertiesDto>();

        CreateMap<SalesOrgHeader, SalesOrgHeaderDto>();

        CreateMap<SalesOrgHierarchy, SalesOrgHierarchyDto>();
        CreateMap<SalesOrgHierarchy, SalesOrgHierarchyWithDetailsDto>();
        CreateMap<SalesOrgHeader, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<SalesOrgHierarchy, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<SalesOrgEmpAssignmentWithNavigationProperties, SalesOrgEmpAssignmentWithNavigationPropertiesDto>();
        CreateMap<SalesOrgHierarchy, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<Company, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<CusAttributeValue, CusAttributeValueDto>();
        CreateMap<CusAttributeValue, CusAttributeValueExcelDto>();
        CreateMap<CusAttributeValue, CusAttributeValueWithDetailsDto>();
        CreateMap<CusAttributeValueWithNavigationProperties, CusAttributeValueWithNavigationPropertiesDto>();
        CreateMap<CustomerAttribute, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AttrName));

        CreateMap<CusAttributeValue, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AttrValName));

        CreateMap<Customer, CustomerDto>();
        CreateMap<Customer, CustomerWithDetailsDto>();
        CreateMap<Customer, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<Customer, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<CustomerContact, CustomerContactDto>();
        CreateMap<CustomerContact, CustomerContactExcelDto>();
        CreateMap<CustomerContact, CustomerContactWithDetailsDto>();
        CreateMap<CustomerContactWithNavigationProperties, CustomerContactWithNavigationPropertiesDto>();

        CreateMap<CustomerAttachment, CustomerAttachmentDto>();
        CreateMap<CustomerAttachment, CustomerAttachmentExcelDto>();
        CreateMap<CustomerAttachment, CustomerAttachmentWithDetailsDto>();
        CreateMap<CustomerAttachmentWithNavigationProperties, CustomerAttachmentWithNavigationPropertiesDto>();

        CreateMap<Vendor, VendorDto>();
        CreateMap<Vendor, VendorWithDetailsDto>();

        CreateMap<CustomerAssignmentWithNavigationProperties, CustomerAssignmentWithNavigationPropertiesDto>();

        CreateMap<CusAttributeValue, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AttrValName));

        CreateMap<HolidayDetailWithNavigationProperties, HolidayDetailWithNavigationPropertiesDto>();
        CreateMap<Holiday, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Description));

        CreateMap<MCPHeaderWithNavigationProperties, MCPHeaderWithNavigationPropertiesDto>();

        CreateMap<MCPDetailWithNavigationProperties, MCPDetailWithNavigationPropertiesDto>();

        CreateMap<MCPHeader, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<MCPDetail, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<SystemData, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<SystemConfig, SystemConfigDto>();
        CreateMap<SystemConfig, SystemConfigExcelDto>();

        CreateMap<CompanyIdentityUserAssignment, CompanyIdentityUserAssignmentDto>();
        CreateMap<CompanyIdentityUserAssignment, CompanyIdentityUserAssignmentExcelDto>();
        CreateMap<CompanyIdentityUserAssignmentWithNavigationProperties, CompanyIdentityUserAssignmentWithNavigationPropertiesDto>();

        CreateMap<ItemAttribute, ItemAttributeDto>();

        CreateMap<ItemAttributeValue, ItemAttributeValueDto>();
        CreateMap<ItemAttributeValue, ItemAttributeValueWithDetailsDto>();
        CreateMap<ItemAttribute, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AttrName));

        CreateMap<ItemAttributeValue, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AttrValName));

        CreateMap<ItemGroupAttribute, ItemGroupAttributeDto>();
        CreateMap<ItemGroupAttribute, ItemGroupAttributeWithDetailsDto>();

        CreateMap<Item, ItemDto>();
        CreateMap<Item, ItemWithDetailsDto>();
        CreateMap<SystemData, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));
        CreateMap<UOMGroupDetail, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Id));
        CreateMap<UOMGroupDetail, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Id));
        CreateMap<UOMGroupDetail, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Id));

        CreateMap<Item, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<ItemImage, ItemImageDto>();
        CreateMap<ItemImage, ItemImageWithDetailsDto>();
        CreateMap<ItemImage, ItemImageExcelDto>();
        CreateMap<ItemImageWithNavigationProperties, ItemImageWithNavigationPropertiesDto>();

        CreateMap<ItemAttachment, ItemAttachmentDto>();
        CreateMap<ItemAttachment, ItemAttachmentWithDetailsDto>();
        CreateMap<ItemAttachment, ItemAttachmentExcelDto>();
        CreateMap<ItemAttachmentWithNavigationProperties, ItemAttachmentWithNavigationPropertiesDto>();

        CreateMap<ItemGroupList, ItemGroupListDto>();
        CreateMap<ItemGroupList, ItemGroupListWithDetailsDto>();

        CreateMap<ItemGroup, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<ItemAttributeValue, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AttrValName));

        CreateMap<WorkingPosition, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Code));

        CreateMap<CustomerImage, CustomerImageDto>();
        CreateMap<CustomerImage, CustomerImageWithDetailsDto>();
        CreateMap<CustomerImage, CustomerImageExcelDto>();
        CreateMap<CustomerImageWithNavigationProperties, CustomerImageWithNavigationPropertiesDto>();

        CreateMap<NumberingConfigDetail, NumberingConfigDetailDto>().Ignore(x => x.SuggestedCode);
        CreateMap<NumberingConfigDetail, NumberingConfigDetailWithDetailsDto>();
        CreateMap<NumberingConfig, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Description));

        CreateMap<CustomerGroupList, CustomerGroupListDto>();
        CreateMap<CustomerGroupList, CustomerGroupListWithDetailsDto>();

        CreateMap<CustomerGroupGeo, CustomerGroupGeoDto>();
        CreateMap<CustomerGroupGeo, CustomerGroupGeoWithDetailsDto>();

        CreateMap<CustomerAttributeValue, CustomerAttributeValueDto>();
        CreateMap<CustomerAttributeValue, CustomerAttributeValueWithDetailsDto>();

        CreateMap<CustomerAttributeValue, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AttrValName));

        CreateMap<CustomerGroupAttribute, CustomerGroupAttributeDto>();
        CreateMap<CustomerGroupAttribute, CustomerGroupAttributeWithDetailsDto>();
    }
}