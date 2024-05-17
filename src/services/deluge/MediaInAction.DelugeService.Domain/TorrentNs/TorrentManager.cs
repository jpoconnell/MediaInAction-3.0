using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.DelugeService.TorrentNs
{
    public class TorrentManager : DomainService
    {
        private readonly ITorrentRepository _torrentRepository;
        private readonly IDistributedEventBus _distributedEventBus;
        private ILogger<TorrentManager> _logger;
        
        public TorrentManager(
            ITorrentRepository torrentRepository,
            IDistributedEventBus distributedEventBus,
            ILogger<TorrentManager> logger)
        {
            _distributedEventBus = distributedEventBus;
            _torrentRepository = torrentRepository;
            _logger = logger;
        }

        public async Task<Torrent> CreateAsync(
            string comment,
            string name,
            bool isSeed,
            string hash,
            bool paused,
            double ratio,
            string message,
            string label,
            long added,
            double completed,
            string downloadLocation)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(hash, nameof(hash));
            var existingTorrent = await _torrentRepository.GetByHashAsync(hash);
            if (existingTorrent == null)
            {
                var newTorrent = new Torrent(
                    GuidGenerator.Create(),
                    comment: comment,
                    isSeed: isSeed,
                    hash: hash,
                    paused: paused,
                    ratio: ratio,
                    message: message,
                    name: name,
                    label: label,
                    added: added,
                    completeTime: completed,
                    location: downloadLocation
                );
                try
                {
                    var createdTorrent = await _torrentRepository.InsertAsync(newTorrent, true);
                    await SendCreateEvent(createdTorrent);
                }
                catch (Exception ex)
                {
                    _logger.LogDebug("CreateAsync:" + ex.Message);
                    return null;
                }
            }
            else
            {
                _logger.LogInformation("Update if Changed");
            }
            return null;
        }

        private async Task SendCreateEvent(Torrent torrent)
        {
            // Publish Torrent create event
            _logger.LogInformation("Sending Torrent Created Event: "  );
            await _distributedEventBus.PublishAsync(new TorrentCreatedEto
            {
                Name = torrent.Name,
                Hash = torrent.Hash,
                Added = torrent.Added,
                CompleteTime = torrent.CompleteTime,
                IsSeed = torrent.IsSeed,
                Label = torrent.Label,
                Paused = torrent.Paused
            });
            _logger.LogInformation("Sending Torrent Created Event Sent: "  );
        }
    }
}
