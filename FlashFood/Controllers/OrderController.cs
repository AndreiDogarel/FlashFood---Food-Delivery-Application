using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User.Management.Data.Data;
using User.Management.Service.Models;
using User.Management.Service.Services;
using User.Management.Service.Services.OrderService;

namespace FlashFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;
        public OrderController(IOrderService orderService, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderModel orderModel)
        {
            var user = await _userManager.FindByNameAsync(orderModel.UserName);

            if (user == null)
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim != null)
                {
                    user = await _userManager.FindByIdAsync(userIdClaim);
                }
            }

            if (user == null)
            {
                return NotFound("User not found.");
            }

            try
            {
                var order = await _orderService.CreateOrderAsync(user, orderModel);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get-orders")]
        public async Task<IActionResult> GetUserOrders(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var orders = await _orderService.GetUserOrdersAsync(user.Id);
            if (orders != null)
            {
                return Ok(orders);
            }
            else
            {
                return NotFound("This user hasn't ordered anything!");
            }
        }
    }
}
