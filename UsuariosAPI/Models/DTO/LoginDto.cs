using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Models.DTO
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string recaptcha { get; set; }
    }
}
