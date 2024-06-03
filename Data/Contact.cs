using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Data
{
    public class Contact
    {
        [Key]
        public int ID { set; get; }

        [Display(Name = "Họ và tên")]
        [StringLength(30)]
        [Required(ErrorMessage = "Vui lòng nhập họ tên!")]
        public int Name { set; get; }

        [Display(Name = "Địa chỉ Email")]
        [EmailAddress]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ Email!")]
        public string Email { set; get; }

        [Display(Name = "Địa chỉ Email")]
        [StringLength(maximumLength: 500, ErrorMessage = "Nội dung không vượt quá 500 ký tự!")]
        [Required(ErrorMessage = "Vui lòng nhập nội dung")]
        public string Content { set; get; }
    }
}