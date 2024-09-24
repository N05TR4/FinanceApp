

using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Domain.Entities
{
    public sealed class MetodoPago
    {
        [Key]
        public int MetodoPagoID { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
    }
}
