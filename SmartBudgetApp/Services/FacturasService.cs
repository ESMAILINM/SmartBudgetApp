using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmartBudgetApp.Data;
using SmartBudgetApp.Models;

namespace SmartBudgetApp.Services
{
    public class FacturasService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        private readonly GastosServices _gastosService;

        public FacturasService(IDbContextFactory<ApplicationDbContext> dbFactory, GastosServices gastosService)
        {
            _dbFactory = dbFactory;
            _gastosService = gastosService;
        }

        public async Task<bool> Guardar(Facturas factura)
        {
            bool resultado;

            if (!await Existe(factura.FacturaId))
            {
                resultado = await Insertar(factura);
            }
            else
            {
                resultado = await Modificar(factura);
            }

            // Si la factura se guardó y el estado es "Pago", registrar el gasto
            if (resultado && factura.Estado.Equals("Pago", StringComparison.OrdinalIgnoreCase))
            {
                // Cargar la categoría si no está inicializada
                if (factura.Categoria == null)
                {
                    await using var contexto = await _dbFactory.CreateDbContextAsync();
                    factura.Categoria = await contexto.Categorias.FindAsync(factura.CategoriaId);
                }

                await RegistrarGastoDesdeFactura(factura);
            }

            return resultado;
        }

        public async Task<Facturas?> Buscar(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Facturas
                .Include(f => f.Categoria) // Incluir la categoría para evitar null
                .FirstOrDefaultAsync(f => f.FacturaId == id);
        }

        private async Task RegistrarGastoDesdeFactura(Facturas factura)
        {
            // Evitar NullReferenceException
            var categoriaNombre = factura.Categoria != null ? factura.Categoria.Nombre : "Sin categoría";

            var nuevoGasto = new Gastos
            {
                Descripcion = $"Factura {factura.FacturaId} - {categoriaNombre}",
                Monto = factura.Total,
                Fecha = factura.Fecha,
                CategoriaId = factura.CategoriaId
            };

            await _gastosService.Guardar(nuevoGasto);
        }

        public async Task<bool> Existe(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Facturas
                .Include(f => f.Categoria)
                .AnyAsync(f => f.FacturaId == id);
        }

        public async Task<bool> Insertar(Facturas factura)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.Facturas.Add(factura);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Modificar(Facturas factura)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            contexto.Facturas.Update(factura);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<List<Facturas>> Listar(Expression<Func<Facturas, bool>> criterio)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Facturas
                .Include(f => f.Categoria)
                .Where(criterio)
                .ToListAsync();
        }

        public async Task<bool> Eliminar(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            var factura = await contexto.Facturas.FindAsync(id);
            if (factura != null)
            {
                contexto.Facturas.Remove(factura);
                return await contexto.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
