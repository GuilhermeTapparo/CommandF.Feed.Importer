using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandF.Feed.Importer.Services.Feed.Models
{
    public class FeedConfig
    {
        public Guid Id { get; set; }

        [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("Metadata", NullValueHandling = NullValueHandling.Ignore)]
        public Metadata Metadata { get; set; }

        [JsonProperty("Feed", NullValueHandling = NullValueHandling.Ignore)]
        public Feed Feed { get; set; }
    }

    public class Feed
    {
        [JsonProperty("Url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }

        [JsonProperty("RefreshCycleInMinutes", NullValueHandling = NullValueHandling.Ignore)]
        public long RefreshCycleInMinutes { get; set; }

        [JsonProperty("Topic", NullValueHandling = NullValueHandling.Ignore)]
        public string Topic { get; set; }

        [JsonProperty("Tags", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Tags { get; set; }
    }

    public class Metadata
    {
        [JsonProperty("Username", NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }

        [JsonProperty("IsGlobal", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsGlobal { get; set; }

        [JsonProperty("Status", NullValueHandling = NullValueHandling.Ignore)]
        public FeedStatus Status { get; set; }

        [JsonProperty("CreatedAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("UpdatedAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime UpdatedAt { get; set; }
    }
}
