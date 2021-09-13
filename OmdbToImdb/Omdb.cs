using RestSharp;

namespace OmdbToImdb
{
    public class Omdb
    {
        private static readonly NLog.Logger SLogger = NLog.LogManager.GetCurrentClassLogger();
        private readonly string _queryString = "";

        public Omdb(string omdbApiKey, string omdbAccessToken)
        {
            if (omdbApiKey != "")
            {
                _queryString = omdbApiKey;
            }
            else if (omdbAccessToken != "")
            {
            }
        }

        private string UrlBuilder(string showName)
        {
            return $"http://www.omdbapi.com/?apikey={_queryString}&t={showName}";
        }

        private static IRestResponse Get(string url)
        {
            var client = new RestClient();
            var request = new RestRequest(url, Method.GET);

            return client.Execute(request);
        }

        public IRestResponse Create(string entity, string data)
        {
            SLogger.Info($"Attempting to create a {entity} record.");
            return Get(UrlBuilder(data));
        }
    }
}