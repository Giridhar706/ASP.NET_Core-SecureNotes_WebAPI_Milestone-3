using System.ComponentModel.DataAnnotations;

namespace SecureNotesAPI.Models
{
    public class RegisterDto
    {
        [Required]
        [MinLength(4)]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}