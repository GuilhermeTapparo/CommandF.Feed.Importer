using CommandF.Feed.Importer.Models;
using CommandF.Feed.Importer.Repositories.Exceptions;
using CommandF.Feed.Importer.Repositories.Feeds.Index;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CommandF.Feed.Importer.Repositories.Feeds
{
    public class FeedRepository : IFeedRepository
    {
        private IMongoCollection<CFFeedItem> collection;
        private IIndexManager indexManager;

        public FeedRepository(IMongoClient mongoClient, IIndexManager indexManager)
        {
            var client = mongoClient ?? throw new ArgumentNullException(nameof(mongoClient));
            this.indexManager = indexManager ?? throw new ArgumentNullException(nameof(indexManager));

            this.indexManager.EnsureIndexesExistence();
            collection = client.GetDatabase("Feeds").GetCollection<CFFeedItem>("FeedItems");
        }

        public async Task Add(CFFeedItem item)
        {
            try
            {
                item.Id = Guid.NewGuid();
                collection.InsertOne(item);
            }
            catch (Exception e)
            {
                throw new RepositoryException($"{e.Message} - {e.StackTrace}", e);
            }
        }

        public async Task<bool> AlreadyExists(string sourceId)
        {
            try
            {
                var idFilter = Builders<CFFeedItem>.Filter.Eq(_ => _.Metadata.SourceId, sourceId);
                var docs = collection.Find(idFilter);

                return docs.Any();
            }
            catch (Exception e)
            {
                throw new RepositoryException($"{e.Message} - {e.StackTrace}", e);
            }
        }
    }
}
