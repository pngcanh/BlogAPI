using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace BlogAPI.Model
{
    public class AuthorModel
    {
        public int ID { set; get; }

        [Display(Name = "Tên tác giả")]
        [Column(TypeName = "nvarchar")]
        [StringLength(maximumLength: 35)]
        [Required(ErrorMessage = "Vui lòng nhập tên tác giả!")]
        public string AuthorName { set; get; }

        [Display(Name = "Email")]
        [EmailAddress]
        [StringLength(maximumLength: 300)]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ Email")]
        public string Email { set; get; }

        [Display(Name = "Giới tính")]
        [Column(TypeName = "bit")]
        public bool Gender { set; get; }

    }
}