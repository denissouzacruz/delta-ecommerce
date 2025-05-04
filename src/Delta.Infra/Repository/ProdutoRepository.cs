using Delta.Business.Data;
using Delta.Business.Models;
using Delta.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Infra.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(DeltaDbContext deltaDbContext) : base(deltaDbContext)
        {
                
        }

        public async Task<IEnumerable<Produto>> ObterProdutoCategoria()
        {
            return await _dbSet.AsNoTracking()
                    .Include(x => x.Categoria)
                    .Include(x=> x.Vendedor)
                    .ToListAsync();
        }

        public async Task<Produto> ObterProdutoCategoria(Guid id)
        {
            return await _dbSet
                        .AsNoTracking()
                        .Include(x => x.Categoria)
                        .Include(v=> v.Vendedor)
                        .FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterProdutoCategoriaPorVendedor(Guid idVendedor)
        {
            return await _dbSet
                        .AsNoTracking()
                        .Include(x => x.Categoria)
                        .Include(v => v.Vendedor)
                        .Where(x => x.VendedorId == idVendedor).ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutoPorCategoria(Guid idCategoria)
        {
            return await _dbSet
                        .AsNoTracking()
                        .Include(x => x.Categoria)
                        .Include(v => v.Vendedor)
                        .Where(x => x.CategoriaId == idCategoria).ToListAsync();
        }
    }
}
