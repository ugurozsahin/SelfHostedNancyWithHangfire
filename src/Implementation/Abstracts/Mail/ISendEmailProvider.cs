using Implementation.Abstracts.Lifestyles;
using Implementation.Concretes.Models.Mail;

namespace Implementation.Abstracts.Mail
{
    public interface ISendEmailProvider : ISingletonService
    {
        void Send(Email email);
    }
}