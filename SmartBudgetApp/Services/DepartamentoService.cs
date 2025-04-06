using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmartBudgetApp.Data;
using SmartBudgetApp.Models;

namespace SmartBudgetApp.Services
{
    public class DepartamentoService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public DepartamentoService(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public async Task<List<Departamento>> ListarDepartamento(Expression<Func<Departamento, bool>> criterio)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Departamento.Where(criterio).ToListAsync();
        }
    }
}
