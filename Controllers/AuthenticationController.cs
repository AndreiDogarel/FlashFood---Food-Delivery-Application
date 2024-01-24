using FlashFood.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User.Management.Service.Models;
using User.Management.Service.Models.Authentication.SignUp;
using User.Management.Service.Models.Authentication.LogIn;
using User.Management.Service.Services;
using User.Management.Data.Data;
using User.Management.Service.Models.Authentication.User;

namespace FlashFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public AuthenticationController(UserManager<ApplicationUser> userManager,
            IEmailService emailService,
            IUserService user,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _emailService = emailService;
            _userService = user;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
        {
            var tokenResponse = await _userService.CreateUserWithTokenAsync(registerUser);
            if (tokenResponse.IsSuccess && tokenResponse.Response != null)
            {
                await _userService.AssignRoleToUserAsync(registerUser.Roles, tokenResponse.Response.User);
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { tokenResponse.Response.Token, email = registerUser.Email }, Request.Scheme);
                var message = new Message(new string[] { registerUser.Email! }, "FlashFood ~ Email Confirmation Link", confirmationLink!);
                var responseMsg = _emailService.SendEmail(message);
                return StatusCode(StatusCodes.Status200OK,
                    new Response { IsSuccess = true, Message = $"{tokenResponse.Message} {responseMsg}" });

            }

            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response { Message = tokenResponse.Message, IsSuccess = false });
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Message = "Email Verified Successfully!" });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response { Status = "Error", Message = "This User Doesn't Exist!" });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var loginOtpResponse = await _userService.GetOtpByLoginAsync(loginModel);
            if (loginOtpResponse.Response != null)
            {
                var user = loginOtpResponse.Response.User;
                if (user.TwoFactorEnabled)
                {
                    var token = loginOtpResponse.Response.Token;
                    var message = new Message(new string[] { user.Email! }, "OTP Confirmation", token);
                    _emailService.SendEmail(message);
                    return StatusCode(StatusCodes.Status200OK,
                        new Response { IsSuccess = loginOtpResponse.IsSuccess, Status = "Success", Message = $"We Have Sent An OTP To Your Email: {user.Email}." });
                }
                if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
                {
                    var serviceResponse = await _userService.GetJwtTokenAsync(user);
                    return Ok(serviceResponse);

                }
            }
            return Unauthorized();

        }

        [HttpPost]
        [Route("login-2FA")]
        public async Task<IActionResult> LoginWithOTP(string code, string userName)
        {
            var jwt = await _userService.LoginUserWithJWTokenAsync(code, userName);
            if (jwt.IsSuccess)
            {
                return Ok(jwt);
            }
            return StatusCode(StatusCodes.Status404NotFound,
                new Response { Status = "Success", Message = $"Invalid Code" });
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(LoginResponse tokens)
        {
            var jwt = await _userService.RenewAccessTokenAsync(tokens);
            if (jwt.IsSuccess)
            {
                return Ok(jwt);
            }
            return StatusCode(StatusCodes.Status404NotFound,
                new Response { Status = "Success", Message = $"Invalid Code" });
        }
    }
}
