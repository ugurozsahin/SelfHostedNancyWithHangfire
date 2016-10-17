using Implementation.Abstracts.Jobs.Mail;
using Implementation.Abstracts.Mail;
using Implementation.Concretes.Models.Mail;

namespace Implementation.Concretes.Jobs.Mail
{
    public class DummySendEmailJob : ISendEmailJob
    {
        private readonly ISendEmailProvider _sendEmailProvider;

        public DummySendEmailJob(ISendEmailProvider sendEmailProvider)
        {
            _sendEmailProvider = sendEmailProvider;
        }

        public string Execute(Email email)
        {
            _sendEmailProvider.Send(email);
            return string.Format("{0} email sending transaction succeded!!!", email.To);
        }
    }
}
