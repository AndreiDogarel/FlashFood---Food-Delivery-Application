using User.Management.Service.Models;

namespace User.Management.Service.Services.EmailService
{
    public interface IEmailService
    {
        public void SendEmail(Message message);
    }
}
