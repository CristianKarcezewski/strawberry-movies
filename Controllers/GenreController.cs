using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strawberry.Models.Domain;
using Strawberry.Repositories.Abstract;

namespace Strawberry.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GenreController : Controller
    {
        private readonly IGenreService genreService;
        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Genre genre)
        {
            if(!ModelState.IsValid)
            {
                return View(genre);
            }
            var result = genreService.Create(genre);
            if (result)
            {
                TempData["msg"] = "Genero adicionado com sucesso.";
                return RedirectToAction(nameof(Create));
            }
            else
            {
                TempData["msg"] = "Erro ao salvar genero.";
                return View(genre);
            }
        }

        public IActionResult Edit(int id)
        {
            var data = genreService.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return View(genre);
            }
            var result = genreService.Update(genre);
            if (result)
            {
                TempData["msg"] = "Genero atualizado com sucesso.";
                return RedirectToAction(nameof(List));
            }
            else
            {
                TempData["msg"] = "Erro ao atualizar genero.";
                return View(genre);
            }
        }

        public IActionResult List()
        {
            var data = genreService.Fetch().ToList() ;
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var result = genreService.Delete(id);
            return RedirectToAction(nameof(List));
        }
    }
}
