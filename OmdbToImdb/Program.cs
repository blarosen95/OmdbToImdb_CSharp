using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CsvHelper;
using Newtonsoft.Json.Linq;
using NLog;
using RestSharp;

namespace OmdbToImdb
{
    internal static class Program
    {
        private static readonly Logger SLogger = LogManager.GetCurrentClassLogger();

        private static void Main()
        {
            var config = new NLog.Config.LoggingConfiguration();
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "file.txt" };
            var logConsole = new NLog.Targets.ConsoleTarget("logConsole");
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logConsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);
            LogManager.Configuration = config;

            Config.FromEnv();
            Secrets.FromEnv();
            Context.FromEnv();

            Console.WriteLine($"This run is in mode: {Context.GetValue("run_mode")}");

            var omdbShow = new Omdb(Secrets.GetValue("omdb_api_key"),
                Secrets.GetValue("omdb_access_token"));

            using var csv = new CsvReader(new StreamReader($"hack{Path.DirectorySeparatorChar}exports.csv"),
                CultureInfo.InvariantCulture);
            csv.Read();
            csv.ReadHeader();
            while (csv.Read())
            {
                if (!bool.Parse(Config.GetValue("make_show"))) continue;

                var dataRecord = Enumerable.ToList(csv.GetRecord<dynamic>());
                SLogger.Info($"Creating Show: {string.Join(",", dataRecord)}");

                var resp = omdbShow.Create("show", csv.GetField<string>("favorite_show"));

                dynamic show = JObject.Parse(resp.Content);

                var imdbId = show.imdbID;

                IRestResponse imdbResp = Imdb.Create("search", imdbId.ToString());

                dynamic queryResult = JObject.Parse(Regex.Replace(
                    Regex.Replace(imdbResp.Content, @"^imdb\$tt\d{7,}\(", ""),
                    @"\)$", ""));

                var cast = queryResult.d[0].s;

                Console.WriteLine(
                    $"{csv.GetField<string>("first_name")} {csv.GetField<string>("last_name")}'s favorite show, {csv.GetField<string>("favorite_show")}, stars: {cast}.");
            }
        }
    }
}