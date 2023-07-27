namespace NZWalksAPi.Models.DTO
{
    public class ImageUploadRequestDto
    {
        public IFormFile File { get; set; }

        public string FileName { get; set; }
        public String? FileDescription { get; set; }


}
}