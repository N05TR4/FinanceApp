

namespace FinanceApp.Domain.Core
{
    public abstract class BaseCategoria
    {
        public string Nombre { get; set; }
        public int? UsuarioID { get; set; }
        public bool Estado { get; set; }
    }
}
