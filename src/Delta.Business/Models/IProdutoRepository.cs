using Delta.Business.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Business.Models
{
    public interface IProdutoRepository: IRepository<Produto>
    {
        //Métodos específicos
        Task<IEnumerable<Produto>> ObterProdutoCategoria();
        Task<Produto> ObterProdutoCategoria(Guid id);
        Task<IEnumerable<Produto>> ObterProdutoCategoriaPorVendedor(Guid idVendedor);

        Task<IEnumerable<Produto>> ObterProdutoPorCategoria(Guid idCategoria);

    }
}
