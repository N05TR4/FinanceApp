

using FinanceApp.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Domain.Entities
{
    public sealed class Ingreso : BaseEntity
    {
        [Key]
        public int IngresoID { get; set; }
        public int CategoriaIngresoID { get; set; }
    }
}
