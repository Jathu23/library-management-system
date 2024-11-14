using library_management_system.Database;
using library_management_system.Database.Entiy;
using Microsoft.EntityFrameworkCore;

namespace library_management_system.Repositories
{
    public class EbookRepository
    {
        private readonly LibraryDbContext _context;

        public EbookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddEbookWithMetadataAsync(Ebook ebook, EbookMetadata metadata)
        {
            _context.Ebooks.Add(ebook);
            await _context.SaveChangesAsync(); // Save Ebook to get the Ebook ID

            metadata.EbookId = ebook.Id; // Associate metadata with the Ebook
            _context.EbookMetadatas.Add(metadata);
            await _context.SaveChangesAsync(); // Save Metadata

            return ebook.Id;
        }
    }
}
