using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using DMSpro.OMS.Shared.Hosting.Microservices.DbMigrations;
using Volo.Abp.Data;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;
using Volo.Saas.Tenants;
using DMSpro.OMS.Shared.Domain.DistributedEvents;
using DMSpro.OMS.MdmService.Companies;

namespace DMSpro.OMS.MdmService.DbMigrations;

public class MdmServiceDatabaseMigrationEventHandler
    : DatabaseMigrationEventHandlerBase<MdmServiceDbContext>,
        IDistributedEventHandler<TenantCreatedEto>,
        IDistributedEventHandler<TenantConnectionStringUpdatedEto>,
        IDistributedEventHandler<ApplyDatabaseMigrationsEto>,
        IDistributedEventHandler<AdminTenantCreatedEto>
{
    private readonly ICompaniesInternalAppService _companiesInternalAppService;

    public MdmServiceDatabaseMigrationEventHandler(
        ILoggerFactory loggerFactory,
        ICurrentTenant currentTenant,
        IUnitOfWorkManager unitOfWorkManager,
        ITenantStore tenantStore,
        ITenantRepository tenantRepository,
        IDistributedEventBus distributedEventBus,
        ICompaniesInternalAppService companiesInternalAppService
    ) : base(
        loggerFactory,
        currentTenant,
        unitOfWorkManager,
        tenantStore,
        tenantRepository,
        distributedEventBus,
        MdmServiceDbProperties.ConnectionStringName)
    {
        _companiesInternalAppService = companiesInternalAppService;
    }

    public async Task HandleEventAsync(ApplyDatabaseMigrationsEto eventData)
    {
        if (eventData.DatabaseName != DatabaseName)
        {
            return;
        }

        try
        {
            var schemaMigrated = await MigrateDatabaseSchemaAsync(eventData.TenantId);

            if (eventData.TenantId == null && schemaMigrated)
            {
                /* Migrate tenant databases after host migration */
                await QueueTenantMigrationsAsync();
            }
        }
        catch (Exception ex)
        {
            await HandleErrorOnApplyDatabaseMigrationAsync(eventData, ex);
        }
    }

    public async Task HandleEventAsync(TenantCreatedEto eventData)
    {
        try
        {
            await MigrateDatabaseSchemaAsync(eventData.Id);
        }
        catch (Exception ex)
        {
            await HandleErrorTenantCreatedAsync(eventData, ex);
        }
    }

    public async Task HandleEventAsync(AdminTenantCreatedEto eventData)
    {
        try
        {
            Console.WriteLine("kaklaic");
            Console.WriteLine(eventData);
            await _companiesInternalAppService.SeedHOCompanyAndAssignAdminToHO(eventData.TenantId.Value,eventData.IdentityUserId);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Loi seed AdminCreatedEto");
            Console.WriteLine(ex);
            //await HandleErrorTenantCreatedAsync(eventData, ex);
        }
    }


    public async Task HandleEventAsync(TenantConnectionStringUpdatedEto eventData)
    {
        if (eventData.ConnectionStringName != DatabaseName && eventData.ConnectionStringName != ConnectionStrings.DefaultConnectionStringName ||
            eventData.NewValue.IsNullOrWhiteSpace())
        {
            return;
        }

        try
        {
            await MigrateDatabaseSchemaAsync(eventData.Id);

            /* You may want to move your data from the old database to the new database!
            * It is up to you. If you don't make it, new database will be empty. */
        }
        catch (Exception ex)
        {
            await HandleErrorTenantConnectionStringUpdatedAsync(eventData, ex);
        }
    }
}
