using Microsoft.AspNetCore.Mvc;
using Strawberry.Models.Domain;
using Strawberry.Repositories.Abstract;

namespace Strawberry.Controllers
{
    public class StreamingController : Controller
    {
        private readonly IStreamingService streamingService;
        public StreamingController(IStreamingService streamingService)
        {
            this.streamingService = streamingService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Streaming streaming)
        {
            if (!ModelState.IsValid)
            {
                return View(streaming);
            }

            var result = streamingService.Create(streaming);
            if (result)
            {
                TempData["msg"] = "Streaming adicionado com sucesso.";
                return RedirectToAction(nameof(Create));
            }
            else
            {
                TempData["msg"] = "Erro ao salvar streaming.";
                return View(streaming);
            }
        }

        public IActionResult Edit(int id)
        {
            var data = streamingService.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(Streaming streaming)
        {
            if (!ModelState.IsValid)
            {
                return View(streaming);
            }
            var result = streamingService.Update(streaming);
            if (result)
            {
                TempData["msg"] = "Streaming atualizado com sucesso.";
                return RedirectToAction(nameof(List));
            }
            else
            {
                TempData["msg"] = "Erro ao atualizar streaming.";
                return View(streaming);
            }
        }

        public IActionResult List()
        {
            var data = streamingService.Fetch().ToList();
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var result = streamingService.Delete(id);
            return RedirectToAction(nameof(List));
        }
    }
}
