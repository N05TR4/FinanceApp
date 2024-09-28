

namespace FinanceApp.Domain.Core
{
    public abstract class BaseEntity
    {
        public int UsuarioID { get; set; }
        public int MetodoPagoID { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string? Descripcion { get; set; }
        public bool Recurrente { get; set; }
        public bool Estado { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime? FechaModificacion { get; set; }
    }
}
