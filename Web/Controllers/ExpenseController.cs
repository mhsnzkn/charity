using Business.Abstract;
using Data.Dtos;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseManager expenseManager;

        public ExpenseController(IExpenseManager expenseManager)
        {
            this.expenseManager = expenseManager;
        }
        // GET: api/<ExpenseController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ExpenseTableParamsDto param)
        {
            return Ok(await expenseManager.GetTable(param));
        }

        // GET api/<ExpenseController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await expenseManager.GetModelById(id));
        }

        // POST api/<ExpenseController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Expense expense, [FromForm] IFormFile formFile)
        {
            return Ok(await expenseManager.Add(expense, formFile));
        }

        // PUT api/<ExpenseController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ExpenseController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
