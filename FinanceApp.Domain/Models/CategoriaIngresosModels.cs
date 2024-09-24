

namespace FinanceApp.Domain.Models
{
    public class CategoriaIngresosModels
    {
        public int CategoriaIngresoID { get; set; }
        public string Nombre { get; set; }
        public int? UsuarioID { get; set; }
        public string? NombreUsuario { get; set; }
    }
}
