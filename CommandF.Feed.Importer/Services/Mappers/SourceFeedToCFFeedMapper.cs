using CommandF.Feed.Importer.Models;
using CommandF.Feed.Importer.Services.Feed.Models;
using CommandF.Feed.Importer.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CommandF.Feed.Importer.Services.Mappers
{
    public class SourceFeedToCFFeedMapper : IMapper<CodeHollow.FeedReader.Feed, List<CFFeedItem>>
    {
        private readonly ILogger<SourceFeedToCFFeedMapper> logger;

        public SourceFeedToCFFeedMapper(ILogger<SourceFeedToCFFeedMapper> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public List<CFFeedItem> Map(CodeHollow.FeedReader.Feed source, FeedConfig config, Guid operationId)
        {
            var result = new List<CFFeedItem>();

            foreach (var sourceItem in source.Items)
            {
                logger.LogInformation($"[{operationId} - {BrazilDateTime.GetCurrentDate()}] Reading: {sourceItem.Title}");
                try
                {
                    var cfItem = new CFFeedItem();
                    cfItem.Metadata = new Models.Metadata();
                    cfItem.Metadata.IsGlobal = config.Metadata.IsGlobal;
                    cfItem.Metadata.Language = "pt-br";
                    cfItem.Metadata.SourceCategory = sourceItem.Categories.FirstOrDefault();
                    cfItem.Metadata.DestinationCategory = config.Feed.Topic;
                    cfItem.Metadata.SourceId = sourceItem.Id;
                    cfItem.Metadata.SystemSource = config.Name;
                    cfItem.Metadata.Username = config.Metadata.Username;
                    cfItem.Metadata.ImportedAt = BrazilDateTime.GetCurrentDate();
                    cfItem.Metadata.UpdatedAt = BrazilDateTime.GetCurrentDate();

                    cfItem.Content = new Content();
                    cfItem.Content.Author = sourceItem.Author;
                    cfItem.Content.Description = string.IsNullOrEmpty(sourceItem.Content) ? sourceItem.Description : sourceItem.Content;
                    cfItem.Content.MainImage = Regex.Match(sourceItem.Description, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value;
                    cfItem.Content.PubDate = sourceItem.PublishingDate;
                    cfItem.Content.SourceLink = sourceItem.Link;
                    cfItem.Content.Sumary = sourceItem.Description;
                    cfItem.Content.Title = sourceItem.Title;
                    cfItem.Content.Tags = config.Feed.Tags;

                    result.Add(cfItem);
                }
                catch (Exception e)
                {
                    var errorMessage = $"[{operationId} - {BrazilDateTime.GetCurrentDate()}] SKIPPING MAP ITEM: {e.Message} - {e.StackTrace}";
                    logger.LogWarning(errorMessage);
                }
            }

            return result;
        }
    }
}
