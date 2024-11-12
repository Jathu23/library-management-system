namespace library_management_system.Utilities
{
    public class ImageService
    {
        private readonly IWebHostEnvironment _environment;

        public ImageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<List<string>> SaveImages(List<IFormFile> imageFiles, string folderName)
        {
            if (imageFiles == null || imageFiles.Count == 0)
            {
                throw new ArgumentException("No files uploaded.");
            }

            var imagePaths = new List<string>();
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var uploadsFolder = Path.Combine(_environment.WebRootPath, folderName);

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            foreach (var imageFile in imageFiles)
            {
                var fileExtension = Path.GetExtension(imageFile.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    throw new ArgumentException($"File '{imageFile.FileName}' is not a supported image type.");
                }

                if (imageFile.Length > 0)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    var imagePath = Path.Combine(folderName, uniqueFileName).Replace("\\", "/");
                    imagePaths.Add(imagePath);
                }
            }

            return imagePaths;
        }


        public async void DeleteImages(List<string> imagePaths, string folderName)
        {
            if (imagePaths == null || imagePaths.Count == 0)
            {
                throw new ArgumentException("No image paths provided.");
            }


            var uploadsFolder = _environment.WebRootPath;

            foreach (var imagePath in imagePaths)
            {

                string fullPath = Path.Combine(uploadsFolder, imagePath);


                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
                else
                {

                    Console.WriteLine($"File not found: {fullPath}");
                }
            }
        }
    }
}
