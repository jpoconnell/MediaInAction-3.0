using MediaInAction.WebGateway.Aggregations.Base;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations;

namespace MediaInAction.WebGateway.Aggregations.ApplicationConfiguration;

public interface IAppConfigurationRemoteService : IAggregateRemoteService<ApplicationConfigurationDto>;