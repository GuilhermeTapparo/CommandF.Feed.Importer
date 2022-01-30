using CommandF.Feed.Importer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CommandF.Feed.Importer.Repositories.Feeds
{
    public interface IFeedRepository
    {
        public Task<bool> AlreadyExists(string sourceId);
        public Task Add(CFFeedItem item);
    }
}
