﻿namespace library_management_system.DTOs.Book
{
    public class NormalBookDetailsDto
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public List<string> Genre { get; set; }
        public int PublishYear { get; set; }
        public string ShelfLocation { get; set; }
        public int AvailableCopies { get; set; }
        public int TotalCopies { get; set; }
        public List<string> CoverImagePath { get; set; }
        public List<BookCopyDto> BookCopies { get; set; }
    }

}