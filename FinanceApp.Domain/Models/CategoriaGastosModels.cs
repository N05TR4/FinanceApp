

namespace FinanceApp.Domain.Models
{
    public class CategoriaGastosModels
    {
        public int CategoriaGastoID { get; set; }
        public string Nombre { get; set; }
        public int? UsuarioID { get; set; }
        public string? NombreUsuario { get; set; }

    }
}
