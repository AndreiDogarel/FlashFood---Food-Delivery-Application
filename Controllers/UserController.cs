using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User.Management.Data.Data;
using User.Management.Data.Data.DTOs;
using User.Management.Service.Models;
using User.Management.Service.Services;

namespace FlashFood.Controllers
{
    [Authorize(Roles = "Admin, User")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(IUserService userService, UserManager<ApplicationUser> userManager) 
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderModel orderModel)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            try
            {
                var order = await _userService.CreateOrderAsync(user, orderModel);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-orders")]
        public async Task<IActionResult> GetUserOrders()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("This user hasn't ordered yet!");
            }

            var orders = await _userService.GetUserOrdersAsync(user.Id);
            return Ok(orders);
        }
    }
}
