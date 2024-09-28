

using FinanceApp.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Domain.Entities
{
    public sealed class Tipo : BaseCM
    {
        [Key]
        public int TipoID { get; set; }
    }
}
