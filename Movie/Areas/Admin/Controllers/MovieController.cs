using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Movie.Areas.Admin.Filters;
using Movie.Context.Repository;
using Movie.Interface;
using Movie.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Movie.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authentication]
    public class MovieController : ControllerBase
    {

        private readonly FilmRepository _filmRepository;
        private readonly SerialRepository _serialRepository;

        public MovieController(Context.MovieContext movieContext)
        {
            _filmRepository = new FilmRepository(movieContext);
            _serialRepository = new SerialRepository(movieContext);
        }

        // GET: api/<MovieController>
        [HttpGet]
        public ActionResult<IEnumerable<object>> Get(string type)
        {
            switch (type)
            {
                case nameof(Models.Film):
                    return _filmRepository.GetAll();
                case nameof(Models.Serial):
                    return _serialRepository.GetAll();
                default:
                    return BadRequest("Error: Not found type!");
            }
        }

        // GET api/<MovieController>/5
        [HttpGet("{id}")]
        public ActionResult<object> Get(int id, string type)
        {
            switch (type)
            {
                case nameof(Models.Film):
                    var film = _filmRepository.GetId(id);
                    if (film == null)
                        return BadRequest($"Error: Not found Id:{id}");
                    return film;
                case nameof(Models.Serial):
                    var serial = _serialRepository.GetId(id);
                    if (serial == null)
                        return BadRequest($"Error: Not found Id:{id}");
                    return serial;
                default:
                    return BadRequest("Error: Not found type!");
            }
        }

        // POST api/<MovieController>
        [HttpPost("{type}")]
        public async Task<IActionResult> PostAsync(string type)
        {
            return BadRequest();
        }

        // PUT api/<MovieController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MovieController>/5
        [HttpDelete("{type}/{id}")]
        public IActionResult Delete(int id, string type)
        {
            switch (type)
            {
                case nameof(Models.Film):
                    if (!_filmRepository.Delete(id))
                        return BadRequest($"Error: Not found Id:{id}");
                    break;
                case nameof(Models.Serial):
                    if (!_serialRepository.Delete(id))
                        return BadRequest($"Error: Not found Id:{id}");
                    break;
                default:
                    return BadRequest("Error: Not found type!");
            }
            return Ok($"Success delete item id:{id}!");
        }
    }
}
