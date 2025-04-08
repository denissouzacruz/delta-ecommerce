using AutoMapper;
using Delta.Api.Models;
using Delta.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Delta.Api.Controllers
{
    [ApiController]
    [Route("api/produtos")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoRepository produtoRepository,
                                    ICategoriaRepository categoriaRepository,
                                    IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Models.Produto>> GetProduto(Guid id)
        {
            var produto = await _produtoRepository.ObterProdutoCategoria(id);

            if (produto == null)
            {
                return NotFound();
            }

            var produtoModel = _mapper.Map<Models.Produto>(produto);
            return produtoModel;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Produto>>> GetProdutos()
        {
            var produtoCategoria = await _produtoRepository.ObterProdutoCategoria();
            var produtoModel = _mapper.Map<IEnumerable<Models.Produto>>(produtoCategoria);

            return produtoModel.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<Models.Produto>> PostProduto(Models.Produto produto)
        {
            var nomeImagem = $"{Guid.NewGuid()}-{produto.Imagem}";
            var produtoModel = _mapper.Map<Business.Models.Produto>(produto);
            //Recuperar o Id do usuário logado
            produtoModel.VendedorId = new Guid("25CC4478-5BE;5-48D2-B986-52B2D61E9650");
            produtoModel.Imagem = nomeImagem;
            await _produtoRepository.Adicionar(produtoModel);

            return CreatedAtAction(nameof(GetProduto), new { Id = produto.Id }, produto);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> PutProduto(Guid id, Models.Produto produto)
        {
            if (id != produto.Id || _produtoRepository.ObterPorId(id) == null)
            {
                return NotFound();
            }
            var produtoBd = _mapper.Map<Business.Models.Produto>(produto);
            await _produtoRepository.Atualizar(produtoBd);

            return NoContent();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteProduto(Guid id)
        {
            var produtoBd = _produtoRepository.ObterPorId(id);
            if (_produtoRepository.ObterPorId(id) == null)
            {
                return NotFound();
            }
            await _produtoRepository.Remover(id);
            return NoContent();
        }
    }
}
