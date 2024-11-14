namespace library_management_system.Utilities
{
    public class AudioBookFileService
    {
        private readonly IWebHostEnvironment _environment;

        public AudioBookFileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string folderName)
        {
            var folderPath = Path.Combine(_environment.WebRootPath, folderName);
            Directory.CreateDirectory(folderPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine(folderName, fileName);
        }
    }
}
