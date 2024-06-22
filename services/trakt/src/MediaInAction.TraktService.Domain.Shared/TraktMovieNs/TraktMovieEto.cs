using System.Collections.Generic;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.TraktService.TraktMovieNs
{
    public class TraktMovieEto : EtoBase
    {
        public string Name { get;  set; }
        public int FirstAiredYear { get; set; }
        public FileStatus MovieStatus { get; set; }
        public bool IsActive { get; set; }
        public List<TraktMovieAliasEto> MovieAliasEtos { get; set; }
    }
}