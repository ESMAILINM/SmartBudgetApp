using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmartBudgetApp.Data;
using SmartBudgetApp.Models;

namespace SmartBudgetApp.Services
{
    public class ProductosService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public ProductosService(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<bool> Guardar(Producto producto)
        {
            if (!await Existe(producto.ProductoId))
            {
                return await Insertar(producto);
            }
            else
            {
                return await Modificar(producto);
            }
        }

        public async Task<bool> Existe(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Producto.AnyAsync(x => x.ProductoId == id);
        }

        public async Task<bool> Insertar(Producto producto)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.Producto.Add(producto);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Modificar(Producto producto)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.Producto.Update(producto);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<Producto?> Buscar(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Producto.FindAsync(id);
        }

        public async Task<bool> Eliminar(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Producto.Where(x => x.ProductoId == id).ExecuteDeleteAsync() > 0;
        }

        public async Task<List<Producto>> Listar(Expression<Func<Producto, bool>> criterio)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Producto.Where(criterio).ToListAsync();
        }

       
    }
}
