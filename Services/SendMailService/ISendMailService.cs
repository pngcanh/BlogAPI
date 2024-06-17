using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Model;

namespace BlogAPI.Services.SendMailService
{
    public interface ISendMailService
    {
        Task SenMail(EmailDTO emailDTO);
    }
}