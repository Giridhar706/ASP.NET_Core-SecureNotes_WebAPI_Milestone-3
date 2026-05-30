using System.ComponentModel.DataAnnotations;

namespace SecureNotesAPI.Models
{
    public class Note
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Content { get; set; }

        // Foreign key
        public int UserId { get; set; }

        public User? User { get; set; }
    }
}