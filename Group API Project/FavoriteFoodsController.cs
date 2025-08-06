using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyControllerTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteFoodsController : ControllerBase
    {
        // 🔹 In-memory fake list of favorite foods
        private static List<FavoriteFood> favoriteFoods = new List<FavoriteFood>
        {
            new FavoriteFood { Id = 1, FoodName = "Pizza", Cuisine = "Italian", IsVegetarian = false, Calories = 300 },
            new FavoriteFood { Id = 2, FoodName = "Falafel", Cuisine = "Middle Eastern", IsVegetarian = true, Calories = 250 }
        };

        /// <summary>
        /// Get the first 5 favorite foods
        /// </summary>
        [HttpGet]
        public ActionResult<List<FavoriteFood>> GetFirstFive()
        {
            return favoriteFoods.Take(5).ToList();
        }

        /// <summary>
        /// Get a favorite food by ID
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<FavoriteFood> GetById(int id)
        {
            var food = favoriteFoods.FirstOrDefault(f => f.Id == id);
            if (food == null) return NotFound();
            return food;
        }

        /// <summary>
        /// Add a new favorite food
        /// </summary>
        [HttpPost]
        public ActionResult<FavoriteFood> Post(FavoriteFood newFood)
        {
            newFood.Id = favoriteFoods.Count + 1;
            favoriteFoods.Add(newFood);
            return CreatedAtAction(nameof(GetById), new { id = newFood.Id }, newFood);
        }

        /// <summary>
        /// Update a favorite food
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Put(int id, FavoriteFood updatedFood)
        {
            var existing = favoriteFoods.FirstOrDefault(f => f.Id == id);
            if (existing == null) return NotFound();

            existing.FoodName = updatedFood.FoodName;
            existing.Cuisine = updatedFood.Cuisine;
            existing.IsVegetarian = updatedFood.IsVegetarian;
            existing.Calories = updatedFood.Calories;

            return NoContent();
        }

        /// <summary>
        /// Delete a favorite food
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var food = favoriteFoods.FirstOrDefault(f => f.Id == id);
            if (food == null) return NotFound();

            favoriteFoods.Remove(food);
            return NoContent();
        }
    }

    // ✅ Temporary fake model with working properties
    public class FavoriteFood
    {
        public int Id { get; set; }
        public string FoodName { get; set; } = string.Empty;
        public string Cuisine { get; set; } = string.Empty;
        public bool IsVegetarian { get; set; }
        public int Calories { get; set; }
    }
}
