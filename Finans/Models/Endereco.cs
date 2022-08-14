using System;
using System.Collections.Generic;

#nullable disable

namespace Finans.Models
{
    public partial class Endereco
    {
        public Endereco()
        {
            Imoveis = new HashSet<Imovei>();
        }

        public int Id { get; set; }
        public string Rua { get; set; }
        public int? Numero { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }

        public virtual ICollection<Imovei> Imoveis { get; set; }
    }
}
