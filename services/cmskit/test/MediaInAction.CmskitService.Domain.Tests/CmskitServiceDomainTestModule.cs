﻿using MediaInAction.CmskitService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace MediaInAction.CmskitService;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(CmskitServiceEntityFrameworkCoreTestModule)
    )]
public class CmskitServiceDomainTestModule : AbpModule
{

}
