using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace MediaInAction.CmskitService.EntityFrameworkCore;

[ConnectionStringName(CmskitServiceDbProperties.ConnectionStringName)]
public interface ICmskitServiceDbContext : IEfCoreDbContext
{
}