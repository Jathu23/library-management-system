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

        public async Task<int> AddEbook(Ebook ebook, EbookMetadata metadata)
        {
            _context.Ebooks.Add(ebook);
            await _context.SaveChangesAsync(); 

            metadata.EbookId = ebook.Id; 
            _context.EbookMetadatas.Add(metadata);
            await _context.SaveChangesAsync();
            return ebook.Id;
        }

        public async Task<Ebook> GetEbookById(int ebookId)
        {
            return await _context.Ebooks.FirstOrDefaultAsync(e => e.Id == ebookId);
        }
        public async Task<EbookMetadata> GetEbookMetadataByEbookId(int ebookId)
        {
            return await _context.EbookMetadatas
                .FirstOrDefaultAsync(metadata => metadata.EbookId == ebookId);
        }


        public async Task<bool> DeleteEbook(int ebookId)
        {
            var ebook = await _context.Ebooks.FindAsync(ebookId);
            if (ebook == null)
                return false;

        
            var metadata = await _context.EbookMetadatas.FindAsync(ebookId);
            if (metadata != null)
                _context.EbookMetadatas.Remove(metadata);

            _context.Ebooks.Remove(ebook);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task UpdateEbook(Ebook ebook, EbookMetadata metadata)
        {
           
            _context.Ebooks.Update(ebook);

           
            if (metadata != null)
            {
                _context.EbookMetadatas.Update(metadata);
            }

            
            await _context.SaveChangesAsync();
        }

    }
}
