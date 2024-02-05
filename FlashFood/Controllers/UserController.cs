using System.Security.Claims;
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
    [Authorize]
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

        [HttpPost("create-feedback")]
        public async Task<IActionResult> CreateFeedback([FromBody] CreateFeedbackModel feedbackModel)
        {
            var feedbackResponse = await _userService.CreateFeedbackAsync(feedbackModel);
            if(feedbackResponse.IsSuccess == false)
            {
                return BadRequest(feedbackResponse.Message);
            }
            return Ok(feedbackResponse.Message);
        }
    }
}
