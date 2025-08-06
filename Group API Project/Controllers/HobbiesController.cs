using Microsoft.AspNetCore.Mvc;
using Group_API_Project.Models;
using Group_API_Project.Data;
using System.Collections.Generic;
using System.Linq;

namespace Group_API_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HobbiesController : ControllerBase
    {
        private readonly JsonDataService _data;

        public HobbiesController(JsonDataService data)
        {
            _data = data;
        }

        [HttpGet]
        public ActionResult<List<Hobby>> GetFirstFive()
        {
            var hobbies = _data.GetCollection<Hobby>();
            return hobbies.Take(5).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Hobby> GetById(int id)
        {
            var hobbies = _data.GetCollection<Hobby>();
            var hobby = hobbies.FirstOrDefault(h => h.Id == id);
            if (hobby == null) return NotFound();
            return hobby;
        }

        [HttpPost]
        public ActionResult<Hobby> Post(Hobby newHobby)
        {
            var hobbies = _data.GetCollection<Hobby>();
            newHobby.Id = hobbies.Count > 0 ? hobbies.Max(h => h.Id) + 1 : 1;
            hobbies.Add(newHobby);
            return CreatedAtAction(nameof(GetById), new { id = newHobby.Id }, newHobby);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Hobby updatedHobby)
        {
            var hobbies = _data.GetCollection<Hobby>();
            var existing = hobbies.FirstOrDefault(h => h.Id == id);
            if (existing == null) return NotFound();

            existing.Name = updatedHobby.Name;
            existing.Category = updatedHobby.Category;
            existing.HoursPerWeek = updatedHobby.HoursPerWeek;
            existing.IsActive = updatedHobby.IsActive;
            existing.DifficultyLevel = updatedHobby.DifficultyLevel;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var hobbies = _data.GetCollection<Hobby>();
            var hobby = hobbies.FirstOrDefault(h => h.Id == id);
            if (hobby == null) return NotFound();

            hobbies.Remove(hobby);
            return NoContent();
        }
    }
}
