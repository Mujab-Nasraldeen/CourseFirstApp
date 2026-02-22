using System.ComponentModel.DataAnnotations.Schema;

namespace CourseFirstApp.Models
{
    public class Movie
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; }
        public double Rate { get; set; }
        public string Location { get; set; } = string.Empty;

        public long GenreId { get; set; } // Foreign Key
        [ForeignKey(nameof(GenreId))]
        public Genre? Genre { get; set; } // Navigation Property
    }
}