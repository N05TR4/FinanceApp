

namespace FinanceApp.Domain.Models
{
    public class UsuarioModels
    {
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
