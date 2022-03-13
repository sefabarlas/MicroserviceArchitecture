using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Web.Models.Catalog
{
    public class CourseCreateInput
    {
        [Display(Name = "Adı")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Fiyat")]
        [Required]
        public decimal Price { get; set; }

        [Display(Name = "Kategori")]
        [Required]
        public string CategoryId { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Açıklama")]
        [Required]
        public string Description { get; set; }

        public string Picture { get; set; }

        public FeatureViewModel Feature { get; set; }
    }
}
