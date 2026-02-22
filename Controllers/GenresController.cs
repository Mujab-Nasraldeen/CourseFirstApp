using CourseFirstApp.IServices;
using CourseFirstApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseFirstApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        // GET: api/Genres
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var genres = await _genreService.GetAllAsync();
            return Ok(genres);
        }

        // GET: api/Genres/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var genre = await _genreService.GetByIdAsync(id);

            if (genre == null)
                return NotFound($"لم يتم العثور على Genre بالـ Id: {id}");

            return Ok(genre);
        }

        // POST: api/Genres
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Genre genre)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _genreService.CreateAsync(genre);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/Genres/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] Genre genre)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _genreService.UpdateAsync(id, genre);

            if (updated == null)
                return NotFound($"لم يتم العثور على Genre بالـ Id: {id}");

            return Ok(updated);
        }

        // DELETE: api/Genres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _genreService.DeleteAsync(id);

            if (deleted == null)
                return NotFound($"لم يتم العثور على Genre بالـ Id: {id}");

            return Ok(deleted);
        }
    }
}