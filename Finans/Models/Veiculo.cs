using System;
using System.Collections.Generic;

#nullable disable

namespace Finans.Models
{
    public partial class Veiculo
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public string Renavam { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
