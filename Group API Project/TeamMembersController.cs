using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyControllerTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamMembersController : ControllerBase
    {
        private static List<TeamMember> teamMembers = new List<TeamMember>
        {
            new TeamMember { Id = 1, FullName = "Kam Paxton", Birthdate = DateTime.Parse("2001-02-02"), CollegeProgram = "IT", YearInProgram = 2 },
            new TeamMember { Id = 2, FullName = "Emily Johnson", Birthdate = DateTime.Parse("2000-05-14"), CollegeProgram = "Business", YearInProgram = 3 }
        };

        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public ActionResult<List<TeamMember>> GetFirstFive()
        {
            return teamMembers.Take(5).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<TeamMember> GetById(int id)
        {
            var member = teamMembers.FirstOrDefault(m => m.Id == id);
            if (member == null) return NotFound();
            return member;
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        public ActionResult<TeamMember> Post(TeamMember newMember)
        {
            newMember.Id = teamMembers.Count + 1;
            teamMembers.Add(newMember);
            return CreatedAtAction(nameof(GetById), new { id = newMember.Id }, newMember);
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Put(int id, TeamMember updatedMember)
        {
            var existing = teamMembers.FirstOrDefault(m => m.Id == id);
            if (existing == null) return NotFound();

            existing.FullName = updatedMember.FullName;
            existing.Birthdate = updatedMember.Birthdate;
            existing.CollegeProgram = updatedMember.CollegeProgram;
            existing.YearInProgram = updatedMember.YearInProgram;

            return NoContent();
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var member = teamMembers.FirstOrDefault(m => m.Id == id);
            if (member == null) return NotFound();

            teamMembers.Remove(member);
            return NoContent();
        }
    }

    public class TeamMember
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public string CollegeProgram { get; set; }
        public int YearInProgram { get; set; }
    }
}
