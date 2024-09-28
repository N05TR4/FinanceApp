

namespace FinanceApp.Domain.Models
{
    public class CategoriaModels
    {
        public int CategoriaID { get; set; }
        public string Nombre { get; set; }
        public int Tipo { get; set; }
        public int? UsuarioID { get; set; }
        public string? NombreUsuario { get; set; }

    }
}
