using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartBudgetApp.Models;

namespace SmartBudgetApp.Data
{
    //public class Contexto : DbContext { }
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Ventas> Ventas { get; set; }
        public DbSet<Ingresos> Ingresos { get; set; }
        public DbSet<Gastos> Gastos { get; set; }
        public DbSet<Facturas> Facturas { get; set; }
        public DbSet<Departamento> Departamento { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity<Tipo>().HasData(
                new Tipo { TipoId=1, Nombre="Plasticos"},
                new Tipo { TipoId= 2,  Nombre="Insumos"},
                new Tipo { TipoId=3, Nombre= "Electrodomesticos"}
                );
            modelbuilder.Entity<Departamento>().HasData(
                new Departamento { DepartamentoId = 1, Nombre = "Reposteria" },
                new Departamento { DepartamentoId = 2, Nombre = "Restaurante" }
                
                );
            modelbuilder.Entity<Categorias>().HasData(
                new Categorias { CategoriaId = 1, Nombre = "Luz" },
                new Categorias { CategoriaId = 2, Nombre = "Agua" },
                new Categorias { CategoriaId = 3, Nombre = "Local" },
                new Categorias { CategoriaId = 4, Nombre = "Internet" }

                );

        }

    }
}
