using System.ComponentModel.DataAnnotations;

namespace LoginFormForGameInUnity.Models
{
    public class UserRegisterRequest
    {
        [Key]
        [RegularExpression(@"^([a-zA-Z0-9 \.\&\'\-]+)$", ErrorMessage = "{0} must be alpha numeric")]
        [Required]
        //public IdentityUser Username { get; set; } = new IdentityUser(); //Mean set string as PK
        public string Username { get; set; } = string.Empty; //Mean set string as PK
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MinLength(6, ErrorMessage = "Please enter at least 6 characters dude!")]
        public string Password { get; set; } = string.Empty;

        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
