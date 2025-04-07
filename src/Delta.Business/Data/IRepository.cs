using Delta.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Business.Data
{
    public interface IRepository<T>: IDisposable where T : EntityBase
    {
        Task Adicionar(T entity);
        Task Remover(Guid id);
        Task Atualizar(T entity);
        Task<T>? ObterPorId(Guid id);
        Task<List<T>> ObterTodos();
        Task<IEnumerable<T>> Pesquisar(Expression<Func<T, bool>> predicate);
        Task<int> SaveChanges();

    }
}
