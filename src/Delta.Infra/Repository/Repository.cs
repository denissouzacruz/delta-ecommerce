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
    public abstract class Repository<T> : IRepository<T> where T : EntityBase, new()
    {
        protected readonly DeltaDbContext _deltaDb;
        protected readonly DbSet<T> _dbSet;

        protected Repository(DeltaDbContext dbContext)
        {
            _deltaDb = dbContext;
            _dbSet = _deltaDb.Set<T>();
        }

        public async Task Adicionar(T entity)
        {
            _dbSet.Add(entity);
           await SaveChanges();
        }

        public async Task Atualizar(T entity)
        {
            _deltaDb.Entry(entity).State = EntityState.Modified;
            await SaveChanges();
        }

        public async Task<T> ObterPorId(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<T>> ObterTodos()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> Pesquisar(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task Remover(Guid id)
        {
            var objeto = new T() {Id = id };
            _deltaDb.Entry(objeto).State = EntityState.Deleted;
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await _deltaDb.SaveChangesAsync();
        }

        public void Dispose()
        {
            _deltaDb?.Dispose();
        }
    }
}
