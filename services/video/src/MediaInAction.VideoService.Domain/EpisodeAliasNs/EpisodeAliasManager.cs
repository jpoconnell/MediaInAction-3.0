using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace MediaInAction.VideoService.EpisodeAliasNs;

public class EpisodeAliasManager : DomainService
{
    private readonly IEpisodeAliasRepository _episodeAliasRepository;
    private readonly ILogger<EpisodeAliasManager> _logger;
    
    public EpisodeAliasManager(
        IEpisodeAliasRepository episodeAliasRepository,
        ILogger<EpisodeAliasManager> logger)
    {
        _episodeAliasRepository = episodeAliasRepository;
        _logger = logger;
    }
    
    public async Task<EpisodeAlias> CreateAsync(
        Guid episodeId,
        [NotNull] string idType,
        [NotNull] string idValue)
    {
        Check.NotNullOrWhiteSpace(idType, nameof(idType));
        Check.NotNullOrWhiteSpace(idValue, nameof(idValue));

        var existingEpisodeAlias = new EpisodeAlias();
        if (idType == "folder")
        {
            existingEpisodeAlias = await _episodeAliasRepository.FindByEpisodeIdType(episodeId, idType);
        }
        else
        {
            existingEpisodeAlias = await _episodeAliasRepository.FindByEpisodeTypeValue(
                episodeId,idType,idValue);
        }
    
        if (existingEpisodeAlias != null)
        {
            var name = idType + "-" + idValue;
            _logger.LogInformation(name);
            // throw new EpisodeAliasAlreadyExistsException(name);
            return existingEpisodeAlias;
        }
        else
        {
            var newEpisodeAlias =  new EpisodeAlias(
                GuidGenerator.Create(),
                episodeId,
                idType,
                idValue
            );
    
            var createEpisode = await _episodeAliasRepository.InsertAsync(newEpisodeAlias, true);
            return createEpisode;
        }
    }
}
