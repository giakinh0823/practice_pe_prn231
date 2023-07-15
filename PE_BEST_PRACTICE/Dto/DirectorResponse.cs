using PE_BEST_PRACTICE.Models;

namespace PE_BEST_PRACTICE.Dto
{
    public class DirectorResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public bool Male { get; set; }
        public DateTime Dob { get; set; }
        public string Nationality { get; set; } = null!;
        public string Description { get; set; } = null!;
        public virtual ICollection<String> Movies { get; set; }
    }
}
