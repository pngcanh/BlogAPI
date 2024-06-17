using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Data;

namespace BlogAPI.Model
{
    public class AddRoleForUser
    {
        public BlogUser blogUser { get; set; } = null!;
        public string[] RoleName { set; get; } = null!;
    }
}