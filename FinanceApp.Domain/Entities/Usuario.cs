

using FinanceApp.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Domain.Entities
{
    public sealed class Usuario : BaseMCU
    {
        [Key]
        public int UsuarioID { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }

    }
}
