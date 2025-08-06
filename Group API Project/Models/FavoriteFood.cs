using System.ComponentModel.DataAnnotations;

namespace Group_API_Project.Models
//emily schweitzer
{
    public class FavoriteFood
    {
        public int Id { get; set; } 

        [Required]
        public string? Name { get; set; } 

        [Required]
        public string? CuisineType { get; set; } 

        [Range(1, 5)]
        public int SpicinessLevel { get; set; }

        public bool IsVegetarian { get; set; }
        public bool IsGlutenFree { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Description { get; set; } 
    }
}