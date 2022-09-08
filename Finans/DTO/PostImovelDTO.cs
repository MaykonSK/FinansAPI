using Finans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finans.DTO
{
    public class PostImovelDTO
    {
        public int UsuarioId { get; set; }
        public string Descricao { get; set; }
        public string CodigoIptu { get; set; }
        public string SitePrefeitura { get; set; }
        public string Imagem { get; set; }
        public virtual Endereco Endereco { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
