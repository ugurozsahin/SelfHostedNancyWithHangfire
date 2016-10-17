using Castle.Windsor;
using Castle.Windsor.Installer;
using Implementation.Abstracts.Hangfire;
using System;

namespace Implementation.Concretes.Common
{
    public sealed class ServiceContext
    {
        private static readonly Lazy<ServiceContext> LazyInstance = new Lazy<ServiceContext>(() => new ServiceContext());
        public IWindsorContainer WindsorContainer { get; set; }
        public IHangfireContext HangfireContext { get; set; }

        public static ServiceContext Instance
        {
            get { return LazyInstance.Value; }
        }

        public ServiceContext()
        {
            WindsorContainer = new WindsorContainer();
            WindsorContainer.Install(FromAssembly.InThisApplication());
            HangfireContext = WindsorContainer.Resolve<IHangfireContext>();
        }

        public T Resolve<T>()
        {
            var obj = WindsorContainer.Resolve<T>();
            return obj;
        }

        public T[] ResolveAll<T>()
        {
            var objArr = WindsorContainer.ResolveAll<T>();
            return objArr;
        }
    }
}
