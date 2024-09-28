namespace FinanceApp.API.Models.Categoria
{
    public class CategoriaCreate
    {
        public string Nombre { get; set; }
        public int Tipo { get; set; }
        public int? UsuarioID { get; set; }
    }
}
