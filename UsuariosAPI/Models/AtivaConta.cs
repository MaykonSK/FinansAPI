using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Models
{
    public class AtivaConta
    {
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public string CodigoAtivacao { get; set; }
    }
}
