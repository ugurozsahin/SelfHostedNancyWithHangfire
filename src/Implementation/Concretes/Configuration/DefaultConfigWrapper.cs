using Implementation.Abstracts.Configuration;
using System;
using System.Configuration;
using System.Linq;

namespace Implementation.Concretes.Configuration
{
    public class DefaultConfigWrapper : IConfigWrapper
    {
        public string RedisConnectionString
        {
            get { return GetAppSettingValue("RedisConnectionString"); }
        }

        public int HangfireRedisDb
        {
            get { return int.Parse(GetAppSettingValue("HangfireRedisDb")); }
        }

        public string HangfireRedisPrefix
        {
            get { return GetAppSettingValue("HangfireRedisPrefix"); }
        }

        public string HangfireDashboardUrl
        {
            get { return GetAppSettingValue("HangfireDashboardUrl"); }
        }

        public int HangfireWorkerCount
        {
            get { return int.Parse(GetAppSettingValue("HangfireWorkerCount")); }
        }

        public string[] HangfireQueues
        {
            get { return GetAppSettingValue("HangfireQueues").Split(';', ',').ToArray(); }
        }

        public string RedisKeyUserList
        {
            get { return GetAppSettingValue("RedisKeyUserList", "UserList"); }
        }
        public int AuthRedisDb
        {
            get { return int.Parse(GetAppSettingValue("AuthRedisDb")); }
        }

        public int RedisConnectionMaxRetryCount
        {
            get { return int.Parse(GetAppSettingValue("RedisConnectionMaxRetryCount", "1")); }
        }

        private static string GetAppSettingValue(string key, string defaultValue = null)
        {
            return Environment.GetEnvironmentVariable(key) ?? ConfigurationManager.AppSettings[key] ?? defaultValue;
        }
    }
}
