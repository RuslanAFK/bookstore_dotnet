using System.ComponentModel.DataAnnotations;

namespace bookstoreserver.Data
{
    internal sealed class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(36)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(400)]
        public string Info { get; set; } = string.Empty;
        [Required]
        [MaxLength(36)]
        public string Genre { get; set; } = string.Empty;
        [Required]
        [MaxLength(1000)]
        public string Image { get; set; } = string.Empty;
        [Required]
        [MaxLength(36)]
        public string Author { get; set; } = string.Empty;
    }
}
