using System.Threading.Tasks;

namespace TCOMSapps.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
