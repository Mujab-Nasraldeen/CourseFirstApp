using CourseFirstApp.Data;
using CourseFirstApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseFirstApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GenresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Genres
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var genres = await _context.Genres.ToListAsync();
            return Ok(genres);
        }

        // GET: api/Genres/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var genre = await _context.Genres.FindAsync(id);

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

            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = genre.Id }, genre);
        }

        // PUT: api/Genres/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] Genre genre)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingGenre = await _context.Genres.FindAsync(id);

            if (existingGenre == null)
                return NotFound($"لم يتم العثور على Genre بالـ Id: {id}");

            existingGenre.Name = genre.Name;

            await _context.SaveChangesAsync();

            return Ok(existingGenre);
        }

        // DELETE: api/Genres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var genre = await _context.Genres.FindAsync(id);

            if (genre == null)
                return NotFound($"لم يتم العثور على Genre بالـ Id: {id}");

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return Ok(genre);
        }
    }
}