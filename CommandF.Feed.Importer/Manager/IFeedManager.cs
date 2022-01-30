using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CommandF.Feed.Importer.Manager
{
    public interface IFeedManager
    {
        Task StartAsync();
    }
}
