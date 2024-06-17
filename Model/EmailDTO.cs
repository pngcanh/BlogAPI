using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Model
{
    public class EmailDTO
    {
        public string To { set; get; } = null!;
        public string Subject { set; get; } = null!;
        public string Body { set; get; } = null!;
    }
}