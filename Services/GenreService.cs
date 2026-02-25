using CourseFirstApp.Data;
using CourseFirstApp.IServices;
using CourseFirstApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseFirstApp.Services
{
    public class GenreService(AppDbContext context) : IGenreService
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<Genre?> GetByIdAsync(long id)
        {
            return await _context.Genres.FindAsync(id);
        }

        public async Task<Genre> CreateAsync(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task<Genre?> UpdateAsync(long id, Genre genre)
        {
            var existingGenre = await _context.Genres.FindAsync(id);
            if (existingGenre == null) return null;

            existingGenre.Name = genre.Name;
            await _context.SaveChangesAsync();
            return existingGenre;
        }

        public async Task<Genre?> DeleteAsync(long id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) return null;

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return genre;
        }
    }
}