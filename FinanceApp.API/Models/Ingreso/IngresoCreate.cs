namespace FinanceApp.API.Models.Ingreso
{
    public class IngresoCreate
    {
        public int CategoriaID { get; set; }
        public int UsuarioID { get; set; }
        public int MetodoPagoID { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string? Descripcion { get; set; }
        public bool Recurrente { get; set; } = false;

    }
}
