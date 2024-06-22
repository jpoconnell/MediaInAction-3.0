using System;
using Volo.Abp.Domain.Entities;

namespace  MediaInAction.EmbyService.EmbyItems;

public class EmbyItem : Entity<Guid>
{
        public string Name { get; set; }
        public int EmbyItemLevel { get; set; }
        public string ServerId { get; set; }
        public string EmbyItemId { get; set; }
        public long? RunTimeTicks { get; set; }
        public string ParentId { get; set; }
        public string Type { get; set; }
        public string MediaType { get; set; }

        public EmbyItem()
        {
        }

        internal EmbyItem(
            Guid id,
            string externalId,
            string serverId,
            string type,
            int level,
            long? runTimeTicks,
            string parentId,
            string mediaType
        )
            : base(id)
        {
            ServerId = serverId;
            Type = type;
            EmbyItemId = externalId;
            EmbyItemLevel = level;
            ParentId = parentId;
            MediaType = mediaType;
            RunTimeTicks = runTimeTicks;
        }
}
