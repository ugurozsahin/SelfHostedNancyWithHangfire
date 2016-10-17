using Implementation.Abstracts.Lifestyles;
using StackExchange.Redis;

namespace Implementation.Abstracts.Common
{
    public interface IRedisClient : ISingletonService
    {
        ConnectionMultiplexer OpenRedisConnection();
    }
}
