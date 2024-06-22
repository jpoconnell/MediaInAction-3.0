using System;
using MediaInAction.FileService.MongoDb;
using MediaInAction.Shared.Hosting.Microservices.DbMigrations.MongoDb;
using Volo.Abp.Data;
using Volo.Abp.DistributedLocking;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace MediaInAction.FileService.DbMigrations;

public class CatalogServiceDatabaseMigrationChecker : PendingMongoDbMigrationsChecker<FileServiceMongoDbContext>
{
    public CatalogServiceDatabaseMigrationChecker(
        IUnitOfWorkManager unitOfWorkManager,
        IServiceProvider serviceProvider,
        ICurrentTenant currentTenant,
        IDataSeeder dataSeeder,
        IAbpDistributedLock distributedLockProvider)
        : base(
            unitOfWorkManager,
            serviceProvider,
            currentTenant,
            dataSeeder,
            distributedLockProvider,
            FileServiceDbProperties.ConnectionStringName)
    {
    }
}