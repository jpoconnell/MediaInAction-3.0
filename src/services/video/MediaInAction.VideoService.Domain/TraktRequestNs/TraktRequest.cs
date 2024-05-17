using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.VideoService.TraktRequestNs
{
    public class TraktRequest :  AuditedAggregateRoot<Guid>
    {
        public string Command { get; set; }
        public DateTime Created  { get; set; }
        public bool IsComplete { get; set; }
        public DateTime CompleteTime { get; set; }

        public List<TraktRequestItem> RequestItems { get; set; }
        
        protected TraktRequest() { }
        
        public TraktRequest(Guid id,
            string command
        )
            : base(id)
        {
            Command = command;
            Created = DateTime.Now;
            IsComplete = false;
        }
    }
}
