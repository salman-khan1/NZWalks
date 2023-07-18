using System.ComponentModel.DataAnnotations;

namespace NZWalksAPi.Models.DTO
{
    public class UpdateWalkRequestDto
    {
        [Required]
        [MaxLength(40, ErrorMessage = "Name has to be maximum of 40 charcters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "Description has to be of 1000 characters")]
        public string Description { get; set; }
        [Required]
        [Range(0, 50)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
