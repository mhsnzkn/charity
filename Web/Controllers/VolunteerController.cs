using Business.Abstract;
using Data.Models;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolunteerController : ControllerBase
    {
        private readonly IVolunteerManager volunteerManager;

        public VolunteerController(IVolunteerManager volunteerManager)
        {
            this.volunteerManager = volunteerManager;
        }

        // GET: api/<VolunteerController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<VolunteerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<VolunteerController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VolunteerModel model)
        {
            return Ok(await volunteerManager.Add(model));
        }

        // PUT api/<VolunteerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VolunteerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
