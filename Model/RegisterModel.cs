using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Model
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { set; get; } = null!;

        [Required]
        public string UserName { set; get; } = null!;

        [Required]
        public string Password { set; get; } = null!;

        [Required]
        [Compare("Password", ErrorMessage = "Nhạp lại mật  khẩu chưa chính xác!")]
        public string ConfirmPassword { set; get; } = null!;
    }
}