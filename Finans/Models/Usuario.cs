using System;
using System.Collections.Generic;

#nullable disable

namespace Finans.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            ContasRecebers = new HashSet<ContasReceber>();
            Imoveis = new HashSet<Imovel>();
            Veiculos = new HashSet<Veiculo>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }

        public virtual ICollection<ContasReceber> ContasRecebers { get; set; }
        public virtual ICollection<Imovel> Imoveis { get; set; }
        public virtual ICollection<Veiculo> Veiculos { get; set; }
    }
}
