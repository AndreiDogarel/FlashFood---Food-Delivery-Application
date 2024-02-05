using User.Management.Service.Models.Authentication.SignUp;
using User.Management.Service.Models;
using Microsoft.AspNetCore.Identity;
using User.Management.Service.Models.Authentication.User;
using User.Management.Data.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using User.Management.Service.Models.Authentication.LogIn;
using FlashFood.Models;
using User.Management.Service.Repositories.OrderRepository;
using User.Management.Service.Repositories.RestaurantRepository;
using User.Management.Data.Data.DTOs;
using AutoMapper;
using User.Management.Service.Repositories.DishRepository;
using User.Management.Service.Repositories.FeedbackRepository;

namespace User.Management.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public UserService(UserManager<ApplicationUser> userManager,
                            RoleManager<IdentityRole> roleManager,
                            SignInManager<ApplicationUser> signInManager, 
                            IConfiguration configuration,
                            IFeedbackRepository feedbackRepository,
                            IRestaurantRepository restaurantRepository,
                            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _feedbackRepository = feedbackRepository;
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<string>>> AssignRoleToUserAsync(List<string> roles, ApplicationUser user)
        {
            var assignedRole = new List<string>();
            foreach (var role in roles)
            {
                if (await _roleManager.RoleExistsAsync(role))
                {
                    if (!await _userManager.IsInRoleAsync(user, role))
                    {
                        await _userManager.AddToRoleAsync(user, role);
                        assignedRole.Add(role);
                    }
                }
            }

            return new ApiResponse<List<string>>
            {
                IsSuccess = true,
                StatusCode = 200,
                Message = "Roles Has Been Assigned!",
                Response = assignedRole
            };
        }
        public async Task<ApiResponse<CreateUserResponse>> CreateUserWithTokenAsync(RegisterUser registerUser)
        {
            var userExist = await _userManager.FindByEmailAsync(registerUser.Email);
            if (userExist != null)
            {
                return new ApiResponse<CreateUserResponse> { IsSuccess = false, StatusCode = 403, Message = "User Already Exists!" };
            }
            ApplicationUser user = new()
            {
                Email = registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerUser.Username,
                TwoFactorEnabled = true
            };
            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                return new ApiResponse<CreateUserResponse> { Response = new CreateUserResponse() { User = user, Token = token }, IsSuccess = true, StatusCode = 201, Message = "User Created" };
            }
            else
            {
                return new ApiResponse<CreateUserResponse> { IsSuccess = false, StatusCode = 500, Message = "User Failed To Create!" };
            }

        }
        public async Task<ApiResponse<LoginOtpResponse>> GetOtpByLoginAsync(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.Username);
            if (user != null)
            {
                await _signInManager.SignOutAsync();
                await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, true);
                if (user.TwoFactorEnabled)
                {
                    var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
                    return new ApiResponse<LoginOtpResponse>
                    {
                        Response = new LoginOtpResponse()
                        {
                            User = user,
                            Token = token,
                            IsTwoFactorEnable = user.TwoFactorEnabled
                        },
                        IsSuccess = true,
                        StatusCode = 200,
                        Message = $"OTP Sent To The Email: {user.Email}."
                    };

                }
                else
                {
                    return new ApiResponse<LoginOtpResponse>
                    {
                        Response = new LoginOtpResponse()
                        {
                            User = user,
                            Token = string.Empty,
                            IsTwoFactorEnable = user.TwoFactorEnabled
                        },
                        IsSuccess = true,
                        StatusCode = 200,
                        Message = $"Two-Factor Authentication Is Not Enabled!"
                    };
                }
            }
            else
            {
                return new ApiResponse<LoginOtpResponse>
                {
                    IsSuccess = false,
                    StatusCode = 404,
                    Message = $"This User Doesn't Exist!"
                };
            }
        }
        public async Task<ApiResponse<LoginResponse>> GetJwtTokenAsync(ApplicationUser user)
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtToken = GetToken(authClaims);
            var refreshToken = GenerateRefreshToken();
            _ = int.TryParse(_configuration["JWT:RefreshTokenValidity"], out int refreshTokenValidity);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(refreshTokenValidity);

            await _userManager.UpdateAsync(user);

            return new ApiResponse<LoginResponse>
            {
                Response = new LoginResponse()
                {
                    AccessToken = new TokenType()
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                        ExpiryTokenDate = jwtToken.ValidTo
                    },
                    RefreshToken = new TokenType()
                    {
                        Token = user.RefreshToken,
                        ExpiryTokenDate = (DateTime)user.RefreshTokenExpiry
                    }
                },

                IsSuccess = true,
                StatusCode = 200,
                Message = $"Token Created!"
            };
        }
        public async Task<ApiResponse<LoginResponse>> LoginUserWithJWTokenAsync(string otp, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var signIn = await _signInManager.TwoFactorSignInAsync("Email", otp, false, false);
            if (signIn.Succeeded)
            {
                if (user != null)
                {
                    return await GetJwtTokenAsync(user);
                }
            }
            return new ApiResponse<LoginResponse>()
            {

                Response = new LoginResponse()
                {

                },
                IsSuccess = false,
                StatusCode = 400,
                Message = $"Invalid Otp"
            };
        }
        public async Task<ApiResponse<LoginResponse>> RenewAccessTokenAsync(LoginResponse tokens)
        {
            var accessToken = tokens.AccessToken;
            var refreshToken = tokens.RefreshToken;
            var principal = GetClaimsPrincipal(accessToken.Token);
            var user = await _userManager.FindByNameAsync(principal.Identity.Name);
            if (refreshToken.Token != user.RefreshToken && refreshToken.ExpiryTokenDate <= DateTime.Now)
            {
                return new ApiResponse<LoginResponse>
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = $"Token Invalid Or Expired!"
                };
            }
            var response = await GetJwtTokenAsync(user);
            return response;
        }
        public async Task<ApiResponse<Feedback>> CreateFeedbackAsync(CreateFeedbackModel feedbackModel)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(feedbackModel.RestaurantId);
            if (restaurant == null)
            {
                return new ApiResponse<Feedback>
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = "This restaurant doesn't exist!"
                };
            }
            var feedback = new Feedback
            {
                Id = Guid.NewGuid(),
                UserName = feedbackModel.UserName,
                Rating = feedbackModel.Rating,
                Description = feedbackModel.Description,
                RestaurantId = feedbackModel.RestaurantId,
                Restaurant = restaurant
            };
            await _feedbackRepository.AddAsync(feedback);
            return new ApiResponse<Feedback>
            {
                IsSuccess = true,
                StatusCode = 200,
                Message = "Feedback created successfully!",
                Response = feedback
            };
        }
        public async Task DeleteFeedbackAsync(Guid feedbackId)
        {
            await _feedbackRepository.DeleteAsync(feedbackId);
        }
        public async Task<IEnumerable<FeedbackDto>> GetAllFeedbacksByUserAsync(string userName)
        {
            var result = await _feedbackRepository.GetAllFeedbacksByUserAsync(userName);
            return _mapper.Map<IEnumerable<FeedbackDto>>(result);
        }

        #region PrivateMethods
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);
            var expirationTimeUtc = DateTime.UtcNow.AddMinutes(tokenValidityInMinutes);
            var localTimeZone = TimeZoneInfo.Local;
            var expirationTimeInLocalTimeZone = TimeZoneInfo.ConvertTimeFromUtc(expirationTimeUtc, localTimeZone);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: expirationTimeInLocalTimeZone,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new Byte[64];
            var range = RandomNumberGenerator.Create();
            range.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private ClaimsPrincipal GetClaimsPrincipal(string accessToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);

            return principal;

        }
        #endregion
    }
}
