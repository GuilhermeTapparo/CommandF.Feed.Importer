using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandF.Feed.Importer.Models
{
    public class CFFeedItem
    {
        [BsonId]
        public Guid Id { get; set; }

        [JsonProperty("Metadata", NullValueHandling = NullValueHandling.Ignore)]
        public Metadata Metadata { get; set; }

        [JsonProperty("Content", NullValueHandling = NullValueHandling.Ignore)]
        public Content Content { get; set; }
    }

    public class Content
    {
        [JsonProperty("Title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("SourceLink", NullValueHandling = NullValueHandling.Ignore)]
        public string SourceLink { get; set; }

        [JsonProperty("MainImage", NullValueHandling = NullValueHandling.Ignore)]
        public string MainImage { get; set; }

        [JsonProperty("Sumary", NullValueHandling = NullValueHandling.Ignore)]
        public string Sumary { get; set; }

        [JsonProperty("Description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("PubDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? PubDate { get; set; }

        [JsonProperty("Tags", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Tags { get; set; }

        [JsonProperty("Author", NullValueHandling = NullValueHandling.Ignore)]
        public string Author { get; set; }
    }

    public class Metadata
    {
        [JsonProperty("SystemSource", NullValueHandling = NullValueHandling.Ignore)]
        public string SystemSource { get; set; }

        [JsonProperty("IsGlobal", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsGlobal { get; set; }

        [JsonProperty("Username", NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }

        [JsonProperty("SourceCategory", NullValueHandling = NullValueHandling.Ignore)]
        public string SourceCategory { get; set; }

        [JsonProperty("DestinationCategory", NullValueHandling = NullValueHandling.Ignore)]
        public string DestinationCategory { get; set; }

        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }

        [JsonProperty("SourceId", NullValueHandling = NullValueHandling.Ignore)]
        public string SourceId { get; set; }

        [JsonProperty("ImportedAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ImportedAt { get; set; }

        [JsonProperty("UpdatedAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime UpdatedAt { get; set; }
    }
}