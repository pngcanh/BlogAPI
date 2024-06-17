using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Model;
using BlogAPI.Services.SendMailService;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controller.SendMail
{
    [ApiController]
    [Route("api/[controller]")]
    public class SendMail : ControllerBase
    {
        private readonly ISendMailService sendMail;

        public SendMail(ISendMailService sendMail)
        {
            this.sendMail = sendMail;
        }
        [HttpPost("SendMail")]
        public async Task<IActionResult> Email(EmailDTO emailDTO)
        {
            await sendMail.SenMail(emailDTO);
            return Ok("Done");
        }
    }
}