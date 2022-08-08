using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Models
{
    public class SolicitaResetSenha
    {
        [Required]
        public string Email { get; set; }
    }
}
