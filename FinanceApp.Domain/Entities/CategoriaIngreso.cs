

using FinanceApp.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Domain.Entities
{
    public sealed class CategoriaIngreso : BaseCategoria
    {
        [Key]
        public int CategoriaIngresoID { get; set; }
    }
}
