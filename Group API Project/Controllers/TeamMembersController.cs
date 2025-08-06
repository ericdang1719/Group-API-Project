using Microsoft.AspNetCore.Mvc;
using Group_API_Project.Models;
using Group_API_Project.Data;
using System.Collections.Generic;
using System.Linq;

namespace Group_API_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamMembersController : ControllerBase
    {
        private readonly JsonDataService _data;

        public TeamMembersController(JsonDataService data)
        {
            _data = data;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TeamMember>> Get()
        {
            var members = _data.GetCollection<TeamMember>().Take(5).ToList();
            return Ok(members);
        }

        [HttpGet("{id}")]
        public ActionResult<TeamMember> GetById(int id)
        {
            var member = _data.GetCollection<TeamMember>().FirstOrDefault(m => m.Id == id);
            if (member == null) return NotFound();
            return Ok(member);
        }

        [HttpPost]
        public ActionResult<TeamMember> Post([FromBody] TeamMember newMember)
        {
            var members = _data.GetCollection<TeamMember>();
            newMember.Id = members.Count > 0 ? members.Max(m => m.Id) + 1 : 1;
            members.Add(newMember);
            return CreatedAtAction(nameof(GetById), new { id = newMember.Id }, newMember);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TeamMember updated)
        {
            var members = _data.GetCollection<TeamMember>();
            var existing = members.FirstOrDefault(m => m.Id == id);
            if (existing == null) return NotFound();

            existing.FullName = updated.FullName;
            existing.Birthdate = updated.Birthdate;
            existing.CollegeProgram = updated.CollegeProgram;
            existing.YearInProgram = updated.YearInProgram;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var members = _data.GetCollection<TeamMember>();
            var existing = members.FirstOrDefault(m => m.Id == id);
            if (existing == null) return NotFound();

            members.Remove(existing);
            return NoContent();
        }
    }
}
