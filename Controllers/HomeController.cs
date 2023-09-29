using Microsoft.AspNetCore.Mvc;
using Strawberry.Repositories.Abstract;

namespace Strawberry.Controllers
{
    public class HomeController : Controller
    {

        private readonly IMovieService movieService;

        public HomeController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        public IActionResult Index(string search="")
        {
            var movies = movieService.Fetch();
            return View(movies);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult MovieDetail(int movieId)
        {
            var movie = movieService.GetById(movieId);
            return View(movie);
        }
    }
}
