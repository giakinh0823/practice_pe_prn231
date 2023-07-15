using Q1.Models;

namespace Q1.Dto
{
    public class StarResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public bool? Male { get; set; }
        public DateTime? Dob { get; set; }
        public string? Description { get; set; }
        public string? Nationality { get; set; }

        public virtual ICollection<MovieResponse> Movies { get; set; }
    }
}
