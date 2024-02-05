using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User.Management.Data.Data;
using User.Management.Data.Data.DTOs;
using User.Management.Service.Services;

namespace FlashFood.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AdminController(UserManager<ApplicationUser> userManager, IMapper mapper, IUserService userService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet("get-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            var userDtos = _mapper.Map<List<UserDto>>(users);

            return Ok(userDtos);
        }

        [HttpDelete("delete-feedback/{feedbackId}")]
        public async Task<IActionResult> DeleteFeedback(Guid feedbackId)
        {
            await _userService.DeleteFeedbackAsync(feedbackId);
            return Ok("Feedback deleted successfully!");
        }

        [HttpGet("get-feedbacks/{userName}")]
        public async Task<IActionResult> GetUserFeedbacks(string userName)
        {
            var feedbacks = await _userService.GetAllFeedbacksByUserAsync(userName);
            return Ok(feedbacks);
        }
    }
}
