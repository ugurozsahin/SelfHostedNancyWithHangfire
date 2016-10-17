using Hangfire;
using Hangfire.Redis;
using Hangfire.Windsor;
using Implementation.Abstracts.Configuration;
using Implementation.Abstracts.Hangfire;
using Implementation.Concretes.Common;
using Owin;
using System;
using System.Linq.Expressions;

namespace Implementation.Concretes.Hangfire
{
    public class DefaultHangfireContext : IHangfireContext
    {
        private readonly IConfigWrapper _configWrapper;
        private JobStorage _jobStorage;

        public DefaultHangfireContext(
            IConfigWrapper configWrapper)
        {
            _configWrapper = configWrapper;
        }

        public void Initialize(IAppBuilder appBuilder)
        {
            var redisStorageOptions = new RedisStorageOptions()
            {
                Db = _configWrapper.HangfireRedisDb,
                Prefix = _configWrapper.HangfireRedisPrefix
            };

            Console.WriteLine(_configWrapper.RedisConnectionString);

            _jobStorage = new RedisStorage(_configWrapper.RedisConnectionString, redisStorageOptions);

            GlobalConfiguration.Configuration.UseStorage(_jobStorage);

            JobActivator.Current = new WindsorJobActivator(ServiceContext.Instance.WindsorContainer.Kernel);

            appBuilder.UseHangfireDashboard(_configWrapper.HangfireDashboardUrl, new DashboardOptions()
            {
                Authorization = new[] { new CustomDashboardAuthorizationFilter() },
            });

            appBuilder.UseHangfireServer(new BackgroundJobServerOptions()
            {
                WorkerCount = _configWrapper.HangfireWorkerCount,
                Queues = _configWrapper.HangfireQueues,
                Activator = JobActivator.Current
            });

        }


        public string EnqueueJob<T>(Expression<Action<T>> methodCall)
        {
            JobStorage.Current = _jobStorage;
            return BackgroundJob.Enqueue(methodCall);
        }

        public string EnqueueJob(Expression<Action> methodCall)
        {
            JobStorage.Current = _jobStorage;
            return BackgroundJob.Enqueue(methodCall);
        }
    }
}
