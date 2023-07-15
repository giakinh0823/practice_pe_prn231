using Q1.Models;

namespace Q1.Dto
{
    public class MovieResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? ReleaseDate { get; set; }
        public string? Description { get; set; }
        public string Language { get; set; } = null!;
        public int? ProducerId { get; set; }
        public int? DirectorId { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Star> Stars { get; set; }
    }
}
