using Newtonsoft.Json;
using System;

namespace DelugeRPCClient.Net.Models
{
    public class TorrentOptions
    {
        [JsonProperty(PropertyName = "move_completed_path")]
        public String MoveCompletedPath { get; set; }
    }
}
