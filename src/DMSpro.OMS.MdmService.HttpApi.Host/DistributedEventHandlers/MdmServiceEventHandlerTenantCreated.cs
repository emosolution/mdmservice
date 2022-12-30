using AutoMapper.Internal.Mappers;
using DMSpro.OMS.MdmService.CustomerAttributes;
using DMSpro.OMS.MdmService.Permissions;
using DMSpro.OMS.MdmService.ProductAttributes;
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

namespace DMSpro.OMS.MdmService;
public class MdmServiceDistributedEventHandler : IDistributedEventHandler<TenantCreatedEto>, ITransientDependency
{
    private static int PRODUCT_ATTRIBUTE_ROWS = 20;
    private static int CUSTOMER_ATTRIBUTE_ROWS = 20;

    //private readonly ICurrentTenant _currentTenant;
    //private readonly ILogger<MdmServiceDistributedEventHandler> _logger;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    private readonly IProductAttributeRepository _productAttributeRepository;
    private readonly ICustomerAttributeRepository _customerAttributeRepository;
    private readonly IGuidGenerator _guidGenerator;

    public MdmServiceDistributedEventHandler(
        IProductAttributeRepository productAttributeRepository,
        ICustomerAttributeRepository customerAttributeRepository,
        //ICurrentTenant currentTenant,
        //ILogger<MdmServiceDistributedEventHandler> logger,
        IGuidGenerator guidGenerator,
        IUnitOfWorkManager unitOfWorkManager
        )
    {
        _productAttributeRepository = productAttributeRepository;
        _customerAttributeRepository = customerAttributeRepository;

        //_currentTenant = currentTenant;
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
            List<ProductAttribute> seedProductAttributes= new List<ProductAttribute>();
            for (int i = 0; i < PRODUCT_ATTRIBUTE_ROWS; i++)
            {
                short AttrNo = (short)i;
                string AttrName = "Attribute " + i;
                Guid id = _guidGenerator.Create();
                ProductAttribute data = new ProductAttribute(id, AttrNo, AttrName, false, false, null);
                data.TenantId = eventData.Id;
                seedProductAttributes.Add(data);
            }
            await _productAttributeRepository.CreateWithExcepAsync(seedProductAttributes);

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


            await uow.CompleteAsync();
        }
        catch (Exception e)
        {
            // TODO hovanbuu: Implement exception handler
            Console.WriteLine(e.Message);
        }
    }
}
