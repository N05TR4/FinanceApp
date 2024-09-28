namespace FinanceApp.API.Models.Usuario
{
    public class UsuarioCreate
    {
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
    }
}
