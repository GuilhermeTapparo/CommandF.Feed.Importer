using CommandF.Feed.Importer.Models;
using CommandF.Feed.Importer.Repositories.Feeds;
using CommandF.Feed.Importer.Services.Feed;
using CommandF.Feed.Importer.Services.Mappers;
using CommandF.Feed.Importer.Services.RSS;
using CommandF.Feed.Importer.Utils;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace CommandF.Feed.Importer.Manager
{
    public class FeedManager : IFeedManager
    {
        private readonly ILogger<FeedManager> logger;
        private readonly IFeedConfigService configService;
        private readonly IRssService rssService;
        private readonly IFeedRepository repository;
        private readonly IMapper<CodeHollow.FeedReader.Feed, List<CFFeedItem>> mapper;

        public FeedManager(ILogger<FeedManager> logger, IFeedConfigService configService, IRssService rssService, IFeedRepository repository, IMapper<CodeHollow.FeedReader.Feed, List<CFFeedItem>> mapper)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.configService = configService ?? throw new ArgumentNullException(nameof(configService));
            this.rssService = rssService ?? throw new ArgumentNullException(nameof(rssService));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task StartAsync()
        {
            var operationId = Guid.NewGuid();
            try
            {
                var configs = await configService.FetchConfigs();
                foreach(var config in configs)
                    await HandleConfig(operationId, config);

            } catch(Exception e)
            {
                logger.LogError(e.Message, e.StackTrace);
            }
        }

        private async Task HandleConfig(Guid operationId, Services.Feed.Models.FeedConfig config)
        {
            try
            {
                var feedItems = await rssService.GetFeedAsync(config.Feed.Url, operationId);
                var mappedItems = mapper.Map(feedItems, config, operationId);
                await SaveNewFeedsAsync(mappedItems, operationId);
            }
            catch (Exception e)
            {
                var errorMessage = $"[{operationId} - {BrazilDateTime.GetCurrentDate()}] Failed to handle config {config.Feed.Url}. {e.Message} {e.StackTrace}";
                logger.LogError(errorMessage);
            }
        }

        private async Task SaveNewFeedsAsync(List<CFFeedItem> feeds, Guid operationId)
        {
            foreach (var item in feeds)
            {
                try
                {
                    if (await repository.AlreadyExists(item.Metadata.SourceId))
                        continue;

                    await repository.Add(item);
                }
                catch (Exception e)
                {
                    var errorMessage = $"[{operationId} - {BrazilDateTime.GetCurrentDate()}] Failed to save item in database. {e.Message} {e.StackTrace} - Item: {JsonConvert.SerializeObject(item)}";
                    logger.LogError(errorMessage);
                }
            }
        }
    }
}
