namespace OnlineEdu.PresentationLayer.Services
{
    public static class FileService
    {
        public static string FileSaveToServer(IFormFile formFile, string savePath)
        {
            var newImageName = Guid.NewGuid() + ".webp";
            var location = Path.Combine(Directory.GetCurrentDirectory(), savePath, newImageName);
            var stream = new FileStream(location, FileMode.Create);
            formFile.CopyTo(stream);
            
            return newImageName;
        }
    }
}
