using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalksAPi.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }

        [NotMapped]        
        public IFormFile File { get; set; }

        public string FileName { get; set; }
        public String? FileDescription { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }
    }
}
