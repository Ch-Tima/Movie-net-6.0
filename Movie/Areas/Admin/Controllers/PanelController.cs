using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Movie.Areas.Admin.Filters;
using Movie.Context.Repository;
using Movie.Models;

namespace Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authentication]
    public class PanelController : Controller
    {
        private readonly FilmRepository _filmRepository;
        private readonly SerialRepository _serialRepository;

        public PanelController(Context.MovieContext movieContext)
        {
            _filmRepository = new FilmRepository(movieContext);
            _serialRepository = new SerialRepository(movieContext);
        }

        public IActionResult Index(string type = nameof(Models.Film))
        {
            if (type == nameof(Models.Serial))
                return View(new { type = type, obj = _serialRepository.GetAll() });

            return View(new { type = type, obj = _filmRepository.GetAll() });

        }
        [HttpPost]
        public IActionResult Delete(int id, string type)
        {
            bool res = false;
            switch (type)
            {
                case nameof(Film):
                    res = _filmRepository.Delete(id);
                    break;
                case nameof(Serial):
                    res = _serialRepository.Delete(id);
                    break;
                default:
                    return BadRequest($"Error: Failed remove item->'Id:{id}'");
            }
            return res ? Ok("Good!") : BadRequest($"Error: Failed remove item->'Id:{id}'");
        }
    }
}
