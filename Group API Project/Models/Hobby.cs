namespace Group_API_Project.Models
{
    //Emily Schweitzer
    public class Hobby
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public int HoursPerWeek { get; set; }
        public bool IsActive { get; set; }
        public string? DifficultyLevel { get; set; }
    }
}