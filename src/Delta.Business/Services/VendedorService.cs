using Delta.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Business.Services
{
    public class VendedorService : IVendedorService
    {
        private readonly IVendedorRepository _vendedorRepository;
        public VendedorService(IVendedorRepository vendedorRepository)
        {
            _vendedorRepository  = vendedorRepository;
        }

        public async Task Adicionar(Vendedor vendedor)
        {
            await _vendedorRepository.Adicionar(vendedor);
        }

        public async Task Atualizar(Vendedor vendedor)
        {
            await _vendedorRepository.Atualizar(vendedor);
        }

        public async Task Remover(Guid id)
        {
           await _vendedorRepository.Remover(id);
        }

        public void Dispose()
        {
            _vendedorRepository?.Dispose();
        }
    }
}
