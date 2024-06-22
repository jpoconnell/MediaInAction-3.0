using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using Grpc.Core;
using MediaInAction.VideoService.SeriesAliasNs;
using MediaInAction.VideoService.SeriesNs;
using Microsoft.Extensions.Logging;
using VideoService.Series.GrpcServer;


namespace MediaInAction.VideoService.Grpc
{
    /// <summary>
    /// Implementation of series RPC service
    /// </summary>
    public class SeriesGrpcTestService : SeriesGrpcService.SeriesGrpcServiceBase
    {
        private readonly ILogger<SeriesGrpcTestService> _logger;
        private readonly ISeriesRepository _seriesRepository;
        private readonly ISeriesAliasRepository _seriesAliasRepository;
        private readonly SeriesManager _seriesManager;
        
        public SeriesGrpcTestService(ILogger<SeriesGrpcTestService> logger, 
            ISeriesRepository seriesRepository,
            ISeriesAliasRepository seriesAliasRepository,
            SeriesManager seriesManager)
        {
            _logger = logger;
            _seriesRepository = seriesRepository;
            _seriesAliasRepository = seriesAliasRepository;
            _seriesManager = seriesManager;
        }

        public override async Task<SeriesModel> CreateNewSeries(SeriesModel request, ServerCallContext context)
        {
            var seriesCreateDto = TranslateSeriesGrpc(request);
            var response = await _seriesManager.CreateAsync(seriesCreateDto);

            var seriesModel = TranslateSeries(response);
            return seriesModel;
        }

   
        public override async Task<SeriesModel> UpdateSeries(SeriesModel request, ServerCallContext context)
        {
            var dbSeriesAlias = await _seriesAliasRepository.GetByIdValue(request.Slug);
            var dbSeries = await _seriesRepository.GetByIdAsync(dbSeriesAlias.SeriesId);
            if (dbSeries != null)
            {
                var seriesCreateDto = new SeriesCreateDto
                {
                    Name = request.Name,
                    FirstAiredYear = request.Year
                };
                var createdSeries = await _seriesManager.UpdateAsync(seriesCreateDto);
                
                var updateSeries = await _seriesRepository.GetByIdAsync(createdSeries.Id);
                return TranslateSeries(updateSeries);
            }
            else
            {
                return null;
            }
        }
        
        private SeriesAliasModel TranslateSeriesAlias(SeriesAlias seriesAlias)
        {
            var seriesAliasModel = new SeriesAliasModel();
            seriesAliasModel.IdType = seriesAlias.IdType;
            seriesAliasModel.IdValue = seriesAlias.IdValue;
            return seriesAliasModel;
        }
    
        private SeriesModel TranslateSeries(SeriesNs.Series series)
        {
            try
            {
                var slug = "";
                var seriesAliasModels = new RepeatedField<SeriesAliasModel>();

                foreach (var seriesAlias in series.SeriesAliases)
                {
                    var seriesAliasModel = new SeriesAliasModel
                    {
                        IdType = seriesAlias.IdType,
                        IdValue = seriesAlias.IdValue
                    };
                    if (seriesAlias.IdType == "slug")
                    {
                        slug = seriesAlias.IdValue;
                    }
                    seriesAliasModels.Add(seriesAliasModel);
                }

                var seriesModel = new SeriesModel
                {
                    Name = series.Name,
                    Year = series.FirstAiredYear,
                    Slug = slug,
                    Aliases = { seriesAliasModels }
                };
                if (series.ImageName == null)
                {
                    seriesModel.ImageName = "";
                }
                else
                {
                    seriesModel.ImageName = series.ImageName;
                }
                return seriesModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private SeriesCreateDto TranslateSeriesGrpc(SeriesModel request)
        {
            var seriesCreateDto = new SeriesCreateDto
            {
                Name = request.Name,
                FirstAiredYear = request.Year
            };
            seriesCreateDto.SeriesAliases = new List<SeriesAliasCreateDto>();
            foreach (var seriesAlias in request.Aliases)
            {
                seriesCreateDto.SeriesAliases.Add(new SeriesAliasCreateDto
                {
                    IdType = seriesAlias.IdType,
                    IdValue = seriesAlias.IdValue,
                });
            }
            return seriesCreateDto;
        }
    }
}
