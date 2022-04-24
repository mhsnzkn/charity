using Business.Abstract;
using Business.Utility.MailService;
using Data.Constants;
using Data.Dtos;
using Data.Entities;
using Data.Extensions;
using Data.Models;
using Data.Utility.Results;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            selectList.Add(new DropDownItem { Id = "", Name = "All" });
            foreach (VolunteerStatus item in Enum.GetValues(typeof(VolunteerStatus)))
            {
                selectList.Add(new DropDownItem { Id=item.GetHashCode().ToString(), Name= item.GetDescription() });
            }
            return selectList;
        }

        // GET api/<VolunteerController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await volunteerManager.GetDetailDto(id));
        }

        // POST api/<VolunteerController>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VolunteerModel model)
        {
            return Ok(await volunteerManager.AddWithMail(model));
        }
        // POST api/<VolunteerController>
        [AllowAnonymous]
        [HttpPost("Documents")]
        public async Task<IActionResult> PostDocuments([FromForm] VolunteerDocumentPostModel model)
        {
            var result = await volunteerManager.UploadDocuments(model);
            return Ok(result);
        }

        // PUT api/<VolunteerController>
        [HttpPost("actions")]
        public async Task<IActionResult> Put([FromBody]VolunteerActionModel volunteer)
        {
            Result result;
            switch (volunteer.Action)
            {
                case HttpVolunteerActions.Approve:
                    result = await volunteerManager.ApproveAndCheckMail(volunteer.Id);
                    break;
                case HttpVolunteerActions.OnHold:
                    result = await volunteerManager.OnHold(volunteer.Id);
                    break;
                case HttpVolunteerActions.Cancel:
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
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await volunteerManager.Delete(id));
        }
    }
}
