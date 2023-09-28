using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Strawberry.Models.Domain;
using Strawberry.Repositories.Abstract;

namespace Strawberry.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MovieController : Controller
    {
        private readonly IMovieService movieService;
        private readonly IGenreService genreService;
        private readonly IStreamingService streamingService;
        public MovieController(
            IMovieService movieService,
            IGenreService genreService,
            IStreamingService streamingService
        )
        {
            this.movieService = movieService;
            this.genreService = genreService;   
            this.streamingService = streamingService;
        }

        public IActionResult Create()
        {
            Movie movie = new Movie();
            movie.Genres = genreService.Fetch().Select(g => new SelectListItem { Text = g.GenreName, Value = g.Id.ToString() });
            movie.Streamings = streamingService.Fetch().Select(s => new SelectListItem { Text = s.StreamingName, Value = s.Id.ToString() });
            return View(movie);
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            movie.Genres = genreService.Fetch().Select(g => new SelectListItem { Text = g.GenreName, Value = g.Id.ToString() });
            movie.Streamings = streamingService.Fetch().Select(s => new SelectListItem { Text = s.StreamingName, Value = s.Id.ToString() });

            if (!ModelState.IsValid)
            {
                return View(movie);
            }
            var result = movieService.Create(movie);
            if (result)
            {
                TempData["msg"] = "File adicionado com sucesso.";
                return RedirectToAction(nameof(Create));
            }
            else
            {
                TempData["msg"] = "Erro ao salvar o filme.";
                return View(movie);
            }
        }

        public IActionResult Update(int id)
        {
            Movie movie = movieService.GetById(id);
            movie.Genres = genreService.Fetch().Select(g => new SelectListItem { Text = g.GenreName, Value = g.Id.ToString() });
            movie.Streamings = streamingService.Fetch().Select(s => new SelectListItem { Text = s.StreamingName, Value = s.Id.ToString() });
            return View(movie);
        }

        [HttpPut]
        public IActionResult Update(Movie movie)
        {
            movie.Genres = genreService.Fetch().Select(g => new SelectListItem { Text = g.GenreName, Value = g.Id.ToString() });
            movie.Streamings = streamingService.Fetch().Select(s => new SelectListItem { Text = s.StreamingName, Value = s.Id.ToString() });

            if (ModelState.IsValid)
            {
                return View(movie);
            }
            var result = movieService.Update(movie);
            if (result)
            {
                TempData["msg"] = "Filme atualizado com sucesso.";
                return RedirectToAction(nameof(List));
            }
            else
            {
                TempData["msg"] = "Erro ao atualizar o filme.";
                return View(movie);
            }
        }

        public IActionResult List()
        {
            var data = movieService.Fetch().ToList() ;
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var result = movieService.Delete(id);
            return RedirectToAction(nameof(List));
        }
    }
}
