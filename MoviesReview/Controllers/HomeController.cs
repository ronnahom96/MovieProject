﻿using Accord.MachineLearning.Bayes;
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

            var jamal = db.Reviews.Where(review => review.UserID == userID).ToList();

            int[] selectedGeneres = db.Reviews.Where(review => review.UserID == userID).Select(review => review.Movie.GenereID).ToArray();
            int[][] dataset = new int[selectedGeneres.Length][];

            Dictionary<int, int> mapper = new Dictionary<int, int>();
            Dictionary<int, int> mapperOpsite = new Dictionary<int, int>();


            int counter = 0;

            for (int genereIndex = 0; genereIndex < selectedGeneres.Length; genereIndex++)
            {
                dataset[genereIndex] = new int[1];

                if (!mapper.ContainsKey(selectedGeneres[genereIndex]))
                {
                    mapper[selectedGeneres[genereIndex]] = counter;
                    mapperOpsite[counter] = selectedGeneres[genereIndex];

                    counter++;
                }

                
            }

            int[] mappedLabels = new int[selectedGeneres.Length];

            for (int i = 0; i < selectedGeneres.Length; i++)
            {
                mappedLabels[i] = mapper[selectedGeneres[i]];
            }

            //Array.Clear(dataset, 0, dataset.Length);


            var learner = new NaiveBayesLearning();

            NaiveBayes nb = learner.Learn(dataset, mappedLabels);

            int[] prediction = new int[] { default(int)};

            int selectedGenereMapped = nb.Decide(prediction);

            int selectedIndex = mapperOpsite[selectedGenereMapped];

            // Select random movie from the user favorite genere
            Random rnd = new Random();

            List<Movie> allMoviesInGenere = db.Generes.First(x => x.Id == selectedIndex).Movies;

            Movie selectedMovie = allMoviesInGenere[rnd.Next(allMoviesInGenere.Count)];

            return null;
        }
    }
}