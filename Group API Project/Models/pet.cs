using System.ComponentModel.DataAnnotations;

namespace Group_API_Project.Models
    //Emily Schweitzer
{
    public class Pet
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Species { get; set; } 

        [Range(0, 50)]
        public int Age { get; set; } 

        public string? Breed { get; set; }
        public bool IsAdopted { get; set; }

        [DataType(DataType.Date)]
        public DateTime? AdoptionDate { get; set; }
    }
}