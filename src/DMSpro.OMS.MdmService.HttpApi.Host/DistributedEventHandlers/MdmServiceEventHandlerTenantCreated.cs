using DMSpro.OMS.MdmService.CustomerAttributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;
using DMSpro.OMS.MdmService.SystemDatas;
using DMSpro.OMS.MdmService.ItemAttributes;
using DMSpro.OMS.MdmService.NumberingConfigs;

namespace DMSpro.OMS.MdmService;
public class MdmServiceDistributedEventHandler : IDistributedEventHandler<TenantCreatedEto>, ITransientDependency
{
    private static readonly int ITEM_ATTRIBUTE_ROWS = 20;
    private static readonly int CUSTOMER_ATTRIBUTE_ROWS = 20;

    private readonly ICurrentTenant _currentTenant;

    private readonly IItemAttributeRepository _itemAttributeRepository;
    private readonly ICustomerAttributeRepository _customerAttributeRepository;
    private readonly ISystemDataRepository _systemDataRepository;
    private readonly INumberingConfigsInternalAppService _numberingConfigsInternalAppService;

    //private readonly ILogger<MdmServiceDistributedEventHandler> _logger;
    private readonly IGuidGenerator _guidGenerator;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public MdmServiceDistributedEventHandler(
        ICurrentTenant currentTenant,

        IItemAttributeRepository itemAttributeRepository,
        ICustomerAttributeRepository customerAttributeRepository,
        ISystemDataRepository systemDataRepository,
        INumberingConfigsInternalAppService numberingConfigsInternalAppService,

        //ILogger<MdmServiceDistributedEventHandler> logger,
        IGuidGenerator guidGenerator,
        IUnitOfWorkManager unitOfWorkManager)
    {
        _currentTenant = currentTenant;

        _itemAttributeRepository = itemAttributeRepository;
        _customerAttributeRepository = customerAttributeRepository;
        _systemDataRepository = systemDataRepository;
        _numberingConfigsInternalAppService = numberingConfigsInternalAppService;

        //_logger = logger;
        _guidGenerator = guidGenerator;
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task HandleEventAsync(TenantCreatedEto eventData)
    {
        try
        {
            var abpUnitOfWorkOptions = new AbpUnitOfWorkOptions { IsTransactional = true };
            using var uow = _unitOfWorkManager.Begin(abpUnitOfWorkOptions, true);
            using (_currentTenant.Change(eventData.Id))
            {
                await SeedItemAttributes();

                await SeedCustomerAttribute();

                await SeedSystemData();

                await uow.CompleteAsync();

                await _numberingConfigsInternalAppService.CreateAllConfigsForTenantAsync(
                    new List<Guid>() { eventData.Id });
            }
        }
        catch (Exception e)
        {
            // TODO hovanbuu: Implement exception handler
            Console.WriteLine(e.Message);
        }
    }

    private async Task SeedItemAttributes()
    {
        List<ItemAttribute> seedProductAttributes = new();
        for (int i = 0; i < ITEM_ATTRIBUTE_ROWS; i++)
        {
            short AttrNo = (short)i;
            string AttrName = "Attribute " + i;
            Guid id = _guidGenerator.Create();
            ItemAttribute data = new(id, AttrNo, AttrName, false, null);
            seedProductAttributes.Add(data);
        }
        await _itemAttributeRepository.InsertManyAsync(seedProductAttributes);
    }

    private async Task SeedCustomerAttribute()
    {
        List<CustomerAttribute> seedCustomerAttributes = new List<CustomerAttribute>();
        for (int i = 0; i < CUSTOMER_ATTRIBUTE_ROWS; i++)
        {
            short AttrNo = (short)i;
            string AttrName = "Attribute " + i;
            Guid id = _guidGenerator.Create();
            CustomerAttribute data = new(id, AttrNo, AttrName, false, null);
            seedCustomerAttributes.Add(data);
        }
        await _customerAttributeRepository.InsertManyAsync(seedCustomerAttributes);
    }

    private async Task SeedSystemData()
    {
        List<(string Code, string ValueCode, string ValueName)> seedData = new()
        {
            //Seed ItemType - MD02
            (Code: "MD02", ValueCode: "I", ValueName: "Item"),
            (Code: "MD02", ValueCode: "P", ValueName: "POSM"),
            (Code: "MD02", ValueCode: "D", ValueName: "Discount"),
            (Code: "MD02", ValueCode: "V", ValueName: "Voucher"),

            //MD05 - Payment Term
            (Code: "MD05", ValueCode: "5", ValueName: "5"),
            (Code: "MD05", ValueCode: "7", ValueName: "7"),
            (Code: "MD05", ValueCode: "10", ValueName: "10"),
            (Code: "MD05", ValueCode: "15", ValueName: "15"),
            (Code: "MD05", ValueCode: "30", ValueName: "30"),
            (Code: "MD05", ValueCode: "45", ValueName: "45"),
            (Code: "MD05", ValueCode: "60", ValueName: "60"),

            //MD06 - Route Type
            (Code: "MD06", ValueCode: "1", ValueName: "Sales"),
            (Code: "MD06", ValueCode: "2", ValueName: "Delivery"),
            (Code: "MD06", ValueCode: "3", ValueName: "Display"),
            (Code: "MD06", ValueCode: "4", ValueName: "Audit"),

            //MD07 - Item Group Applicable 
            (Code: "MD07", ValueCode: "1", ValueName: "KPI"),
            (Code: "MD07", ValueCode: "2", ValueName: "Promotion (On/Off)"),
            (Code: "MD07", ValueCode: "3", ValueName: "Stock Count"),

            //MD09 - Sale Material Type
            (Code: "MD09", ValueCode: "1", ValueName: "Sales"),
            (Code: "MD09", ValueCode: "2", ValueName: "Training"),

            //IN01 - Inventory Transaction Type
            (Code: "IN01", ValueCode: "1", ValueName: "Item"),
            (Code: "IN01", ValueCode: "2", ValueName: "Promotion"),
            (Code: "IN01", ValueCode: "3", ValueName: "Sampling"),
            (Code: "IN01", ValueCode: "4", ValueName: "Incentive"),

            //SY01 - Object Type
            // Documents for SalesOrder
            (Code: "SY01", ValueCode: "S1", ValueName: "Sales Orders"),
            (Code: "SY01", ValueCode: "S2", ValueName: "Deliveries"),
            (Code: "SY01", ValueCode: "S3", ValueName: "A/R Invoices"),
            (Code: "SY01", ValueCode: "S4", ValueName: "A/R Credit Memos"),
            (Code: "SY01", ValueCode: "S5", ValueName: "Return Orders"),
            (Code: "SY01", ValueCode: "S6", ValueName: "Sales Requests"),
            // Documents for PurchaseOrder
            (Code: "SY01", ValueCode: "P1", ValueName: "Purchase Requests"),
            (Code: "SY01", ValueCode: "P2", ValueName: "Purchase Orders"),
            (Code: "SY01", ValueCode: "P3", ValueName: "A/P Invoices"),
            (Code: "SY01", ValueCode: "P4", ValueName: "A/P Credit Memos"),
            // General Objects
            (Code: "SY01", ValueCode: "M1", ValueName: "Customers"),
            (Code: "SY01", ValueCode: "M2", ValueName: "Employees"),
            (Code: "SY01", ValueCode: "M3", ValueName: "Routes"),
            (Code: "SY01", ValueCode: "M4", ValueName: "Items"),
            (Code: "SY01", ValueCode: "M5", ValueName: "Vendors"),
            // Inventory
            (Code: "SY01", ValueCode: "I1", ValueName: "Goods Receipts"),
            (Code: "SY01", ValueCode: "I2", ValueName: "Goods Issues"),
            (Code: "SY01", ValueCode: "I3", ValueName: "Inventory Transfers"),
            (Code: "SY01", ValueCode: "I4", ValueName: "Inventory Countings"),
            (Code: "SY01", ValueCode: "I5", ValueName: "Inventory Adjustments"),
        };

        List<SystemData> seedSystemData = new();
        foreach (var data in seedData)
        {
            SystemData seed = new SystemData(id: _guidGenerator.Create(),
                code: data.Code, valueCode: data.ValueCode, valueName: data.ValueName);
        }
        await _systemDataRepository.InsertManyAsync(seedSystemData);
    }
}
