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


        public async Task<Audiobook?> GetAudiobookById(int audiobookId)
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
        public async Task<bool> AddClick(int audiobookId)
        {
            try
            {
                var data = await _context.AudiobookMetadatas
                    .FirstOrDefaultAsync(metadata => metadata.AudiobookId == audiobookId);


                data.Click++;

                _context.AudiobookMetadatas.Update(data);
                var result = await _context.SaveChangesAsync();

                return result > 0; 
            }
            catch (Exception ex)
            {
               
                return false;
            }
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

        public async Task<(List<Audiobook>, int)> GetPaginatedAudiobooksAsync(int page, int pageSize)
        {
            var audiobooks = await _context.Audiobooks.Include(a => a.Metadata)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Total count for pagination metadata
            int totalCount = await _context.Audiobooks.Include(a => a.Metadata).CountAsync();

            return (audiobooks, totalCount);
        }

        public async Task<(List<Audiobook>, int)> SearchAsync(string searchString, int pageNumber, int pageSize)
        {
            // Query with metadata included
            var query = _context.Audiobooks.Include(a => a.Metadata).AsQueryable();

            // Apply search filter if searchString is not null or empty
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(b =>
                    b.Genre.Contains(searchString) ||    // Search in Genre
                    b.Author.Contains(searchString) ||   // Search in Author
                    b.Title.Contains(searchString) ||    // Search in Title
                    b.ISBN.Contains(searchString) ||     // Search in ISBN
                    b.PublishYear.ToString().Contains(searchString) // Search in PublishYear
                );
            }

            // Get total count of records that match the search criteria
            int totalRecords = await query.CountAsync();

            // Paginate the results
            var audioBooks = await query
                .Skip((pageNumber - 1) * pageSize) // Skip records based on page number
                .Take(pageSize)                   // Take the specified page size
                .ToListAsync();

            // Return the paginated result and the total record count
            return (audioBooks, totalRecords);
        }


        //DbFunctions to get 3 books from the Audio table -------------------------------------------
        public async Task<List<Audiobook>> GetTopAudiobooksAsync(int count)
		{
			return await _context.Audiobooks
				.Include(a => a.Metadata) // Include related Metadata
				.OrderByDescending(a => a.AddedDate) 
				.Take(count) // Get the top 3 audiobooks
				.ToListAsync();
		}




	}
}
