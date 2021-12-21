using Business.Abstract;
using Data.Constants;
using Data.Models;
using Data.Utility.Results;
using Data.Utility.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Data.Constants.Constants;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager userManager;

        public UserController(IUserManager userManager)
        {
            this.userManager = userManager;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
        // GET: api/<UserController>/info
        [HttpGet("info")]
        public async Task<IActionResult> GetInfo()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return Ok(await userManager.GetUserInfo(userId));
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
        // POST api/<UserController>
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            var loginResult = await userManager.Login(model);
            if(loginResult.Error)
                return Ok((Result)loginResult);

            var token = userManager.CreateAccessToken(loginResult.Data);

            return Ok(new ResultData<string>(token));
        }

        [HttpPost("Validate")]
        public IActionResult Validate()
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            return Ok(new ResultData<string>(token));
        }

        // PUT api/<UserController>
        [HttpPut]
        public void Put(int id, [FromBody] string value)
        {
        }
        // PUT api/<UserController>/UpdateInfo
        [HttpPost("UpdateInfo")]
        public async Task<IActionResult> PutInfo([FromBody] UserInfoUpdateModel model)
        {
            model.Id = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Result result;
            switch (model.Action)
            {
                case HttpUserActions.EmailChange:
                    result = await userManager.EmailChange(model.Id, model.Email);
                    break;
                case HttpUserActions.PasswordChange:
                    result = await userManager.PasswordChange(model.Id, model.Password);
                    break;
                case HttpUserActions.JobChange:
                    result = await userManager.JobChange(model.Id, model.Job);
                    break;
                default:
                    result = new Result();
                    result.SetError(UserMessages.ActionNotFound);
                    break;
            }
            return Ok(result);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
