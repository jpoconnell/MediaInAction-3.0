using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.EpisodeNs.Dtos;
using MediaInAction.VideoService.FileEntryNs;
using MediaInAction.VideoService.FileEntryNs.Dtos;
using MediaInAction.VideoService.MovieAliasNs;
using MediaInAction.VideoService.MovieAliasNs.Dtos;
using MediaInAction.VideoService.MovieNs;
using MediaInAction.VideoService.SeriesAliasNs;
using MediaInAction.VideoService.SeriesAliasNs.Dtos;
using MediaInAction.VideoService.SeriesNs;
using MediaInAction.VideoService.TorrentNs.Dtos;
using Microsoft.Extensions.Logging;

namespace  MediaInAction.VideoService.ParserNs;

public class ParserService : IParserService
    {
        private readonly ILogger<IParserService> _logger;
        private readonly IFileEntryLibService _fileEntryService;
        private readonly IEpisodeService _episodeService;
        private readonly ISeriesService _seriesService;
        private readonly ISeriesAliasService _seriesAliasService;
        private readonly IMovieService _movieService;
        private readonly IMovieAliasLibService _movieAliasService;
        
        public ParserService( ILogger<ParserService> logger,
            IFileEntryLibService fileEntryService,
            ISeriesService seriesService,
            IEpisodeService episodeService,
            ISeriesAliasService seriesAliasService,
            IMovieService movieService,
            IMovieAliasLibService movieAliasService)
        {
            _logger = logger;
            _fileEntryService = fileEntryService;
            _episodeService = episodeService;
            _seriesService = seriesService;
            _movieService = movieService;
            _seriesAliasService = seriesAliasService;
            _movieAliasService = movieAliasService;
        }

        private ParserDto CreateParserDto()
        {
            var parser = new ParserDto
            {
                ListName = ListType.Other,
                MediaType = MediaType.Other,
                ParseType = ParseType.Other,
                Link = Guid.Empty,
                IncomingName = "",
                IncomingFullPath = "",
                CleanFileName = "",
                SeriesLink = Guid.Empty,
                SeriesName = "",
                Resolution = "",
                Extn = "",
                Category = "",
                Directory = "",
                EpisodeName = "",
                EpisodeId = "",
           
            };
            return parser;
        }

        public async Task<ParserDto> MapProcess(FileEntryDto input)
        {
            var parser = MapToParser(input);
            parser.ParseType = ParseType.File;
            parser.IncomingFullPath = input.Directory + "/" + input.FileName + "." + input.Extn;
            parser.Extn = input.Extn;
            var newFileName = input.FileName;
            if (newFileName.Contains("a100") )
            {
                newFileName = newFileName.Substring(4, newFileName.Length - 4);
            }
            if (newFileName.Contains("adefault") )
            {
                newFileName = newFileName.Substring(8, newFileName.Length - 8);
            }
            if (newFileName.Contains("s800") )
            {
                newFileName = newFileName.Substring(4, newFileName.Length - 4);
            }
            parser.IncomingName = newFileName;
            await ProcessMe(parser);
            return parser;
        }

        public async Task<ParserDto> MapProcess(EpisodeDto input)
        {
            var parser = await MapToParser(input);
            parser.ParseType = ParseType.Episode;
            await ProcessEpisodeFile(parser);
            return parser;
        }

        public async Task<ParserDto> MapProcess(TorrentDto torrentDto)
        {
            var parser = MapToParser(torrentDto);
            parser.ParseType = ParseType.Torrent;
            await ProcessTorrent(parser);
            return parser;
        }

        public async Task<ParserDto> GetMoveTo(Guid link)
        {
            var episode = await _episodeService.GetByIdAsync(link);

            var myParser = await MapToParser(episode);
            await BuildFilename(myParser);
            return myParser;
        }

        private async Task ProcessTorrent(ParserDto parser)
        {
           await ProcessMe(parser);
        }

        public ParserDto MapToParser(TorrentDto torrentDto)
        {
            var parser = CreateParserDto();
            parser.Directory = ""; 
            parser.IncomingFullPath = torrentDto.Name.Replace("'", "");
            parser.IncomingName = torrentDto.Name.Replace("'", "");
            parser.ListName = ListType.Torrent;
            
            if (torrentDto.EpisodeLink != Guid.Empty)
            {
                parser.Link = torrentDto.EpisodeLink;
            }
            if (torrentDto.MediaLink != Guid.Empty)
            {
                parser.SeriesLink = torrentDto.MediaLink;
            }
            
            if (parser.MediaType == MediaType.Other)
            {
                if (torrentDto.Type != MediaType.Other)
                {
                    parser.MediaType = torrentDto.Type;
                }
            }

            return parser;
        }

        public async Task<ParserDto> MapToParser(EpisodeDto episode)
        {
            var parser = CreateParserDto();
            parser.Link = episode.Id;
            parser.SeasonNum = episode.SeasonNum;
            parser.EpisodeNum = episode.EpisodeNum;
            parser.SeriesLink = episode.SeriesId;
            
            if ((parser.SeriesLink != Guid.Empty) && (parser.SeriesName.IsNullOrEmpty()))
            {
                var series = await _seriesService.GetByIdAsync(parser.SeriesLink);
                parser.SeriesName = series.Name;
            }

            var fileEntryList = await _fileEntryService.GetByLink(parser.Link);
            
            if (fileEntryList.Count > 0)
            {
                var fileEntry = fileEntryList[0];
                parser.Resolution = fileEntry.Resolution;
                if (fileEntry.ListName == ListType.Compressed)
                {
                    parser.Extn = fileEntry.Extn;
                    //  parser.FullPathOut = fileEntry.FullPath;
                }
            }
            BuildSeasonEpisode(parser);
            await BuildFilename(parser);

            return parser;
        }
        
        private ParserDto MapToParser(FileEntryDto fileEntry)
        {
            var parser = CreateParserDto();
            parser.Directory = fileEntry.Directory; 
            parser.IncomingFullPath = fileEntry.Directory.Replace("'", "");
            parser.IncomingName = fileEntry.FileName.Replace("'", "");
            parser.ListName = fileEntry.ListName;
            parser.MediaType = fileEntry.MediaType;
            if (fileEntry.Link != Guid.Empty)
            {
                parser.Link = fileEntry.Link;
            }
            
            if (!fileEntry.Resolution.IsNullOrEmpty())
            {
                parser.Resolution = fileEntry.Resolution;
            }
            if ((parser.ListName == ListType.Current) && (parser.MediaType == MediaType.Other))
            {
                string pattern = @"TV Shows";
                Match m = Regex.Match(parser.Directory, pattern, RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    parser.MediaType = MediaType.Episode;
                }
                pattern = @"Movie";
                m = Regex.Match(parser.Directory, pattern, RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    parser.MediaType = MediaType.Movie;
                }
            }
            if (parser.MediaType == MediaType.Other)
            {
                if (fileEntry.MediaType != MediaType.Other)
                {
                    parser.MediaType = fileEntry.MediaType;
                }
            }
            if (!fileEntry.Extn.IsNullOrEmpty())
            {
                parser.Extn = fileEntry.Extn;
            }
            return parser;
        }

        private async Task ProcessMovieFile(ParserDto parser)
        {
            try
            {
                if (parser.Link != Guid.Empty)
                {
                    var episode = await _episodeService.GetByIdAsync(parser.Link);
                    var series = await _seriesService.GetByIdAsync(episode.SeriesId);
                    parser.SeriesLink = series.Id;
                    parser.SeriesName = series.Name;
                
                    if (series.Type != MediaType.Other)
                        parser.MediaType = series.Type;

                    // get file to move 
                    var fileEntryList = await _fileEntryService.GetByLink(episode.Id);
                    
                    // many files can be mapped to one episode
                    if (fileEntryList.Count() > 0)
                    {
                        foreach(var file in fileEntryList)
                        {
                            if (file.ListName == ListType.Uncompressed)
                            {
                                parser.IncomingFullPath = file.Directory + file.FileName;
                                parser.IncomingName = file.FileName;
                                parser.Extn = file.Extn;
                                parser.Resolution = file.Resolution;
                            }
                        }
                    }
                    await BuildOutputFullPath(parser);
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug("ProcessMovieFile Error:" + parser.IncomingName + ":" + ex.Message);
            }
        }

        private async Task ProcessEpisodeFile(ParserDto parser)
        {
            try
            {
                if (parser.Link != Guid.Empty)
                {
                    var episode = await _episodeService.GetByIdAsync(parser.Link);
                    var series = await _seriesService.GetByIdAsync(episode.SeriesId);
                    parser.SeriesLink = series.Id;
                    parser.SeriesName = series.Name;
                
                    if (series.Type != MediaType.Other)
                        parser.MediaType = series.Type;

                    // get file to move 
                    var fileEntryList = await _fileEntryService.GetByLink(episode.Id);
                    
                    // many files can be mapped to one episode
                    if (fileEntryList.Count() > 0)
                    {
                        foreach(var file in fileEntryList)
                        {
                            if (file.ListName == ListType.Uncompressed)
                            {
                                parser.IncomingFullPath = file.Directory + file.FileName;
                                parser.IncomingName = file.FileName;
                                parser.Extn = file.Extn;
                                parser.Resolution = file.Resolution;
                            }
                        }
                    }
                    await BuildOutputFullPath(parser);
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug("ProcessEpisodeFile Error:" + parser.IncomingName + ":" + ex.Message);
            }
        }

        private async Task BuildOutputFullPath(ParserDto parser)
        {
            BuildSeasonEpisode(parser);
            await BuildFilename(parser);
        }

        private async Task ProcessMe(ParserDto parser)
        {
            try
            {
                string fname = parser.IncomingName;
                
                if (parser.MediaType == MediaType.Other)
                {
                    var mediaType = GetMediaType(parser );
                }
                if (parser.MediaType == MediaType.Movie)
                {
                    await ParseMovie( parser);
                }
                if (parser.MediaType == MediaType.Episode)
                {
                    await ParseTvShow( parser);
                } 
                GetResolution( parser);

                checkToBeMapped(parser);
            }
            catch (Exception ex)
            {
                _logger.LogDebug("ProcessMe Error:" + parser.IncomingName
                    + ":" + ex.Message);
            }
        }
        
        private List<string> SpiltName(string input)
        {
            string toBeParsed = "";
        
            string[] dirs = input.Split('/');
            if (dirs.Count() > 1)
            {
                toBeParsed = dirs[dirs.Count()-1];
            }
            else 
            {
                toBeParsed = input;
            }

            List<string> outList = new List<string>();
            string[] prases = toBeParsed.Split('.');
            string[] words;
            foreach (string prase in prases)
            {
                words = prase.Split(' ');
                foreach (string word in words)
                {
                    outList.Add(word);
                }
            }
            return outList;
        } 
        
        private async Task ParseTvShow(ParserDto parser)
        {
            try
            {
                if (parser.IncomingName == "")
                {
                    parser.IncomingName = Path.GetFileName(parser.IncomingFullPath);
                }
                var words = SpiltName(parser.IncomingName);
                if (parser.MediaType == MediaType.Episode)
                {
                    await GetSeriesName( parser);

                    await GetSeasonEps( parser);
                    if (parser.SeriesLink == Guid.Empty)
                    {
                        var seriesAlias = new SeriesAliasDto();
                        seriesAlias = await _seriesAliasService.GetByIdValue(parser.SeriesName.ToLower());
                        
                        if (seriesAlias != null)
                        {
                            parser.SeriesLink = seriesAlias.SeriesId;
                            await BuildFilename(parser);
                            await GetEpisodeLink(parser);
                           // parser.OutFullPath = folder + "/" + parser.FileName;
                        }
                        else
                        {
                            parser.ToBeMapped = true;
                        }
                    }
                    else
                    {
                        if (parser.SeriesLink != Guid.Empty)
                        {
                            if ((parser.SeasonNum > 0) && (parser.EpisodeNum > 0))
                            {
                                await GetEpisodeLink(parser);
                            }
                        }
                    }
                }

                if (parser.Directory.Length < 1)
                {
                    await GetSeriesDirectory(parser);
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug("ParserService.ParseTvShow"+ ex.Message);
            }
        }

        private async Task ParseMovie(ParserDto parser)
        {
            try
            {
                await GetMovieName( parser);
                if (parser.SeriesName != null)
                {
                    if (parser.SeriesLink == Guid.Empty)
                    {
                        var movieAlias = new MovieAliasDto();
                        movieAlias = await _movieAliasService.GetByIdValue(parser.SeriesName.ToLower());
                        
                        if (movieAlias != null)
                        {
                            parser.SeriesLink = movieAlias.MovieId;
                            await BuildMovieFileName(parser);
                        }
                        else
                        {
                            parser.ToBeMapped = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug("ParserService.ParseMovie:" + ex.Message);
            }
        }

        private async Task BuildMovieFileName(ParserDto parser)
        {
            if (parser.SeriesLink != Guid.Empty) 
            {
                var movieList = await _movieService.GetByIdAsync(parser.SeriesLink);

                if (movieList != null)
                {
                    parser.OutFullPath = movieList.Name + " (" + movieList.FirstAiredYear.ToString() + ")";
                    parser.OutputName = movieList.Name + parser.Resolution + parser.Extn;
                }
            }
        }

        private async Task<Movie> GetMovieName( ParserDto parser)
        {
            if (parser.Link == Guid.Empty)
            {
                string movieName = "";
                int movieYear = 1980;
                bool movieNameEnd = false;
                var words = SpiltName(parser.IncomingName);
                foreach (string word in words)
                {
                    if ((movieNameEnd == false) && (word.Contains("mkv")))
                    {
                        movieNameEnd = true;
                    }
                    Regex rgx = new Regex(@"^(19|20)\d{2}$");
                    if ((movieNameEnd == false) && (rgx.IsMatch(word)))
                    {
                        movieNameEnd = true;
                        movieYear = Convert.ToInt32(word);
                        break;
                    }
                    if (word.Length == 6)
                    {
                        var tmpword = word.Substring(1,4);
                        if ((movieNameEnd == false) && (rgx.IsMatch(tmpword)))
                        {
                            movieNameEnd = true;
                            movieYear = Convert.ToInt32(tmpword);
                            break;
                        }
                    }
                    if (movieNameEnd == false)
                    {
                        movieName = (movieName + " " + word).TrimStart().ToLower();
                    }
                }

                parser.SeriesName = movieName + movieYear.ToString();
                
                var movieDto = await _movieService.GetByName(movieName);
                
                if (movieDto != null)
                {
                    parser.Link = movieDto.Id;
                }
                else
                {
                    var movieName2 = ConvertToProperNameCase(movieName);
                    parser.SeriesName = movieName2;
                    parser.ToBeMapped = true;
                }
                
            }

            return null;
        }

        public void GetEpisodeName( ParserDto parser)
        {
            Boolean epStart = false;
            Boolean epsNameStart = false;
            string _series = "";
            string theRest = "";
            string _last2dt = "";
            int cnt = 0;
            int enCnt = 0;
            var words = SpiltName(parser.IncomingName);
            try
            {
                if (parser.EpisodeId.Length > 2)
                {
                    _last2dt = parser.EpisodeId.Substring(parser.EpisodeId.Length - 2, 2);
                }
                foreach (string word in words)
                {
                    if ((epStart == false) && (_series.Length == 0))
                    {
                        _series = _series + word;
                        epsNameStart = false;
                    }
                    else
                    {
                        if ((cnt > 0) && (epsNameStart == false) && (enCnt == 0))
                        {
                            enCnt++;
                            epStart = true;
                            epsNameStart = false;
                        }
                        else if ((parser.EpisodeType == "date") && (word == _last2dt) && (enCnt == 2))
                        {
                            enCnt++;
                            epStart = false;
                            epsNameStart = false;
                        }
                        else if ((parser.EpisodeType == "date") && (enCnt > 0) && (epStart == true))
                        {
                            enCnt++;
                            epStart = true;
                            epsNameStart = false;
                        }
                        else if ((parser.EpisodeType == "date") && (enCnt == 3))
                        {
                            epsNameStart = true;
                        }
                        if (epsNameStart == true)
                        {
                            if ((word == "XXX") ||(word == "2160p") || (word == "1080p") || (word == "720p"))
                            {
                                epsNameStart = false;
                                epStart = false;
                            }
                            else
                            {
                                if ((word == "rar") || (word == "mp4"))
                                {
                                    theRest = theRest + "";
                                }
                                else if ((word.Length > 3) && (word.Substring(0, 3).ToLower() == "mp4"))
                                {
                                    theRest = theRest + "";
                                }
                                else if (epsNameStart == true)
                                {
                                    theRest = theRest + " " + word;
                                }
                            }
                        }
                    }
                    cnt++;
                }
                parser.EpisodeName = ConvertToProperNameCase(theRest.Trim());
            }
            catch (Exception ex)
            {
                _logger.LogDebug("GetEpisodeName" + ex.Message);
            }
        }

        public async Task<ParserDto> BuildFullPath(EpisodeDto episode)
        {
            var parserDto = await MapToParser(episode);
            await BuildOutputFullPath(parserDto);
            return parserDto;
        }
        
        public string ConvertToProperNameCase(string input)
        {
            char[] chars = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower()).ToCharArray();

            for (int i = 0; i + 1 < chars.Length; i++)
            {
                if ((chars[i].Equals('\'')) ||
                    (chars[i].Equals('-')))
                {
                    chars[i + 1] = Char.ToUpper(chars[i + 1]);
                }
            }
            return new string(chars);
        }

        private void GetMediaTypeFromDir(ParserDto parser )
        {
            var words = SpiltName(parser.IncomingName);
            if (parser.MediaType == MediaType.Other) 
            {
                foreach(var word in words)
                {
                    if (word == "Movies")
                    {
                        parser.MediaType = MediaType.Movie;
                    }
                }
            }
        }
        
        private List<string> SpiltDir(string input)
        {
            List<string> outList = new List<string>();
            string[] prases = input.Split('/');
            if (prases.Count() > 1)
            {
                foreach (string prase in prases)
                {
                    outList.Add(prase); 
                }
            }
            return outList;
        }
        
        public async Task<MediaType> GetMediaType(ParserDto parser)
        {
            var result = parser.MediaType;

            if (parser.MediaType == MediaType.Other)
            {
                if  (parser.ListName == ListType.Index)
                {
                    if (parser.Category.Length > 0)
                    {
                        if (parser.Category.Substring(0, 2) == "TV")
                        {
                            parser.MediaType = MediaType.Episode;
                        }
                        else if (parser.Category.Substring(0, 2) == "Mov")
                        {
                            parser.MediaType = MediaType.Movie;
                        }
                    }
                }
                if (parser.ListName == ListType.Current)
                {
                    List<string> dirwords = new List<string>();
                    if (parser.Directory.Length > 0)
                    {
                        dirwords = SpiltDir(parser.Directory);
                        if (parser.MediaType == MediaType.Other)
                        {
                            GetMediaTypeFromDir(parser);
                        }
                    }
                }
                if (parser.MediaType == MediaType.Other) 
                {
                    await GetOtherName(parser);
                }
            }
            return result;
        }

        public void checkToBeMapped(ParserDto parser)
        {
            if (parser.ToBeMapped == true)
            {
                if ((parser.MediaType == MediaType.Episode) 
                    && (parser.SeriesLink != Guid.Empty)
                    && (parser.SeasonNum > 0) && (parser.EpisodeNum > 0))
                {
                    parser.ToBeMapped = false;
                }
                if ((parser.MediaType == MediaType.Episode) 
                    && (parser.SeriesLink != Guid.Empty) 
                    && (parser.SeasonNum == 0) 
                    && (parser.EpisodeNum > 0))
                {
                    parser.ToBeMapped = false;
                }
            }
        }

        public void GetResolution( ParserDto parser)
        {
            if(parser.IncomingName.IsNullOrEmpty())
            {
                parser.IncomingName  = Path.GetFileName(parser.IncomingFullPath);
            }

            var words = SpiltName(parser.IncomingName);
            if (parser.Resolution.IsNullOrEmpty())
            {
                foreach (string word in words)
                {
                    if (word.ToUpper() == "1080P")
                    {
                        parser.Resolution = "1080p";
                    }
                    else if (word.ToUpper() == "720P")
                    {
                        parser.Resolution = "720p";
                    }
                }
            }
            // second try
            if (parser.Resolution.IsNullOrEmpty())
            {
                var dirwords2 = SpiltDir(parser.Directory);

                foreach (var word in dirwords2)
                {
                    if (word.ToUpper() == "1080P")
                    {
                        parser.Resolution = "1080p";
                    }
                    else if (word.ToUpper() == "720P")
                    {
                        parser.Resolution = "720p";
                    }
                }
            }
        }

        private async Task GetEpisodeLink(ParserDto parser)
        {
            var series = await  _seriesService.GetByIdAsync(parser.SeriesLink);
            try
            {
                var episodeDto = await _episodeService.FindBySeriesIdSeasonEpisodeNum(series.Id,
                    parser.SeasonNum, parser.EpisodeNum);

                if (episodeDto != null)
                {
                    if (episodeDto.Id != Guid.Empty)
                    {
                        parser.Link = episodeDto.Id;
                    }
                }
                else
                {
                    parser.Link = Guid.Empty;   
                }
            }
            catch
            {
                parser.Link = Guid.Empty;
            }
        }

        private async Task GetSeriesDirectory(ParserDto parser)
        {
            var myLink = parser.SeriesLink;
            if (myLink != Guid.Empty)
            {
                var sa = await _seriesAliasService.GetBySeriesIdType(
                    myLink,"folder");
                parser.Directory = sa.IdValue;
            }
            else
            {
                parser.Directory = "";
            }
        }

        public async Task GetOtherName(ParserDto parser)
        {
            int idx;
            bool isSeries = false;
            string mediaName = "";
            try
            {
                string pattern = @"\.S\d+.";
                string x = Regex.Match(parser.IncomingName.ToUpper(), pattern).ToString();
                if (x.Length > 0)
                {
                    idx = parser.IncomingName.ToUpper().LastIndexOf(x, StringComparison.Ordinal);
                    mediaName = parser.IncomingName.Substring(0, idx);
                    parser.MediaType = MediaType.Episode;
                    isSeries = true;
                }

                if (isSeries == false)
                {
                    pattern = @"\ S\d+.";
                    x = Regex.Match(parser.IncomingName.ToUpper(), pattern).ToString();
                    if (x.Length > 0)
                    {
                        idx = parser.IncomingName.ToUpper().LastIndexOf(x, StringComparison.Ordinal);
                        mediaName = parser.IncomingName.Substring(0, idx);
                    }

                    if (mediaName.Length == 0)
                    {
                        pattern = @"\.\d+.";
                        x = Regex.Match(parser.IncomingName, pattern).ToString();
                        if (x.Length > 0)
                        {
                            idx = parser.IncomingName.LastIndexOf(x, StringComparison.Ordinal);
                            mediaName = parser.IncomingName.Substring(0, idx);
                            parser.MediaType = MediaType.Movie;
                        }
                    }

                    if (mediaName.Length == 0)
                    {
                        pattern = @"\d+x\d+";
                        x = Regex.Match(parser.IncomingName, pattern).ToString();
                        if (x.Length > 0)
                        {
                            idx = parser.IncomingName.LastIndexOf(x, StringComparison.Ordinal);
                            mediaName = parser.IncomingName.Substring(0, idx).TrimEnd();
                            if (mediaName.Substring(mediaName.Length - 1, 1) == "-")
                            {
                                mediaName = mediaName.Substring(0, mediaName.Length - 1).TrimEnd();
                            }
                        }
                    }

                    if (mediaName.Length == 0)
                    {
                        pattern = @"\ S\d+.";
                        x = Regex.Match(parser.IncomingName, pattern).ToString();
                        if (x.Length > 0)
                        {
                            idx = parser.IncomingName.LastIndexOf(x, StringComparison.Ordinal);
                            mediaName = parser.IncomingName.Substring(0, idx).TrimEnd();
                            if (mediaName.Substring(mediaName.Length - 1, 1) == "-")
                            {
                                mediaName = mediaName.Substring(0, mediaName.Length - 1).TrimEnd();
                            }
                        }
                    }

                    if (mediaName.Length == 0)
                    {
                        pattern = @"\ \d+.";
                        x = Regex.Match(parser.IncomingName, pattern).ToString();
                        if (x.Length > 0)
                        {
                            idx = parser.IncomingName.LastIndexOf(x, StringComparison.Ordinal);
                            mediaName = parser.IncomingName.Substring(0, idx).TrimEnd();
                            if (mediaName.Substring(mediaName.Length - 1, 1) == "-")
                            {
                                mediaName = mediaName.Substring(0, mediaName.Length - 1).TrimEnd();
                            }
                        }
                    }
                }

                if ((mediaName.Length > 1) && (parser.MediaType == MediaType.Episode))
                {
                    string sName = ConvertToProperNameCase(mediaName);
                    sName = sName.Trim();
                    parser.SeriesName = sName;
                    parser.ToBeMapped = true;
                    parser.MediaType = MediaType.Episode;  
                    var seriesAlias = await _seriesAliasService.GetByIdValue(parser.SeriesName);
                
                    if (seriesAlias != null)
                    {
                        parser.SeriesLink = seriesAlias.SeriesId;
                    }
                }
                else if ((mediaName.Length > 1) && (parser.MediaType == MediaType.Movie))
                {
                    string sName = ConvertToProperNameCase(mediaName);
                    sName = sName.Trim();
                    parser.SeriesName = sName;
                    parser.ToBeMapped = true;
             
                    var movieAlias = await _movieAliasService.GetByIdValue(parser.SeriesName.ToLower());
                
                    if (movieAlias != null)
                    {
                        parser.SeriesLink = movieAlias.MovieId;
                    }
                }
            }
            catch
            {
                string pattern = @"\.\d+.";
                string x = Regex.Match(parser.IncomingName, pattern).ToString();
                if (x.Length > 0)
                {
                    idx = parser.IncomingName.LastIndexOf(x, StringComparison.Ordinal);
                    mediaName = parser.IncomingName.Substring(0, idx);
                }
            }
        }
        
        private async Task GetSeriesName( ParserDto parser)
        {
            int idx;
            string series = "";
            try
            {
                string pattern = @"\.S\d+.";
                string x = Regex.Match(parser.IncomingName.ToUpper(), pattern).ToString();
                if (x.Length > 0)
                {
                    idx = parser.IncomingName.ToUpper().LastIndexOf(x, StringComparison.Ordinal);
                    series = parser.IncomingName.Substring(0, idx);
                }
                pattern = @"\ S\d+.";
                x = Regex.Match(parser.IncomingName.ToUpper(), pattern).ToString();
                if (x.Length > 0)
                {
                    idx = parser.IncomingName.ToUpper().LastIndexOf(x, StringComparison.Ordinal);
                    series = parser.IncomingName.Substring(0, idx);
                }
                if (series.Length == 0)
                {
                    pattern = @"\.\d+.";
                    x = Regex.Match(parser.IncomingName, pattern).ToString();
                    if (x.Length > 0)
                    {
                        idx = parser.IncomingName.LastIndexOf(x, StringComparison.Ordinal);
                        series = parser.IncomingName.Substring(0, idx);
                    }
                }
                if (series.Length == 0)
                {
                    pattern = @"\d+x\d+";
                    x = Regex.Match(parser.IncomingName, pattern).ToString();
                    if (x.Length > 0)
                    {
                        idx = parser.IncomingName.LastIndexOf(x, StringComparison.Ordinal);
                        series = parser.IncomingName.Substring(0, idx).TrimEnd();
                        if (series.Substring(series.Length - 1, 1) == "-")
                        {
                            series = series.Substring(0, series.Length - 1).TrimEnd();
                        }
                    }
                }
                if (series.Length == 0)
                {
                    pattern = @"\ S\d+.";
                    x = Regex.Match(parser.IncomingName, pattern).ToString();
                    if (x.Length > 0)
                    {
                        idx = parser.IncomingName.LastIndexOf(x, StringComparison.Ordinal);
                        series = parser.IncomingName.Substring(0, idx).TrimEnd();
                        if (series.Substring(series.Length - 1, 1) == "-")
                        {
                            series = series.Substring(0, series.Length - 1).TrimEnd();
                        }
                    }
                }
                if (series.Length == 0)
                {
                    pattern = @"\ \d+.";
                    x = Regex.Match(parser.IncomingName, pattern).ToString();
                    if (x.Length > 0)
                    {
                        idx = parser.IncomingName.LastIndexOf(x, StringComparison.Ordinal);
                        series = parser.IncomingName.Substring(0, idx).TrimEnd();
                        if (series.Substring(series.Length - 1, 1) == "-")
                        {
                            series = series.Substring(0, series.Length - 1).TrimEnd();
                        }
                    }
                }
                if (series.Length > 1)
                {
                    string sName = ConvertToProperNameCase(series);
                    sName = sName.Trim();
                    parser.SeriesName = sName;
                    parser.ToBeMapped = true;
                    parser.MediaType = MediaType.Episode;  
                    var seriesAlias = await _seriesAliasService.GetByIdValue(parser.SeriesName);
                
                    if (seriesAlias != null)
                    {
                        parser.SeriesLink = seriesAlias.SeriesId;
                    }
                }
                else
                { 
                    _logger.LogDebug("Cannot Parse Season and Episode Number for:" + parser.IncomingName);
                }
            }
            catch
            {
                string pattern = @"\.\d+.";
                string x = Regex.Match(parser.IncomingName, pattern).ToString();
                if (x.Length > 0)
                {
                    idx = parser.IncomingName.LastIndexOf(x, StringComparison.Ordinal);
                    series = parser.IncomingName.Substring(0, idx);
                }
            }
        }

        public async Task BuildFilename(ParserDto parser)
        {
            string filename = "";
            if (parser.SeriesLink != Guid.Empty) 
            {
                var seriesList = await _seriesService.GetByIdAsync(parser.SeriesLink);

                if (seriesList != null)
                {
                    parser.SeriesName = seriesList.Name;
                }
            }

            if (parser.Extn.IsNullOrEmpty())
            {
                
            }
            if (parser.MediaType == MediaType.Episode)
            {
                if ((parser.Extn == null) || (parser.Extn == ""))
                {
                    parser.Extn = Path.GetExtension(parser.IncomingName);
                }
                if ((parser.Resolution == null) || (parser.Resolution == ""))
                {
                    GetResolution(parser);
                    if (parser.Resolution.IsNullOrEmpty())
                    {
                        filename = parser.SeriesName + "." + parser.SeasonEpisode
                            + parser.Extn;
                    }
                    else 
                    {
                        filename = parser.SeriesName + "." + parser.SeasonEpisode
                        + "." + parser.Resolution + parser.Extn;
                    }
                }
                else
                {
                    //BuildSeasonEpisode(parser);
                    filename = parser.SeriesName + "." + parser.SeasonEpisode
                        + "." + parser.Resolution + parser.Extn;
                }
            }
            if (Regex.Matches(filename, @"\.\.").Count == 1)
            {
                filename = Regex.Replace(filename, @"\.\.", @".");
            }
            if (parser.SeriesName != null)
            {
                parser.OutputName = filename;
            }
        }

        public async Task GetSeriesEpisodeId( ParserDto parser, string _format)
        {
            Boolean epStart = false;

            string series = "";
            string episode_id = "";
            int wordCnt = 0;
            var words = SpiltName(parser.IncomingName);
            try
            {
                if ((parser.MediaType == MediaType.Episode) && (_format == "date"))
                {
                    epStart = false;
                    bool epIdStart = false;
                    wordCnt = 0;

                    foreach (string word in words)
                    {
                        if ((epStart == false) && (series.Length == 0) && (wordCnt == 0))
                        {
                            series = series + word;
                            epStart = true;
                            epIdStart = false;
                        }
                        if ((epStart == true) && (series.Length > 0) && (wordCnt > 0))
                        {
                            int o;
                            if ((int.TryParse(word, out o) == true) && (epStart == true))
                            {
                                epIdStart = true;
                                epStart = false;
                            }
                            else if (word.Substring(0, 1).ToUpper() == "E")
                            {
                                string epsNo = word.Substring(1, word.Length - 1);
                                if ((int.TryParse(epsNo, out o) == true))
                                {
                                    epStart = true;
                                }
                            }
                            else
                            {
                                series = series + " " + word;
                                epStart = true;
                            }
                        }
                        if ((epStart == false) && (epIdStart == true) && (episode_id.Length == 0))
                        {
                            episode_id = episode_id + word.ToUpper();
                        }
                        else
                        {
                            if ((epIdStart == true) && (wordCnt > 1))
                            {
                                int p;
                                if ((int.TryParse(word, out p) == false) && (epIdStart == true))
                                {
                                    epIdStart = false;
                                }
                                else
                                {
                                    episode_id = episode_id + word.ToUpper();
                                }
                            }
                        }
                        wordCnt++;
                        if (episode_id.Length == 3)
                        {
                            parser.EpisodeId = episode_id;
                        }
                    }
                }
                if ((parser.MediaType == MediaType.Episode) && (_format == ""))
                {
                    await GetSeasonEps( parser);
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
            }
        }

        private async Task GetSeasonEps( ParserDto parser)
        {
            var seseps = "";
            var alias = parser.SeriesName;
            List<string> words = SpiltName(parser.IncomingName);
          
            try
            {
                foreach (string word in words)
                {
                    string pattern = @"\bs\d+e\d+\b";
                    string wordUpper = word.ToUpper();
                    Match m = Regex.Match(wordUpper, pattern, RegexOptions.IgnoreCase);
                    if (m.Success)
                    {
                        seseps = word.ToUpper();
                        break;
                    }
                }
                if (seseps.Length > 0)
                {
                    if (seseps.Length == 3)
                    {
                        parser.SeasonNum = Convert.ToInt32(seseps.Substring(0, 1));
                        parser.EpisodeNum = Convert.ToInt32(seseps.Substring(1, 2));
                    }
                    else if (seseps.Length == 4)
                    {
                        parser.SeasonNum = Convert.ToInt32(seseps.Substring(0, 2));
                        parser.EpisodeNum = Convert.ToInt32(seseps.Substring(2, 2));
                    }
                    else if (seseps.Length == 6)
                    {
                        string[] tmp = seseps.Split('E');
                        parser.SeasonNum = Convert.ToInt32(tmp[0].Substring(1, 2));
                        parser.EpisodeNum = Convert.ToInt32(tmp[1]);
                    }
                    else
                    {
                        string[] tmp = seseps.Split('E');
                        parser.SeasonNum = Convert.ToInt32(tmp[0].Substring(1, 2));
                        parser.EpisodeNum = Convert.ToInt32(tmp[1]);
                    }
                }
                await Task.Run(() => BuildSeasonEpisode(parser));
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
            }
        }

        public void BuildSeasonEpisode(ParserDto parser)
        {
            string seasonEps = "";
            if ((parser.SeasonNum > 0) && (parser.EpisodeNum > 0))
            {
                seasonEps = "S" + parser.SeasonNum.ToString().PadLeft(2, '0')
                                + "E" + parser.EpisodeNum.ToString().PadLeft(2, '0');
            }
            parser.SeasonEpisode = seasonEps;
        }
    }