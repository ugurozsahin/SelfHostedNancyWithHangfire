using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Implementation.Abstracts.Auth;
using Implementation.Abstracts.Auth.Data;
using Implementation.Abstracts.Common;
using Implementation.Abstracts.Configuration;
using Implementation.Abstracts.Hangfire;
using Implementation.Abstracts.Mail;
using Implementation.Concretes.Auth;
using Implementation.Concretes.Auth.Data;
using Implementation.Concretes.Configuration;
using Implementation.Concretes.Hangfire;
using Implementation.Concretes.Jobs.Mail;
using Implementation.Concretes.Mail;
using Nancy.Authentication.Forms;

namespace Implementation.Concretes.Common
{
    public class CustomWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //container.Register(Classes.FromThisAssembly()
            //    .BasedOn<ISingletonService>()
            //    .WithServiceFirstInterface()
            //    .LifestyleSingleton());

            //container.Register(Classes.FromThisAssembly()
            //    .BasedOn<IJobService>()
            //    .WithServiceSelf()
            //    .LifestylePerThread());

            //container.Register(Classes.FromThisAssembly()
            //    .BasedOn<IPerThreadService>()
            //    .WithServiceFirstInterface()
            //    .LifestylePerThread());

            container.Register(Component.For<IRedisClient, DefaultRedisClient>());

            container.Register(Component.For<ISendEmailProvider, DefaultSendEmailProvider>());

            container.Register(Component.For<ISerializer, DefaultJsonSerializer>());

            container.Register(Component.For<IHangfireContext, DefaultHangfireContext>());

            container.Register(Component.For<IConfigWrapper, DefaultConfigWrapper>());

            container.Register(Component.For<IAuthDataService, AuthDataService>());

            container.Register(Component.For<DummySendEmailJob>().LifestylePerThread());

            container.Register(Component.For<IUserMapper, IAuthService, AuthService>());
        }
    }
}
