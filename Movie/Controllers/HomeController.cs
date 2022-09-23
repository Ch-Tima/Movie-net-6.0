using Microsoft.AspNetCore.Mvc;
using Movie.Context;
using Movie.Context.Repository;
using Movie.Models;
using System.Diagnostics;

namespace Movie.Controllers
{
    public class HomeController : Controller
    {
        private readonly FilmRepository _film;
        private readonly SerialRepository _serial;

        public HomeController(MovieContext movieContext)
        {
            _film = new FilmRepository(movieContext);
            _serial = new SerialRepository(movieContext);
        }

        /// <summary>
        /// Loading the start page.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index(int pageNumber = 0, string type = nameof(Film))
        {
            if (pageNumber < 0)
                pageNumber = 0;

            int countElemen = 4;
            switch (type)
            {
                case "Serial":
                    ViewBag.PageCount = _serial.GetCount() / countElemen;
                    return View(_serial.GetPart(pageNumber * countElemen, countElemen));
                default:
                    ViewBag.PageCount = _film.GetCount() / countElemen;
                    return View(_film.GetPart(pageNumber * countElemen, countElemen));
            }
        }

        /// <summary>
        /// Open the "PageMovie" page with detalis.
        /// </summary>
        /// <param name="Id">Model Id</param>
        /// <param name="Type">Model Type</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult PageMovie(int Id, string Type)
        {
            try
            {
                object model;
                switch (Type)
                {
                    case "Film":
                        model = new { type = Type, obj = _film.GetId(Id) };
                        break;
                    case "Serial":
                        model = new { type = Type, obj = _serial.GetId(Id) };
                        break;
                    default:
                        throw new Exception("Error: Request \"PageMovie\"");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return Redirect("~/Home");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}