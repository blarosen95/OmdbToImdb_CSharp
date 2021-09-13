using RestSharp;

namespace OmdbToImdb
{
    public static class Imdb
    {
        private static readonly NLog.Logger SLogger = NLog.LogManager.GetCurrentClassLogger();

        private static string UrlBuilder(string imdbId)
        {
            return $"https://v2.sg.media-imdb.com/suggests/t/{imdbId}.json";
        }

        private static IRestResponse Get(string url)
        {
            var client = new RestClient();
            var request = new RestRequest(url, Method.GET);

            return client.Execute(request);
        }

        public static IRestResponse Create(string entity, string data)
        {
            SLogger.Info($"Attempting to create a {entity} record.");
            return Get(UrlBuilder(data));
        }
    }
}