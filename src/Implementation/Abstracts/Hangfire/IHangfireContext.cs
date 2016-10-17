using Implementation.Abstracts.Lifestyles;
using Owin;
using System;
using System.Linq.Expressions;

namespace Implementation.Abstracts.Hangfire
{
    public interface IHangfireContext : ISingletonService
    {
        void Initialize(IAppBuilder appBuilder);
        string EnqueueJob<T>(Expression<Action<T>> methodCall);
        string EnqueueJob(Expression<Action> methodCall);
    }
}