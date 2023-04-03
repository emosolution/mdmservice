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
    private readonly ISystemDatasInternalAppService _systemDatasInternalAppService;
    private readonly INumberingConfigsInternalAppService _numberingConfigsInternalAppService;

    //private readonly ILogger<MdmServiceDistributedEventHandler> _logger;
    private readonly IGuidGenerator _guidGenerator;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public MdmServiceDistributedEventHandler(
        ICurrentTenant currentTenant,

        IItemAttributeRepository itemAttributeRepository,
        ICustomerAttributeRepository customerAttributeRepository,
        ISystemDataRepository systemDataRepository,
        ISystemDatasInternalAppService systemDatasInternalAppService,
        INumberingConfigsInternalAppService numberingConfigsInternalAppService,

        //ILogger<MdmServiceDistributedEventHandler> logger,
        IGuidGenerator guidGenerator,
        IUnitOfWorkManager unitOfWorkManager)
    {
        _currentTenant = currentTenant;

        _itemAttributeRepository = itemAttributeRepository;
        _customerAttributeRepository = customerAttributeRepository;
        _systemDatasInternalAppService = systemDatasInternalAppService;
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

                await _systemDatasInternalAppService.CreateAllForTenantAsync(new List<Guid>() { eventData.Id });

                await uow.CompleteAsync();
                
                // Must be after UOW completed or there would be no seeded system data 
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
}
