
using FinanceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Infraestructure.Context
{
    public partial class FinanceAppDbContext : DbContext
    {
        public FinanceAppDbContext(DbContextOptions<FinanceAppDbContext> dbContext) : base(dbContext)
        {
            
        }

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Tipo> Tipo { get; set; }
        public DbSet<Gasto> Gasto { get; set; }
        public DbSet<Ingreso> Ingreso { get; set; }
        public DbSet<MetodoPago> MetodoPago { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
