using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OmdbToImdb
{
    public static class Config
    {
        private static readonly Dictionary<string, string> SDic = new();

        public static void FromEnv()
        {
            foreach (var de in Environment.GetEnvironmentVariables().Cast<DictionaryEntry>()
                .Where(de => de.Key.ToString().StartsWith("PAN_CFG_")))
            {
                SDic.Add(de.Key.ToString().Replace("PAN_CFG_", "").ToLower(),
                    de.Value.ToString().Replace("\\n", "").Replace("\n", ""));
            }
        }

        public static string GetValue(string key)
        {
            return SDic.ContainsKey(key) ? SDic[key] : "";
        }
    }

    public static class Secrets
    {
        private static readonly Dictionary<string, string> SDic = new();

        public static void FromEnv()
        {
            foreach (var de in Environment.GetEnvironmentVariables().Cast<DictionaryEntry>()
                .Where(de => de.Key.ToString().StartsWith("PAN_SEC_")))
            {
                SDic.Add(de.Key.ToString().Replace("PAN_SEC_", "").ToLower(),
                    de.Value.ToString().Replace("\\n", "").Replace("\n", ""));
            }
        }

        public static string GetValue(string key)
        {
            return SDic.ContainsKey(key) ? SDic[key] : "";
        }
    }

    public static class Context
    {
        private static readonly Dictionary<string, string> SDic = new();

        public static void FromEnv()
        {
            foreach (var de in Environment.GetEnvironmentVariables().Cast<DictionaryEntry>()
                .Where(de => de.Key.ToString().StartsWith("PAN_CTX_")))
            {
                SDic.Add(de.Key.ToString().Replace("PAN_CTX_", "").ToLower(),
                    de.Value.ToString().Replace("\\n", "").Replace("\n", ""));
            }
        }

        public static string GetValue(string key)
        {
            return SDic.ContainsKey(key) ? SDic[key] : "";
        }
    }
}