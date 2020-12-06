using AccountingNotebook.Services;
using AccountingNotebook.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AccountingNotebook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsService _service;

        public TransactionsController(ITransactionsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Inavlid transaction id");
            }

            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound($"No transaction with id {id}");
            }

            return Ok(result);
        }
    }
}
