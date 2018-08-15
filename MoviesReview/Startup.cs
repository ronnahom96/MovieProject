using Microsoft.Owin;
using Owin;
using Reviews.Models;
using Reviews.Models.Db;

[assembly: OwinStartupAttribute(typeof(MoviesReview.Startup))]
namespace MoviesReview
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            using (var db = new MoviesContext())
            {
                // Create and save a new Blog 
                var name = "Rotem's Movie";

                var genere = new Genere { Name = name };
                db.Generes.Add(genere);
                db.SaveChanges();

            }
        }
    }
}
