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
            var selectList = new List<DropDownItem>
            {
                new DropDownItem { Id = "", Name = "All" }
            };
            foreach (VolunteerStatus item in Enum.GetValues(typeof(VolunteerStatus)))
            {
                selectList.Add(new DropDownItem { Id=((int)item).ToString(), Name= item.GetDescription() });
            }
            return selectList;
        }
        // GET: api/<VolunteerController>
        [HttpGet("VolunteersForDropDown")]
        public async Task<List<DropDownItem>> GetVolunteerForDropDown()
        {
            return await volunteerManager.GetVolunteersForDropDown();
        }

        // GET api/<VolunteerController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
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

        [HttpPost("actions")]
        public async Task<IActionResult> Put([FromBody]VolunteerActionModel volunteerModel)
        {
            Result result;
            switch (volunteerModel.Action)
            {
                case HttpVolunteerActions.Approve:
                    result = await volunteerManager.ApproveAndCheckMail(volunteerModel.Id);
                    break;
                case HttpVolunteerActions.OnHold:
                    result = await volunteerManager.OnHold(volunteerModel.Id);
                    break;
                case HttpVolunteerActions.Cancel:
                    result = await volunteerManager.Cancel(volunteerModel.Id, volunteerModel.CancellationReason);
                    break;
                default:
                    result = new Result();
                    result.SetError(UserMessages.ActionNotFound);
                    break;
            }
            return Ok(result);
        }

        [HttpPost("SendMail")]
        public async Task<IActionResult> ResendStatusMail([FromBody] VolunteerActionModel volunteerModel)
        {
            return Ok(await volunteerManager.SendStatusMail(volunteerModel.Id));
        }

        // DELETE api/<VolunteerController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await volunteerManager.Delete(id));
        }
    }
}
