using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MoviesReview.Models; // TODO: DELETE THIS SHIT

namespace MoviesReview.Models
{
    public class MoviesContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Genere> Generes { get; set; }
        public DbSet<Movie> Movies{ get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}