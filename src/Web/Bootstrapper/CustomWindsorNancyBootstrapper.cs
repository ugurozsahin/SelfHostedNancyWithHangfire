using Castle.Windsor;
using Implementation.Concretes.Common;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Windsor;
using Nancy.Cryptography;

namespace Web.Bootstrapper
{
    public class CustomWindsorNancyBootstrapper : WindsorNancyBootstrapper
    {
        protected override IWindsorContainer GetApplicationContainer()
        {
            return ServiceContext.Instance.WindsorContainer;
        }

        protected override void ApplicationStartup(IWindsorContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            var cryptographyConfiguration = new CryptographyConfiguration(
                new RijndaelEncryptionProvider(new PassphraseKeyGenerator("YourSuperSecretPass", new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 })),
                new DefaultHmacProvider(new PassphraseKeyGenerator("YourUberSuperSecure", new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 })));

            var formsAuthConfiguration =
                new FormsAuthenticationConfiguration()
                {
                    CryptographyConfiguration = cryptographyConfiguration,
                    RedirectUrl = "~/login",
                    UserMapper = container.Resolve<IUserMapper>()
                };
            FormsAuthentication.Enable(pipelines, formsAuthConfiguration);
        }

        protected override void ConfigureApplicationContainer(IWindsorContainer existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);
        }

        protected override void RequestStartup(IWindsorContainer container, IPipelines pipelines, NancyContext context)
        {
            // No registrations should be performed in here, however you may
            // resolve things that are needed during request startup.
            base.RequestStartup(container, pipelines, context);
        }
    }
}