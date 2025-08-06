using System.ComponentModel.DataAnnotations;

namespace Group_API_Project.Models
{
    public class TeamMember
    {
      //Emily Schweitzer
        public TeamMember()
        {
            FullName = string.Empty;
            CollegeProgram = string.Empty;
            YearInProgram = string.Empty;
        }

        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        [Required]
        public string CollegeProgram { get; set; }

        [Required]
        public string YearInProgram { get; set; }
    }
}