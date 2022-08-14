using System;
using System.Collections.Generic;

#nullable disable

namespace Finans.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            ContasPagars = new HashSet<ContasPagar>();
            ContasRecebers = new HashSet<ContasReceber>();
            Imoveis = new HashSet<Imovei>();
            Veiculos = new HashSet<Veiculo>();
        }

        public int UsuarioId { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<ContasPagar> ContasPagars { get; set; }
        public virtual ICollection<ContasReceber> ContasRecebers { get; set; }
        public virtual ICollection<Imovei> Imoveis { get; set; }
        public virtual ICollection<Veiculo> Veiculos { get; set; }
    }
}
