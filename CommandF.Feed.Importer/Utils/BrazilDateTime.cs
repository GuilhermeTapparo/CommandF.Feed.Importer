using System;
using System.Collections.Generic;
using System.Text;

namespace CommandF.Feed.Importer.Utils
{
    public static class BrazilDateTime
    {
        public static DateTime GetCurrentDate()
        {
            return DateTime.UtcNow.AddHours(-3);
        }
    }
}
