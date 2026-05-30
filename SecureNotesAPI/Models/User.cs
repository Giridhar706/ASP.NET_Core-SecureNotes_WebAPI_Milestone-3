using System.ComponentModel.DataAnnotations;

namespace SecureNotesAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        public string Username { get; set; }

        // Hashed password will be stored
        public string PasswordHash { get; set; }

        public ICollection<Note>? Notes { get; set; }
    }
}