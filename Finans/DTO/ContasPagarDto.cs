using System;

namespace Finans.DTO
{
    public class ContasPagarDto
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Vencimento { get; set; }
        public int Tipo { get; set; }
    }
}
