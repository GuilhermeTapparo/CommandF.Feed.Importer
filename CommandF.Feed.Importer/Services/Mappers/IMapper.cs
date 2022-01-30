using CommandF.Feed.Importer.Services.Feed.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandF.Feed.Importer.Services.Mappers
{
    public interface IMapper<TSource, TDestination>
    {
        TDestination Map(TSource source, FeedConfig config, Guid operationId);
    }
}
