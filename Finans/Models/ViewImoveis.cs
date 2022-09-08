using System;
using System.Collections.Generic;

#nullable disable

namespace Finans.Models
{
    public partial class ViewImoveis
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Descricao { get; set; }
        public string CodigoIptu { get; set; }
        public string SitePrefeitura { get; set; }
        public string Rua { get; set; }
        public int? Numero { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
    }
}
