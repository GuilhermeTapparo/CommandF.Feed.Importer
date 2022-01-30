using CommandF.Feed.Importer.Models;
using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace CommandF.Feed.Importer.Services.RSS
{
    public interface IRssService
    {
        public List<SyndicationItem> GetFeedItems(string feedUrl, Guid operationId);
        public Task<CodeHollow.FeedReader.Feed> GetFeedAsync(string url, Guid operationId);
    }
}
