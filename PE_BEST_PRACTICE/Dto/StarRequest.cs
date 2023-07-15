namespace PE_BEST_PRACTICE.Dto
{
    public class StarRequest
    {
        public string FullName { get; set; } = null!;
        public bool? Male { get; set; }
        public DateTime? Dob { get; set; }
        public string? Description { get; set; }
        public string? Nationality { get; set; }
    }
}
