using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmartBudgetApp.Data;
using SmartBudgetApp.Models;

namespace SmartBudgetApp.Services
{
    public class VentasService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public VentasService(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<bool> Guardar(Ventas venta)
        {
            if (!await Existe(venta.VentaId))
            {
                return await Insertar(venta);
            }
            else
            {
                return await Modificar(venta);
            }
        }

        public async Task<bool> Existe(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Ventas.AnyAsync(x => x.VentaId == id);
        }

        public async Task<bool> Insertar(Ventas venta)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            var departamento = await contexto.Departamento.FindAsync(venta.DepartamentoId);
            if (departamento == null)
            {
                throw new InvalidOperationException("El DepartamentoId no es válido.");
            }
            venta.Departamento = departamento;

            contexto.Ventas.Add(venta);
            return await contexto.SaveChangesAsync() > 0;
        }




        public async Task<bool> Modificar(Ventas venta)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.Ventas.Update(venta);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<Ventas?> Buscar(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Ventas.FindAsync(id);
        }

        public async Task<bool> Eliminar(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Ventas.Where(x => x.VentaId == id).ExecuteDeleteAsync() > 0;
        }

        public async Task<List<Ventas>> Listar(Expression<Func<Ventas, bool>> criterio)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Ventas
                .Include(v => v.Departamento) 
                .Where(criterio)
                .ToListAsync();
        }


    }
}
