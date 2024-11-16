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
        public DbSet<NormalBook> NormalBooks { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<Ebook> Ebooks { get; set; }
        public DbSet<EbookMetadata> EbookMetadatas { get; set; }
        public DbSet<Audiobook> Audiobooks { get; set; }
        public DbSet<AudiobookMetadata> AudiobookMetadatas { get; set; }
        public DbSet<GlobalSubscription> globalSubscriptions { get; set; }



    }

}
