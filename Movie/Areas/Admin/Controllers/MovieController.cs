using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using System.Security.Cryptography;

using Movie.Models;
using Movie.Context;
using Movie.Context.Repository;
using Movie.Areas.Admin.Filters;

namespace Movie.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authentication]
    public class MovieController : ControllerBase
    {

        private readonly FilmRepository _filmRepository;
        private readonly SerialRepository _serialRepository;
        private readonly IWebHostEnvironment _appEnvironment;

        public MovieController(MovieContext movieContext, IWebHostEnvironment appEnvironment)
        {
            _filmRepository = new FilmRepository(movieContext);
            _serialRepository = new SerialRepository(movieContext);
            _appEnvironment = appEnvironment;
        }

        // GET: api/<MovieController>
        [HttpGet]
        public ActionResult<IEnumerable<object>> Get(string type)
        {
            switch (type)
            {
                case nameof(Film):
                    return _filmRepository.GetAll();
                case nameof(Serial):
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
        public async Task<ActionResult> PostAsync([FromRoute] string type, [FromForm] IFormFile file)
        {

            try
            {
                if (file == null)
                    throw new Exception("File 'Image' equals is null!");

                string namePoster = HashTime() + ".png";
                await SaveFile(file, namePoster);

                switch (type)
                {
                    case nameof(Film):
                        {
                            var temp = Request.Form.ToImmutableDictionary();

                            var film = new Film();
                            film.Name = temp["Name"];
                            film.Evaluation = float.Parse(temp["Evaluation"]);
                            film.Description = temp["Description"];
                            film.PosterPath = "/img/FileBD/" + namePoster;

                            if (film.Name == null || film.Description == null || film.Evaluation < 0)
                                throw new Exception("One of the partners equals is null!");

                            await _filmRepository.AddAsync(film);
                        }
                        break;
                    case nameof(Serial):
                        {
                            var temp = Request.Form.ToImmutableDictionary();

                            var serial = new Serial();
                            serial.Name = temp["Name"];
                            serial.Evaluation = float.Parse(temp["Evaluation"]);
                            serial.Description = temp["Description"];
                            serial.PosterPath = "img/FileBD/" + namePoster;

                            serial.Episode = int.Parse(temp["Episode"]);
                            serial.Season = int.Parse(temp["Season"]);
                            serial.Completed = bool.Parse(temp["Completed"]);

                            if (serial.Name == null || serial.Description == null || serial.Evaluation < 0)
                                throw new Exception("One of the partners equals is null!");

                            await _serialRepository.AddAsync(serial);
                        }
                        break;
                    default:
                        return BadRequest("Error: Not found type!");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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

        private async Task SaveFile(IFormFile file, string newName)
        {
            try
            {
                string path = "/img/FileBD/" + newName;
                using (var fs = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        private string HashTime()
        {
            using (var sha = SHA256.Create())
            {
                var hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(DateTime.Now.ToString()));
                var hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return hash;
            }

        }
    }
}
