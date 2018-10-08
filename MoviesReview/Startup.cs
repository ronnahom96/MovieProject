using Microsoft.Owin;
using Owin;
using MoviesReview.Models;
using MoviesReview.Models;

[assembly: OwinStartupAttribute(typeof(MoviesReview.Startup))]
namespace MoviesReview
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
