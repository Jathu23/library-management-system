﻿using System.ComponentModel.DataAnnotations;

namespace library_management_system.Database.Entiy
{
    public class Admin
    {
        [Key]
        public int id { get; set; }
        public string AdminNic { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public  IFormFile? ProfileImage { get; set; }
        public required string PasswordHash { get; set; }

    };
}