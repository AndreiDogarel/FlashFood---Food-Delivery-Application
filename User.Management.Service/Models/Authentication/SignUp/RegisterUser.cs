﻿using System.ComponentModel.DataAnnotations;

namespace User.Management.Service.Models.Authentication.SignUp
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "User Name is required!")]
        public string Username { get; set; } = null!;
        [EmailAddress]
        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; } = null!;
        [Required]
        public List<string> Roles { get; set; } = null!;
    }
}
