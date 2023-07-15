namespace PE_BEST_PRACTICE.Dto
{
    public class DirectorRequest
    {
        public string FullName { get; set; } = null!;
        public bool Male { get; set; }
        public DateTime Dob { get; set; }
        public string Nationality { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
