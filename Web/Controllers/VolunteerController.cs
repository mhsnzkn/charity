using Business.Abstract;
using Data.Constants;
using Data.Dtos;
using Data.Entities;
using Data.Models;
using Data.Utility.Results;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Data.Constants.Constants;
using static Data.Constants.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Get([FromQuery]VolunteerTableParamsDto param)
        {
            return Ok(await volunteerManager.GetTable(param));
        }

        // GET: api/<VolunteerController>
        [HttpGet("GetVolunteerStatus")]
        public IEnumerable<DropDownItem> GetVolunteerStatus()
        {
            var selectList = new List<DropDownItem>();
            foreach (VolunteerStatus item in Enum.GetValues(typeof(VolunteerStatus)))
            {
                selectList.Add(new DropDownItem { Id=item.GetHashCode(), Name= item.ToString() });
            }
            return selectList;
        }

        // GET api/<VolunteerController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await volunteerManager.GetByIdAsync(id));
        }

        // POST api/<VolunteerController>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VolunteerModel model)
        {
            return Ok(await volunteerManager.Add(model));
        }

        // PUT api/<VolunteerController>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]VolunteerActionDto volunteer)
        {
            Result result;
            switch (volunteer.Action)
            {
                case HttpActions.Approve:
                    result = await volunteerManager.Approve(volunteer.Id);
                    break;
                case HttpActions.Cancel:
                    result = await volunteerManager.Cancel(volunteer.Id, volunteer.CancellationReason);
                    break;
                default:
                    result = new Result();
                    result.SetError(UserMessages.ActionNotFound);
                    break;
            }
            return Ok(result);
        }

        // DELETE api/<VolunteerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
