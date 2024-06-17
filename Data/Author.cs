using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace BlogAPI.Data
{
    public class Author
    {
        [Key]
        public int ID { set; get; }

        [Display(Name = "Tên tác giả")]
        [Column(TypeName = "nvarchar")]
        [StringLength(maximumLength: 35)]
        [Required(ErrorMessage = "Vui lòng nhập tên tác giả!")]
        public string AuthorName { set; get; } = null!;

        [Display(Name = "Email")]
        [EmailAddress]
        [StringLength(maximumLength: 300)]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ Email")]
        public string Email { set; get; } = null!;

        [Display(Name = "Giới tính")]
        [Column(TypeName = "bit")]
        public bool Gender { set; get; }

    }
}