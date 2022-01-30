using CommandF.Feed.Importer.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandF.Feed.Importer.Repositories.Feeds.Index
{
    public class IndexManager : IIndexManager
    {
        private readonly IMongoClient mongoClient;
        private bool ensuredIndexes;

        public IndexManager(IMongoClient mongoClient)
        {
            this.mongoClient = mongoClient ?? throw new ArgumentNullException(nameof(mongoClient));
            this.ensuredIndexes = false;
        }

        public void EnsureIndexesExistence()
        {
            if (ensuredIndexes)
                return;

            var db = mongoClient.GetDatabase("Feeds");

            var configsCollection = db.GetCollection<CFFeedItem>("FeedItems");

            configsCollection.Indexes.CreateOne(new CreateIndexModel<CFFeedItem>(
                Builders<CFFeedItem>.IndexKeys.Ascending(_ => _.Id)));

            configsCollection.Indexes.CreateOne(new CreateIndexModel<CFFeedItem>(
                Builders<CFFeedItem>.IndexKeys.Ascending(_ => _.Metadata.IsGlobal)));

            configsCollection.Indexes.CreateOne(new CreateIndexModel<CFFeedItem>(
                Builders<CFFeedItem>.IndexKeys.Ascending(_ => _.Metadata.Username)));

            configsCollection.Indexes.CreateOne(new CreateIndexModel<CFFeedItem>(
                Builders<CFFeedItem>.IndexKeys.Ascending(_ => _.Content.PubDate)));

            configsCollection.Indexes.CreateOne(new CreateIndexModel<CFFeedItem>(
                Builders<CFFeedItem>.IndexKeys.Ascending(_ => _.Content.Title)));

            configsCollection.Indexes.CreateOne(new CreateIndexModel<CFFeedItem>(
                Builders<CFFeedItem>.IndexKeys.Ascending(_ => _.Content.Tags)));

            configsCollection.Indexes.CreateOne(new CreateIndexModel<CFFeedItem>(
                Builders<CFFeedItem>.IndexKeys.Ascending(_ => _.Metadata.DestinationCategory)));

            ensuredIndexes = true;
        }
    }
}
