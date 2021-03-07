using System.Threading.Tasks;
using GreenHealthApi.Endpoints.Customers.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GreenHealthApi.Endpoints.Customers
{
    [Route("api/v1/")]
    [ApiController]
    public class Controller : ControllerBase
    {
        private readonly ICustomerReader _reader; 
        private readonly ICustomerWriter _writer;

        public Controller(ICustomerReader reader, ICustomerWriter writer)
        {
            _reader = reader;
            _writer = writer;
        }

        [HttpGet]
        [Route("customers/{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _reader.GetCustomerByIdAsync(id);
            if (customer != null)
                return Ok(customer);
            else
                return NotFound();
        }
        //TODO Test this endpoint
        [HttpPost]
        [Route("customer/add")]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerAddEntity customer)
        {
            var isAdded = await _writer.AddCustomerAsync(customer);
            if (isAdded)
            {
                return Ok();
            }
            else
            {
                return Conflict();
            }
        }

        [HttpPut]
        [Route("users/change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePass([FromBody] ChangePswModel c)
        {
            var result = await _writer.ChangePassword(User.Identity.Name, c.OldPass, c.NewPass);
            if (result == true)
                return Ok();
            else
                return NotFound();
        }
    }
}
