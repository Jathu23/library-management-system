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
        public DbSet<LoginT> LoginPort { get; set; }
        public DbSet<NormalBook> NormalBooks { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<Ebook> Ebooks { get; set; }
        public DbSet<EbookMetadata> EbookMetadatas { get; set; }
        public DbSet<Audiobook> Audiobooks { get; set; }
        public DbSet<AudiobookMetadata> AudiobookMetadatas { get; set; }

        public DbSet<GlobalSubscription> globalSubscriptions { get; set; }

        public DbSet<LentRecord> LentRecords { get; set; }
        public DbSet<RentHistory> RentHistory { get; set; }

        public DbSet<SubscriptionPlan> SubscriptionPlan { get; set; }
        public DbSet<UserSubscription> UserSubscription { get; set; }
        public DbSet<PaymentDuration> PaymentDuration { get; set; }
        public DbSet<Payment> Payment { get; set; }

        public DbSet<LikeDislike> LikeDislikes { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationship between BookCopy and NormalBook
            modelBuilder.Entity<BookCopy>()
                .HasOne(bc => bc.Book)
                .WithMany(nb => nb.BookCopies)
                .HasForeignKey(bc => bc.BookId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete ensures dependent copies are deleted when a book is deleted.

            // Define relationships in RentHistory
            modelBuilder.Entity<RentHistory>()
                .HasOne(rh => rh.BookCopy)
                .WithMany() // No navigation property back to RentHistory in BookCopy
                .HasForeignKey(rh => rh.BookCopyId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting a BookCopy if referenced by RentHistory

            modelBuilder.Entity<RentHistory>()
                .HasOne(rh => rh.User)
                .WithMany() // No navigation property back to RentHistory in User
                .HasForeignKey(rh => rh.UserId);

            modelBuilder.Entity<RentHistory>()
                .HasOne(rh => rh.IssuingAdmin)
                .WithMany() // No navigation property back to RentHistory in Admin
                .HasForeignKey(rh => rh.IAdminId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RentHistory>()
                .HasOne(rh => rh.ReceivingAdmin)
                .WithMany() // No navigation property back to RentHistory in Admin
                .HasForeignKey(rh => rh.RAdminId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RentHistory>()
               .HasOne(rh => rh.BookCopy)
               .WithMany(bc => bc.RentHistories)
               .HasForeignKey(rh => rh.BookCopyId);




            modelBuilder.Entity<UserSubscription>()
              .HasOne(us => us.SubscriptionPlan)
              .WithMany()
              .HasForeignKey(us => us.SubscriptionPlanId);

            modelBuilder.Entity<UserSubscription>()
                 .HasOne(p => p.PaymentDuration)
                .WithMany()
                .HasForeignKey(p => p.PaymentDurationId);


            modelBuilder.Entity<Payment>()
                .HasOne(p => p.SubscriptionPlan)
                .WithMany()
                .HasForeignKey(p => p.SubscriptionPlanId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.PaymentDuration)
                .WithMany()
                .HasForeignKey(p => p.PaymentDurationId);


            // Configure relationships for LikeDislike
            modelBuilder.Entity<LikeDislike>()
                .HasOne(ld => ld.NormalBook)
                .WithMany()
                .HasForeignKey(ld => ld.BookId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_LikeDislikes_NormalBook");

            modelBuilder.Entity<LikeDislike>()
                .HasOne(ld => ld.EBook)
                .WithMany()
                .HasForeignKey(ld => ld.BookId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_LikeDislikes_Ebook");

            modelBuilder.Entity<LikeDislike>()
                .HasOne(ld => ld.AudioBook)
                .WithMany()
                .HasForeignKey(ld => ld.BookId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_LikeDislikes_Audiobook");

            // Configure relationships for Review
            modelBuilder.Entity<Review>()
                .HasOne(r => r.NormalBook)
                .WithMany()
                .HasForeignKey(r => r.BookId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Reviews_NormalBook");

            modelBuilder.Entity<Review>()
                .HasOne(r => r.EBook)
                .WithMany()
                .HasForeignKey(r => r.BookId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Reviews_Ebook");

            modelBuilder.Entity<Review>()
                .HasOne(r => r.AudioBook)
                .WithMany()
                .HasForeignKey(r => r.BookId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Reviews_Audiobook");


            base.OnModelCreating(modelBuilder);
        }

       


    }

}
