using MailSend.Models;
using MailSend.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_management_system.Utilities.Mail
{
    [Route("api/[controller]")]
    [ApiController]

    public class SendMailController(sendmailService _sendmailService) : ControllerBase
    {
        [HttpPost("Send-Mail")]
        public async Task<IActionResult> Sendmail([FromQuery] SendMailRequest sendMailRequest)
        {
            var res = await _sendmailService.Sendmail(sendMailRequest).ConfigureAwait(false);
            return Ok(res);
        }


    }

}
