using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Reviews.Models
{
    public class Genere : BaseModel
    {
        [Required]
        [DisplayName("Genere")]
        public string Name { get; set; }

        public virtual List<Movie> Movies { get; set; }
        
    }
}