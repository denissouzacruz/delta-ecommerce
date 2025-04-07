using Delta.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Business.Services
{
    public interface IVendedorService: IDisposable
    {
        public Task Adicionar(Vendedor vendedor);
        public Task Remover(Guid id);
        public Task Atualizar(Vendedor vendedor);
        
    }
}
