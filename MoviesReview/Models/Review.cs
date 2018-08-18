using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reviews.Models
{
    public class Review : BaseModel
    {
        [MaxLength(20)]
        [Required]
        [DisplayName("Title")]
        public string Title { get; set; }

        [MaxLength(8000)]
        [Required]
        public string Content { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Created at")]
        public DateTime CreationDate { get; set; }
      
        [Required]
        [DisplayName("Posting User")]
        [ForeignKey("User")]
        public int UserID { get; set; }
        public virtual User User { get; set; }


        public virtual List<Comment> Comments { get; set; }
    }
}