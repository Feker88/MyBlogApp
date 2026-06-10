using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CodePulse.Application.DTOs.Authentification
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage ="Email is Required")]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string Email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Password is Required")]
        [StringLength(100, ErrorMessage = "Password must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", 
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirmed Password is Required")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; } = string.Empty;        
    }
}
