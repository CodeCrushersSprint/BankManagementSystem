using System.ComponentModel.DataAnnotations;

namespace BMSWebApi.DTO
{
    public class AdminDTO
    {
        [Required]
        public int AdminId { get; set; }

        [Required]
        public string AdminName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
