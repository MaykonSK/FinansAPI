using System;
using System.Collections.Generic;

#nullable disable

namespace Finans.Models
{
    public partial class VcontasPagar
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Vencimento { get; set; }
        public bool Recorrente { get; set; }
        public int UsuarioId { get; set; }
        public bool? Paga { get; set; }
        public string Status { get; set; }
    }
}
