using MailSend.Models;
using MailSend.Repo;
using MimeKit;
using System.Net.Mail;

namespace MailSend.Service
{
    public class sendmailService(SendMailRepository _sendMailRepository, EmailServiceProvider _emailServiceProvider)
    {
        public async Task<string> Sendmail(SendMailRequest sendMailRequest)
        {
            if (sendMailRequest == null) throw new ArgumentNullException(nameof(sendMailRequest));

            var template = await _sendMailRepository.GetTemplate(sendMailRequest.EmailType).ConfigureAwait(false);
            if (template == null) throw new Exception("Template not found");

            var bodyGenerated = await EmailBodyGenerate(template.Body, sendMailRequest.Name, sendMailRequest.Otp , sendMailRequest.amountpayed);

            MailModel mailModel = new MailModel
            {
                Subject = template.Title ?? string.Empty,
                Body = bodyGenerated ?? string.Empty,
                SenderName = "Sample System",
                To = sendMailRequest.Email ?? throw new Exception("Recipient email address is required")
            };

             await _emailServiceProvider.SendMail(mailModel).ConfigureAwait(false);

            return "email was sent successfully";
        }

        public async Task<string> EmailBodyGenerate(string emailbody, string? name = null, string? otp = null, decimal? amountpayed = null)
        {
            var replacements = new Dictionary<string, string?>
    {
        { "{Name}", name },
        { "{Otp}", otp },
        { "{amountpayed}", amountpayed.HasValue ? amountpayed.Value.ToString("C2") : null }  // Format amountpayed as currency if it's not null
    };

            foreach (var replacement in replacements)
            {
                // Only replace if the replacement value is not null or empty
                if (!string.IsNullOrEmpty(replacement.Value))
                {
                    emailbody = emailbody.Replace(replacement.Key, replacement.Value, StringComparison.OrdinalIgnoreCase);
                }
            }

            return emailbody;
        }


    }
}
