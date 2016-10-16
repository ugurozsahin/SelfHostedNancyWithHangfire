using Implementation.Abstracts.Lifestyles;

namespace Implementation.Abstracts.Configuration
{
    public interface IConfigWrapper : ISingletonService
    {
        string RedisConnectionString { get; }
        int HangfireRedisDb { get; }
        string HangfireRedisPrefix { get; }
        string HangfireDashboardUrl { get; }
        int HangfireWorkerCount { get; }
        string[] HangfireQueues { get; }
        int AuthRedisDb { get; }
        string RedisKeyUserList { get; }
        int RedisConnectionMaxRetryCount { get; }
    }
}