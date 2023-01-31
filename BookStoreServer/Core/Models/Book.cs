using System.ComponentModel.DataAnnotations;

namespace BookStoreServer.Core.Models
{
    public sealed class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Info { get; set; }
        public string Genre { get; set; }
        
        public string Image { get; set; }
        public string Author { get; set; }
    }
}
