using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyControllerTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        // 🔹 Fake in-memory list of pets
        private static List<Pet> pets = new List<Pet>
        {
            new Pet { Id = 1, Name = "Buster", Species = "Dog", Breed = "Labrador", Age = 3 },
            new Pet { Id = 2, Name = "Whiskers", Species = "Cat", Breed = "Siamese", Age = 2 }
        };

        /// <summary>
        /// Get the first 5 pets
        /// </summary>
        [HttpGet]
        public ActionResult<List<Pet>> GetFirstFive()
        {
            return pets.Take(5).ToList();
        }

        /// <summary>
        /// Get a pet by ID
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<Pet> GetById(int id)
        {
            var pet = pets.FirstOrDefault(p => p.Id == id);
            if (pet == null) return NotFound();
            return pet;
        }

        /// <summary>
        /// Add a new pet
        /// </summary>
        [HttpPost]
        public ActionResult<Pet> Post(Pet newPet)
        {
            newPet.Id = pets.Count + 1;
            pets.Add(newPet);
            return CreatedAtAction(nameof(GetById), new { id = newPet.Id }, newPet);
        }

        /// <summary>
        /// Update a pet
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Put(int id, Pet updatedPet)
        {
            var existing = pets.FirstOrDefault(p => p.Id == id);
            if (existing == null) return NotFound();

            existing.Name = updatedPet.Name;
            existing.Species = updatedPet.Species;
            existing.Breed = updatedPet.Breed;
            existing.Age = updatedPet.Age;

            return NoContent();
        }

        /// <summary>
        /// Delete a pet
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pet = pets.FirstOrDefault(p => p.Id == id);
            if (pet == null) return NotFound();

            pets.Remove(pet);
            return NoContent();
        }
    }

    // 🔸 Temporary fake Pet model
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }
    }
}
