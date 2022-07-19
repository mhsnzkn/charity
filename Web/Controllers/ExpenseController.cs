using Business.Abstract;
using Data.Constants;
using Data.Dtos;
using Data.Entities;
using Data.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        // GET: api/<ExpenseController>
        [HttpGet("GetExpenseStatus")]
        public IEnumerable<DropDownItem> GetExpenseStatus()
        {
            var selectList = new List<DropDownItem>();
            selectList.Add(new DropDownItem { Id = "", Name = "All" });
            foreach (ExpenseStatus item in Enum.GetValues(typeof(ExpenseStatus)))
            {
                selectList.Add(new DropDownItem { Id = ((int)item).ToString(), Name = item.GetDescription() });
            }
            return selectList;
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
