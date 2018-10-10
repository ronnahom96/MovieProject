using Accord.MachineLearning.Bayes;
using Accord.Statistics.Filters;
using MoviesReview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesReview.Controllers
{
    public class HomeController : Controller
    {
        private MoviesContext db = new MoviesContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult RecommendMovie()
        {
            int userID = ((User)Session["User"]).Id;

            int[] selectedGeneres = db.Reviews.Where(review => review.UserID == userID).Select(review => review.Movie.GenereID).ToArray();
            int[][] dataset = new int[selectedGeneres.Length][];
                
            for (int genereIndex = 0; genereIndex < selectedGeneres.Length; genereIndex++)
            {
                dataset[genereIndex] = new int[1];
            }

            //Array.Clear(dataset, 0, dataset.Length);

            var learner = new NaiveBayesLearning();

            NaiveBayes nb = learner.Learn(dataset, selectedGeneres);

            int selectedGenere = nb.Decide(new int[default(int)]);


            // Select random movie from the user favorite genere
            Random rnd = new Random();

            List<Movie> allMoviesInGenere = db.Generes.First(x => x.Id == 1).Movies;

            Movie selectedMovie = allMoviesInGenere[rnd.Next(allMoviesInGenere.Count)];

            return null;
        }
    }
}