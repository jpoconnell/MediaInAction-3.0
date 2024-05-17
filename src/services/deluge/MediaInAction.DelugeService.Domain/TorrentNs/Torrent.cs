using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.DelugeService.TorrentNs
{
    public class Torrent : AuditedAggregateRoot<Guid>
    {
        public string Comment { get; set; }
        public bool IsSeed { get; set; }
        public string Hash { get; set; }
        public bool Paused { get; set; }
        public double Ratio { get; set; }

        public string Message { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public long Added { get; set; }
        public double CompleteTime { get; set; }
        public string DownloadLocation { get; set; }
        

        private Torrent()
        {
        }

        internal Torrent(
            Guid id,
            string comment,
            bool isSeed,
            string hash,
            bool paused,
            double ratio, 
            string message,
            string name,
            string label,
            long added,
            double completeTime,
            string location
        )
            : base(id)
        {
            Name = name;
            Hash = hash;
            Label = label;
            Added = added;
            Ratio = ratio;
            Comment = comment;
            CompleteTime = completeTime;
            DownloadLocation = location;
            IsSeed = isSeed;
            Paused = paused;
            Message = message;
        }
    }
}
