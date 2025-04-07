using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartBudgetApp.Models;

namespace SmartBudgetApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Ventas> Ventas { get; set; }
        public DbSet<Ingresos> Ingresos { get; set; }
        public DbSet<Gastos> Gastos { get; set; }
        public DbSet<Facturas> Facturas { get; set; }
        public DbSet<Departamento> Departamento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            // Insertar datos por defecto para los Departamentos
            modelbuilder.Entity<Departamento>().HasData(
                new Departamento { DepartamentoId = 1, Nombre = "Reposteria" },
                new Departamento { DepartamentoId = 2, Nombre = "Restaurante" }
            );

            // Insertar datos por defecto para las Categorias
            modelbuilder.Entity<Categorias>().HasData(
                new Categorias { CategoriaId = 1, Nombre = "Luz" },
                new Categorias { CategoriaId = 2, Nombre = "Agua" },
                new Categorias { CategoriaId = 3, Nombre = "Local" },
                new Categorias { CategoriaId = 4, Nombre = "Internet" }
            );
        }
    }
}
