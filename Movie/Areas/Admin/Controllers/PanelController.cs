
using Microsoft.AspNetCore.Mvc;
using Movie.Areas.Admin.Filters;
using Movie.Context.Repository;
using Movie.Models;

namespace Movie.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authentication]
    public class PanelController : Controller
    {
        public IActionResult Index() => View();

        /*

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
*/
    }
}
