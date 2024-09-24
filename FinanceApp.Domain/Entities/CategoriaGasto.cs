

using FinanceApp.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Domain.Entities
{
    public sealed class CategoriaGasto : BaseCategoria
    {
        [Key]
        public int CategoriaGastoID { get; set; }
    }
}
