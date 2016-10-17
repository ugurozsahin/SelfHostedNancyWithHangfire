using Implementation.Abstracts.Auth;
using Implementation.Concretes.Common;
using Implementation.Concretes.Jobs.Mail;
using Implementation.Concretes.Models.Mail;
using Nancy.Authentication.Forms;
using Nancy.Extensions;
using System;
using System.Collections;
using System.Text;
using Web.Models;

namespace Web.Modules
{
    public class MainModule : BaseModule
    {
        private readonly IAuthService _authService;
        private readonly DummySendEmailJob _sendEmailJob;
        public MainModule(
            IAuthService authService,
            DummySendEmailJob sendEmailJob)
        {
            _authService = authService;
            _sendEmailJob = sendEmailJob;

            Get["/"] = x =>
            {
                Model.index = new IndexViewModel();
                Model.index.HelloWorld = "Self Hosted Nancy & Hangfire (Mono, Docker, Redis)";
                return View["index", Model];
            };

            Get["/info"] = x =>
            {
                var infoBuilder = new StringBuilder();
                foreach (DictionaryEntry environmentVariable in Environment.GetEnvironmentVariables())
                {

                    infoBuilder.AppendFormat("{0} : {1} <br />", environmentVariable.Key.ToString(),
                        environmentVariable.Value.ToString());

                }
                return infoBuilder.ToString();
            };

            Get["/enqueue"] = x =>
            {
                var count = int.Parse(Request.Query.QueueCount.ToString());
                for (int i = 0; i < count; i++)
                {
                    ServiceContext.Instance.HangfireContext.EnqueueJob(() => _sendEmailJob.Execute(new Email()
                    {
                        To = string.Format("{0}@mail.com", i),
                        Body = "test",
                        From = "sender@mail.com",
                        Subjects = "test"
                    }));
                }
                return "Process Succeded!!!";
            };

            Get["/login"] = x =>
            {
                Model.login = new LoginViewModel() { Error = this.Request.Query.error.HasValue, ReturnUrl = this.Request.Url };
                return View["login", Model];
            };

            Post["/login"] = x =>
            {
                var userId = _authService.ValidateUser((string)this.Request.Form.Username, (string)this.Request.Form.Password);

                if (userId == null)
                {
                    return Context.GetRedirect("~/login?error=true&username=" + (string)this.Request.Form.Username);
                }

                DateTime? expiry = null;
                if (this.Request.Form.RememberMe.HasValue)
                {
                    expiry = DateTime.Now.AddDays(7);
                }

                return this.LoginAndRedirect(userId.Value, expiry);
            };
            Post["/logout"] = x => this.LogoutAndRedirect("/");

        }
    }
}