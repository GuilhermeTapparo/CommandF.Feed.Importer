using CommandF.Feed.Importer.Models;
using CommandF.Feed.Importer.Services.Feed.Models;
using CommandF.Feed.Importer.Services.Mappers.Exceptions;
using CommandF.Feed.Importer.Utils;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;

namespace CommandF.Feed.Importer.Services.Mappers
{
    public class SyndicationToCFFeedMapper : IMapper<SyndicationItem, CFFeedItem>, IMapper<List<SyndicationItem>, List<CFFeedItem>>
    {
        private readonly ILogger<SyndicationToCFFeedMapper> logger;

        //Deprecated
        public SyndicationToCFFeedMapper(ILogger<SyndicationToCFFeedMapper> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public CFFeedItem Map(SyndicationItem source, FeedConfig config, Guid operationId)
        {
            try
            {
                var cfItem = new CFFeedItem();
                cfItem.Metadata = MapMetadata(source, config);
                cfItem.Content = MapContent(source, config);

                return cfItem;
            } catch(Exception e)
            {
                var errorMessage = $"Failed to map item. Error: {e.Message} - {e.StackTrace} - SourceItem: {JsonConvert.SerializeObject(source)}";
                throw new MappingException(errorMessage);
            }
        }

        public List<CFFeedItem> Map(List<SyndicationItem> source, FeedConfig config, Guid operationId)
        {
            var cfItems = new List<CFFeedItem>();

            foreach (var sourceItem in source)
            {
                try
                {
                    cfItems.Add(this.Map(sourceItem, config, operationId));
                }
                catch (Exception e)
                {
                    var errorMessage = $"[{operationId} - {BrazilDateTime.GetCurrentDate()}] SKIPPING MAP ITEM: {e.Message} - {e.StackTrace}";
                    logger.LogWarning(errorMessage);
                }
            }

            return cfItems;
        }

        private Models.Metadata MapMetadata(SyndicationItem source, FeedConfig config)
        {
            var metadata = new Models.Metadata();

            metadata.SystemSource = config.Name;
            metadata.SourceCategory = source.Categories.FirstOrDefault()?.Name;
            metadata.DestinationCategory = config.Feed.Topic;
            metadata.Language = "pt-br";
            metadata.SourceId = source.Id;
            metadata.ImportedAt = BrazilDateTime.GetCurrentDate();
            metadata.UpdatedAt = BrazilDateTime.GetCurrentDate();
            metadata.IsGlobal = config.Metadata.IsGlobal;
            metadata.Username = config.Metadata.Username;

            return metadata;
        }

        private Content MapContent(SyndicationItem source, FeedConfig config)
        {
            var content = new Content();

            content.Title = source.Title.Text;
            content.SourceLink = source.Links.FirstOrDefault()?.Uri.ToString();
            content.MainImage = Regex.Match(source.Summary.Text, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value;
            content.Sumary = source.Summary.Text;
            content.Description = source.Summary.Text;
            content.PubDate = source.PublishDate.DateTime;
            content.Tags = config.Feed.Tags;
            content.Author = string.Join(" | ", source.Authors.Select(a => a.Name));

            return content;
        }
    }
}
