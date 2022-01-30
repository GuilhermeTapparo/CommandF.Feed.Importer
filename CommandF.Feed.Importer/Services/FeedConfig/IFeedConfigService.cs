using CommandF.Feed.Importer.Services.Feed.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CommandF.Feed.Importer.Services.Feed
{
    public interface IFeedConfigService
    {
        Task<List<FeedConfig>> FetchConfigs();
    }
}
