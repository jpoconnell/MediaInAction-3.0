using MediaInAction.CmskitService.EntityFrameworkCore;
using MediaInAction.Shared.Hosting.Microservices.DbMigrations.EfCore;
using System;
using Volo.Abp.DistributedLocking;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace MediaInAction.CmskitService.DbMigrations;

public class CmskitServiceDatabaseMigrationChecker : PendingEfCoreMigrationsChecker<CmskitServiceDbContext>
{
    public CmskitServiceDatabaseMigrationChecker(
        IUnitOfWorkManager unitOfWorkManager,
        IServiceProvider serviceProvider,
        ICurrentTenant currentTenant,
        IDistributedEventBus distributedEventBus,
        IAbpDistributedLock abpDistributedLock)
        : base(
            unitOfWorkManager,
            serviceProvider,
            currentTenant,
            distributedEventBus,
            abpDistributedLock,
            CmskitServiceDbProperties.ConnectionStringName)
    {
    }
}