

using FinanceApp.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Domain.Entities
{
    public sealed class Gasto : BaseEntity
    {
        [Key]
        public int GastoID { get; set; }
        public int CategoriaGastoID { get; set; }

    }
}
