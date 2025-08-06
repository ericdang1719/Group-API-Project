using Microsoft.AspNetCore.Mvc;
using Group_API_Project.Models;
using Group_API_Project.Data;
using System.Collections.Generic;
using System.Linq;

namespace Group_API_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly JsonDataService _data;

        public PetsController(JsonDataService data)
        {
            _data = data;
        }

        [HttpGet]
        public ActionResult<List<Pet>> GetFirstFive()
        {
            var pets = _data.GetCollection<Pet>();
            return pets.Take(5).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Pet> GetById(int id)
        {
            var pets = _data.GetCollection<Pet>();
            var pet = pets.FirstOrDefault(p => p.Id == id);
            if (pet == null) return NotFound();
            return pet;
        }

        [HttpPost]
        public ActionResult<Pet> Post(Pet newPet)
        {
            var pets = _data.GetCollection<Pet>();
            newPet.Id = pets.Count + 1;
            pets.Add(newPet);
            return CreatedAtAction(nameof(GetById), new { id = newPet.Id }, newPet);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Pet updatedPet)
        {
            var pets = _data.GetCollection<Pet>();
            var existing = pets.FirstOrDefault(p => p.Id == id);
            if (existing == null) return NotFound();

            existing.Name = updatedPet.Name;
            existing.Species = updatedPet.Species;
            existing.Breed = updatedPet.Breed;
            existing.Age = updatedPet.Age;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pets = _data.GetCollection<Pet>();
            var pet = pets.FirstOrDefault(p => p.Id == id);
            if (pet == null) return NotFound();

            pets.Remove(pet);
            return NoContent();
        }
    }
}
