
using System.ComponentModel.DataAnnotations;

namespace NZWalksAPi.Models.DTO
{
    public class RegisterRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public String[] Roles { get; set; }
    }
}
