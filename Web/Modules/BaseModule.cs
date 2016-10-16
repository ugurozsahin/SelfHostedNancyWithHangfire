using Nancy;
using System.Dynamic;
using Web.Models;

namespace Web.Modules
{
    public abstract class BaseModule : NancyModule
    {
        public dynamic Model = new ExpandoObject();

        protected BaseModule()
        {
            SetupModelDefaults();
        }

        protected BaseModule(string modulepath)
            : base(modulepath)
        {
            SetupModelDefaults();
        }

        private void SetupModelDefaults()
        {
            Before.AddItemToEndOfPipeline(ctx =>
            {
                Model.MasterPage = new MasterPageViewModel();
                Model.MasterPage.Title = "Nancy & Hangfire - Hello World!";
                Model.MasterPage.IsAuthenticated = (ctx.CurrentUser == null);
                return null;
            });
            After.AddItemToEndOfPipeline(ctx =>
            {
            });
        }
    }
}