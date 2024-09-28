

using FinanceApp.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Domain.Entities
{
    public sealed class Categoria : BaseMCU
    {
        [Key]
        public int CategoriaID { get; set; }
        public int Tipo { get; set; }
        public int? UsuarioID { get; set; }
    }
}
