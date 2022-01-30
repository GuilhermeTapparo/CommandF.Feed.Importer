using CommandF.Feed.Importer.Manager;
using CommandF.Feed.Importer.Models;
using CommandF.Feed.Importer.Repositories.Feeds;
using CommandF.Feed.Importer.Repositories.Feeds.Index;
using CommandF.Feed.Importer.Services.Feed;
using CommandF.Feed.Importer.Services.Mappers;
using CommandF.Feed.Importer.Services.RSS;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;

namespace CommandF.Feed.Importer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ServiceCollection collection = new ServiceCollection();


            collection.AddLogging(_ => {
                _.AddConsole();
                _.AddDebug();
            });

            var client = new MongoClient(EnvironmentConfig.MongoDb.ConnectionString);
            collection.AddSingleton<IMongoClient>(_ => client);
            collection.AddSingleton<IIndexManager, IndexManager>();
            collection.AddScoped<IFeedRepository, FeedRepository>();

            collection.AddSingleton<IFeedManager, FeedManager>();
            collection.AddScoped<IFeedConfigService, FeedConfigService>();
            collection.AddScoped<IRssService, RssService>();

            // collection.AddScoped<IMapper<List<SyndicationItem>, List<CFFeedItem>>, SyndicationToCFFeedMapper>();
            collection.AddScoped<IMapper<CodeHollow.FeedReader.Feed, List<CFFeedItem>>, SourceFeedToCFFeedMapper>();

            collection.AddScoped<IRestClient, RestClient>();
            var provider = collection.BuildServiceProvider();
            IFeedManager integration = provider.GetService<IFeedManager>();
            integration.StartAsync();

        }
    }
}
