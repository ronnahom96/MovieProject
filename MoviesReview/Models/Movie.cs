using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesReview.Models
{
    public class Movie : BaseModel
    {
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Genere")]
        [ForeignKey("Genere")]
        public int GenereID { get; set; }
        public virtual Genere Genere { get; set; }

        [Required]
        [DisplayName("Image Url")]
        public string ImageUrl { get; set; }

        [Required]
        [DisplayName("Trailer Url")]
        public string TrailerUrl { get; set; }

        public virtual List<Review> Reviews { get; set; }
    }
}