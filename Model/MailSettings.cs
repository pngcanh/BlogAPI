using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Model
{
    public class MailSettings
    {
        public string SenderName { set; get; } = null!;
        public string SenderEmail { set; get; } = null!;
        public string Server { set; get; } = null!;
        public int Port { set; get; }
        public string Password { set; get; } = null!;


    }
}