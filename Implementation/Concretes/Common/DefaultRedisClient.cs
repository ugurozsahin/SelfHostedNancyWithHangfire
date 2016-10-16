using Implementation.Abstracts.Common;
using Implementation.Abstracts.Configuration;
using StackExchange.Redis;
using System;
using System.Threading;

namespace Implementation.Concretes.Common
{
    public class DefaultRedisClient : IRedisClient
    {
        private readonly IConfigWrapper _configWrapper;

        public DefaultRedisClient(IConfigWrapper configWrapper)
        {
            _configWrapper = configWrapper;
        }

        public ConnectionMultiplexer OpenRedisConnection()
        {
            var retryCount = 0;
            while (retryCount++ < _configWrapper.RedisConnectionMaxRetryCount)
            {
                try
                {
                    return ConnectionMultiplexer.Connect(_configWrapper.RedisConnectionString);
                }
                catch (RedisConnectionException)
                {
                    Console.Error.WriteLine("Waiting for redis");
                    Thread.Sleep(1000);
                }
            }
            throw new ApplicationException("Redis Connection Fail");
        }
    }
}