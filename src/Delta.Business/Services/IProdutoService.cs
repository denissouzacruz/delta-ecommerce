using Delta.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Business.Services
{
    public interface IProdutoService: IDisposable
    {
        public Task Adicionar(Produto produto);
        public Task Remover(Guid id);
        public Task Atualizar(Produto produto);
    }
}
