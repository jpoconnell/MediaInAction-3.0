using System;

namespace MediaInAction.VideoService.MovieAliasNs.Dtos;
    public class MovieAliasCreateDto
    {
        public Guid MovieId { get; set; }
        public string IdType { get; set; }
        public string IdValue { get; set; }
    }


