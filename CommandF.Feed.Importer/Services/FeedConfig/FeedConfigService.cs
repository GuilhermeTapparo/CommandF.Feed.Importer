using CommandF.Feed.Importer.Services.Feed.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CommandF.Feed.Importer.Services.Feed
{
    public class FeedConfigService : IFeedConfigService
    {
        private readonly IRestClient client;

        public FeedConfigService(IRestClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.client.BaseUrl = new Uri(EnvironmentConfig.ConfigApi.Host);
        }

        public async Task<List<FeedConfig>> FetchConfigs()
        {
            try
            {
                var request = new RestRequest();
                request.Method = Method.GET;
                request.Resource = "Config/Fetch";

                var response = client.Execute(request);
                var responseObj = JsonConvert.DeserializeObject<List<FeedConfig>>(response.Content);

                return responseObj;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception();
            }
        }
    }
}
