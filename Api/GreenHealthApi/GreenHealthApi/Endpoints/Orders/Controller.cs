using System.Threading.Tasks;
using GreenHealthApi.Endpoints.Orders.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GreenHealthApi.Endpoints.Orders
{
    [ApiController]
    [Route("api/v1/orders")]
    public class Controller : ControllerBase
    {
        private readonly IOrdersWriter _writer;
        private readonly IOrdersReader _reader;

        public Controller(IOrdersWriter writer, IOrdersReader reader)
        {
            _writer = writer;
            _reader = reader;
        }

        [HttpPost]
        [Route("place-order/")]
        public async Task<IActionResult> AddOrder([FromBody] OrderModel order)
        {
            await _writer.AddOrderAsync(order);
            return Ok();
        }

        [HttpGet]
        [Route("get-unconfirmed/{isTrueInt}")]
        public async Task<IActionResult> GetUnconfirmed(int isTrueInt)
        {
            if (isTrueInt ==1)
            {
                var orders = await _reader.GetUnconfirmed();
                return Ok(orders);
            }
            else
            {
                var orders = await _reader.GetСonfirmed();
                return Ok(orders);
            }
            
        }

        [HttpGet]
        [Authorize]
        [Route("get-my/")]
        public async Task<IActionResult> GetMy()
        {
            var a = User.Identity.Name;
            var orders = await _reader.GetAllUsersOrders(User.Identity.Name);
            return Ok(orders);
        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]// ?????????
        [Authorize]
        [Route("confirm/{id}")]
        public async Task<IActionResult> ConfirmOrder([FromRoute] int id)
        {
            await _writer.ConfirmOrderById(id);
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("del/{id}")]
        public async Task<IActionResult> DelOrder([FromRoute] int id)
        {
            await _writer.DeleteOrderById(id);
            return Ok();
        }

        [HttpGet]
        [Route("get-all-categories/")]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _reader.GetAllCategories());
        }

        [HttpPost]
        [Authorize]
        [Route("del-category/{id}")]
        public async Task<IActionResult> DelCategory([FromRoute] int id)
        {
            await _writer.DelCategory(id);
            return Ok();
        }

        [HttpPost]
        [Route("changeel")]
        public async Task<IActionResult> changeEl([FromQuery]int or, [FromQuery]int  orEl, [FromQuery]int count)
        {
            await _writer.ChangeOrderElCount(or, orEl, count);
            
            return Ok();
        }
}
}