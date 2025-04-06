using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using SmartBudgetApp.Data;
using SmartBudgetApp.Models;
using System.Linq.Expressions;

namespace SmartBudgetApp.Services
{
    public class GastosServices
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public GastosServices(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public async Task<bool> Guardar(Gastos gastos)
        {
            if (!await Existe(gastos.GastoId))
            {
                return await Insertar(gastos);
            }
            else
            {
                return await Modificar( gastos);
            }
        }

        public async Task<bool> Existe(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Gastos.AnyAsync(x => x.GastoId == id);
        }

        public async Task<bool> Insertar(Gastos gastos)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.Gastos.Add(gastos);
            return await contexto.SaveChangesAsync() > 0;
        }
        public async Task<bool> Modificar(Gastos gastos)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.Gastos.Update(gastos);
            return await contexto.SaveChangesAsync() > 0;
        }
        public async Task<bool> Eliminar(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Gastos.Where(x => x.GastoId == id).ExecuteDeleteAsync() > 0;
        }
        public async Task<List<Gastos>> Listar(Expression<Func<Gastos, bool>> criterio)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Gastos
                .Include(g => g.Categorias)
                .Where(criterio)
                .ToListAsync();
        }

    }

}

