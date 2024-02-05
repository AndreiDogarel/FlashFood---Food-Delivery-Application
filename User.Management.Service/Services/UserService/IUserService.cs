using FlashFood.Models;
using User.Management.Data.Data;
using User.Management.Data.Data.DTOs;
using User.Management.Service.Models;
using User.Management.Service.Models.Authentication.LogIn;
using User.Management.Service.Models.Authentication.SignUp;
using User.Management.Service.Models.Authentication.User;

namespace User.Management.Service.Services
{
    public interface IUserService
    {
        Task<ApiResponse<CreateUserResponse>> CreateUserWithTokenAsync(RegisterUser registerUser);
        Task<ApiResponse<List<string>>> AssignRoleToUserAsync(List<string> roles, ApplicationUser user);
        Task<ApiResponse<LoginOtpResponse>> GetOtpByLoginAsync(LoginModel loginModel);
        Task<ApiResponse<LoginResponse>> GetJwtTokenAsync(ApplicationUser user);
        Task<ApiResponse<LoginResponse>> LoginUserWithJWTokenAsync(string otp, string userName);
        Task<ApiResponse<LoginResponse>> RenewAccessTokenAsync(LoginResponse tokens);
        Task<ApiResponse<Feedback>> CreateFeedbackAsync(CreateFeedbackModel feedback);
        Task DeleteFeedbackAsync(Guid feedbackId);
        Task<IEnumerable<FeedbackDto>> GetAllFeedbacksByUserAsync(string userName);
    }
}
