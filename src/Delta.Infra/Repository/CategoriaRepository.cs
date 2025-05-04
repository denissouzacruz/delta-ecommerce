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
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(DeltaDbContext deltaDbContext): base(deltaDbContext)
        {
                
        }

        public async Task<Categoria> ObterCategoriaProduto(Guid id)
        {
            return await _dbSet.AsNoTracking()
                    .Include(x => x.Produtos)
                    .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
