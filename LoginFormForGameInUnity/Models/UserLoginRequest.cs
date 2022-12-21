using System.ComponentModel.DataAnnotations;

namespace LoginFormForGameInUnity.Models
{
    public class UserLoginRequest
    {
        [Key]
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
