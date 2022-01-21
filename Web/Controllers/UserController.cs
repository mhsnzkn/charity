using Business.Abstract;
using Data.Constants;
using Data.Dtos;
using Data.Models;
using Data.Utility.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Get([FromQuery] UserTableParamsDto param)
        {
            return Ok(await userManager.GetTable(param));
        }
        // GET: api/<UserController>/info
        [HttpGet("info")]
        public async Task<IActionResult> GetInfo()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return Ok(await userManager.GetUserInfo(userId));
        }
        // GET: api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEditInfo(int id)
        {
            return Ok(await userManager.GetUser(id));
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserEditModel model)
        {
            Result result;
            if (model.Id == 0)
                result = await userManager.Add(model);
            else
                result = await userManager.Update(model);

            return Ok(result);
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
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (model.Id == 1 && userId != 1)
            {
                return Unauthorized(UserMessages.UnauthorizedAccess);
            }
            if(model.Id == 0)
            {
               model.Id = userId;
            }

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
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 1)
                return Unauthorized(UserMessages.UnauthorizedAccess);
            return Ok(await userManager.Delete(id));
        }
    }
}
