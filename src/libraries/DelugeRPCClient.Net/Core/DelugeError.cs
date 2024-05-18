using Newtonsoft.Json;
using System;

namespace DelugeRPCClient.Net.Core
{
    internal class DelugeError
    {
        [JsonProperty(PropertyName = "messag")]
        public String Message { get; set; }

        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }
    }
}
