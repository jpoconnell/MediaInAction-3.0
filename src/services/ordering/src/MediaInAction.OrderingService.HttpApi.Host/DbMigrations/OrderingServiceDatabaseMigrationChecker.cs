using MediaInAction.OrderingService.EntityFrameworkCore;
using System;
using MediaInAction.Shared.Hosting.Microservices.DbMigrations.EfCore;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;
using Volo.Abp.DistributedLocking;

namespace MediaInAction.OrderingService.DbMigrations;

public class OrderingServiceDatabaseMigrationChecker : PendingEfCoreMigrationsChecker<OrderingServiceDbContext>
{
    public OrderingServiceDatabaseMigrationChecker(
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
            OrderingServiceDbProperties.ConnectionStringName)
    {
    }
}