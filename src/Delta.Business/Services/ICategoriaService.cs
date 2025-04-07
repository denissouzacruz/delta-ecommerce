using Delta.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Business.Services
{
    public interface ICategoriaService: IDisposable
    {
        public Task Adicionar(Categoria categoria);
        public Task Remover(Guid id);
        public Task Atualizar(Categoria categoria);
    }
}
