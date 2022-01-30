using System;
using System.Collections.Generic;
using System.Text;

namespace CommandF.Feed.Importer
{
    public static class EnvironmentConfig
    {
        public static class MongoDb
        {
            public static string ConnectionString { get { return GetVariableOrDefault("MongoDb.ConnectionString", "mongodb://root:D1k5sbdItg@127.0.0.1:27017/?authSource=admin&readPreference=primary&appname=ConfigApi&ssl=false"); } }
        }

        public static class ConfigApi
        {
            public static string Host { get { return GetVariableOrDefault("ConfigAPI.Host", "http://35.237.68.73:5000"); } }
        }
        private static string GetVariableOrDefault(string variable, string @default = "")
        {
            var value = Environment.GetEnvironmentVariable(variable);

            if (String.IsNullOrEmpty(value))
                return @default;

            return value;
        }

    }
}