using Business.Abstract;
using Data.Dtos;
using Data.Dtos.Datatable;
using Data.Entities;
using Data.Models;
using Data.Utility.Results;
using Microsoft.AspNetCore.Authorization;
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
    public class AgreementController : ControllerBase
    {
        private readonly IAgreementManager agreementManager;
        private readonly IVolunteerManager volunteerManager;

        public AgreementController(IAgreementManager agreementManager, IVolunteerManager volunteerManager)
        {
            this.agreementManager = agreementManager;
            this.volunteerManager = volunteerManager;
        }
        // GET: api/<AgreementController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TableParams param)
        {
            return Ok(await agreementManager.GetTable(param));
        }

        // GET api/<AgreementController>/5
        [HttpGet("{id}")]
        public async Task<Agreement> Get(int id)
        {
            return await agreementManager.GetById(id);
        }

        // GET api/<AgreementController>/5
        [AllowAnonymous]
        [HttpGet("Volunteer/{key}")]
        public async Task<IActionResult> GetActiveAgreements(Guid key)
        {
            var volunteer = await volunteerManager.GetVolunteerByKey(key);
            if (volunteer == null)
                return NotFound();

            return Ok(await agreementManager.GetActiveAgreements());
        }

        // POST api/<AgreementController>
        [HttpPost]
        public async Task<Result> Post([FromBody] AgreementModel agreement)
        {
            if(agreement.Id > 0)
                return await agreementManager.Update(agreement);
            return await agreementManager.Add(agreement);
        }
        // POST api/<AgreementController>
        [AllowAnonymous]
        [HttpPost("Volunteer")]
        public async Task<IActionResult> PostAgreements([FromBody] VolunteerAgreementPostModel model)
        {
            var result = await agreementManager.SaveVolunteerAgreements(model);
            return Ok(result);
        }

        // DELETE api/<AgreementController>/5
        [HttpDelete("{id}")]
        public async Task<Result> Delete(int id)
        {
            return await agreementManager.Delete(id);
        }
    }
}
