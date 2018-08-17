using System.Collections.Generic;
using System.ComponentModel;
using Reviews.Models;

namespace MoviesReview.Models.ViewModels
{
    public class UserReviewsViewModel : BaseViewModel
    {
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Reviews")]
        public IEnumerable<Review> Reviews { get; set; }
    }
}