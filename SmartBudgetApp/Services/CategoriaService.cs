using Microsoft.EntityFrameworkCore;
using SmartBudgetApp.Data;
using SmartBudgetApp.Models;
using System.Linq.Expressions;

namespace SmartBudgetApp.Services
{
    public class CategoriasService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public CategoriasService(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        // Insertar una nueva categoría
        public async Task<bool> Insertar(Categorias categoria)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.Categorias.Add(categoria); // Agrega la categoría al contexto
            return await contexto.SaveChangesAsync() > 0; // Guarda los cambios y verifica si se guardó correctamente
        }

        // Listar todas las categorías con un criterio
        public async Task<List<Categorias>> Listar(Expression<Func<Categorias, bool>> criterio)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Categorias.Where(criterio).ToListAsync(); // Filtra las categorías según el criterio
        }

        // Buscar una categoría por ID
        public async Task<Categorias> ObtenerPorId(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Categorias.FindAsync(id); // Busca la categoría por ID
        }

        // Actualizar una categoría
        public async Task<bool> Actualizar(int categoriaId, string nuevoNombre)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            var categoria = await contexto.Categorias.FindAsync(categoriaId); // Busca la categoría por ID

            if (categoria != null)
            {
                categoria.Nombre = nuevoNombre; // Modifica el nombre de la categoría
                contexto.Categorias.Update(categoria); // Actualiza la categoría en el contexto
                return await contexto.SaveChangesAsync() > 0; // Guarda los cambios y verifica si se guardó correctamente
            }

            return false; // Si la categoría no se encuentra, retorna false
        }

        // Eliminar una categoría
        public async Task<bool> Eliminar(int categoriaId)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            var categoria = await contexto.Categorias.FindAsync(categoriaId); // Busca la categoría por ID

            if (categoria != null)
            {
                contexto.Categorias.Remove(categoria); // Elimina la categoría
                return await contexto.SaveChangesAsync() > 0; // Guarda los cambios y verifica si se eliminó correctamente
            }

            return false; // Si la categoría no se encuentra, retorna false
        }
    }
}
