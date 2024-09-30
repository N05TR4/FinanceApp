namespace FinanceApp.API.Models.Ingreso
{
    public class IngresoUpdate
    {
        public int CategoriaIngresoID { get; set; }
        public int UsuarioID { get; set; }
        public int MetodoPagoID { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string? Descripcion { get; set; }
        public bool Recurrente { get; set; } = false;
    }
}
