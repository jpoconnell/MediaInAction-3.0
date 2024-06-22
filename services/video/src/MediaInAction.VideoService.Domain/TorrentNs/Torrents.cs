using System;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.VideoService.TorrentNs
{
    public class Torrent :  AuditedAggregateRoot<Guid>
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
        public FileStatus TorrentStatus { get; set; }
        public MediaType Type { get; set; }
        public Guid MediaLink { get; set; }
        public Guid EpisodeLink { get; set; }
        public bool IsMapped { get; set; }
        
        protected Torrent() { }
        
        public Torrent(Guid id,
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
            string downloadLocation,
            string status,
            MediaType type = MediaType.Unknown,
            bool isMapped = false
        )
        
        {
            Comment = comment;
            IsSeed = isSeed;
            Hash = hash;
            Paused = paused;
            Ratio = ratio;
            Message = message;
            Name = name;
            Label = label;
            Added = added;
            CompleteTime = completeTime;
            DownloadLocation = downloadLocation;
            TorrentStatus = TranslateStatus(status) ;;
            Type = type;
            MediaLink =  Guid.Empty;
            EpisodeLink = Guid.Empty;
            IsMapped = isMapped;
        }

        private FileStatus TranslateStatus(string status)
        {
            return FileStatus.New;
        }
    }
}
