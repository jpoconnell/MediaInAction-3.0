using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyMovieNs.Dtos;
using MediaInAction.EmbyService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.EmbyService.EmbyMovieNs;

[Authorize(EmbyServicePermissions.Movie.Default)]
public class EmbyMovieAppService : EmbyServiceAppService, IEmbyMovieAppService
{
    private readonly ILogger<EmbyMovieAppService> _logger;
    private readonly IEmbyMovieRepository _embyMovieRepository;
    private readonly EmbyMovieManager _embyMovieManager;

    public EmbyMovieAppService(
        IEmbyMovieRepository embyMovieRepository,
        ILogger<EmbyMovieAppService> logger,
        EmbyMovieManager embyMovieManager)
    {
        _embyMovieRepository = embyMovieRepository;
        _embyMovieManager = embyMovieManager;
        _logger = logger;
    }
    
    public async Task<EmbyMovieDto> GetAsync(Guid id)
    {
        var embyMovie = await _embyMovieRepository.GetAsync(id);
        return ObjectMapper.Map<EmbyMovie, EmbyMovieDto>(embyMovie);
    }

    public Task<PagedResultDto<EmbyMovieDto>> GetListAsync(GetEmbyMovieListDto input)
    {
        throw new NotImplementedException();
    }

    public PagedResultDto<EmbyMovieDto> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(EmbyMovie.Name);
        }
        // var totalCount = await AsyncExecuter.CountAsync(
       
        return new PagedResultDto<EmbyMovieDto>(
            0,
            ObjectMapper.Map<List<EmbyMovie>, List<EmbyMovieDto>>(null)
        );
    }

    [Authorize(EmbyServicePermissions.Movie.Create)]
    public async Task<EmbyMovieDto> CreateAsync(EmbyMovieCreateDto input)
    {
        var newMovieAliases = GetMovieAliasTuple(input.MovieAliases);

        var newMovie = await _embyMovieManager.CreateAsync
        (
            name: input.Name,
            year: input.Year,
            embyMovieAliases: newMovieAliases
        );

       return ObjectMapper.Map<EmbyMovie, EmbyMovieDto>(newMovie);
    }

    public Task<EmbyMovieDto> GetMovieAsync(string name, int year)
    {
        throw new NotImplementedException();
    }

    [Authorize(EmbyServicePermissions.Movie.Update)]
    public async Task UpdateAsync(Guid id, EmbyMovieCreateDto input)
    {
        var embyMovie = await _embyMovieRepository.GetAsync(id);

        embyMovie.Name = input.Name;
        embyMovie.FirstAiredYear = input.Year;
        await _embyMovieRepository.UpdateAsync(embyMovie);
    }
    
    [Authorize(EmbyServicePermissions.Movie.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _embyMovieRepository.DeleteAsync(id);
    }
    

    private List<( string idType, string idValue)> 
        GetMovieAliasTuple(List<EmbyMovieAliasCreateDto> inMovieAliases)
    {
        var movieAliases =
            new List<( string idType, string idValue)>();
        foreach (var inMovieAlias in inMovieAliases)
        {
            movieAliases.Add((inMovieAlias.IdType, inMovieAlias.IdValue));
        }

        return movieAliases;
    }
    
    private List<EmbyMovieDto> CreateMovieDtoMapping(List<EmbyMovie> movies)
    {
        List<EmbyMovieDto> dtoList = new List<EmbyMovieDto>();
        foreach (var movie in movies)
        {
            dtoList.Add(CreateMovieDtoMapping(movie));
        }

        return dtoList;
    }

    private EmbyMovieDto CreateMovieDtoMapping(EmbyMovie movie)
    {
        return new EmbyMovieDto()
        {
            Name = movie.Name,
            FirstAiredYear = movie.FirstAiredYear
        };
    }
}