using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Model
{
    public class CategoryModel
    {
        public int ID { set; get; }

        [Display(Name = "Thể loại")]
        [Column(TypeName = "nvarchar")]
        [StringLength(maximumLength: 35)]
        public string CategoryName { set; get; }

        [Display(Name = "Mô tả")]
        [Column(TypeName = "nvarchar")]
        [StringLength(maximumLength: 300)]
        public string Description { set; get; }
    }
}