using library_management_system.Database;
using library_management_system.Database.Entiy;
using Microsoft.EntityFrameworkCore;

namespace library_management_system.Repositories
{
    public class AudioBookRepository
    {
        private readonly LibraryDbContext _context;

        public AudioBookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAudiobook(Audiobook audiobook, AudiobookMetadata metadata)
        {
           
            _context.Audiobooks.Add(audiobook);
            await _context.SaveChangesAsync();

           
            metadata.AudiobookId = audiobook.Id;
            _context.AudiobookMetadatas.Add(metadata);
            await _context.SaveChangesAsync();

            return audiobook.Id; 
        }


        public async Task<Audiobook> GetAudiobookById(int audiobookId)
        {
            return await _context.Audiobooks
                .Include(a => a.Metadata)
                .FirstOrDefaultAsync(a => a.Id == audiobookId);
        }
        public async Task<AudiobookMetadata?> GetAudiobookMetadataByAudiobookId(int audiobookId)
        {
            return await _context.AudiobookMetadatas
                .FirstOrDefaultAsync(metadata => metadata.AudiobookId == audiobookId);
        }

        public async Task UpdateAudiobook(Audiobook audiobook, AudiobookMetadata metadata)
        {
            _context.Audiobooks.Update(audiobook);        
            _context.AudiobookMetadatas.Update(metadata);    
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAudiobook(int audiobookId)
        {
           
            var audiobook = await _context.Audiobooks.FindAsync(audiobookId);
            if (audiobook == null) return false; 

            var metadata = await _context.AudiobookMetadatas
                .FirstOrDefaultAsync(m => m.AudiobookId == audiobookId);

         
            if (metadata != null)
            {
                _context.AudiobookMetadatas.Remove(metadata);
            }

           
            _context.Audiobooks.Remove(audiobook);
            await _context.SaveChangesAsync();

            return true;
        }



    }
}
