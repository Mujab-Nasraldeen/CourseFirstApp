namespace CourseFirstApp.Models
{
    public class Genre
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation Property - علاقة One-to-Many مع Movies
        //public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}