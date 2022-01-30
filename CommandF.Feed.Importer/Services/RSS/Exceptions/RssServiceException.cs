using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CommandF.Feed.Importer.Services.RSS.Exceptions
{
    public class RssServiceException : Exception
    {
        public RssServiceException()
        {
        }

        public RssServiceException(string message) : base(message)
        {
        }

        public RssServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RssServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
