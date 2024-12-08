namespace library_management_system.Database
{
    using library_management_system.Database.Entiy;
    using library_management_system.Database.Entiy.LikeDisLike;
    using library_management_system.Database.Entiy.ReviewEntitys;
    using Microsoft.EntityFrameworkCore;
    using System.Diagnostics.Eventing.Reader;
    using static library_management_system.Controllers.LikeDislikeController;

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

        public DbSet<NormalBookReview> NormalBookReviews { get; set; }
        public DbSet<EbookReview> EbookReviews { get; set; }
        public DbSet<AudiobookReview> AudiobookReviews { get; set; }


        public DbSet<NormalBookLikeDislike> NormalBookLikeDislikes { get; set; }
        public DbSet<EbookLikeDislike> EbookLikeDislikes { get; set; }
        public DbSet<AudiobookLikeDislike> AudiobookLikeDislikes { get; set; }

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




            // Configure EbookLikeDislike
            modelBuilder.Entity<EbookLikeDislike>()
                .HasOne(el => el.Ebook)  // Navigation property
                .WithMany()              // Assuming Ebook doesn't track its LikeDislikes
                .HasForeignKey(el => el.BookId)  // Use BookId as the foreign key
                .OnDelete(DeleteBehavior.Cascade);  // Define delete behavior

            modelBuilder.Entity<EbookLikeDislike>()
                .HasOne(el => el.User)  // Navigation property for User
                .WithMany()             // Assuming User doesn't track EbookLikeDislikes
                .HasForeignKey(el => el.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure AudiobookLikeDislike
            modelBuilder.Entity<AudiobookLikeDislike>()
                .HasOne(al => al.Audiobook)  // Navigation property
                .WithMany()                  // Assuming Audiobook doesn't track LikeDislikes
                .HasForeignKey(al => al.BookId)  // Use BookId as the foreign key
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AudiobookLikeDislike>()
                .HasOne(al => al.User)  // Navigation property for User
                .WithMany()             // Assuming User doesn't track AudiobookLikeDislikes
                .HasForeignKey(al => al.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure NormalBookLikeDislike
            modelBuilder.Entity<NormalBookLikeDislike>()
                .HasOne(nbl => nbl.NormalBook)  // Navigation property
                .WithMany()                    // Assuming NormalBook doesn't track LikeDislikes
                .HasForeignKey(nbl => nbl.BookId)  // Use BookId as the foreign key
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NormalBookLikeDislike>()
                .HasOne(nbl => nbl.User)  // Navigation property for User
                .WithMany()               // Assuming User doesn't track NormalBookLikeDislikes
                .HasForeignKey(nbl => nbl.UserId)
                .OnDelete(DeleteBehavior.Cascade);



            base.OnModelCreating(modelBuilder);
        }

       


    }

}
