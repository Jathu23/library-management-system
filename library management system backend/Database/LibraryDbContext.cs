namespace library_management_system.Database
{
    using library_management_system.Database.Entiy;
    using Microsoft.EntityFrameworkCore;
    using System.Diagnostics.Eventing.Reader;

    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Admin> Admins { get; set; }



    }

}
