using AutoMapper.Internal.Mappers;
using DMSpro.OMS.MdmService.CustomerAttributes;
using DMSpro.OMS.MdmService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;
using Volo.Saas;
using Volo.Saas.Tenants;
using DMSpro.OMS.MdmService.SystemDatas;
using DMSpro.OMS.MdmService.ItemAttributes;

namespace DMSpro.OMS.MdmService;
public class MdmServiceDistributedEventHandler : IDistributedEventHandler<TenantCreatedEto>, ITransientDependency
{
    private static int ITEM_ATTRIBUTE_ROWS = 20;
    private static int CUSTOMER_ATTRIBUTE_ROWS = 20;

    //private readonly ICurrentTenant _currentTenant;
    //private readonly ILogger<MdmServiceDistributedEventHandler> _logger;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    private readonly IItemAttributeCustomRepository _itemAttributeCustomRepository;
    private readonly ICustomerAttributeRepository _customerAttributeRepository;
    private readonly IGuidGenerator _guidGenerator;

    private readonly ISystemDataRepository _systemDataRepository;

    public MdmServiceDistributedEventHandler(
        IItemAttributeCustomRepository itemAttributeCustomRepository,
        ICustomerAttributeRepository customerAttributeRepository,
        //ICurrentTenant currentTenant,
        //ILogger<MdmServiceDistributedEventHandler> logger,
        IGuidGenerator guidGenerator,
        IUnitOfWorkManager unitOfWorkManager,
        ISystemDataRepository systemDataRepository
        )
    {
        _itemAttributeCustomRepository = itemAttributeCustomRepository;
        _customerAttributeRepository = customerAttributeRepository;

        //_currentTenant = currentTenant;
        //_logger = logger;
        _guidGenerator = guidGenerator;
        _unitOfWorkManager = unitOfWorkManager;
        //SystemData
        _systemDataRepository = systemDataRepository;
    }

    public async Task HandleEventAsync(TenantCreatedEto eventData)
    {
        try
        {
            var abpUnitOfWorkOptions = new AbpUnitOfWorkOptions { IsTransactional = true };
            using var uow = _unitOfWorkManager.Begin(abpUnitOfWorkOptions, true);
            List<ItemAttribute> seedProductAttributes = new List<ItemAttribute>();
            for (int i = 0; i < ITEM_ATTRIBUTE_ROWS; i++)
            {
                short AttrNo = (short)i;
                string AttrName = "Attribute " + i;
                Guid id = _guidGenerator.Create();
                ItemAttribute data = new ItemAttribute(id, AttrNo, AttrName, false, false, null);
                data.TenantId = eventData.Id;
                seedProductAttributes.Add(data);
            }
            await _itemAttributeCustomRepository.CreateWithExcepAsync(seedProductAttributes);

            List<CustomerAttribute> seedCustomerAttributes= new List<CustomerAttribute>();
            for (int i = 0; i < CUSTOMER_ATTRIBUTE_ROWS; i++)
            {
                short AttrNo = (short)i;
                string AttrName = "Attribute " + i;
                Guid id = _guidGenerator.Create();
                CustomerAttribute data = new CustomerAttribute(id, AttrNo, AttrName, false, null);
                data.TenantId = eventData.Id;
                seedCustomerAttributes.Add(data);
            }
            await _customerAttributeRepository.CreateWithExcepAsync(seedCustomerAttributes);

            //Seed ItemType - MD02
            var systemDefaultData = new List<SystemData> { 
                new SystemData { Code = "MD02", ValueCode = "I", ValueName = "Item", TenantId = eventData.Id },
                new SystemData { Code = "MD02", ValueCode = "P", ValueName = "POSM", TenantId = eventData.Id },
                new SystemData { Code = "MD02", ValueCode = "D", ValueName = "Discount", TenantId = eventData.Id },
                new SystemData { Code = "MD02", ValueCode = "V", ValueName = "Voucher", TenantId = eventData.Id },

                //MD03
                new SystemData { Code = "MD03", ValueCode = "SM", ValueName = "Salesman", TenantId = eventData.Id },
                new SystemData { Code = "MD03", ValueCode = "DM", ValueName = "Deliveryman", TenantId = eventData.Id },
                new SystemData { Code = "MD03", ValueCode = "SUP", ValueName = "Supervisor", TenantId = eventData.Id },
                new SystemData { Code = "MD03", ValueCode = "PG", ValueName = "Promotion Girl", TenantId = eventData.Id },

                //MD04 - CustomerType
                new SystemData { Code = "MD04", ValueCode = "CU", ValueName = "Customer", TenantId = eventData.Id },
                new SystemData { Code = "MD04", ValueCode = "VE", ValueName = "Vendor", TenantId = eventData.Id },
                //MD05 - Payment Term
                new SystemData { Code = "MD05", ValueCode = "5", ValueName = "5 Ngay", TenantId = eventData.Id },
                new SystemData { Code = "MD05", ValueCode = "7", ValueName = "7 Ngay", TenantId = eventData.Id },
                new SystemData { Code = "MD05", ValueCode = "10", ValueName = "10 Ngay", TenantId = eventData.Id },
                new SystemData { Code = "MD05", ValueCode = "15", ValueName = "15 Ngay", TenantId = eventData.Id },
                new SystemData { Code = "MD05", ValueCode = "30", ValueName = "30 Ngay", TenantId = eventData.Id },
                new SystemData { Code = "MD05", ValueCode = "45", ValueName = "45 Ngay", TenantId = eventData.Id },
                new SystemData { Code = "MD05", ValueCode = "60", ValueName = "60 Ngay", TenantId = eventData.Id },

                //MD06
                //MD05 - Payment Term
                new SystemData { Code = "MD06", ValueCode = "1", ValueName = "Sales", TenantId = eventData.Id },
                new SystemData { Code = "MD06", ValueCode = "2", ValueName = "Delivery", TenantId = eventData.Id },
                new SystemData { Code = "MD06", ValueCode = "3", ValueName = "Display", TenantId = eventData.Id },
                new SystemData { Code = "MD06", ValueCode = "4", ValueName = "Audit", TenantId = eventData.Id },

                //MD07 Item Group Applicable 
                new SystemData { Code = "MD07", ValueCode = "1", ValueName = "KPI", TenantId = eventData.Id },
                new SystemData { Code = "MD07", ValueCode = "2", ValueName = "Promotion (On/Off)", TenantId = eventData.Id },
                new SystemData { Code = "MD07", ValueCode = "3", ValueName = "Stock Count", TenantId = eventData.Id },

                //MD08 Working Position
                new SystemData { Code = "MD08", ValueCode = "NSM", ValueName = "National Sales Manager", TenantId = eventData.Id },
                new SystemData { Code = "MD08", ValueCode = "RSM", ValueName = "Regional Sales Manager", TenantId = eventData.Id },
                new SystemData { Code = "MD08", ValueCode = "ASM", ValueName = "Area Sales Manager", TenantId = eventData.Id },
                new SystemData { Code = "MD08", ValueCode = "SS", ValueName = "Sales Supervisor", TenantId = eventData.Id },
                new SystemData { Code = "MD08", ValueCode = "KAM", ValueName = "Key Account Manager", TenantId = eventData.Id },

                //MD09 Sale Material Type
                new SystemData { Code = "MD09", ValueCode = "01", ValueName = "Sales", TenantId = eventData.Id },
                new SystemData { Code = "MD09", ValueCode = "02", ValueName = "Training", TenantId = eventData.Id },

                //IN01 Inventory Transaction Type
                new SystemData { Code = "IN01", ValueCode = "1", ValueName = "Item", TenantId = eventData.Id },
                new SystemData { Code = "IN01", ValueCode = "2", ValueName = "Promotion", TenantId = eventData.Id },
                new SystemData { Code = "IN01", ValueCode = "3", ValueName = "Sampling", TenantId = eventData.Id },
                new SystemData { Code = "IN01", ValueCode = "4", ValueName = "Incentive", TenantId = eventData.Id },

                //IN02 Good Receipt Reason
                new SystemData { Code = "IN02", ValueCode = "1", ValueName = "Purchase", TenantId = eventData.Id },
                new SystemData { Code = "IN02", ValueCode = "2", ValueName = "Adjustment", TenantId = eventData.Id },
                new SystemData { Code = "IN02", ValueCode = "3", ValueName = "Initiate Stock", TenantId = eventData.Id },
                new SystemData { Code = "IN02", ValueCode = "4", ValueName = "Transferring", TenantId = eventData.Id },

                //IN03 Good Issue Reason
                new SystemData { Code = "IN03", ValueCode = "1", ValueName = "Sell", TenantId = eventData.Id },
                new SystemData { Code = "IN03", ValueCode = "2", ValueName = "Adjustment", TenantId = eventData.Id },
                new SystemData { Code = "IN03", ValueCode = "3", ValueName = "Damage", TenantId = eventData.Id },
                new SystemData { Code = "IN03", ValueCode = "4", ValueName = "Transferring", TenantId = eventData.Id },

                //IN04 Inventory Adjustment Reason
                // new SystemData { Code = "IN03", ValueCode = "1", ValueName = "Sell", TenantId = eventData.Id },
                // new SystemData { Code = "IN03", ValueCode = "2", ValueName = "Adjustment", TenantId = eventData.Id },
                // new SystemData { Code = "IN03", ValueCode = "3", ValueName = "Damage", TenantId = eventData.Id },
                // new SystemData { Code = "IN03", ValueCode = "4", ValueName = "Transferring", TenantId = eventData.Id },

                //SY01 Object Type
                new SystemData { Code = "SY01", ValueCode = "M1", ValueName = "Customer", TenantId = eventData.Id },
                new SystemData { Code = "SY01", ValueCode = "M2", ValueName = "Employee", TenantId = eventData.Id },
                new SystemData { Code = "SY01", ValueCode = "M3", ValueName = "Route", TenantId = eventData.Id },
                new SystemData { Code = "SY01", ValueCode = "P1", ValueName = "Purchase Request", TenantId = eventData.Id },
                new SystemData { Code = "SY01", ValueCode = "P2", ValueName = "Purchase Order", TenantId = eventData.Id },
                new SystemData { Code = "SY01", ValueCode = "P3", ValueName = "A/P Invoice", TenantId = eventData.Id },
                new SystemData { Code = "SY01", ValueCode = "P4", ValueName = "A/P Credit Memo", TenantId = eventData.Id },
                new SystemData { Code = "SY01", ValueCode = "I1", ValueName = "Goods Receipt", TenantId = eventData.Id },
                new SystemData { Code = "SY01", ValueCode = "I2", ValueName = "Goods Issue", TenantId = eventData.Id },
                new SystemData { Code = "SY01", ValueCode = "I3", ValueName = "Inventory transfer", TenantId = eventData.Id },
                new SystemData { Code = "SY01", ValueCode = "I4", ValueName = "Inventory Counting", TenantId = eventData.Id },
                new SystemData { Code = "SY01", ValueCode = "I5", ValueName = "Inventory Adjustment", TenantId = eventData.Id },
                // Documents for SalesOrder
                new SystemData { Code = "SY01", ValueCode = "S1", ValueName = "Sales Orders", TenantId = eventData.Id },
                new SystemData { Code = "SY01", ValueCode = "S2", ValueName = "Deliveries", TenantId = eventData.Id },
                new SystemData { Code = "SY01", ValueCode = "S3", ValueName = "A/R Invoices", TenantId = eventData.Id },
                new SystemData { Code = "SY01", ValueCode = "S4", ValueName = "A/R Credit Memos", TenantId = eventData.Id },
                new SystemData { Code = "SY01", ValueCode = "S5", ValueName = "Return Orders", TenantId = eventData.Id },
                new SystemData { Code = "SY01", ValueCode = "S6", ValueName = "Sales Requests", TenantId = eventData.Id },
                //SO01 - Order Type
                new SystemData { Code = "SO01", ValueCode = "SO", ValueName = "Sale Order", TenantId = eventData.Id },
                new SystemData { Code = "SO01", ValueCode = "DR", ValueName = "Delivery Request", TenantId = eventData.Id },
                //SO03 - Order Source
                //new SystemData { Code = "SO03", ValueCode = "MANUAL", ValueName = "Manual", TenantId = eventData.Id },
                //new SystemData { Code = "SO03", ValueCode = "SFA", ValueName = "SFA", TenantId = eventData.Id },
                //new SystemData { Code = "SO03", ValueCode = "BBS", ValueName = "bonbonShop", TenantId = eventData.Id }
            };
            
            await _systemDataRepository.CreateWithExcepAsync(systemDefaultData);

            await uow.CompleteAsync();
        }
        catch (Exception e)
        {
            // TODO hovanbuu: Implement exception handler
            Console.WriteLine(e.Message);
        }
    }
}
