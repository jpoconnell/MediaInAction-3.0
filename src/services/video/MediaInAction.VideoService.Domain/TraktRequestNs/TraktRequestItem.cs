using System;
using Volo.Abp.Domain.Entities;

namespace MediaInAction.VideoService.TraktRequestNs
{
    public class TraktRequestItem :  Entity<Guid>
    {
        public string Slug { get; set; }
        public int Season  { get; set; }
        
        protected TraktRequestItem() { }
        
        public TraktRequestItem(Guid id,
            string slug, 
            Int32 season
        )
            : base(id)
        {
            Slug = slug;
            Season = season;
        }
    }
}
