using System.ComponentModel.DataAnnotations;

namespace BMSWebApi.DTO
{
    public class UserDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserRole { get; set; }
    }
}
