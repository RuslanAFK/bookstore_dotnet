using System.ComponentModel.DataAnnotations;

namespace bookstoreserver.Data
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(16)]
        public string Username { get; set; } = string.Empty;
        [Required]
        [MaxLength(16)]
        public string Password { get; set; } = string.Empty;
        [Required]
        public bool IsAdmin { get; set; }
    }
}
