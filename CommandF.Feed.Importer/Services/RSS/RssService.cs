using CodeHollow.FeedReader;
using CommandF.Feed.Importer.Models;
using CommandF.Feed.Importer.Services.RSS.Exceptions;
using CommandF.Feed.Importer.Utils;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CommandF.Feed.Importer.Services.RSS
{
    public class RssService : IRssService
    {
        private readonly ILogger<RssService> logger;

        public RssService(ILogger<RssService> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public List<SyndicationItem> GetFeedItems(string feedUrl, Guid operationId)
        {
            try
            {
                XmlReader reader = XmlReader.Create(feedUrl);
                SyndicationFeed feed = SyndicationFeed.Load(reader);

                reader.Close();

                return feed.Items.ToList();
            }
            catch (Exception e)
            {
                var errorMessage = $"[{operationId} - {BrazilDateTime.GetCurrentDate()}] {e.Message} - {e.StackTrace}";
                logger.LogError(errorMessage);
                throw new RssServiceException(errorMessage);
            }
        }

        // TODO: usar feed reader
        public async Task<CodeHollow.FeedReader.Feed> GetFeedAsync(string url, Guid operationId)
        {
            try
            {
                return FeedReader.ReadAsync(url).GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                var errorMessage = $"[{operationId} - {BrazilDateTime.GetCurrentDate()}] {e.Message} - {e.StackTrace}";
                logger.LogError(errorMessage);
                throw new RssServiceException(errorMessage);
            }

        }
    }
}
