using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reviews.Models
{
    public class Movie : BaseModel
    {
        [Required]
        [DisplayName("Movie")]
        public string Name { get; set; }

        [Required]
        [ForeignKey("Genere")]
        public int GenereID { get; set; }
        public virtual Genere Genere { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string TrailerUrl { get; set; }

        public virtual List<Review> Reviews { get; set; }
    }
}