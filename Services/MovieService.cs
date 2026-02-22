using CourseFirstApp.Data;
using CourseFirstApp.IServices;
using CourseFirstApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseFirstApp.Services
{
    public class MovieService : IMovieService
    {
        private readonly AppDbContext _context;

        public MovieService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _context.Movies
                .Include(m => m.Genre)
                .ToListAsync();
        }

        public async Task<Movie?> GetByIdAsync(long id)
        {
            return await _context.Movies
                .Include(m => m.Genre)
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Movie?> CreateAsync(Movie movie)
        {
            // التحقق من وجود الـ Genre
            var genreExists = await _context.Genres.AnyAsync(g => g.Id == movie.GenreId);
            if (!genreExists) return null;

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<Movie?> UpdateAsync(long id, Movie movie)
        {
            var existingMovie = await _context.Movies.FindAsync(id);
            if (existingMovie == null) return null;

            // التحقق من وجود الـ Genre
            var genreExists = await _context.Genres.AnyAsync(g => g.Id == movie.GenreId);
            if (!genreExists) return null;

            existingMovie.Title    = movie.Title;
            existingMovie.Year     = movie.Year;
            existingMovie.Rate     = movie.Rate;
            existingMovie.Location = movie.Location;
            existingMovie.GenreId  = movie.GenreId;

            await _context.SaveChangesAsync();
            return existingMovie;
        }

        public async Task<Movie?> DeleteAsync(long id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return null;

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return movie;
        }
    }
}