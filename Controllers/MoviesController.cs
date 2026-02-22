using CourseFirstApp.IServices;
using CourseFirstApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseFirstApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var movies = await _movieService.GetAllAsync();
            return Ok(movies);
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var movie = await _movieService.GetByIdAsync(id);

            if (movie == null)
                return NotFound($"لم يتم العثور على Movie بالـ Id: {id}");

            return Ok(movie);
        }

        // POST: api/Movies
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _movieService.CreateAsync(movie);

            if (created == null)
                return BadRequest($"لا يوجد Genre بالـ Id: {movie.GenreId}");

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/Movies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _movieService.UpdateAsync(id, movie);

            if (updated == null)
                return NotFound($"لم يتم العثور على Movie بالـ Id: {id} أو GenreId غير صحيح");

            return Ok(updated);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _movieService.DeleteAsync(id);

            if (deleted == null)
                return NotFound($"لم يتم العثور على Movie بالـ Id: {id}");

            return Ok(deleted);
        }
    }
}