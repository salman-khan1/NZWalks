using System.ComponentModel.DataAnnotations;

namespace NZWalksAPi.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "code has to be minimum of 2 characters")]
        [MaxLength(4, ErrorMessage = "Code has to be maximum of 4 characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(40, ErrorMessage = "Name has to be maximum of 40 characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
