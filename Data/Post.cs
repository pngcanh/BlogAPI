using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Data
{
    public class Post
    {
        public Post()
        {
            Created = DateTime.Now;
        }
        [Key]
        public int ID { set; get; }

        [Display(Name = "Tiêu đề")]
        [Column(TypeName = "nvarchar")]
        [StringLength(maximumLength: 200)]
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề!")]
        public string Title { set; get; }

        [Display(Name = "Ảnh")]
        [Column(TypeName = "varchar")]
        [StringLength(maximumLength: 200)]
        [Required(ErrorMessage = "Vui lòng chọn ảnh cho bài viết")]
        public string Photo { set; get; }

        [Display(Name = "Nội dung")]
        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "Vui lòng nhập nội dung bài viết!")]
        public string Content { set; get; }

        [Display(Name = "Tiêu đề")]
        [Column(TypeName = "Datetime")]
        [StringLength(maximumLength: 200)]
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề!")]
        public DateTime Created { set; get; }

        [Column(TypeName = "int")]
        public int View { set; get; }

        [Display(Name = "URL")]
        [StringLength(maximumLength: 200)]
        public string Slug { set; get; }

        [Display(Name = "Thể loại")]
        public int CateID { set; get; }
        [ForeignKey("CateID")]
        public Category Category { set; get; }

        [Display(Name = "Tác giả")]
        public int AuthorID { set; get; }
        [ForeignKey("AuthorID")]
        public Author Author { set; get; }
    }
}