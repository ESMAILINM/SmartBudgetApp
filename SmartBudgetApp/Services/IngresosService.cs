using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using SmartBudgetApp.Data;
using SmartBudgetApp.Models;
using System.Linq.Expressions;

namespace SmartBudgetApp.Services
{
    public class IngresosService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public IngresosService(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public async Task<bool> Guardar(Ingresos ingreso)
        {
            if (!await Existe(ingreso.Id))
            {
                return await Insertar(ingreso);
            }
            else
            {
                return await Modificar(ingreso);
            }
        }

        public async Task<bool> Existe(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Ingresos.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> Insertar(Ingresos ingresos)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.Ingresos.Add(ingresos);
            return await contexto.SaveChangesAsync() > 0;
        }
        public async Task<bool> Modificar(Ingresos ingresos)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.Ingresos.Update(ingresos);
            return await contexto.SaveChangesAsync() > 0;
        }
        public async Task<bool> Eliminar(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Ingresos.Where(x => x.Id == id).ExecuteDeleteAsync() > 0;
        }
        public async Task<bool> EliminarIngreso(string descripcion )
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Ingresos.Where(x => x.Descripcion == descripcion).ExecuteDeleteAsync() > 0;
        }
        public async Task<List<Ingresos>> Listar(Expression<Func<Ingresos, bool>> criterio)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.  Ingresos.Where(criterio).ToListAsync();
        }

    }
}
