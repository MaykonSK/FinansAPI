using System;
using System.Collections.Generic;

#nullable disable

namespace Finans.Models
{
    public partial class Imovei
    {
        public int Id { get; set; }
        public int? UsuarioId { get; set; }
        public string Descricao { get; set; }
        public int EnderecoId { get; set; }
        public string CodigoIptu { get; set; }
        public string SitePrefeitura { get; set; }

        public virtual Endereco Endereco { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
