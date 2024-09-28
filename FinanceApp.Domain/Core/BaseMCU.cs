



namespace FinanceApp.Domain.Core
{
    public abstract class BaseMCU
    {
        public string Nombre { get; set; }
        public bool Estado { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime? FechaModificacion { get; set; }
    }
}
