using System;
using Microsoft.Extensions.Logging;
using DMSpro.OMS.MdmService.EntityFrameworkCore;
using DMSpro.OMS.Shared.Hosting.Microservices.DbMigrations.EfCore;
using Volo.Abp.DistributedLocking;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace DMSpro.OMS.MdmService.DbMigrations;

public class MdmServiceDatabaseMigrationChecker : PendingEfCoreMigrationsChecker<MdmServiceDbContext>
{
    public MdmServiceDatabaseMigrationChecker(
        ILoggerFactory loggerFactory,
        IUnitOfWorkManager unitOfWorkManager,
        IServiceProvider serviceProvider,
        ICurrentTenant currentTenant,
        IDistributedEventBus distributedEventBus,
        IAbpDistributedLock abpDistributedLock)
        : base(
            loggerFactory,
            unitOfWorkManager,
            serviceProvider,
            currentTenant,
            distributedEventBus,
            abpDistributedLock,
            MdmServiceDbProperties.ConnectionStringName)
    {

    }
}
