using CourseFirstApp.Data;
using CourseFirstApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseFirstApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MoviesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var movies = await _context.Movies
                .Include(m => m.Genre)
                .ToListAsync();

            return Ok(movies);
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var movie = await _context.Movies
                .Include(m => m.Genre)
                .SingleOrDefaultAsync(m => m.Id == id);

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

            // التحقق من وجود الـ Genre
            var genreExists = await _context.Genres.AnyAsync(g => g.Id == movie.GenreId);
            if (!genreExists)
                return BadRequest($"لا يوجد Genre بالـ Id: {movie.GenreId}");

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie);
        }

        // PUT: api/Movies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingMovie = await _context.Movies.FindAsync(id);

            if (existingMovie == null)
                return NotFound($"لم يتم العثور على Movie بالـ Id: {id}");

            // التحقق من وجود الـ Genre
            var genreExists = await _context.Genres.AnyAsync(g => g.Id == movie.GenreId);
            if (!genreExists)
                return BadRequest($"لا يوجد Genre بالـ Id: {movie.GenreId}");

            existingMovie.Title    = movie.Title;
            existingMovie.Year     = movie.Year;
            existingMovie.Rate     = movie.Rate;
            existingMovie.Location = movie.Location;
            existingMovie.GenreId  = movie.GenreId;

            await _context.SaveChangesAsync();

            return Ok(existingMovie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
                return NotFound($"لم يتم العثور على Movie بالـ Id: {id}");

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return Ok(movie);
        }
    }
}