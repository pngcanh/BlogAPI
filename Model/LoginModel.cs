using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Model
{
    public class LoginModel
    {
        [Required]
        [StringLength(50)]
        public string Email { set; get; } = null!;
        [Required]
        [DataType(DataType.Password)]
        [StringLength(50)]
        public string Password { set; get; } = null!;
    }
}