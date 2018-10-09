using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MoviesReview.Models;
using MoviesReview.Models;

namespace MoviesReview.Controllers
{
    public class ReviewsController : Controller
    {
        private MoviesContext db = new MoviesContext();

        // GET: Reviews
        public ActionResult Index()
        {
            var reviews = db.Reviews.Include(r => r.Movie).Include(r => r.User);
            return View(reviews.ToList());
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            ViewBag.MovieID = new SelectList(db.Movies, "Id", "Name");
            ViewBag.UserID = new SelectList(db.Users, "Id", "Username");
            return View();
        }

        public ActionResult PostComment(int userId, int reviewId, string content)
        {
            db.Comments.Add(new Comment
            {
                Content = content,
                UserID = userId,
                ReviewID = reviewId,
                CreationDate = DateTime.Now
            });

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content,UserID,MovieID")] Review review)
        {
            review.CreationDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MovieID = new SelectList(db.Movies, "Id", "Name", review.MovieID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "Username", review.UserID);
            
            return View(review);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.MovieID = new SelectList(db.Movies, "Id", "Name", review.MovieID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "Username", review.UserID);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,CreationDate,UserID,MovieID")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MovieID = new SelectList(db.Movies, "Id", "Name", review.MovieID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "Username", review.UserID);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Search(string content, string title, DateTime? date, string movieName)
        {
            var dayAfterDate = date?.AddDays(1);

            return View(db.Reviews
                .Where(review =>
                    (!string.IsNullOrEmpty(content) && review.Content.ToLower().Contains(content.ToLower())) ||
                    !string.IsNullOrEmpty(movieName) && review.Movie.Name.ToLower().Contains(movieName.ToLower()) ||
                    (!string.IsNullOrEmpty(title) && review.Title.ToLower().Contains(title.ToLower())) ||
                    (date.HasValue && dayAfterDate.HasValue && date < review.CreationDate && review.CreationDate < dayAfterDate))
                .OrderByDescending(x => x.CreationDate)
                .ToList());
        }
    }
}
