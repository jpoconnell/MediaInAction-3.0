using System.Threading.Tasks;
using Grpc.Core;
using MediaInAction.VideoService.EpisodeAliasNs;
using MediaInAction.VideoService.EpisodeNs;
using Microsoft.Extensions.Logging;
using VideoService.Episode.GrpcServer;

namespace MediaInAction.VideoService.Grpc;

public class EpisodeGrpcTestService : EpisodeGrpcService.EpisodeGrpcServiceBase 
{
    private readonly ILogger<EpisodeGrpcTestService> _logger;
    private readonly IEpisodeRepository _episodeRepository;
    private readonly IEpisodeAliasRepository _episodeAliasRepository;
    private readonly EpisodeManager _episodeManager;
    
    public EpisodeGrpcTestService(ILogger<EpisodeGrpcTestService> logger, 
        IEpisodeRepository episodeRepository,
        IEpisodeAliasRepository episodeAliasRepository,
        EpisodeManager episodeManager)
    {
        _logger = logger;
        _episodeRepository = episodeRepository;
        _episodeAliasRepository = episodeAliasRepository;
        _episodeManager = episodeManager;
    }

    public override async Task<EpisodeModel> CreateNewEpisode(EpisodeModel request, ServerCallContext context)
    {
        var episodeCreateDto = TranslateEpisodeGrpc(request);
        var response = await _episodeManager.CreateAsync(episodeCreateDto);

        var episodeModel = TranslateEpisode(response);
        return episodeModel;
    }
    
    public override async Task SearchEpisodes(SearchRequest request, IServerStreamWriter<EpisodeModel> responseStream, ServerCallContext context)
    {
        var episode =
            await _episodeRepository.GetBySlugSeasonEpisode(request.Slug, request.Season, request.Episode);
        if (episode != null)
        {
            var episodeModel = TranslateEpisode(episode);
            await responseStream.WriteAsync(episodeModel);
        }
    }
    
    public override async Task<EpisodeModel> UpdateEpisode(EpisodeModel request, ServerCallContext context)
    {
        var episode = await _episodeRepository.GetBySlugSeasonEpisode(request.Slug,request.Season,request.Episode);
        if (episode != null)
        {
            var episodeCreateDto = new EpisodeCreateDto();
            episodeCreateDto.SeasonNum = request.Season;
            episodeCreateDto.EpisodeNum = request.Episode;
            var createdEpisode = await _episodeManager.UpdateAsync(episodeCreateDto);
            var updateEpisode = await _episodeRepository.GetByIdAsync(episode.Id);
            return TranslateEpisode(episode);
        }
        else
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Episode with ID={request.Slug} to many found."));
        }
    }

    private EpisodeModel TranslateEpisode(EpisodeNs.Episode episode)
    {
        var episodeModel = new EpisodeModel();
        episodeModel.Season = episode.SeasonNum;
        episodeModel.Episode = episode.EpisodeNum;
        
        foreach (var episodeAlias in episode.EpisodeAliases)
        {
            var newEpisodeAlias = new EpisodeAliasModel();
            newEpisodeAlias.IdType = episodeAlias.IdType;
            newEpisodeAlias.IdValue = episodeAlias.IdValue;
            //episodeModel.Aliases.Add(newEpisodeAlias);
            if (episodeAlias.IdType == "Slug")
            {
                episodeModel.Slug = episodeAlias.IdValue;
            }
        }
        return episodeModel;
    }

    private EpisodeCreateDto TranslateEpisodeGrpc(EpisodeModel request)
    {
        var episodeCreateDto = new EpisodeCreateDto();
        episodeCreateDto.SeasonNum = request.Season;
        episodeCreateDto.EpisodeNum = request.Episode;
        /*
        foreach (var episodeAlias in request.Aliases)
        {
            episodeCreateDto.EpisodeCreateAliases.Add((episodeAlias.IdType, episodeAlias.IdValue));
        }
        */
        return episodeCreateDto;
    }
}
