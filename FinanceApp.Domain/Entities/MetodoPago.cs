

using FinanceApp.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Domain.Entities
{
    public sealed class MetodoPago : BaseMCU
    {
        [Key]
        public int MetodoPagoID { get; set; }

    }
}
