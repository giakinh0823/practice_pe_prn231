namespace Q1.Dto
{
    public class StarRequest
    {
        public string? FullName { get; set; } = null!;
        public bool? Male { get; set; }
        public DateTime? Dob { get; set; }
        public string? Description { get; set; }
        public string? Nationality { get; set; }
        public List<int>? MovieIds { get; set; }

    }
}
