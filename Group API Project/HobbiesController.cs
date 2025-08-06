using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyControllerTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HobbiesController : ControllerBase
    {
        // 🔹 Fake in-memory list of hobbies
        private static List<Hobby> hobbies = new List<Hobby>
        {
            new Hobby { Id = 1, Name = "Basketball", Description = "Playing on a team", HoursPerWeek = 5, SkillLevel = "Intermediate" },
            new Hobby { Id = 2, Name = "Drawing", Description = "Sketching with pencils", HoursPerWeek = 3, SkillLevel = "Beginner" }
        };

        /// <summary>
        /// Get the first 5 hobbies
        /// </summary>
        [HttpGet]
        public ActionResult<List<Hobby>> GetFirstFive()
        {
            return hobbies.Take(5).ToList();
        }

        /// <summary>
        /// Get a hobby by ID
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<Hobby> GetById(int id)
        {
            var hobby = hobbies.FirstOrDefault(h => h.Id == id);
            if (hobby == null) return NotFound();
            return hobby;
        }

        /// <summary>
        /// Add a new hobby
        /// </summary>
        [HttpPost]
        public ActionResult<Hobby> Post(Hobby newHobby)
        {
            newHobby.Id = hobbies.Count + 1;
            hobbies.Add(newHobby);
            return CreatedAtAction(nameof(GetById), new { id = newHobby.Id }, newHobby);
        }

        /// <summary>
        /// Update a hobby
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Put(int id, Hobby updatedHobby)
        {
            var existing = hobbies.FirstOrDefault(h => h.Id == id);
            if (existing == null) return NotFound();

            existing.Name = updatedHobby.Name;
            existing.Description = updatedHobby.Description;
            existing.HoursPerWeek = updatedHobby.HoursPerWeek;
            existing.SkillLevel = updatedHobby.SkillLevel;

            return NoContent();
        }

        /// <summary>
        /// Delete a hobby
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var hobby = hobbies.FirstOrDefault(h => h.Id == id);
            if (hobby == null) return NotFound();

            hobbies.Remove(hobby);
            return NoContent();
        }
    }

    // 🔸 Temporary fake Hobby model
    public class Hobby
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int HoursPerWeek { get; set; }
        public string SkillLevel { get; set; }
    }
}
