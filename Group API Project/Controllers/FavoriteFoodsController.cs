using Microsoft.AspNetCore.Mvc;
using Group_API_Project.Models;
using Group_API_Project.Data;
using System.Collections.Generic;
using System.Linq;

namespace Group_API_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteFoodsController : ControllerBase
    {
        private readonly JsonDataService _data;

        public FavoriteFoodsController(JsonDataService data)
        {
            _data = data;
        }

        [HttpGet]
        public ActionResult<List<FavoriteFood>> GetFirstFive()
        {
            var foods = _data.GetCollection<FavoriteFood>();
            return foods.Take(5).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<FavoriteFood> GetById(int id)
        {
            var foods = _data.GetCollection<FavoriteFood>();
            var food = foods.FirstOrDefault(f => f.Id == id);
            if (food == null) return NotFound();
            return food;
        }

        [HttpPost]
        public ActionResult<FavoriteFood> Post(FavoriteFood newFood)
        {
            var foods = _data.GetCollection<FavoriteFood>();
            newFood.Id = foods.Count + 1;
            foods.Add(newFood);
            return CreatedAtAction(nameof(GetById), new { id = newFood.Id }, newFood);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, FavoriteFood updatedFood)
        {
            var foods = _data.GetCollection<FavoriteFood>();
            var existing = foods.FirstOrDefault(f => f.Id == id);
            if (existing == null) return NotFound();

            existing.Name = updatedFood.Name;
            existing.CuisineType = updatedFood.CuisineType;
            existing.SpicinessLevel = updatedFood.SpicinessLevel;
            existing.IsVegetarian = updatedFood.IsVegetarian;
            existing.IsGlutenFree = updatedFood.IsGlutenFree;
            existing.Description = updatedFood.Description;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var foods = _data.GetCollection<FavoriteFood>();
            var food = foods.FirstOrDefault(f => f.Id == id);
            if (food == null) return NotFound();

            foods.Remove(food);
            return NoContent();
        }
    }
}
