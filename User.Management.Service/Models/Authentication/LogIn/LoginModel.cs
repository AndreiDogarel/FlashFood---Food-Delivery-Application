using System.ComponentModel.DataAnnotations;

namespace User.Management.Service.Models.Authentication.LogIn
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is Required!")]
        public string Username { get; set; } = null!;
        [Required(ErrorMessage = "Password is Required!")]
        public string Password { get; set; } = null!;
    }
}
