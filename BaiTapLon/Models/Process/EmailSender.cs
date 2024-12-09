using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;
namespace BaiTapLon.Models.Process
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Console.WriteLine($"Sending email to {email}: {subject}");
            return Task.CompletedTask;
        }
    }

}