using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.TraktService.TraktEpisodeAliasNs;

public interface ITraktEpisodeAliasRepository :  IRepository<TraktEpisodeAlias, Guid>
{
    Task<TraktEpisodeAlias> FindByTypeValue(string type, string value);

}