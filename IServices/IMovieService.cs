using CourseFirstApp.Models;

namespace CourseFirstApp.IServices
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie?> GetByIdAsync(long id);
        Task<Movie?> CreateAsync(Movie movie);
        Task<Movie?> UpdateAsync(long id, Movie movie);
        Task<Movie?> DeleteAsync(long id);
    }
}