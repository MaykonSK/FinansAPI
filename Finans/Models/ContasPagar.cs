using System;
using System.Collections.Generic;

#nullable disable

namespace Finans.Models
{
    public partial class ContasPagar
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Vencimento { get; set; }
        public int Tipo { get; set; }
        public int? UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
