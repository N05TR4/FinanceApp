

namespace FinanceApp.Domain.Models
{
    public class GastoModels
    {
        public int GastoID { get; set; }
        public int CategoriaGastoID { get; set; }
        public int UsuarioID { get; set; }
        public int MetodoPagoID { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string? Descripcion { get; set; }
        public bool Recurrente { get; set; }
    }
}
