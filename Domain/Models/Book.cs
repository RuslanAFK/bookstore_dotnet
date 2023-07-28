﻿namespace Domain.Models
{
    public class Book : ISearchable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Info { get; set; }
        public string Genre { get; set; }
        
        public string Image { get; set; }
        public string Author { get; set; }
        public int? BookFileId { get; set; }
        public BookFile? BookFile { get; set; }
    }
}