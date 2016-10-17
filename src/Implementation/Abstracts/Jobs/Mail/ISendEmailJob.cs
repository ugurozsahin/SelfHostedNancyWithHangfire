using Implementation.Abstracts.Lifestyles;
using Implementation.Concretes.Models.Mail;

namespace Implementation.Abstracts.Jobs.Mail
{
    public interface ISendEmailJob : IJobService
    {
        string Execute(Email email);
    }
}