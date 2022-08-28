using System;
using System.Collections.Generic;

#nullable disable

namespace Finans.Models
{
    public partial class Endereco
    {
        public Endereco()
        {
            Imoveis = new HashSet<Imovel>();
        }

        public int Id { get; set; }
        public string Rua { get; set; }
        public int? Numero { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }

        public virtual ICollection<Imovel> Imoveis { get; set; }
    }
}
